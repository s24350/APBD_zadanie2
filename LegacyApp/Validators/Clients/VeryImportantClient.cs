using LegacyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Validators.Clients
{
    public class VeryImportantClient : IClientType
    {
        public void CheckCredit(ref User user)
        {
            user.HasCreditLimit = false;
        }
    }
}
