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
    public partial class DecryptData : Form, IDecryptedView
    {
        string message;
        string id;
        bool isDecrypt;

        DecryptedPresenter decryptedPresenter;
        public DecryptData()
        {
            InitializeComponent();

            decryptedPresenter = new DecryptedPresenter(this);
            performMethod();
        }

        private void performMethod()
        {
            //Back to home view
            btnBack.Click += delegate
            {
                Home home = new Home();
                home.Show();
                this.Hide();

            };

            //Search 
            txtSearch.TextChange += delegate
            {
                SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            //Decrypt Data
            btnDecrypt.Click += delegate
            {

                if (dgv.Rows.Count > 0)
                {
                    id = dgv.CurrentRow.Cells[0].Value.ToString();
                    var name = dgv.CurrentRow.Cells[1].Value.ToString();
                    var result = MessageBox.Show($"Are you sure to Decrypt this file {name}", "accept Decrypt", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DecryptedEvent?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show(message, "Decrypted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (isDecrypt)
                            DeleteEvent?.Invoke(this, EventArgs.Empty);

                    }
                }

                else
                {
                    MessageBox.Show("No there any file encrypted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            };


        }



        //propertes
        public int Id { get { return Convert.ToInt32(id); } }
        public bool IsDecrypted { set { isDecrypt = value; } }
        public string Message { set { message = value; } }
        public string Value { get { return txtSearch.Text; } set { txtSearch.Text = value; } }
        //Events
        public event EventHandler SearchEvent;
        public event EventHandler DecryptedEvent;
        public event EventHandler DeleteEvent;

        //Set data in gridView
        public void SetDataFileEncrypted(BindingSource fileDataList)
        {
            if (fileDataList != null)
            {
                dgv.DataSource = fileDataList;
                lblNull.Visible = false;
            }
            else
                lblNull.Visible = true;

        }

        //Singelton
        private static DecryptData Object = null;
        public static DecryptData Instance()
        {
            if (Object == null)
                Object = new DecryptData();
            return Object;
        }

    }
}
