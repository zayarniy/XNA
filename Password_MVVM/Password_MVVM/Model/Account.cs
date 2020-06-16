using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_MVVM.Model
{
    class Account
    {
        public string Login { get; set; } = "None";
        public string Password { get; set; } = "None";

        public Account(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }

    class Accounts
    {
        public bool CheckLogin(string login)
        {
            if (login.Length < 2 || login.Length > 20) return false;
            return true;
        }

        public bool CheckPassword(string password)
        {
            if (password.Length < 2 || password.Length > 20) return false;
            return true;
        }

        bool Check(Account account)
        {
            return CheckLogin(account.Login) && CheckPassword(account.Password);
        }

        public bool Checks(Account account)
        {
            foreach (Account acc in ListAccounts)
                if (Check(account) == false) return false;
            return true;
        }

        public static List<Account> ListAccounts
        {
            get;
        }
            = new List<Account>() {
                                  new Account("root", "root"),
                                  new Account("login","password"),
                                  new Account("admin","admin")
                                  };
    }
}
