using Administracion.users.tools;
using Data.Dto;
using Data.Entity;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Administracion.users
{
    public partial class UserEditForm : Form
    {
        UserService userService = new UserService();
        UserDetailDto _userDetail;
        string imageId;
        public UserEditForm( UserDetailDto userDetailDto)
        {
            _userDetail = userDetailDto;
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var user = new User();
            user.UserId = _userDetail.UserId;
            user.Ci = txtCi.Text;
            user.Name = txtName.Text;
            user.Email = txtEmail.Text;
            user.RoleId = cbUserType.SelectedIndex+1;
            user.Photography = imageId;

            ValidationUser validationUser = new ValidationUser();

            var userValid = validationUser.userValid(user);

            if (userValid.success)
            {
                var result = userService.Update(user);
                if (result)
                {
                    MessageBox.Show("Usuario actualizado");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar al Usuario");
                }
            }
            else
            {
                MessageBox.Show("No se pudo actualizar al Usuario");

            }
        }

        private void UserEditForm_Load(object sender, EventArgs e)
        {
            FillCb();
            txtCi.Text = _userDetail.Ci.ToString();
            txtEmail.Text = _userDetail.Email.ToString();
            txtName.Text = _userDetail.Name.ToString();
            cbUserType.SelectedIndex = _userDetail.RoleId-1;
            ShowImage(_userDetail.Photography);

        }

        private void ShowImage(string id)
        {
            string imageUrl = $"https://drive.google.com/uc?export=download&id={id}";
            try
            {
                WebClient webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(imageUrl);

                using (var ms = new System.IO.MemoryStream(imageBytes))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    imageId = id;
                    pictureBox1.Image = img;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen: " + ex.Message);
            }
        }

        private void FillCb()
        {
            List<string> listaDatos = new List<string>
            {
                "Administrador",
                "Tirador"
            };

            cbUserType.Items.AddRange(listaDatos.ToArray());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg, *.png)|*.jpg;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImageUpload uploadImge = new ImageUpload();
                var response = uploadImge.UploadImg(openFileDialog);

                ShowImage(response.Result.Id);
            }
        }
    }
}
