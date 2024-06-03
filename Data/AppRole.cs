using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Ogani.Data
{
    public class AppRole : IdentityRole<Guid>
    {
        public List<AppUserRole> AppUserRoles { get; set; }
    }
}