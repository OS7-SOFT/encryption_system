using Encryption_System.Logic.Services;
using Encryption_System.Views.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Logic.Presenter
{
    public class LoginPresenter
    {
        ILoginView view;

        public LoginPresenter(ILoginView view)
        {
            this.view = view;
            this.view.LoginEvent += LoginMethod;
        }

        private void LoginMethod(object sender, EventArgs e)
        {
            var accounts = GetUseresServices.GetAllAccounts();

            if (CheckInput())
            {
                for (int i = 0; i < accounts.Rows.Count; i++)
                {
                    byte[] decodedBytes = Convert.FromBase64String(accounts.Rows[i]["Password"].ToString());
                    string decodedPassword = System.Text.Encoding.UTF8.GetString(decodedBytes);
                    if (view.UserName == accounts.Rows[i]["UserName"].ToString() && view.Password == decodedPassword)
                        view.IsLogged = true;
                    else
                        view.Message = "Login attempet is invalid";
                }
            }
        }

        //Check input is correct
        private bool CheckInput()
        {
            if (view.UserName.Trim() == "")
            {
                view.Message = "Please fill User Name fild";
                return false;
            }
            if (view.Password.Trim() == "")
            {
                view.Message = "Please fill Password fild";
                return false;
            }

            return true;

        }
    }
}
