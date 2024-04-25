using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Model
{
    public class RegisterModel
    {
        private string userName;
        private string password;
        private string favorite;

        public string UserName { get { return userName; } set { userName = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Favorite { get { return favorite; } set { favorite = value; } }

       
    }
}
