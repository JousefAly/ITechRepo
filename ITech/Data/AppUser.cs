using Microsoft.AspNetCore.Identity;

namespace ITech.Data
{
    public class AppUser : IdentityUser
    {
        //IdentityUser already has Id prop
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
