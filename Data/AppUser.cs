using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Ogani.Data
{
    public class AppUser : IdentityUser<Guid>
    {
        public List<AppUserRole> AppUserRoles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<Order> Orders { get; set; }
        public string Avatar { set; get; }
        public List<Blog> Blogs { get; set; }
        public DateTime CreateAt { set; get; }
    }
}