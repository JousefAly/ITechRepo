using ITech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.ViewModels
{
    public class JobsViewModel
    {
        public List<Job> Jobs { get; set; }
        public List<JobApplication> JobApplications { get; set; }
    }
}
