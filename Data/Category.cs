using System;
using System.Collections.Generic;

namespace Ogani.Data
{
    public class Category
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public List<ProductCategory> ProductCategories { set; get; }
    }
}