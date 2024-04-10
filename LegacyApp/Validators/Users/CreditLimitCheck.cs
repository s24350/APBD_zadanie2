using LegacyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Validators.Users
{
    internal class CreditLimitCheck : ICreditLimitCheck
    {
        private ICreditLimitService _creditLimitService;

        public CreditLimitCheck(){
            _creditLimitService = new UserCreditService();
            
        }
        
        public void CheckCreditLimit(ref User user, string clientType)
        {
            //to nas boli - DIP, if wyniesc abstrakcyjnie poza te klase.
            // logika biznesowa wymieszana z infrastrutktura
            if (clientType == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (clientType == "ImportantClient")
            {
                //using działa podobnie jak try with resources.
                //jak dojdzie się do końca bloku to uruchomiona zostanie metoda zwalniająca zadoby
                //wymaga implementacji Interfejsu IDisposable

                int creditLimit = _creditLimitService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
            else
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
}
