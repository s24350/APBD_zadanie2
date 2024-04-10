using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Core.Interfaces
{
    internal interface ICreditLimitCheck
    {
        public void CheckCreditLimit(ref User user, string clientType);
    }
}
