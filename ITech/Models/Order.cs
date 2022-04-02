using ITech.Data.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "please enter your address ")]
        [Display(Name = "Address Line ")]
        [StringLength(100)]
        public string AddressLine { get; set; }

        [Required(ErrorMessage = "please enter your city")]
        [StringLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "please enter your Governorate")]
        [StringLength(50)]
        public string Governorate { get; set; }

        [Required(ErrorMessage = "please enter your country")]
        [StringLength(50)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }


    }
}
