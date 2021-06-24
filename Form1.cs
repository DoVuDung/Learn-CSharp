using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRAC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        clsDB clsdb = new clsDB();
        private void Form1_Load(object sender, EventArgs e)
        {
            loadDV();
        }

        void loadDV()
        {
            DataSet ds = clsdb.getDataset("select * from DON_VI");
            toolStripComboBox_donvi.ComboBox.DataSource = ds.Tables[0];
            toolStripComboBox_donvi.ComboBox.DisplayMember = "TEN_DON_VI";
            toolStripComboBox_donvi.ComboBox.ValueMember = "MA_DON_VI";

            
        }

        private void toolStripComboBox_donvi_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadNV();
        }
        void loadNV()
        {
            dataGridView1.DataSource = null;
            if (toolStripComboBox_donvi.ComboBox.SelectedValue + "" == "System.Data.DataRowView") return;

            //DataRowView dr = (DataRowView)toolStripComboBox_donvi.ComboBox.SelectedValue;

            DataSet ds = clsdb.getDataset("select NHAN_VIEN.*, TEN_CHUC_VU, case when GIOI_TINH=1 then N'Nam' else N'Nữ' end as GT from NHAN_VIEN inner join CHUC_VU on NHAN_VIEN.MA_CV=CHUC_VU.MA_CV where MA_DON_VI=@MADV",
                new object[] { "MADV" }, new object[] { toolStripComboBox_donvi.ComboBox.SelectedValue});

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            if (f.ShowDialog() == DialogResult.OK)
                loadNV();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();

            if (dataGridView1.CurrentRow.Cells[0].Value + "" != "")
                f.MANV = dataGridView1.CurrentRow.Cells[0].Value;

            if (f.ShowDialog() == DialogResult.OK)
                loadNV();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
