using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Encryption_System.Views.Forms
{
    public partial class SplashScreen : DevExpress.XtraSplashScreen.SplashScreen
    {
        public SplashScreen()
        {
            InitializeComponent();
            this.labelCopyright.Text = "Copyright © " + DateTime.Now.Year.ToString();
            this.Load += delegate
            {

                timer1.Enabled = true;
                timer1.Tick += delegate
                {
                    progressBar1.Increment(3);

                    if (progressBar1.Value == progressBar1.Maximum)
                    {
                        timer1.Enabled = false;
                        this.Close();
                    }
                };
            };
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }
    }
}