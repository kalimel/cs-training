using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantisTest
{
    public class AccountData
    {
        private string username;
        private string password;

        public AccountData(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                this.username = username;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                this.password = password;
            }
        }
    }
}
