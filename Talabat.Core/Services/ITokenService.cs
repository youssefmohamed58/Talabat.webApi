using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity_Entities;

namespace Talabat.Service
{
    public interface ITokenService
    {
      Task<string> CreateTokenAsync(AppUser user , UserManager<AppUser> usermanager);
    }
}
