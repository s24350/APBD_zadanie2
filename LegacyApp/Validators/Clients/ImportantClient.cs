using LegacyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Validators.Clients
{
    public class ImportantClient : IClientType
    {
        private ICreditLimitService _creditLimitService;
        public void CheckCredit(ref User user) {
            int creditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
            creditLimit = creditLimit * 2;
            user.CreditLimit = creditLimit;
        }
    }
}
