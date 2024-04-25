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
    public partial class instructionForm : Form
    {
        public instructionForm()
        {
            InitializeComponent();
            shadow.SetShadowForm(this);

            //pack to home
            btnBack.Click += delegate
            {
                this.Hide();
            };
        }



        //Singelton
        private static instructionForm Object = null;
        public static instructionForm Instance()
        {
            if (Object == null)
                Object = new instructionForm();
            return Object;
        }

       
    }
}
