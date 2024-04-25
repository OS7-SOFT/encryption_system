using Encryption_System.Logic.Presenter;
using Encryption_System.Views.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryption_System.Views.Forms
{
    public partial class Home : Form , IHome
    {
        string fileName;
        string path; 
        string message;
        bool isEncrypted;

        HomePresenter homePresenter;

        public Home()
        {
            InitializeComponent();

            homePresenter = new HomePresenter(this);
            perfomMethod();
            
        }

        private void perfomMethod()
        {
            //Exit
            btnExit.Click += delegate {
                var result = MessageBox.Show("Are you sure to exit ?","Warring",MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if(result == DialogResult.Yes)
                    Application.Exit();
            }; 

            //Create Account
            btnCreateAccuont.Click += delegate {
                Register r = Register.instance();
                r.ShowDialog();
            };

            //Encrypt File
            btnEncrypt.Click += delegate { 
               OpenFileDialog ofd = new OpenFileDialog();
               DialogResult result = ofd.ShowDialog();
               if(result == DialogResult.OK)
                {
                    var accept = MessageBox.Show($"Are you sure to encrypt this file {ofd.FileName}","Accept Encrypt",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if(accept == DialogResult.Yes)
                    {
                        fileName = Path.GetFileName(ofd.FileName);
                        path = ofd.FileName;
                        EncryptedEvent?.Invoke(this, EventArgs.Empty);
                        if (isEncrypted)
                        {
                            MessageBox.Show(message,"Successed",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    
                }
            };

            //Show Decrypt form
            btnDecrypt.Click += delegate {
                
                DecryptData.Instance().Show();
                this.Close();
            };

            //Show instruction form
            btnInstructions.Click += delegate {
                instructionForm.Instance().Show();

            };
        }
        
        public string FileName
        {
            get { return fileName; }
         
        }
        public string PathFile
        {
            get { return path; }
        }
        public bool IsEncrypted
        {
            get { return isEncrypted; }
            set { isEncrypted = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public string EncryptCount
        {
            set { lblEncryptCount.Text = value; }
        }
        public string LastDateEncrypt
        {
            set { lblDate.Text = value; }
        }

        //Event
        public event EventHandler EncryptedEvent;

        //Singelton
        private static Home Object = null;
        public static Home Instance()
        {
            if (Object == null)
                Object = new Home();
            return Object;
        }

    }
}
