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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public object MANV { get; set; }

        clsDB clsdb = new clsDB();
        private void Form2_Load(object sender, EventArgs e)
        {
            loadDV();
            loadCV();
            loadNV(MANV);
        }

        void loadDV()
        {
            DataSet ds = clsdb.getDataset("select * from DON_VI");
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "TEN_DON_VI";
            comboBox1.ValueMember = "MA_DON_VI";
        }
        void loadCV()
        {
            DataSet ds = clsdb.getDataset("select * from CHUC_VU");
            comboBox2.DataSource = ds.Tables[0];
            comboBox2.DisplayMember = "TEN_CHUC_VU";
            comboBox2.ValueMember = "MA_CV";
        }

        void loadNV(object MANV)
        {
            if (MANV + "" == "") return;
            DataSet ds = clsdb.getDataset("select * from NHAN_VIEN where MA_NV=@MANV",
                new object[] { "MANV" }, new object[] { MANV });
            if (ds.Tables[0].Rows.Count > 0)
            {
                textBox1.Text = ds.Tables[0].Rows[0]["MA_NV"] + "";
                textBox2.Text = ds.Tables[0].Rows[0]["HO_TEN"] + "";
                if (ds.Tables[0].Rows[0]["NGAY_SINH"] + "" != "") dateTimePicker1.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["NGAY_SINH"]);
                comboBox1.SelectedValue = ds.Tables[0].Rows[0]["MA_DON_VI"] + "";
                comboBox2.SelectedValue = ds.Tables[0].Rows[0]["MA_CV"] + "";

                radioButton1.Checked = ds.Tables[0].Rows[0]["GIOI_TINH"] + "" == "True";
                radioButton2.Checked = ds.Tables[0].Rows[0]["GIOI_TINH"] + "" == "False";
                textBox3.Text = ds.Tables[0].Rows[0]["DIA_CHI"] + "";

                textBox1.ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.ReadOnly)
                clsdb.execNonquery("insert into NHAN_VIEN (MA_NV,HO_TEN, NGAY_SINH, GIOI_TINH, DIA_CHI, MA_DON_VI, MA_CV) values (@MANV, @HT, @NS, @GT, @DC, @MADV, @MACV)",
                    new object[] { "MANV", "HT", "NS", "GT", "DC", "MADV", "MACV" },
                    new object[] { textBox1.Text, textBox2.Text, dateTimePicker1.Value, radioButton1.Checked, textBox3.Text, comboBox1.SelectedValue, comboBox2.SelectedValue });
            else
                clsdb.execNonquery("update NHAN_VIEN set HO_TEN=@HT, MA_DON_VI=@MADV, GIOI_TINH=@GT, DIA_CHI=@DC, NGAY_SINH=@NS, MA_CV=@MACV where MA_NV=@MANV",
                    new object[] { "MANV", "HT", "NS", "GT", "DC", "MADV", "MACV" },
                    new object[] { textBox1.Text, textBox2.Text, dateTimePicker1.Value, radioButton1.Checked, textBox3.Text, comboBox1.SelectedValue, comboBox2.SelectedValue });


            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
