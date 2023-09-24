using Data.Dto;
using Data.Entity;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administracion.poligono
{
    public partial class PConfigurationForm : Form
    {
        WeaponService weaponService = new WeaponService();
        UserService userService = new UserService();

        List<Weapon> weapons = new List<Weapon>();
        List<UserDetailDto> users = new List<UserDetailDto>();

        public MainForm _form;

        public PConfigurationForm(MainForm form)
        {
            InitializeComponent();

            _form = form;

            createTableUser();
            createTableWeapon();
            fillTableUser();
            fillTableWeapon();

            //dataGridView2.Visible = false;

        }

        private void fillTableUser()
        {
            users = userService.GetAll();

            dataGridView1.DataSource = null;

            dataGridView1.DataSource = users;

        }

        private void fillTableWeapon()
        {
            weapons = weaponService.GetAll();
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = weapons;
        }
        private void createTableUser()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.RowHeadersVisible = false;

            DataGridViewTextBoxColumn ciColumn = new DataGridViewTextBoxColumn();
            ciColumn.DataPropertyName = "Ci";
            ciColumn.HeaderText = "CI";
            ciColumn.Width = 88;

            DataGridViewTextBoxColumn nombreColumn = new DataGridViewTextBoxColumn();
            nombreColumn.DataPropertyName = "Name";
            nombreColumn.HeaderText = "Nombre";
            nombreColumn.Width = 200;

            DataGridViewTextBoxColumn emailColumn = new DataGridViewTextBoxColumn();
            emailColumn.DataPropertyName = "Email";
            emailColumn.HeaderText = "Email";
            emailColumn.Width = 150;

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Acciones";
            buttonColumn.Text = "Agregar";
            buttonColumn.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add(ciColumn);
             dataGridView1.Columns.Add(nombreColumn);
             dataGridView1.Columns.Add(emailColumn);
             //dataGridView1.Columns.Add(tipoTiradorColumn);
             dataGridView1.Columns.Add(buttonColumn);

             dataGridView1.AllowUserToResizeRows = false;

        }

        private void createTableWeapon()
        {
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.RowHeadersVisible = false;

            DataGridViewTextBoxColumn weaponNumber = new DataGridViewTextBoxColumn();
            weaponNumber.DataPropertyName = "weaponNumber";
            weaponNumber.HeaderText = "numero de arma";
            weaponNumber.Width = 215;

            DataGridViewTextBoxColumn weaponName = new DataGridViewTextBoxColumn();
            weaponName.DataPropertyName = "WeaponName";
            weaponName.HeaderText = "Nombre de arma";
            weaponName.Width = 222;

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "Acciones";
            buttonColumn.Text = "Agregar";
            buttonColumn.UseColumnTextForButtonValue = true;

            dataGridView2.Columns.Add(weaponNumber);
            dataGridView2.Columns.Add(weaponName);
            dataGridView2.Columns.Add(buttonColumn);

            dataGridView2.AllowUserToResizeRows = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                txtWeaponNumber.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtWeaponName.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                txtCi.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
               // dataGridView1.Visible = false;
               // dataGridView2.Visible = true;
                //var userSelected = userDetailList.Find(user => user.Ci == ci);

                //var editUserForm = new UserEditForm(userSelected);
                //_form.showMessage();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchUser();
        }

        private void searchWeapon()
        {
            weapons = weaponService.Search(textBox2.Text);

            dataGridView2.DataSource = null;

            dataGridView2.DataSource = weapons;
        }

        private void searchUser()
        {
            users = userService.Search(textBox1.Text);

            dataGridView1.DataSource = null;

            dataGridView1.DataSource = users;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //searchWeapon();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            searchUser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            searchWeapon();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                searchWeapon();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                searchUser();
            }
        }
    }
}
