using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WindowsApp1;
using Service;

namespace Administracion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();


        }

        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.Dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void Login_Load(object sender, EventArgs e)
        {
            defineFontAndColor();
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            if(txtEmail.Text == "Email")
            {
                txtEmail.Text = string.Empty;
                txtEmail.ForeColor = Tools.textColor;
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                txtEmail.Text = "Email";
                txtEmail.ForeColor = Tools.textColorBackground;
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Contraseña")
            {
                txtPass.Text = string.Empty;
                txtPass.ForeColor = Tools.textColor;
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "Contraseña";
                txtPass.ForeColor = Tools.textColorBackground;
                txtPass.UseSystemPasswordChar = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm form = new MainForm();
            form.Show();
        }

        private void defineFontAndColor()
        {
            linkLabel.ForeColor = Tools.textColor;
            lblLogin.ForeColor = Tools.textColor;

            txtEmail.ForeColor = Tools.textColorBackground;
            txtPass.ForeColor = Tools.textColorBackground;

            btnLogin.ForeColor = Tools.textColor;
            linkLabel.LinkColor = Tools.textColor;

            label1.ForeColor = Tools.textColor;
            label3.ForeColor = Tools.textColor;
        }
    }
}
