using Administracion.users;
using Administracion.weapon;
using Data.Entity;
using Service;
using Service.Tools;
using Service.Validations;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administracion
{
    public partial class WeaponDashboard : Form
    {
        WeaponService weaponService = new WeaponService();
        List<Weapon> weaponsList = new List<Weapon>();
        public WeaponDashboard()
        {
            InitializeComponent();
            CreateTable();
            fillTable();
        }

        private void fillTable()
        {
            weaponsList = weaponService.GetAll();
            dataGridView1.DataSource = weaponsList;
        }
        private void CreateTable()
        {
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn weaponId = new DataGridViewTextBoxColumn();
            weaponId.DataPropertyName = "WeaponId";
            weaponId.HeaderText = "ID";
            weaponId.Width = (int)(dataGridView1.Width * 0.1);

            DataGridViewTextBoxColumn weaponNumber = new DataGridViewTextBoxColumn();
            weaponNumber.DataPropertyName = "weaponNumber";
            weaponNumber.HeaderText = "numero de arma";
            weaponNumber.Width = (int)(dataGridView1.Width * 0.3);

            DataGridViewTextBoxColumn weaponName = new DataGridViewTextBoxColumn();
            weaponName.DataPropertyName = "WeaponName";
            weaponName.HeaderText = "Nombre de arma";
            weaponName.Width = (int)(dataGridView1.Width * 0.3);

            DataGridViewTextBoxColumn description = new DataGridViewTextBoxColumn();
            description.DataPropertyName = "Description";
            description.HeaderText = "Descripcion";
            description.Width = (int)(dataGridView1.Width * 0.3);

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Acciones";
            buttonColumn.Text = "Editar";
            buttonColumn.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add(weaponId);
            dataGridView1.Columns.Add(weaponNumber);
            dataGridView1.Columns.Add(weaponName);
            dataGridView1.Columns.Add(description);
            dataGridView1.Columns.Add(buttonColumn);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            var weapon = new Weapon();

            weapon.WeaponNumber = txtWeapon.Text;
            weapon.WeaponName = txtNameWeapon.Text;
            weapon.Description = richtxtDescription.Text;
            //weapon.WeaponId = 

            var result = weaponService.Create(weapon);

            if (result.Success)
            {
                MessageBox.Show("Registro realizado con éxito");
                cleanFields();
                fillTable();
            }
            else
            {
                StringBuilder messageBuilder = new StringBuilder();

                foreach (string message in result.Messages)
                {
                    messageBuilder.AppendLine("- " + message);
                }

                string mess = messageBuilder.ToString();

                MessageBox.Show(mess);
            }
        }

        private void cleanFields()
        {
            txtWeapon.Text = "";
            txtNameWeapon.Text = "";
            richtxtDescription.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de Excel|*.xlsx;*.xls|Todos los archivos|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var excel = new ExcelDataLoading();
                var weaponValidation = new WeaponsValidation();
                var weaponList = excel.uploadExcel(openFileDialog);
                var weaponListN = new List<Weapon>();
                if (weaponList != null)
                {
                    foreach(Weapon weapon in weaponList)
                    {
                        var result = weaponValidation.WeaponValid(weapon);

                        if (result.Success)
                        {
                            weaponService.Create(weapon);
                        }
                        else
                        {
                            weaponListN.Add(weapon);
                        }
                    }
                    excel.generateReport(weaponListN);
                    fillTable();
                }
                else
                {
                    MessageBox.Show("El excel esta vacio");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var excel = new ExcelDataLoading();

            excel.generateTemplate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchWeapon(txtSearch.Text);
        }

        private void searchWeapon(string text)
        {
            weaponService = new WeaponService();
            var weapons = weaponService.Search(text);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = weapons;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                searchWeapon(txtSearch.Text);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {
                string weaponNumber = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                var weaponSelected = weaponsList.Find(weapon => weapon.WeaponNumber == weaponNumber);

                var editUserForm = new EditWeaponForm(weaponSelected);

                editUserForm.ShowDialog();
                fillTable();
            }
        }
    }
}
