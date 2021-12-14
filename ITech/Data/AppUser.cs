using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data
{
    public class AppUser : IdentityUser
    {
        //IdentityUser already has Id prop
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
