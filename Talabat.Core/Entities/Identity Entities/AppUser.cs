using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Talabat.Core.Entities.Identity_Entities
{
    public class AppUser : IdentityUser
    {
       public string DisplayName {  get; set; }
       
        public Address Address { get; set; }
    }
}
