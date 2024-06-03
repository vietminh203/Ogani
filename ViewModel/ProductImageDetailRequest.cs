using System;
using System.Collections.Generic;

namespace Ogani.ViewModel
{
    public class ProductImageDetailRequest
    {
        public Guid Id { get; set; }
        public List<SelectItem> ProductImageDetail { get; set; } = new List<SelectItem>();
    }
}
