using ITech.Data;
using System;

namespace ITech.Models
{
    public class JobApplication
    {
        public JobApplication()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public bool Accepted { get; set; }
        public string JobId { get; set; }
        public Job Job { get; set; }
        public string ApplicantId { get; set; }
        public AppUser Applicant { get; set; }
    }
}
