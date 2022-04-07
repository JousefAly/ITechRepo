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
            SentTime = DateTime.Now;
        }
        private bool _checked = false;
        private DateTime _lastCheckTime = new DateTime();
        public string Id { get; set; }
        public string Message { get; set; }
        public DateTime SentTime { get; set; }
        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                if (value)
                    _lastCheckTime = DateTime.Now;               
            }
        }
        public DateTime LastCheckTime { get; set; }
        public string ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
        public string SenderId { get; set; }
        public AppUser Sender { get; set; }
    }
}
