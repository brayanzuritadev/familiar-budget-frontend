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

namespace Administracion.figura
{
    public partial class MilitaryForm : Form
    {
        FigureService figureService = new FigureService();

        public Figure Figure = new Figure();

        public MilitaryForm(FigureDetailDto figure)
        {
            InitializeComponent();
            Figure.FigureId = figure.FigureId;
            Figure.FigureName = figure.FigureName;

            txtFigureName.Text = figure.FigureName;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFigureName.Text))
            {
                MessageBox.Show("Se requiere un nombre para la figura");
            }
            else
            {
                Figure.FigureName = txtFigureName.Text;
                if (figureService.Update(Figure))
                {
                    MessageBox.Show("Se edito de manera correcta");
                    clearFields();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Algo salio mal");
                }
            }
        }
        private void clearFields()
        {
            txtFigureName.Text = string.Empty;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtFigureName_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MilitaryForm_Load(object sender, EventArgs e)
        {

        }
    }
}
