using Microsoft.AspNetCore.Identity;
using System;

namespace Ogani.Data
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public AppUser AppUser { get; set; }
        public AppRole AppRole { get; set; }
    }
}