using LegacyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Validators.Clients
{
    

    public class CommonClient : IClientType
    {

        public void CheckCredit(ref User user)
        {
            user.HasCreditLimit = true;
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
        }

    }
}
