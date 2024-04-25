using Encryption_System.Logic.Services;
using Encryption_System.Model;
using Encryption_System.Views.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption_System.Logic.Presenter
{
    public class RegisterPresenter
    {

        RegisterModel model = new RegisterModel();

        IRegisterView view;

        public RegisterPresenter(IRegisterView view)
        {
            this.view = view;
            this.view.RegisterEvent += CreateNewAccount;
            this.view.CancelEvent += CancelAccount;
        }

        private void ConnectModelWithView()
        {
            model.UserName = view.UserName;
            model.Password = view.Password;
            model.Favorite = view.Favorite;
        }

        private void CancelAccount(object sender, EventArgs e)
        {
            CleareAllFields();
        }

        private void CreateNewAccount(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                ConnectModelWithView();
                //Encoded Password
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(model.Password);
                string passwordEncoded = Convert.ToBase64String(passwordBytes);

                byte[] favoriteBytes = System.Text.Encoding.UTF8.GetBytes(model.Favorite);
                string favoriteEncoded = Convert.ToBase64String(favoriteBytes);

                if(GetUseresServices.GetAllAccounts().Rows.Count == 1 && GetUseresServices.GetAllAccounts().Rows[0]["UserName"].ToString() == "admin" )
                    RegisterServices.DeleteAdmin();

                RegisterServices.Add(model.UserName, passwordEncoded,favoriteEncoded);
                view.isSuccessful = true;
                view.Message = "Craete Account Successfully";
            }
            else
                view.isSuccessful = false;
        }

        private bool CheckInput()
        {
            
            if(view.Favorite.Trim() == "" || view.UserName.Trim() == "" || view.Password.Trim() == "" || view.ConfirmPassword.Trim() == "")
            {
                view.Message = "Please Fill fields";
                return false;
            }
            if(view.Password != view.ConfirmPassword)
            {
                view.Message = "Confirm Password and Password do not match";
                return false; 
            }

            return true;
        }

        private void CleareAllFields()
        {
            view.UserName = "";
            view.Password = "";
            view.ConfirmPassword = "";
            view.Favorite = "";
        }
    }
}
