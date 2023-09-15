using Data.Dto;
using Data.Entity;
using Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Administracion
{
    public partial class Users : Form
    {
        UserService service = new UserService();
        List<UserDetailDto> userDetailList;
        string imageId;
        private const string DEFAULT_IMG = "1xnMwqSCBRPTNZqCyGqg_Bg6_FhLecf7W";
        public Users()
        {
            InitializeComponent();
            LlenarComboBox();
            CreateTable();
            FillTable();

            txtRPass.UseSystemPasswordChar = true;
            txtPass.UseSystemPasswordChar = true;
        }

        private void FillTable()
        {
            userDetailList = service.GetAll();

            dataGridView1.DataSource = userDetailList;
        }

        private void CreateTable()
        {
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn ciColumn = new DataGridViewTextBoxColumn();
            ciColumn.DataPropertyName = "Ci";
            ciColumn.HeaderText = "CI";
            ciColumn.Width = 80;

            DataGridViewTextBoxColumn nombreColumn = new DataGridViewTextBoxColumn();
            nombreColumn.DataPropertyName = "Name";
            nombreColumn.HeaderText = "Nombre";
            nombreColumn.Width = 250;

            DataGridViewTextBoxColumn emailColumn = new DataGridViewTextBoxColumn();
            emailColumn.DataPropertyName = "Email";
            emailColumn.HeaderText = "Email";
            emailColumn.Width = 250;

            DataGridViewTextBoxColumn tipoTiradorColumn = new DataGridViewTextBoxColumn();
            tipoTiradorColumn.DataPropertyName = "rolename";
            tipoTiradorColumn.HeaderText = "Tipo de tirador";

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Acciones";
            buttonColumn.Text = "Editar";
            buttonColumn.UseColumnTextForButtonValue = true; 

            dataGridView1.Columns.Add(ciColumn);
            dataGridView1.Columns.Add(nombreColumn);
            dataGridView1.Columns.Add(emailColumn);
            dataGridView1.Columns.Add(tipoTiradorColumn);
            dataGridView1.Columns.Add(buttonColumn);
        }

        private void LlenarComboBox()
        {
            List<string> listaDatos = new List<string>
            {
                "Administrador",
                "Tirador"
            };

            cbUserType.Items.AddRange(listaDatos.ToArray());
            cbUserType.SelectedIndex = 0;
        }

        private bool ValidateFields(User user)
        {
            if (txtCi.Text=="Cedula de identidad" || txtCi.Text == "")
            {
                txtCi.ForeColor = Color.Red;
                return false;
            }
            else
            {
                txtCi.ForeColor = Color.Black;
            }

            var userEntity = service.Get(user);

            if (userEntity?.UserId == user.UserId)
            {
                txtCi.ForeColor = Color.Red;
                MessageBox.Show("La cedula de identidad pertenece a un usuario existente");
                return false;
            }

            if (userEntity?.Email == user.Email)
            {
                txtEmail.ForeColor = Color.Red;
                MessageBox.Show("El email pertenece a un usuario existente");
                return false;
            }

            if (txtName.Text == "Nombre completo" || txtName.Text == "")
            {
                txtName.ForeColor = Color.Red;
                MessageBox.Show("El nombre completo es requerido");
                return false;
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(emailPattern);

            if (!regex.IsMatch(txtEmail.Text))
            {
                txtEmail.ForeColor = Color.Red;
                MessageBox.Show("El formato de su Email no es el correcto");
                return false;
            }

            if (cbUserType.SelectedIndex == 0)
            {
                if (txtPass.Text != txtRPass.Text)
                {
                    txtPass.ForeColor = Color.Red; // Cambiar el color de texto a rojo.
                    txtRPass.ForeColor = Color.Red;
                    MessageBox.Show("Los campos contraseña y repetir contraseña no son iguales");
                    return false;
                }
                else
                {
                    txtPass.ForeColor = Color.Black; // Cambiar el color de texto a rojo.
                    txtRPass.ForeColor = Color.Black;
                }

                if (txtPass.Text.Length < 6)
                {
                    txtPass.ForeColor = Color.Red; // Cambiar el color de texto a rojo.
                    txtRPass.ForeColor = Color.Red;
                    MessageBox.Show("Su contraseña debe de ser mayor a 5 caracteres");
                    return false;
                }
            }

            return true;
        }

        private void txtCi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(emailPattern);

            if (regex.IsMatch(txtEmail.Text))
            {
                txtEmail.ForeColor = Color.Black; 
            }
            else
            {
                txtEmail.ForeColor = Color.Red;
            }
        }

        private void cbUserType_DropDown(object sender, EventArgs e)
        {
            cbUserType.ForeColor = Tools.textColor2;
        }

        private void cbUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUserType.SelectedIndex == 1)
            {
                txtPass.Visible = false;
                txtRPass.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
            }
            else
            {
                txtPass.Visible = true;
                txtRPass.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
            }
        }

        private void CleanFields()
        {
            cbUserType.SelectedIndex = 0;

            txtName.Text = "";
            txtCi.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";
            txtRPass.Text = "";

            txtName.ForeColor = Tools.txtColorBackground2;
            txtPass.ForeColor = Tools.txtColorBackground2;
            txtEmail.ForeColor = Tools.txtColorBackground2;
            txtRPass.ForeColor = Tools.txtColorBackground2;
            txtCi.ForeColor = Tools.txtColorBackground2;
        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            //var upload = new UploadImage();

            //upload.uploadImg();

            /*string imageUrl = $"https://drive.google.com/uc?export=download&id={id}";
            //string imageUrl = "URL_DE_TU_IMAGEN_EN_GOOGLE_DRIVE";
            try
            {
                // Descargar la imagen
                WebClient webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(imageUrl);

                // Crear una imagen desde los bytes descargados
                using (var ms = new System.IO.MemoryStream(imageBytes))
                {
                    Image img = Image.FromStream(ms);

                    // Mostrar la imagen en el PictureBox
                    pictureBox1.Image = img;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen: " + ex.Message);
            }*/
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica si se hizo clic en la columna de botones
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {
                // Accede a los datos de la fila haciendo referencia a las propiedades
                string ci = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                var userSelected = userDetailList.Find(x => x.Ci == int.Parse(ci));

                // Realiza la lógica que deseas con los datos de la fila
                MessageBox.Show($"Se hizo clic en el botón de la fila {e.RowIndex + 1}\n" +
                                $"CI: {ci}\n" +
                                $"Nombre: {userSelected.Name}\n" +
                                $"Email: {userSelected.Email}\n" +
                                $"Nombre: {userSelected.Photography}\n" +
                                $"Tipo de tirador: {userSelected.RoleName}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = richTextBox1.Text;

            Clipboard.SetText(url);

            MessageBox.Show("El enlace se ha copiado al portapapeles.");
        }
        private async void btnUpload_Click_1(object sender, EventArgs e)
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
        private void ShowImage(string id)
        {
            string imageUrl = $"https://drive.google.com/uc?export=download&id={id}";
            try
            {
                WebClient webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(imageUrl);

                using (var ms = new System.IO.MemoryStream(imageBytes))
                {
                    Image img = Image.FromStream(ms);

                    pictureBox1.Image = img;
                    imageId = id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen: " + ex.Message);
            }
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            var user = new User();

            user.RoleId = cbUserType.SelectedIndex + 1;
            user.Name = txtName.Text;
            user.Email = txtEmail.Text;
            user.Ci = txtCi.Text;
            user.Password = txtPass.Text;
            user.Photography = imageId == null ? DEFAULT_IMG : imageId;

            if (ValidateFields(user))
            {

                if (cbUserType.SelectedIndex == 1)
                {
                    user.Password = txtCi.Text;
                }

                if (service.Create(user))
                {
                    MessageBox.Show("Usuario registrado correctamente");
                    FillTable();
                    CleanFields();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar al usuario");
                }
            }
        }
    }
}
