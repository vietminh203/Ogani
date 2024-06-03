using System;
using System.Collections.Generic;

namespace Ogani.Data
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime CreateAt { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string Total { set; get; }
        public string Address { set; get; }
        public string Method { set; get; }
        public bool Status { set; get; }
        public List<ProductOrder> ProductOrders { set; get; }
    }
}
