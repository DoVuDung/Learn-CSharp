using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MID
{
    public partial class frmSinhVien : Form
    {
        public frmSinhVien()
        {
            InitializeComponent();
        }

        clsDB clsdb = new clsDB();
        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            loadKhoiLop();
            loadData();
        }

        void loadKhoiLop()
        {
            DataSet ds = clsdb.getDataset("select * from LOPHOC");
            Column_khoilop.DataSource = ds.Tables[0];
            Column_khoilop.DisplayMember="TENLOP";
            Column_khoilop.ValueMember = "MALOP";
        }

        void loadData()
        {
            DataSet ds = clsdb.getDataset("select * from SINHVIEN");
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoGenerateColumns = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //ktra
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value + "" == "")
                {
                    MessageBox.Show("Chưa nhập Mã SV tại dòng " + (i + 1), "TB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            //ktra trung
            //

            DataTable dt = (DataTable)dataGridView1.DataSource;
            clsdb.updateDB(dt, "select * from SINHVIEN");

            loadData();
        }

    }
}
