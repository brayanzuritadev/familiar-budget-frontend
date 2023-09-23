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

namespace Administracion.weapon
{
    public partial class EditWeaponForm : Form
    {
        WeaponService weaponService = new WeaponService();
        Weapon _weapon = new Weapon();
        public EditWeaponForm(Weapon weapon)
        {
            InitializeComponent();

            this._weapon = weapon;

            this.txtNameWeapon.Text = weapon.WeaponName;
            this.txtWeapon.Text = weapon.WeaponNumber;
            this.richtxtDescription.Text = weapon.Description;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Weapon weapon = new Weapon();
            weapon.WeaponNumber = this.txtWeapon.Text;
            weapon.WeaponName = this.txtNameWeapon.Text;
            weapon.Description = this.richtxtDescription.Text;
            weapon.WeaponId = _weapon.WeaponId;

            if (weaponService.Update(weapon))
            {
                MessageBox.Show("Se edito de manera correcta");
            }
        }

        private void EditWeaponForm_Load(object sender, EventArgs e)
        {

        }
    }
}
