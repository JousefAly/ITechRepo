using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Entites
{
    public class Rating
    {
        public int Id { get; set; }
        public Rate Rate { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
    public enum Rate
    {
        One,
        Two,
        Three,
        Four,
        Five

    }
}
