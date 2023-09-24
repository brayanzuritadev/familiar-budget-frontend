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

namespace Administracion
{
    public partial class PTable : Form
    {
        public List<PoligonoDetailDto> poligonoDetail = new List<PoligonoDetailDto>();
        public PTable()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void filltable(List<PoligonoDetailDto> poligonoList)
        {   
            dataGridView1.DataSource = null;

            dataGridView1.DataSource = poligonoList;
            dataGridView1.Refresh();
        }
    }
}
