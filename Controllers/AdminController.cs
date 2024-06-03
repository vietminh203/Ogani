using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ogani.Data;
using Ogani.Service;
using Ogani.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ogani.Controllers
{
    [Authorize(Roles = "admin,employee")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OganiDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IStorageService _storageService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(IWebHostEnvironment webHostEnvironment, IStorageService storageService, RoleManager<AppRole> roleManager, ILogger<HomeController> logger, OganiDbContext dbContext, UserManager<AppUser> userManager)
        {
            _storageService = storageService;
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
            _logger = logger;
            _hostEnvironment = webHostEnvironment;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<IActionResult> Index(string keyword, string sorting = null, int p = 1, int s = 10)
        {
            var query = _dbContext.Orders.Include(x => x.ProductOrders).ThenInclude(x => x.Product).Include(x => x.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.Email.Contains(keyword) || x.Address.Contains(keyword) ||
                x.AppUser.UserName.Contains(keyword));
            }

            ViewBag.Orders = await query.Skip((p - 1) * s).Take(s).ToListAsync();
            return View(new PagedResultBase()
            {
                PageIndex = p,
                Keyword = keyword,
                PageSize = s,
                TotalRecords = query.Count()
            });
            return View();
        }

        [Route("/admin/getreport")]
        public async Task<IActionResult> GetReport(DateTime startDate, DateTime endDate)
        {
            var result = await _dbContext.Orders.Include(x => x.ProductOrders).ThenInclude(x => x.Product)
                 .Where(x => x.CreateAt.Date >= startDate.Date && x.CreateAt.Date <= endDate.Date).OrderByDescending(x => x.CreateAt).
                 GroupBy(x => new { Time = x.CreateAt.Date }).Select(x => new
                 {
                     Total = x.Count(),
                     //CreateAt = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.Key.Time)
                     CreateAt = x.Key.Time.ToString("MM/dd/yyyy")
                 }).ToListAsync();

            return Json(result);
        }

        public async Task<IActionResult> ExportToExcelOrder()
        {
            DateTime startDate = DateTime.Parse(Request.Form["startDate"]);
            DateTime endDate = DateTime.Parse(Request.Form["endDate"]);
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("FullName"),
                                        new DataColumn("Email"),
                                        new DataColumn("Phone"),
                                        new DataColumn("UserName"),
                                        new DataColumn("List Product"),
                                        new DataColumn("CreateAt"),
                                        new DataColumn("Method Payment"),
                                        new DataColumn("Status"),
                                        new DataColumn("Total")});

            var orders = _dbContext.Orders.Include(x => x.ProductOrders).ThenInclude(x => x.Product).
                Include(x => x.AppUser).Where(x => x.CreateAt.Date >= startDate.Date && x.CreateAt.Date <= endDate.Date)
                .ToList();

            foreach (var order in orders)
            {
                dt.Rows.Add(order.FirstName + order.LastName, order.Email, order.Phone,
                    order.AppUser.UserName, String.Join(",", order.ProductOrders.Select(x => x.Product.Name).ToList()),
                    order.CreateAt, order.Method == "0" ? "Delivery" : "Momo", order.Status == false ? "During Processing" : "Done", order.Total+"$");
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report Orders " + DateTime.Now.ToString("MM/dd/yyyy") + ".xlsx");
                }
            }
        }

        public async Task<IActionResult> OrderView(string keyword, string sorting = null, int p = 1, int s = 10)
        {
            var query = _dbContext.Orders.Include(x => x.ProductOrders).ThenInclude(x => x.Product).Include(x => x.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.Email.Contains(keyword) || x.Address.Contains(keyword) ||
                x.AppUser.UserName.Contains(keyword));
            }

            ViewBag.Orders = await query.Skip((p - 1) * s).Take(s).ToListAsync();
            return View(new PagedResultBase()
            {
                PageIndex = p,
                Keyword = keyword,
                PageSize = s,
                TotalRecords = query.Count()
            });
            return View();
        }

        public async Task<IActionResult> UpdateOrderStatus(Guid Id)
        {
            var order = await _dbContext.Orders.FindAsync(Id);
            order.Status = !order.Status;
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(OrderView));
        }

        public async Task<IActionResult> ProductView(string keyword, string sorting = null, int p = 1, int s = 10)
        {
            var query = _dbContext.Products.Include(x => x.ProductImages).Include(x => x.ProductCategories).
               ThenInclude(x => x.Category).Include(x => x.Supplier).AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword) ||
                x.CurrentPrice.Contains(keyword));
            }
            if (!string.IsNullOrWhiteSpace(sorting) && !sorting.StartsWith("-"))
            {
                query = sorting switch
                {
                    nameof(Product.Name) => query.OrderBy(x => x.Name),
                    nameof(Product.Description) => query.OrderBy(query => query.Description),
                    nameof(Product.ToTalRemaining) => query.OrderBy(x => x.ToTalRemaining),
                    nameof(Product.CurrentPrice) => query.OrderBy(x => x.CurrentPrice),
                    nameof(Product.ReducePrice) => query.OrderBy(x => x.ReducePrice),
                    _ => query.OrderBy(x => x.CreateAt),
                };
            }
            else
            {
                query = sorting switch
                {
                    "-" + nameof(Product.Name) => query.OrderByDescending(x => x.Name),
                    "-" + nameof(Product.Description) => query.OrderByDescending(query => query.Description),
                    "-" + nameof(Product.ToTalRemaining) => query.OrderByDescending(x => x.ToTalRemaining),
                    "-" + nameof(Product.CurrentPrice) => query.OrderByDescending(x => x.CurrentPrice),
                    "-" + nameof(Product.ReducePrice) => query.OrderByDescending(x => x.ReducePrice),
                    _ => query.OrderByDescending(x => x.CreateAt),
                };
            }
            ViewBag.Products = await query.Skip((p - 1) * s).Take(s).ToListAsync();
            return View(new PagedResultBase()
            {
                PageIndex = p,
                Keyword = keyword,
                PageSize = s,
                TotalRecords = query.Count()
            });
        }

        //Product Create
        [HttpGet]
        public IActionResult ProductCreate()
        {
            ViewBag.Categories = _dbContext.Categories.ToList();
            ViewBag.Suppliers = _dbContext.Suppliers.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductCreaterequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                CreateAt = DateTime.Now,
                SupplierId = request.SupplierId,
                CurrentPrice = request.CurrentPrice,
                Description = request.Description,
                ReducePrice = request.ReducePrice,
                ToTalRemaining = request.ToTalRemaining,
            };
            if (request.Image != null)
            {
                product.Image = await this.SaveFile(request.Image);
            }
            var result = await _dbContext.Products.AddAsync(product);
            if (request.CategoryId != null)
            {
                await _dbContext.ProductCategories.AddAsync(new ProductCategory()
                {
                    CategoryId = request.CategoryId,
                    ProductId = product.Id
                });
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(ProductView));
        }

        public async Task<IActionResult> ProductUpdate(Guid Id)
        {
            var product = await _dbContext.Products.FindAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _dbContext.Categories.ToList();
            ViewBag.Suppliers = _dbContext.Suppliers.ToList();
            ViewBag.Image = product.Image;
            return View(new ProductUpdateRequest()
            {
                CurrentPrice = product.CurrentPrice,
                Description = product.Description,
                Id = Id,
                Name = product.Name,
                ReducePrice = product.ReducePrice,
                ToTalRemaining = product.ToTalRemaining,
                SupplierId = product.SupplierId,
            });
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductUpdateRequest request)
        {
            var product = new Product()
            {
                CurrentPrice = request.CurrentPrice,
                Description = request.Description,
                Id = request.Id,
                Name = request.Name,
                SupplierId = request.SupplierId,
                ToTalRemaining = request.ToTalRemaining,
                ReducePrice = request.ReducePrice,
            };
            if (request.Image != null)
            {
                if (Request.Form["img-replace"].Count > 0) await _storageService.DeleteFileAsync(Request.Form["img-replace"]);
                product.Image = await this.SaveFile(request.Image);
            }
            else
            {
                product.Image = Request.Form["img-replace"];
            }
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(ProductView));
        }

        //delete product
        public async Task<IActionResult> ProductDelete(Guid Id)
        {
            var product = await _dbContext.Products.FindAsync(Id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                if (product.Image != null) await _storageService.DeleteFileAsync(product.Image);

                await _dbContext.SaveChangesAsync();
            }
            TempData["err"] = "can't find product";
            return RedirectToAction(nameof(ProductView));
        }

        // Product Detail Image
        public async Task<IActionResult> ProductImageDetail(Guid Id, int p = 1, int s = 10)
        {
            var productImageDetail = await _dbContext.ProductImages.Include(x => x.Product).Skip((p - 1) * s).Take(s).Where(x => x.ProductId.Equals(Id)).ToListAsync();
            if (productImageDetail != null)
            {
                ViewBag.ProductImageDetail = productImageDetail;
                ViewBag.Id = Id;
                return View(new PagedResultBase()
                {
                    PageIndex = p,
                    PageSize = s,
                    TotalRecords = productImageDetail.Count()
                });
            }
            return NotFound(Id);
        }

        public IActionResult ProductImageDetailCreate(Guid Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductImageDetailCreate()
        {
            if (Request.Form.Files["img"] == null) return NotFound();
            var productIm = new ProductImage()
            {
                Id = Guid.NewGuid(),
                Name = await this.SaveFile(Request.Form.Files["img"]),
                ProductId = new Guid(Request.Form["id"]),
            };
            await _dbContext.ProductImages.AddAsync(productIm);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(ProductView));
        }

        public async Task<IActionResult> ProductImageDetailDelete(Guid Id)
        {
            var pm = await _dbContext.ProductImages.FindAsync(Id);
            if (pm != null)
            {
                _dbContext.ProductImages.Remove(pm);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(ProductView));
            }
            return NotFound();
        }

        public async Task<IActionResult> ProductImageUpdate(Guid Id)
        {
            var productImageDetail = await _dbContext.ProductImages.FindAsync(Id);
            return View(productImageDetail);
        }

        [HttpPost]
        public async Task<IActionResult> ProductImageUpdate(ProductImage pd)
        {
            var product = await _dbContext.ProductImages.FindAsync(pd.Id);
            if (product != null)
            {
                if (product.Name != null) await _storageService.DeleteFileAsync(product.Name);
                product.Name = await this.SaveFile(Request.Form.Files["Img"]);
                _dbContext.ProductImages.Update(product);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(ProductImageDetailCreate));
            }
            return NotFound();
        }

        //Category
        // Product Detail Image
        public async Task<IActionResult> ProductCategoryDetail(Guid Id)
        {
            var categories = _dbContext.Categories.ToList();
            var categoryOfProduct = _dbContext.Categories.Include(x => x.ProductCategories).
                Where(x => x.ProductCategories.Any(x => x.ProductId.Equals(Id))).ToList();
            var categoryAssignRequest = new CategoryAssignRequest();
            foreach (var category in categories)
            {
                categoryAssignRequest.Categories.Add(new SelectItem()
                {
                    Id = category.Id.ToString(),
                    Name = category.Name,
                    Selected = categoryOfProduct.Contains(category)
                });
            }
            return View(categoryAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> ProductCategoryDetail(Guid Id, CategoryAssignRequest request)
        {
            var productCategory = _dbContext.ProductCategories.Where(x => x.ProductId.Equals(Id)).ToList();
            if (productCategory != null)
            {
                _dbContext.ProductCategories.RemoveRange(productCategory);
                foreach (var category in request.Categories)
                {
                    if (category.Selected == true)
                    {
                        _dbContext.ProductCategories.Add(new ProductCategory()
                        {
                            CategoryId = Guid.Parse(category.Id),
                            ProductId = Id
                        });
                    }
                }
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(ProductCategoryDetail));
            }
            return NotFound();
        }

        //End categoryProduct

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UserView(string keyword, string sorting = null, int p = 1, int s = 10)
        {
            var query = _userManager.Users.Include(x => x.AppUserRoles).ThenInclude(x => x.AppRole).Include(x => x.Orders).AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.FirstName.Contains(keyword) || x.LastName.Contains(keyword) ||
                x.UserName.Contains(keyword));
            }
            if (!string.IsNullOrWhiteSpace(sorting) && !sorting.StartsWith("-"))
            {
                query = sorting switch
                {
                    nameof(AppUser.FirstName) => query.OrderBy(x => x.FirstName),
                    nameof(AppUser.LastName) => query.OrderBy(query => query.LastName),
                    nameof(AppUser.UserName) => query.OrderBy(x => x.UserName),
                    nameof(AppUser.Email) => query.OrderBy(x => x.Email),
                    nameof(AppUser.PhoneNumber) => query.OrderBy(x => x.PhoneNumber),
                    _ => query.OrderBy(x => x.CreateAt),
                };
            }
            else
            {
                query = sorting switch
                {
                    "-" + nameof(AppUser.FirstName) => query.OrderBy(x => x.FirstName),
                    "-" + nameof(AppUser.LastName) => query.OrderBy(query => query.LastName),
                    "-" + nameof(AppUser.UserName) => query.OrderBy(x => x.UserName),
                    "-" + nameof(AppUser.Email) => query.OrderBy(x => x.Email),
                    "-" + nameof(AppUser.PhoneNumber) => query.OrderBy(x => x.PhoneNumber),
                    _ => query.OrderBy(x => x.CreateAt),
                };
            }
            ViewBag.Users = await query.Skip((p - 1) * s).Take(s).AsNoTracking().ToListAsync();
            return View(new PagedResultBase()
            {
                PageIndex = p,
                Keyword = keyword,
                PageSize = s,
                TotalRecords = query.Count()
            });
        }

        // Role Assign
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RoleAssign(Guid Id)
        {
            var roles = _roleManager.Roles;
            var rolesOfUser = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(Id.ToString()));
            var roleAssignRequest = new RoleAssignRequest();
            foreach (var role in roles)
            {
                roleAssignRequest.Roles.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = rolesOfUser.Contains(role.Name)
                });
            }
            return View(roleAssignRequest);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RoleAssign(Guid Id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var result = await _userManager.RemoveFromRolesAsync(user, roles);
                var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
                foreach (var roleName in addedRoles)
                {
                    if (await _userManager.IsInRoleAsync(user, roleName) == false)
                    {
                        await _userManager.AddToRoleAsync(user, roleName);
                    }
                }
            }
            return RedirectToAction(nameof(RoleAssign));
        }

        //Delete user
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(UserView));
        }

        //Update user
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUser(AppUser user)
        {
            var u = await _userManager.FindByIdAsync(user.Id.ToString());
            u.UserName = user.UserName;
            u.Email = user.Email;
            u.DateOfBirth = user.DateOfBirth;
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.PhoneNumber = user.PhoneNumber;

            if (this.Request.Form.Files["avatar"] != null)
            {
                u.Avatar = await this.SaveFile(this.Request.Form.Files["avatar"]);
            }
            await _userManager.UpdateAsync(u);
            return RedirectToAction(nameof(UserView));
        }

        // blog manage
        public async Task<IActionResult> BlogView(string keyword, string sorting = null, int p = 1, int s = 10)
        {
            var query = _dbContext.Blogs.Include(x => x.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.Content.Contains(keyword) || x.Title.Contains(keyword) ||
                x.CreateAt.Equals(keyword));
            }
            if (!string.IsNullOrWhiteSpace(sorting) && !sorting.StartsWith("-"))
            {
                query = sorting switch
                {
                    nameof(Blog.Content) => query.OrderBy(x => x.Content),
                    nameof(Blog.Title) => query.OrderBy(query => query.Title),
                    _ => query.OrderBy(x => x.CreateAt),
                };
            }
            else
            {
                query = sorting switch
                {
                    "-" + nameof(Blog.Content) => query.OrderByDescending(x => x.Content),
                    "-" + nameof(Blog.Title) => query.OrderByDescending(query => query.Title),
                    _ => query.OrderByDescending(x => x.CreateAt),
                };
            }
            ViewBag.Blogs = await query.Skip((p - 1) * s).Take(s).ToListAsync();
            return View(new PagedResultBase()
            {
                PageIndex = p,
                Keyword = keyword,
                PageSize = s,
                TotalRecords = query.Count()
            });
        }

        public IActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(Blog blog)
        {
            if (Request.Form.Files["Image"] != null)
            {
                blog.Image = await this.SaveFile(Request.Form.Files["Image"]);
            }
            var user = await _userManager.GetUserAsync(User);
            blog.UserId = user.Id;
            blog.CreateAt = DateTime.Now;
            var result = await _dbContext.Blogs.AddAsync(blog);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(BlogView));
        }

        public async Task<IActionResult> UpdateBlog(Guid Id)
        {
            var blog = await _dbContext.Blogs.FindAsync(Id);
            ViewBag.Image = blog.Image;
            if (blog != null)
            {
                return View(blog);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog(Blog blog)
        {
            var result = await _dbContext.Blogs.FindAsync(blog.Id);
            if (Request.Form.Files["Image"] != null)
            {
                if (result.Image != null)
                {
                    await _storageService.DeleteFileAsync(result.Image);
                }
                result.Image = await this.SaveFile(Request.Form.Files["Image"]);
            }
            result.Content = blog.Content;
            result.Title = blog.Title;
            _dbContext.Blogs.Update(result);

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(BlogView));
        }

        public async Task<IActionResult> DeleteBlog(Guid Id)
        {
            var blog = await _dbContext.Blogs.FindAsync(Id);
            if (blog != null)
            {
                if (blog.Image != null)
                {
                    await _storageService.DeleteFileAsync(blog.Image);
                }
                _dbContext.Blogs.Remove(blog);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(BlogView));
            }
            return NotFound();
        }

        public async Task<IActionResult> SupplierView(string keyword, string sorting = null, int p = 1, int s = 10)
        {
            var query = _dbContext.Suppliers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            if (!string.IsNullOrWhiteSpace(sorting) && !sorting.StartsWith("-"))
            {
                query = sorting switch
                {
                    nameof(Product.Name) => query.OrderBy(x => x.Name),
                    nameof(Product.Description) => query.OrderBy(query => query.Description),
                    _ => query.OrderBy(x => x.Name),
                };
            }
            else
            {
                query = sorting switch
                {
                    "-" + nameof(Product.Name) => query.OrderByDescending(x => x.Name),
                    "-" + nameof(Product.Description) => query.OrderByDescending(query => query.Description),
                    _ => query.OrderByDescending(x => x.Name),
                };
            }
            ViewBag.Suppliers = await query.Skip((p - 1) * s).Take(s).ToListAsync();
            return View(new PagedResultBase()
            {
                PageIndex = p,
                Keyword = keyword,
                PageSize = s,
                TotalRecords = query.Count()
            });
        }

        [HttpGet]
        public IActionResult SupplierCreate()
        {
            return View();
        }

        public async Task<IActionResult> SupplierCreate(Supplier supplier)
        {
            await _dbContext.Suppliers.AddAsync(supplier);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(SupplierView));
        }

        [HttpGet]
        public IActionResult SupplierUpdate(Guid Id)
        {
            var supplier = _dbContext.Suppliers.Find(Id);
            return View(supplier);
        }

        public async Task<IActionResult> SupplierUpdate(Supplier supplier)
        {
            _dbContext.Suppliers.Update(supplier);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(SupplierView));
        }

        public IActionResult SupplierDelete(Guid Id)
        {
            var supplier = _dbContext.Suppliers.Find(Id);
            if (supplier == null) return NotFound();
            _dbContext.Suppliers.Remove(supplier);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(SupplierView));
        }

        [HttpPost]
        public ActionResult UploadImage(List<IFormFile> fIles)
        {
            var filePath = "";
            foreach (IFormFile photo in Request.Form.Files)
            {
                string serverMapPath = Path.Combine(_hostEnvironment.WebRootPath, "Image", photo.FileName);
                using (var stream = new FileStream(serverMapPath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                filePath = "https://localhost:5001/" + "Image/" + photo.FileName;
            }
            return Json(new { url = filePath });
        }
    }
}
