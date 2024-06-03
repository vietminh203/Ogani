using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Ogani.ViewModel
{
    public class ProductCreaterequest
    {
        public Guid Id { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public int ToTalRemaining { set; get; }
        public Guid SupplierId { set; get; }
        public string CurrentPrice { set; get; }
        public string ReducePrice { set; get; }
        public DateTime CreateAt { set; get; }
        public int Rate { set; get; }
        public IFormFile Image { set; get; }
        public Guid CategoryId { set; get; }
    }
}