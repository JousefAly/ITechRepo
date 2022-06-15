using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Entites
{
    public class Rating
    {
        public int Id { get; set; }
        [Range(1,5)]
        public int Rate { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
    
}
