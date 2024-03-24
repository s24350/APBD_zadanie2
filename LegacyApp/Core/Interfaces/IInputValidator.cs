using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Core.Interfaces
{
    internal interface IInputValidator
    {
        public bool ValidateEmail(string email);
        public bool ValidateName(string firstName, string lastName);
        public bool ValidateDate(DateTime dateOfBirth);
    }
}
