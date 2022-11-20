using ITech.Models;
using ITech.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ITech.Tests.Validator
{
    public class OrderValidatorTests
    {
        public OrderValidatorTests()
        {

        }
        [Fact]
        public void ShouldBeValid()
        {
            //arrange
            var order = new Order
            {
                OrderId = 22,
                PhoneNumber = "01112255663"
            };
            //act

            var orderValidator = new OrderValidator(null);
            bool validatorResult = orderValidator.IsValid(order);

            //assert
            Assert.True(validatorResult);
        }
    }
}
