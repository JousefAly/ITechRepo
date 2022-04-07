using ITech.Data.Entites;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ITech.Data
{
    public class AppUser : IdentityUser
    {
        //IdentityUser already has Id prop
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Notification>  Notifications { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
