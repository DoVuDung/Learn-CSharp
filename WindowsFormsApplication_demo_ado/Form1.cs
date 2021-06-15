using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApplication_demo_ado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string constr = @"Data Source=DESKTOP-5U0CT07\SQL2012; Initial Catalog=DEMO; Uid=sa; Pwd=0123";
            //SqlConnection con = new SqlConnection(constr);
            //if (con.State != ConnectionState.Open)
            //    con.Open();
            ////
            ////SqlCommand cmd = new SqlCommand("insert into NHANVIEN (MASO,HOTEN) values ('NV03','CCC')", con);
            ////SqlCommand cmd = new SqlCommand("update NHANVIEN set HOTEN='Nguyen Van C' where MASO='NV03'", con);
            ////SqlCommand cmd = new SqlCommand("delete from NHANVIEN where MASO='NV03'", con);
            ////int i = cmd.ExecuteNonQuery();

            ////SqlCommand cmd = new SqlCommand("select top 1 HOTEN from NHANVIEN where MASO='NV02'", con);
            ////object o = cmd.ExecuteScalar();

            //SqlCommand cmd = new SqlCommand("select * from NHANVIEN", con);
            //SqlDataReader reader = cmd.ExecuteReader();

            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        //MessageBox.Show(reader.GetString(0) + " - " + reader.GetString(1));
            //        MessageBox.Show(reader["MASO"] + " - " + reader["HOTEN"]);
            //    }
            //    reader.Close();
            //}

            ////
            //con.Close();

            string constr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\huynh\Desktop\WindowsFormsApplication_demo_ado\WindowsFormsApplication_demo_ado\STUDENT.mdf;Integrated Security=True;User Instance=True";
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter("select * from LOP", con);

            DataSet ds = new DataSet();
            da.Fill(ds);

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "TENLOP";
            comboBox1.ValueMember = "MALOP";

            Column_lop.DataSource = ds.Tables[0];
            Column_lop.DisplayMember = "TENLOP";
            Column_lop.ValueMember = "MALOP";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string constr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\huynh\Desktop\WindowsFormsApplication_demo_ado\WindowsFormsApplication_demo_ado\STUDENT.mdf;Integrated Security=True;User Instance=True";

            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            if (con.State != ConnectionState.Open)
                con.Open();

            SqlCommand cmd = new SqlCommand("select * from SINHVIEN", con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MessageBox.Show(reader["MASV"] + " - " + reader["HOTEN"] + " - " + reader["MALOP"]);
                }
                reader.Close();
            }

            //
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);

            SqlDataAdapter da = new SqlDataAdapter("select * from SINHVIEN select * from LOP", con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            comboBox1.DataSource = ds.Tables[1];
            comboBox1.DisplayMember = "TENLOP";
            comboBox1.ValueMember = "MALOP";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string constr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
            string constr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\huynh\Desktop\WindowsFormsApplication_demo_ado\WindowsFormsApplication_demo_ado\STUDENT.mdf;Integrated Security=True;User Instance=True";
            SqlConnection con = new SqlConnection(constr);

            SqlDataAdapter da = new SqlDataAdapter("select * from SINHVIEN", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ds.Tables[0].Rows[0]["HOTEN"] = "AAA";
            DataRow r = ds.Tables[0].NewRow();
            r["MASV"] = "SV02";
            r["HOTEN"] = "CCC";
            r["MALOP"] = "TTT";
            ds.Tables[0].Rows.Add(r);

            da.Update(ds);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string constr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\huynh\Desktop\WindowsFormsApplication_demo_ado\WindowsFormsApplication_demo_ado\STUDENT.mdf;Integrated Security=True;User Instance=True";
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into SINHVIEN (MASV, HOTEN, NGAYSINH, MALOP, HINHANH) values (@MASV, @HOTEN, @NGAYSINH, @MALOP, @HINHANH)";
            cmd.Parameters.Add(new SqlParameter("@MASV", textBox1.Text));
            cmd.Parameters.Add(new SqlParameter("@HOTEN", textBox2.Text));
            cmd.Parameters.Add(new SqlParameter("@NGAYSINH", dateTimePicker1.Value.Date));
            cmd.Parameters.Add(new SqlParameter("@MALOP", textBox3.Text));

            SqlParameter para = new SqlParameter("@HINHANH", SqlDbType.Image);
            para.Value = ImageToByte(pictureBox1.Image);
            cmd.Parameters.Add(para);

            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public Image ByteToImage(byte[] byteArray)
        {
            MemoryStream ms = new MemoryStream(byteArray);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public byte[] ImageToByte(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Anh jpg|*.jpg";
            if (f.ShowDialog() == DialogResult.OK)
                pictureBox1.Image = Image.FromFile(f.FileName);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string constr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\huynh\Desktop\WindowsFormsApplication_demo_ado\WindowsFormsApplication_demo_ado\STUDENT.mdf;Integrated Security=True;User Instance=True";
            SqlConnection con = new SqlConnection(constr);

            //SqlDataAdapter da = new SqlDataAdapter("select * from SINHVIEN where MASV='SV07'", con);
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = new SqlCommand("getSinhvien", con);
            cmd.Parameters.Add(new SqlParameter("@MASV", "SV07"));
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;

            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
                MessageBox.Show(dt.Rows[i]["MASV"] + "");

            foreach (DataRow r in dt.Rows) MessageBox.Show(r["MASV"] + "");

            DataRow dr = dt.NewRow();
            dr["MASV"] = "CCC";
            //
            dt.Rows.Add(dr);



            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[0]["MASV"] + "";
                textBox2.Text = dt.Rows[0]["HOTEN"] + "";
                textBox3.Text = dt.Rows[0]["MALOP"] + "";
                dateTimePicker1.Value = dt.Rows[0]["NGAYSINH"] + "" != "" ? Convert.ToDateTime(dt.Rows[0]["NGAYSINH"]) : DateTime.Now;
                pictureBox1.Image = dt.Rows[0]["HINHANH"] + "" != "" ? ByteToImage((byte[])dt.Rows[0]["HINHANH"]) : null;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string constr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\huynh\Desktop\WindowsFormsApplication_demo_ado\WindowsFormsApplication_demo_ado\STUDENT.mdf;Integrated Security=True;User Instance=True";
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter("select * from SINHVIEN select * from LOP", con);

            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];

            comboBox1.DataSource = ds.Tables[1];
            comboBox1.DisplayMember = "TENLOP";
            comboBox1.ValueMember = "MALOP";

            //DataView dv = dt.DefaultView;
            //dv.Sort = "HOTEN";
            //dv.RowFilter = "MALOP='TTT'";
            //object o = dt.Compute("count(MASV)", "MALOP='TTT'");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue + "" == "") return;

            string constr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\huynh\Desktop\WindowsFormsApplication_demo_ado\WindowsFormsApplication_demo_ado\STUDENT.mdf;Integrated Security=True;User Instance=True";
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter("select * from SINHVIEN where MALOP='" + comboBox1.SelectedValue + "'", con);

            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = ds.Tables[0];
           

            textBox1.DataBindings.Clear();
            textBox2.DataBindings.Clear();
            textBox3.DataBindings.Clear();
            dateTimePicker1.DataBindings.Clear();
            //pictureBox1.DataBindings.Clear();

            textBox1.DataBindings.Add("Text", ds.Tables[0], "MASV");
            textBox2.DataBindings.Add("Text", ds.Tables[0], "HOTEN");
            textBox3.DataBindings.Add("Text", ds.Tables[0], "MALOP");
            dateTimePicker1.DataBindings.Add("Text", ds.Tables[0], "NGAYSINH");
            //pictureBox1.DataBindings.Add("Image", ds.Tables[0], "HINHANH");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
