using Ogani.Data;

namespace Ogani.ViewModel
{
    public class Item
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }

    public class ProductCart
    {
        public string Name { get; set; }
    }
}