using Administracion.Unit_Military;
using Data.Dto;
using Data.Entity;
using DocumentFormat.OpenXml.Drawing.Diagrams;
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

namespace Administracion
{
    public partial class MilitaryDashboardForm : Form
    {
        UnitMilitaryService unitMilitaryService = new UnitMilitaryService();
        List<UnitMilitary> unitMilitaryList = new List<UnitMilitary>();
        public MilitaryDashboardForm()
        {
            InitializeComponent();
            fillcb();
            createTable();
            fillTable();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void fillcb()
        {
            cbDepartment.Items.Add("Cochabamba");
            cbDepartment.Items.Add("La Paz");
            cbDepartment.Items.Add("Santa Cruz");
            cbDepartment.Items.Add("Pando");
            cbDepartment.Items.Add("Beni");
            cbDepartment.Items.Add("Potosi");
            cbDepartment.Items.Add("Chuquisaca");
            cbDepartment.Items.Add("Tarija");
            cbDepartment.Items.Add("Oruro");

            cbDepartment.SelectedIndex = 0;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var military = new UnitMilitary();
            military.MilitaryUnitName = txtUnitName.Text;
            military.Department = cbDepartment.SelectedItem as string;

            if (military.MilitaryUnitName == "")
            {
                MessageBox.Show("Se necesita un nombre");
                return;
            }

            if (unitMilitaryService.Create(military))
            {
                MessageBox.Show("Se registro correctamente");
                cleanFields();
                fillTable();

            }
            else
            {
                MessageBox.Show("Algo salio mal");
            }
        }

        public void fillTable()
        {
            unitMilitaryList = unitMilitaryService.GetAll();

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = unitMilitaryList;
                dataGridView1.Refresh();
            
        }

        public void createTable()
        {
            dataGridView1.AutoGenerateColumns = false;

                DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "militaryunitId";
            idColumn.HeaderText = "ID";
            idColumn.Width = 100;

                DataGridViewTextBoxColumn militaryNameColumn = new DataGridViewTextBoxColumn();
            militaryNameColumn.DataPropertyName = "militaryunitname";
            militaryNameColumn.HeaderText = "Nombre de figura";
            militaryNameColumn.Width = 300;

            DataGridViewTextBoxColumn departmentcolumn = new DataGridViewTextBoxColumn();
            departmentcolumn.DataPropertyName = "department";
            departmentcolumn.HeaderText = "Nombre de figura";
            departmentcolumn.Width = 150;

            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                buttonColumn.HeaderText = "Acciones";
                buttonColumn.Text = "Editar";
                buttonColumn.UseColumnTextForButtonValue = true;

                dataGridView1.Columns.Add(idColumn);
                dataGridView1.Columns.Add(militaryNameColumn);
            dataGridView1.Columns.Add(departmentcolumn);
            dataGridView1.Columns.Add(buttonColumn);
        }

        private void cleanFields()
        {
            txtUnitName.Text = string.Empty;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void search()
        {
            var militaryList = unitMilitaryService.Search(txtSearch.Text);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = militaryList;

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                search();
            }
        }
    }
}
