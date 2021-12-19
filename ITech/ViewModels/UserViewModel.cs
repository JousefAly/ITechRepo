using System.Collections.Generic;

namespace ITech.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        //Roles Names
        public IEnumerable<string> Roles { get; set; }
    }
}
