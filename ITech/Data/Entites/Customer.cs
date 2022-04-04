using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Entites
{
    public class Customer
    {
        public string Id { get; set; }
        public  string UserId { get; set; }
        public AppUser User { get; set; }       
    }
}
