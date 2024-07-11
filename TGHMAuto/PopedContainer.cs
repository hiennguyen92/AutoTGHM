using POJO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using ImageProgressing;
using System.Windows.Input;

namespace TGHMAuto
{
    public partial class PopedContainer : UserControl
    {
        private Form frParent;

        private Account account;


        public PopedContainer(Form frParent)
        {
            InitializeComponent();
            this.frParent = frParent;

            cbHP.CheckedChanged += CbHP_CheckedChanged;
            cbMP.CheckedChanged += CbMP_CheckedChanged;
            cbFollowingKey.CheckedChanged += CbFollowingKey_CheckedChanged;





            btnStart.Click += BtnStart_Click;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //var sc = new ScreenCapture();
            //var bitmap = sc.GetScreenshot(account.HWnd);
            //sc.WriteBitmapToFile("temp.jpg", bitmap);


            var windowCapture = new WindowCapture(this.frParent, account.HWnd, account.File);

            windowCapture.Start();

            //Bitmap icon = (Bitmap)Bitmap.FromFile("./key.png");
            //Bitmap full = (Bitmap)Bitmap.FromFile("./full.png");
            //var point = ImageScanOpenCV.FindOutPoint(full, icon);
            //Console.WriteLine("POINT: "+ point);

            //ImageProgressing.Keyboard.f1 = true;
            //ImageProgressing.Keyboard.pressKey(account.HWnd, "F1");

        }

        public void SetAccount(Account account)
        {
            this.account = account;
            init();
        }

        private void init()
        {
            if(this.account != null)
            {
                cbHP.Checked = this.account.IsHP;
                cbMP.Checked = this.account.IsMP;
            }
        }

        private void CbFollowingKey_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFollowingKey.Checked)
            {
                cbJumpFollowing.Enabled = true;
            }
            else
            {
                cbJumpFollowing.Enabled = false;
            }
        }

        private void CbMP_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMP.Checked)
            {
                ccbKeyMP.Enabled = true;
                ccbPercentMP.Enabled = true;
            }
            else
            {
                ccbKeyMP.Enabled = false;
                ccbPercentMP.Enabled = false;
            }
        }

        private void CbHP_CheckedChanged(object sender, EventArgs e)
        {
            if(cbHP.Checked) {
                ccbKeyHP.Enabled = true;
                ccbPercentHP.Enabled = true;
            }
            else{
                ccbKeyHP.Enabled = false;
                ccbPercentHP.Enabled = false;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {// Alt+F4 is to closing
            if ((keyData & Keys.Alt) == Keys.Alt)
                if ((keyData & Keys.F4) == Keys.F4)
                {
                    this.Parent.Hide();
                    return true;
                }

            if ((keyData & Keys.Enter) == Keys.Enter)
            {
                if (this.ActiveControl is Button)
                {
                    (this.ActiveControl as Button).PerformClick();
                    return true;
                }
            }

            return base.ProcessDialogKey(keyData);
        }
    }
}
