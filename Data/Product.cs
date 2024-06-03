using System;
using System.Collections.Generic;

namespace Ogani.Data
{
    public class Product
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public int ToTalRemaining { set; get; }
        public Guid SupplierId { set; get; }
        public string CurrentPrice { set; get; }
        public string ReducePrice { set; get; }
        public Supplier Supplier { set; get; }
        public DateTime CreateAt { set; get; }
        public int Rate { set; get; }
        public string Image { set; get; }
       // public string Weight { set; get; }
        public List<ProductImage> ProductImages { set; get; }
        public List<ProductCategory> ProductCategories { set; get; }
        public List<ProductOrder> ProductOrders { set; get; }
    }
}
