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
    public partial class frmKhoiLop : Form
    {
        public frmKhoiLop()
        {
            InitializeComponent();
        }

        clsDB clsdb = new clsDB();

        private void frmKhoiLop_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            DataSet ds = clsdb.getDataset("select * from LOPHOC");
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
                    MessageBox.Show("Chưa nhập Mã lớp tại dòng " + (i + 1), "TB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            //ktra trung
            //

            DataTable dt = (DataTable)dataGridView1.DataSource;
            clsdb.updateDB(dt, "select * from LOPHOC");

            loadData();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value + "" == "") return;

            object ma = clsdb.execScalar("select top 1 MASV from SINHVIEN where MALOP=@MALOP", new object[] { "MALOP" }, new object[] { dataGridView1.CurrentRow.Cells[0].Value });
            if (ma + "" != "")
            {
                MessageBox.Show("Lớp đang có SV", "TB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Có muốn xóa ko?", "TB", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                clsdb.execNonquery("delete from LOPHOC where MALOP=@MALOP", new object[] { "MALOP" }, new object[] { dataGridView1.CurrentRow.Cells[0].Value });

                loadData();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
