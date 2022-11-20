using ITech.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ITech.Tests.Validator
{
    public class GlobalValidatorTests
    {
        [Fact]
        public void ShoudBeValidPhoneNumber()
        {
            var phoneNumber = "01114455663";
            var globalValidator = new GlobalValidator();
            Assert.True(globalValidator.IsValidPhoneNumber(phoneNumber));
        }
    }
}
