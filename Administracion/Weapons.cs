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

namespace Administracion
{
    public partial class WeaponDashboard : Form
    {
        WeaponService weaponService = new WeaponService();
        public WeaponDashboard()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            var weapon = new Weapon();

            weapon.WeaponNumber = txtWeapon.Text;
            weapon.Description = richtxtDescription.Text;

            weaponService.Create(weapon);
        }
    }
}
