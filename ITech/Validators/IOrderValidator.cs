using ITech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITech.Validators
{
    public interface IOrderValidator
    {
        bool IsValid(Order order);
    }
}
