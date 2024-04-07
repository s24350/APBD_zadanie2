using LegacyApp.Core.Interfaces;
using LegacyApp.Validators.Users;
using System;

namespace LegacyApp
{
/*
 * UI - user interface (html, console)
 * BL - business logic
 * IO - infrastruktura (input/output, np. komunikacja z baza danych)
 */
    public class UserService
    {

        private IInputValidator _inputValidator;
        private IClientRepository _clientRepository;
        private ICreditLimitService _creditLimitService;

        public UserService() { //alternatywnie IInputValidator _inputValidator w konstruktorze;
            _inputValidator = new InputValidator();
            //this._validator = _validator;

            _clientRepository = new ClientRepository();
            _creditLimitService = new UserCreditService();
        }

        
        

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            //Single responsibility - nie moze byc klasa za kilka rzeczy odpowiedzialna

            
            if (!_inputValidator.ValidateEmail(email))
            {
                return false;
            }
            if (!_inputValidator.ValidateName(firstName, lastName))
            {
                return false;
            }
            if (!_inputValidator.ValidateDate(dateOfBirth))
            {
                return false;
            }

            

            //DIP - sama klasa nie powinna byc za to odpowiedzialna, wyniesc abstrakcyjnie poza te metode
            //Infrastruktura
            
            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            //to nas boli - DIP, if wyniesc abstrakcyjnie poza te klase.
            // logika biznesowa wymieszana z infrastrutktura
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
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

            //Logika biznesowa
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            //statyczna klasa UserDataAccess
            //Infastruktura (zapis do bazy danych)
            UserDataAccess.AddUser(user);
            return true;
        }

        
    }
}
