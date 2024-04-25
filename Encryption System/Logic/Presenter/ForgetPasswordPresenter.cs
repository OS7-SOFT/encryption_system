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
    public class ForgetPasswordPresenter
    {
        IForgetPasswordView view;
        ForgetPasswordModel model = new ForgetPasswordModel();
        public ForgetPasswordPresenter(IForgetPasswordView view)
        {
            this.view = view;
            this.view.CheckEvent += CheckFavorite;
            this.view.EditEvent += EditPassword;
        }

        void ConnectModelWithView() {
            model.Password = view.newPassword;
        }

        private void CheckFavorite(object sender, EventArgs e)
        {
            var accounts = GetUseresServices.GetAllAccounts();

            if(view.Favorite != null)
            {
                for (int i = 0; i < accounts.Rows.Count; i++)
                {
                    byte[] decodedBytes = Convert.FromBase64String(accounts.Rows[i]["Favorite"].ToString());
                    string decodedFavorite = System.Text.Encoding.UTF8.GetString(decodedBytes);
                    if (view.UserName == accounts.Rows[i]["UserName"].ToString() && view.Favorite == decodedFavorite)
                    {
                        model.Id = (int)accounts.Rows[i]["Id"];
                        view.IsValid = true;
                        break;
                    }
                        
                    else
                    {
                        view.IsValid = false;
                        view.Message = "invalid Favorite !";

                    }
                }
            }
        }

        private void EditPassword(object sender, EventArgs e)
        {
            if (CheckField())
            {
                ConnectModelWithView();
                //Encoded Password
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(model.Password);
                string passwordEncoded = Convert.ToBase64String(passwordBytes);
                ForgetPasswordServices.EditPassword(model.Id,passwordEncoded);
                view.IsEdited = true;
                view.Message = "Password change Successfully";

            }
            else
                view.IsEdited = false;
            
        }

        private bool CheckField()
        {
            if (view.Favorite.Trim() == "" || view.newPassword.Trim() == "" || view.ConfirmPass.Trim() == "")
            {
                view.Message = "Please Fill fields";
                return false;
            }
            if (view.newPassword != view.ConfirmPass)
            {
                view.Message = "Confirm Password and Password do not match";
                return false;
            }

            return true;
        }

        private void CleareAllFields()
        {
            view.newPassword = "";
            view.ConfirmPass = "";
            view.Favorite = "";
        }
    }
}
