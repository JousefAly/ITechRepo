using ITech.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.ViewModels
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string[] RoleNames { get; set; }
        public IList<string> UserRoleNames { get; set; }
        
    }
}
