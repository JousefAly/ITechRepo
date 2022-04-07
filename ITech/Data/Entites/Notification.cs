using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Data.Entites
{
    public class Notification
    {
        public Notification()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Message { get; set; }
        public string ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
        public string SenderId { get; set; }
        public AppUser Sender { get; set; }
    }
}
