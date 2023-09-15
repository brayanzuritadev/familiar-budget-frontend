using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsApp1;

namespace Administracion
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            defineColorAndFont();
        }

        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMax.Visible = false;
            btnRes.Visible = true;
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRes.Visible = false;
            btnMax.Visible= true;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void titleBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void btnUsers_Click(object sender, EventArgs e)
        {
            openChildForm(new Users());  
        }

        private void defineColorAndFont()
        {
            btnUsers.ForeColor = Tools.textColor;
            btnArm.ForeColor = Tools.textColor;
            btnPointValue.ForeColor = Tools.textColor;
            btnPolig.ForeColor = Tools.textColor;
            btnReport.ForeColor = Tools.textColor;

            btnUsers.Font = Tools.fontTitleButtons;
            btnArm.Font = Tools.fontTitleButtons;
            btnPointValue.Font = Tools.fontTitleButtons;
            btnPolig.Font = Tools.fontTitleButtons;
            btnReport.Font = Tools.fontTitleButtons;
        }

        private void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private bool openChildForm(object childForm)
        {
            if (this.container.Controls.Count > 0)
                this.container.Controls.RemoveAt(0);

            Form cf = childForm as Form;
            cf.TopLevel = false;
            cf.FormBorderStyle = FormBorderStyle.None;

            cf.Size = this.container.Size;

            cf.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.container.Controls.Add(cf);
            this.container.Tag = cf;
            cf.Show();
            return true;
        }

        private void container_Resize(object sender, EventArgs e)
        {
            CenterChildControls();
        }

        private void CenterChildControls()
        {
            foreach (Control control in container.Controls)
            {
                //calculate the position for center the child form
                int x = (container.Width - control.Width) / 2;
                int y = (container.Height - control.Height) / 2;
                control.Location = new Point(x, y);
            }
        }

        private void btnPolig_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
        }

        private void btnArm_Click(object sender, EventArgs e)
        {
            openChildForm(new Weapons());
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPointValue_Click(object sender, EventArgs e)
        {
            openChildForm(new PointValue());
        }
    }
}
