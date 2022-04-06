using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Models
{
    public class Job
    {
        public Job()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string JobTitle { get; set; }
        public List<JobApplication> JobApplications { get; set; }
    }
}
