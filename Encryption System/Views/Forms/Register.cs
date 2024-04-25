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
    public partial class Register : Form,IRegisterView
    {
        string message;
        bool isSuccessed;

        RegisterPresenter registerPresenter;

        public Register()
        {
            InitializeComponent();

            registerPresenter = new RegisterPresenter(this);
            //set shadow to form
            shadow.SetShadowForm(this);
            //set animation to form
            animateWindow.SetAnimateWindow(this);
            
            performMethod();
        }

        private void performMethod()
        {
            //register
            btnRegister.Click += delegate { 
                RegisterEvent?.Invoke(this, new EventArgs());
                if (isSuccessed)
                {
                    MessageBox.Show(message, "Create Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    MessageBox.Show(message, "Error in input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            //Cancel
            btnCancel.Click += delegate {
                CancelEvent?.Invoke(this, new EventArgs());
                this.Close();
            };
        }

        public string UserName 
        {
            get { return txtUserName.Text; } 
            set { txtUserName.Text = value; }
        }
        public string Password
        {
            get { return txtPassowrd.Text; }
            set { txtPassowrd.Text = value; }
        }
        public string ConfirmPassword
        {
            get { return txtConfirmPassword.Text; }
            set { txtConfirmPassword.Text = value; }
        }
        public string Favorite
        {
            get { return txtFavorite.Text; }
            set { txtFavorite.Text = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public bool isSuccessful
        {
            get { return isSuccessed; }
            set { isSuccessed = value; }
        }

        public event EventHandler RegisterEvent;
        public event EventHandler CancelEvent;


        //Singelton
        private static Register obj = null;
        public static Register instance()
        {
            if (obj == null)
            {
                return obj = new Register();
            }
            return obj;
        }
    }
}
