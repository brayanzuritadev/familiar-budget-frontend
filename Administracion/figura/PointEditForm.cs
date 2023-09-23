using Data.Dto;
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
    public partial class PointEditForm : Form
    {
        PointDetailDto pointDetailDto = new PointDetailDto();
        FigureService figureService = new FigureService();
        PointValueService pointValueService = new PointValueService();
        public PointEditForm(PointDetailDto pointDetailDto)
        {
            InitializeComponent();
            this.pointDetailDto = pointDetailDto;
            fillCb();

            txtNamepv.Text = pointDetailDto.PointName;
            txtValue.Text = Convert.ToString(pointDetailDto.Value);
            cbFigure.SelectedValue = pointDetailDto.FigureId;
        }

        private void fillCb()
        {
            var figuresDetails = figureService.GetAll();

            cbFigure.DataSource = figuresDetails;
            cbFigure.DisplayMember = "FigureName";
            cbFigure.ValueMember = "FigureId";
        }


        private void PointEditForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Data.Entity.Point point = new Data.Entity.Point();
            point.FigureId = Convert.ToInt32(cbFigure.SelectedValue);
            point.PointName = txtNamepv.Text;
            point.Value = Convert.ToInt32(txtValue.Text);
            point.PointId = pointDetailDto.PointId;

            if (pointValueService.Update(point))
            {
                MessageBox.Show("Se edito de manera correcta");
                cleanFields();
                this.Close();
            }
            else
            {
                MessageBox.Show("Algo salio mal");
            }
            
        }
        private void cleanFields()
        {
            txtNamepv.Text = string.Empty; 
            txtValue.Text = string.Empty;
        }
    }
}
