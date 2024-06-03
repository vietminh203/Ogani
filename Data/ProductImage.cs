using System;

namespace Ogani.Data
{
    public class ProductImage
    {
        public Guid Id { set; get; }
        public Product Product { set; get; }
        public Guid ProductId { set; get; }
        public string Name { set; get; }

    }
}
