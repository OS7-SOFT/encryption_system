using Encryption_System.Logic.Presenter;
using Encryption_System.Views.Forms;
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

namespace Encryption_System
{
    public partial class Login : Form, ILoginView
    {
        string message;
        public static bool isLogged;
        public static string userName;

        

        LoginPresenter loginPresenter;

        public Login()
        {
            InitializeComponent();

            loginPresenter = new LoginPresenter(this);
            //set shadow to form 
            shadow.SetShadowForm(this);
            //set animation to form
            animateWindow.SetAnimateWindow(this);

            performMethod();
        }

        private void performMethod()
        {
            //login
            btnLogin.Click += delegate
            {
                LoginEvent?.Invoke(this, EventArgs.Empty);
                if (isLogged)
                {
                    this.Hide();
                    Home.Instance().Show();
                }
                else
                    MessageBox.Show(message, "Error in input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            //Exit
            btnExit.Click += delegate
            {
                Application.Exit();
            };
            //Forget Password
            lblForget.Click += delegate
            {
                if (txtUserName.Text.Trim() == "")
                    MessageBox.Show("Please input username !" , "Warring",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                else
                {
                    userName = txtUserName.Text;
                    ForgetPassword.instance().ShowDialog();
                }
                    
            };
        }

        public string UserName
        {
            get { return txtUserName.Text; }
            set { txtPassword.Text = value; }
        }
        public string Password 
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }
        public bool IsLogged
        {
            get { return isLogged; }
            set { isLogged = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public event EventHandler LoginEvent;


        //Singelton
        private static Login obj = null;
        public static Login instance()
        {
            if (obj == null)
            {
                return obj = new Login();
            }
            return obj;
        }

        
    }
}
