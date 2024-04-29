using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity_Entities;

namespace Talabat.Repository.Identity
{
    public class IdentityModuleDbContext : IdentityDbContext<AppUser>
    {
        public IdentityModuleDbContext(DbContextOptions<IdentityModuleDbContext> options) : base(options)
        {
            
        }
    }
}
