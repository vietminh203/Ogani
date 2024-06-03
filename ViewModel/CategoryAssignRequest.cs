using System;
using System.Collections.Generic;

namespace Ogani.ViewModel
{
    public class CategoryAssignRequest
    {
        public Guid Id { get; set; }
        public List<SelectItem> Categories { get; set; } = new List<SelectItem>();
    }
}