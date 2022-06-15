using ITech.Data.Entites;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ITech.Data
{
    public class AppUser : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Notification>  Notifications { get; set; }
        public List<Notification> SentNotifications { get; set; }
        public List<Product> SavedProducts { get; set; }
        public List<Rating> Ratings { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
