using ITech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Validators
{
    public class OrderValidator : IOrderValidator
    {
        private readonly IGlobalValidator globalValidator;

        public OrderValidator(IGlobalValidator globalValidator)
        {
            this.globalValidator = globalValidator;
        }
        public bool IsValid(Order order)
        {
            return order.OrderId > 0 && globalValidator.IsValidPhoneNumber(order.PhoneNumber);
        }
    }
}
