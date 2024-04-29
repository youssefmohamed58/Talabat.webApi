using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity_Entities;

namespace Talabat.Repository.Identity
{
    public static class IdentityDbContextSeed
    {
        public static async Task UserSeedAsync (UserManager<AppUser> usermanager)
        {
            if (!usermanager.Users.Any())
            {
                var User = new AppUser ()
                {
                    DisplayName = "Youssef Mohamed",
                    Email = "YoussefMohamed58@gmail.com",
                    UserName = "YoussefMohamed",
                    PhoneNumber = "011252314568"
                };
                await usermanager.CreateAsync(User , "Pa$$w0rd");   
            }
        }
    }
}
