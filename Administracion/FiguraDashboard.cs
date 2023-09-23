using Administracion.figura;
using Administracion.weapon;
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
using Data.Entity;

namespace Administracion
{
    public partial class FiguraDashboard : Form
    {
        private readonly string component;
        private FigureService figureService = new FigureService();
        private PointValueService pointValueService = new PointValueService();
        List<FigureDetailDto> figuresList = new List<FigureDetailDto>();
        List<PointDetailDto> pointsList = new List<PointDetailDto>();
        public FiguraDashboard(string component)
        {
            InitializeComponent();
            this.component = component;
        }

        private void FiguraDashboard_Load(object sender, EventArgs e)
        {
            createTable();
            fillCb();
            showComponent();
            fillTable();
        }

        private void showComponent()
        {
            if (component == "PointValue")
            {
                this.panel4.Visible = true;
                this.txtSearch.Visible = false;
                this.btnSearch.Visible = false;
                this.cbFigure.Visible = true;
                this.label2.Visible = true;
                this.btnActualizar.Visible = true;
            }
            else
            {
                this.panel4.Visible = false;
                this.txtSearch.Visible = true;
                this.btnSearch.Visible = true;
                this.cbFigure.Visible = false;
                this.label2.Visible = false;
                this.btnActualizar.Visible= false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }
        private void fillCb()
        {
            var figuresDetails = figureService.GetAll();
            
            cbFigure.DataSource = figuresDetails;
            cbFigure.DisplayMember = "FigureName";
            cbFigure.ValueMember = "FigureId";
        }

        public void fillTable()
        {
            if (component == "Figure")
            {
                var figureList = figureService.GetAll();
                figuresList = figureList;
                dataGridView1.DataSource = null;

                dataGridView1.DataSource = figureList;
                dataGridView1.Refresh();
            }
        }

        public void fillTablePoint()
        {
            if (cbFigure.SelectedItem != null && cbFigure.SelectedItem is FigureDetailDto)
            {
                var selectedFigure = (FigureDetailDto)cbFigure.SelectedItem;
                int figureId = selectedFigure.FigureId;

                var pointsList = pointValueService.GetAll().Where(item => item.FigureId == figureId).ToList();

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = pointsList;
                dataGridView1.Refresh();
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
            }
            pointsList = pointValueService.GetAll();
        }

        public void createTable()
        {
            dataGridView1.AutoGenerateColumns = false;
            
            if (component == "Figure")
            {
                DataGridViewTextBoxColumn figureidColumn = new DataGridViewTextBoxColumn();
                figureidColumn.DataPropertyName = "figureid";
                figureidColumn.HeaderText = "ID";
                figureidColumn.Width = 100;

                DataGridViewTextBoxColumn figureNameColumn = new DataGridViewTextBoxColumn();
                figureNameColumn.DataPropertyName = "figureName";
                figureNameColumn.HeaderText = "Nombre de figura";
                figureNameColumn.Width = dataGridView1.Width / 2;

                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                buttonColumn.HeaderText = "Acciones";
                buttonColumn.Text = "Editar";
                buttonColumn.UseColumnTextForButtonValue = true;

                dataGridView1.Columns.Add(figureidColumn);
                dataGridView1.Columns.Add(figureNameColumn);
                dataGridView1.Columns.Add(buttonColumn);
            }
            else
            {
                DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
                idColumn.DataPropertyName = "pointid";
                idColumn.HeaderText = "ID";
                idColumn.Width = 100;

                DataGridViewTextBoxColumn pointNameColumn = new DataGridViewTextBoxColumn();
                pointNameColumn.DataPropertyName = "pointname";
                pointNameColumn.HeaderText = "Nombre de punto";
                pointNameColumn.Width = 280;

                DataGridViewTextBoxColumn pointValueColumn = new DataGridViewTextBoxColumn();
                pointValueColumn.DataPropertyName = "value";
                pointValueColumn.HeaderText = "Valor de punto";
                pointValueColumn.Width = 80;

                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                buttonColumn.HeaderText = "Acciones";
                buttonColumn.Text = "Editar";
                buttonColumn.UseColumnTextForButtonValue = true;

                dataGridView1.Columns.Add(idColumn);
                dataGridView1.Columns.Add(pointNameColumn);
                dataGridView1.Columns.Add(pointValueColumn);
                dataGridView1.Columns.Add(buttonColumn);
            }


        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            var figure = new Figure();
            if (txtFigureName.Text == "")
            {
                MessageBox.Show("Debe enviar el nombre de la Figura");
                return;
            }
            figure.FigureName = txtFigureName.Text;

            if (figureService.Create(figure))
            {
                fillTable();
                MessageBox.Show("Registro realizado con éxito");
                clearFields();
            }
            else
            {
                MessageBox.Show("Algo Salio mal");
            }
        }
        private void clearFields()
        {
            txtFigureName.Text = string.Empty;

            txtNamepv.Text = string.Empty;
            txtValue.Text = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                string figureId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                var figureselected = figuresList.Find(figure => figure.FigureId == Convert.ToInt32(figureId));

                var figureForm = new MilitaryForm(figureselected);

                figureForm.ShowDialog();
                fillTable();
            }

            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                string pointId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                var pointselected = pointsList.Find(figure => figure.PointId == Convert.ToInt32(pointId));

                var pointValueForm = new PointEditForm(pointselected);

                pointValueForm.ShowDialog();
                fillTable();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchWeapon(txtSearch.Text);
        }
        private void searchWeapon(string text)
        {
            var weapons = figureService.Search(text);

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

        private void button1_Click(object sender, EventArgs e)
        {
            var pointValue = new Data.Entity.Point();
            pointValue.FigureId = Convert.ToInt32(cbFigure.SelectedValue);
            pointValue.PointName = txtNamepv.Text;
            pointValue.Value = Convert.ToInt32(txtValue.Text);

            if (pointValueService.Create(pointValue))
            {
                MessageBox.Show("Se registro de manera correcta");
                clearFields();
            }
            else
            {
                MessageBox.Show("Revise que los campos no esten vacios");
            }
        }

        private void cbFigure_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillTablePoint();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fillTablePoint();
        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
