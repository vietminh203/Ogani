using System;
using System.Collections.Generic;

namespace Ogani.Data
{
    public class Supplier
    {
        public Guid Id { set; get; }
        public string Name { get; set; }
        public string Address { set; get; }
        public string Description { set; get; }
        public List<Product> Products { get; set; }
    }
}
