using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ITech.Validators
{
    public class GlobalValidator : IGlobalValidator
    {
        public  bool IsValidPhoneNumber(string number)
        {
            //Regex phoneNumberRegex = new Regex("^\\+?[1-9][0-9]{7,14}$");
            //return phoneNumberRegex.IsMatch(number);
            return !string.IsNullOrEmpty(number) && number.Length > 9;
        }
    }
}
