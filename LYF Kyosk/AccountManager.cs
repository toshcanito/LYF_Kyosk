using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYF_Kyosk.Models;

namespace LYF_Kyosk
{
    public class AccountManager
    {
        private static Account _SessionAccount { get; set; }
        private AccountManager() { }
        private static Account GetSessionAccountInstance() 
        {
            if (_SessionAccount == null)
            {
                _SessionAccount = new Account();
            }
            return _SessionAccount;
        }

        public static void LoadSessionAccount(Account account) 
        {
            GetSessionAccountInstance();
            _SessionAccount = account;
        }

        public static Account GetSessionAccount() 
        {
            return GetSessionAccountInstance();
        }
    }
}
