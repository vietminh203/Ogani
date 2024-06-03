using Microsoft.AspNetCore.Http;
using System;

namespace Ogani.ViewModel
{
    public class ProductUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public int ToTalRemaining { set; get; }
        public Guid SupplierId { set; get; }
        public string CurrentPrice { set; get; }
        public string ReducePrice { set; get; }
        public IFormFile Image { set; get; }
        public Guid CategoryId { set; get; }
        public int Rate { set; get; }
    }
}
