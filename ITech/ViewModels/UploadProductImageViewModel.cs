using ITech.Data.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.ViewModels
{
    public class UploadProductImageViewModel
    {
        [Required]
        public int ImageNumber { get; set; }
        
        [Required]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public Product Product { get; set; }

        //[DisplayName("Image Name")]
        //public string ImageName { get; set; }
    }
}
