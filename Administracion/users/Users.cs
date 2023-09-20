﻿using Administracion.users;
using Administracion.users.tools;
using Data.Dto;
using Data.Entity;
using Service;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Administracion
{
    public partial class Users : Form
    {
        UserService service = new UserService();
        List<UserDetailDto> userDetailList;
        protected UserDetailDto userDetail;
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

            /*DataGridViewTextBoxColumn IdColumn = new DataGridViewTextBoxColumn();
            IdColumn.DataPropertyName = "Id";
            IdColumn.HeaderText = "ID";
            IdColumn.Width = 40;*/

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

            //dataGridView1.Columns.Add(IdColumn);
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

            if (userEntity?.Ci == user.Ci)
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
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {
                string ci = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                var userSelected = userDetailList.Find(user => user.Ci == ci);

                var editUserForm = new UserEditForm(userSelected);

                editUserForm.ShowDialog();
                FillTable();
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
            registerUser();
        }

        private void registerUser()
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

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                var userList = service.Search(txtSearch.Text);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = userList;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            FillTable();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var userList = service.Search(txtSearch.Text);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = userList;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_KeyPress(object sender, KeyPressEventArgs e)
        {
            registerUser();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<UserNoValid> usersNoValid= new List<UserNoValid>();

                var validationUser = new ValidationUser();

            var googleSheet = new GoogleSheet();
            var usersList = googleSheet.ValuesSheet();

            foreach (var usersheet in usersList)
            {
                    User user = new User();
                    user.Ci = usersheet.Ci;
                    user.Name = usersheet.Name;
                    user.Email = usersheet.Email;
                    user.Photography = usersheet.Photography;

                    if (!string.IsNullOrEmpty(user.Photography))
                    {
                        string[] parts = user.Photography.Split('=');
                        user.Photography = parts[1];
                    }

                    var resultValidationUser = validationUser.userValid(user);
                    if (!resultValidationUser.success)
                    {
                        var usernv = new UserNoValid();
                        usernv.user=user;
                        usernv.result = resultValidationUser;
                        usersNoValid.Add(usernv);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(user.Photography))
                        {
                            user.Photography = DEFAULT_IMG;
                        }
                        user.Password = user.Ci;
                        user.RoleId = 2;

                        service.Create(user);
                    }
            }
            FillTable();

            SLDocument newSL = new SLDocument();

                newSL.SetCellValue("A1", "CI");
                newSL.SetCellValue("B1", "Nombre");
                newSL.SetCellValue("C1", "Email");

                int rowIndex = 2;
                foreach (var user in usersNoValid)
                {
                    newSL.SetCellValue("A" + rowIndex, user.user.Ci);
                    newSL.SetCellValue("B" + rowIndex, user.user.Name);
                    newSL.SetCellValue("C" + rowIndex, user.user.Email);
                    newSL.SetCellValue("D" + rowIndex, user.result?.messages[0]);
                    newSL.SetCellValue("E" + rowIndex, user.result?.messages[1]);
                    newSL.SetCellValue("F" + rowIndex, user.result?.messages[2]);
                    rowIndex++;
                }
                newSL.AutoFitColumn(1);
                newSL.AutoFitColumn(2);
                newSL.AutoFitColumn(3);
                newSL.AutoFitColumn(4);
                newSL.AutoFitColumn(5);
                newSL.AutoFitColumn(6);

                try
                {
                    DateTime currentDate = DateTime.Now;

                    string fechaFormateada = currentDate.ToString("yyyyMMddhhmmss");

                    string fileName = $@"C:\Users\Brayan\Downloads\UsuariosNoValidos_{fechaFormateada}.xlsx";

                    newSL.SaveAs(fileName);
                    

                    if (File.Exists(fileName))
                    {
                        Process.Start(fileName);
                    }
                    else
                    {
                        MessageBox.Show("El archivo Excel no existe en la ruta especificada.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al guardar el archivo: " + ex.Message);
                }


            googleSheet.CleanSheet();

        }
    }
}
/*
 List<UserNoValid> usersNoValid= new List<UserNoValid>();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var validationUser = new ValidationUser();

                SLDocument sl = new SLDocument(openFileDialog.FileName);

                int iRow = 2;

                List<User> lst = new List<User>();
                while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow,1)))
                {
                    User user = new User();
                    user.Ci = sl.GetCellValueAsString(iRow,2);
                    user.Name = sl.GetCellValueAsString(iRow,3);
                    user.Email = sl.GetCellValueAsString(iRow, 4);
                    user.Photography = sl.GetCellValueAsString(iRow, 5);

                    if (!string.IsNullOrEmpty(user.Photography))
                    {
                        string[] parts = user.Photography.Split('=');
                        user.Photography = parts[1];
                    }

                    var resultValidationUser = validationUser.userValid(user);
                    if (!resultValidationUser.success)
                    {
                        var usernv = new UserNoValid();
                        usernv.user=user;
                        usernv.result = resultValidationUser;
                        usersNoValid.Add(usernv);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(user.Photography))
                        {
                            user.Photography = DEFAULT_IMG;
                        }
                        user.Password = user.Ci;
                        user.RoleId = 2;

                        service.Create(user);
                    }

                    iRow++;
                }

                SLDocument newSL = new SLDocument();

                newSL.SetCellValue("A1", "CI");
                newSL.SetCellValue("B1", "Nombre");
                newSL.SetCellValue("C1", "Email");

                int rowIndex = 2;
                foreach (var user in usersNoValid)
                {
                    newSL.SetCellValue("A" + rowIndex, user.user.Ci);
                    newSL.SetCellValue("B" + rowIndex, user.user.Name);
                    newSL.SetCellValue("C" + rowIndex, user.user.Email);
                    newSL.SetCellValue("D" + rowIndex, user.result?.messages[0]);
                    newSL.SetCellValue("E" + rowIndex, user.result?.messages[1]);
                    newSL.SetCellValue("F" + rowIndex, user.result?.messages[2]);
                    rowIndex++;
                }
                newSL.AutoFitColumn(1);
                newSL.AutoFitColumn(2);
                newSL.AutoFitColumn(3);
                newSL.AutoFitColumn(4);
                newSL.AutoFitColumn(5);
                newSL.AutoFitColumn(6);

                try
                {
                    newSL.SaveAs(@"C:\Users\Brayan\Downloads\UsuariosNoValidos.xlsx");
                    MessageBox.Show("Archivo guardado con éxito.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al guardar el archivo: " + ex.Message);
                }

                MessageBox.Show("Se logro subir los usuario por favor revise el reporte");
                var googlesheet = new GoogleSheet();
                googlesheet.ValuesSheet();
               
            }
 */