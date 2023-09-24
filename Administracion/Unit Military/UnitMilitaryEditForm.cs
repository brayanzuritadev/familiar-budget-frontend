using Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administracion.Unit_Military
{
    public partial class UnitMilitaryEditForm : Form
    {
        private readonly UnitMilitary _unitMilitary = new UnitMilitary();

        public UnitMilitaryEditForm(UnitMilitary unitMilitary)
        {
            InitializeComponent();
            this._unitMilitary = unitMilitary;
            fillcb();

            this.txtMilitary.Text = unitMilitary.MilitaryUnitName;
            this.cbDepartment.SelectedItem = unitMilitary.Department;
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
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void cbUserType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtWeapon_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
