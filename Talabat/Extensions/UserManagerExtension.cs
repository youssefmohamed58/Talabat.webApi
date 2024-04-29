using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity_Entities;

namespace Talabat.Apis.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser?> FindUserWithAddressAsync(this UserManager<AppUser> _usermanager , ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            var User = await _usermanager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email); 

            return User;
        }
    }
}
