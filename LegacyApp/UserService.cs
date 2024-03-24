using LegacyApp.Core.Interfaces;
using LegacyApp.Validators.Users;
using System;

namespace LegacyApp
{
    public class UserService
    {

        private IInputValidator _validator;

        public UserService() { //IInputValidator _validator ==> w konstruktorze;
            _validator = new InputValidator();
            //this._validator = _validator;
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            //Single responsibility - nie moze byc klasa za kilka rzeczy odpowiedzialna

            if (!_validator.ValidateEmail(email))
            {
                return false;
            }
            if (!_validator.ValidateName(firstName, lastName))
            {
                return false;
            }
            if (!_validator.ValidateDate(dateOfBirth))
            {
                return false;
            }



            //DIP - sama klasa nie powinna byc za to odpowiedzialna, wyniesc abstrakcyjnie poza te metode
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            //to nas boli - DIP, if wyniesc abstrakcyjnie poza te klase.
            
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
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

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        
    }
}
