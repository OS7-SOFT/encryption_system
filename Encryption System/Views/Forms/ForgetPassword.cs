using Encryption_System.Logic.Presenter;
using Encryption_System.Views.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryption_System.Views.Forms
{
    public partial class ForgetPassword : Form,IForgetPasswordView
    {
        string message;
        bool isVaild;
        bool isEdited;

        Home homeForm = Home.Instance();
        ForgetPasswordPresenter forgetPasswordPresenter;

        public ForgetPassword()
        {
            InitializeComponent();
            shadow.SetShadowForm(this);
            forgetPasswordPresenter = new ForgetPasswordPresenter(this);
            perfomMethod();
        }

        private void perfomMethod()
        {
            //Check Favorite
            btnOk.Click += delegate
            {
                CheckEvent.Invoke(this, EventArgs.Empty);
                if (isVaild)
                    newPassPanel.Visible = true;
                else
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            //Edit Password
            btnEdit.Click += delegate { 
                EditEvent.Invoke(this,EventArgs.Empty);
                if (isEdited)
                {
                    MessageBox.Show(message, "Edited Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();                    
                }
                else
                    MessageBox.Show(message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            //Cancel 
            btnCancel.Click += delegate
            {
                this.Close();
            };
        }


        public string Favorite
        {
            get { return txtFavorite.Text; }
            set { txtFavorite.Text = value; }
        }
        public string newPassword
        {
            get { return txtNewPassword.Text; }
            set { txtNewPassword.Text = value; }
        }
        public string ConfirmPass
        {
            get { return txtConfirmPassword.Text; }
            set { txtConfirmPassword.Text = value; }
        }
        public string UserName{ get { return Login.userName; } }
        public string Message { set { message = value; } }
        public bool IsValid { set { isVaild = value; } }
        public bool IsEdited { set { isEdited = value; } }
      

        public event EventHandler CheckEvent;
        public event EventHandler EditEvent;

        //Singelton
        private static ForgetPassword obj = null;
        public static ForgetPassword instance()
        {
            if (obj == null)
            {
                return obj = new ForgetPassword();
            }
            return obj;
        }

    }
}
