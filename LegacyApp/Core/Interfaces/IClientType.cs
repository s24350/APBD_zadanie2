using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Core.Interfaces
{
    public interface IClientType
    {
        public void CheckCredit(ref User user);
    }
}
