using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace MID
{
    class clsDB
    {
        string conStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\huynh\Desktop\MID\MID\QLSV.mdf;Integrated Security=True;User Instance=True";
        SqlConnection con;

        public clsDB()
        {
            con = new SqlConnection(conStr);
        }

        void Open()
        {
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();
        }
        void Close()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
        }

        public DataSet getDataset(string sql, object[] paraName = null, object[] paraValue = null)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            if (paraName != null)
            {
                for (int i = 0; i < paraName.Length; i++)
                    cmd.Parameters.AddWithValue("@" + paraName[i], paraValue[i]);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public int execNonquery(string sql, object[] paraName, object[] paraValue)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            if (paraName != null)
            {
                for (int i = 0; i < paraName.Length; i++)
                    cmd.Parameters.AddWithValue("@" + paraName[i], paraValue[i]);
            }
            Open();
            int res = cmd.ExecuteNonQuery();
            Close();
            return res;
        }
        public object execScalar(string sql, object[] paraName, object[] paraValue)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            if (paraName != null)
            {
                for (int i = 0; i < paraName.Length; i++)
                    cmd.Parameters.AddWithValue("@" + paraName[i], paraValue[i]);
            }
            Open();
            object res = cmd.ExecuteScalar();
            Close();
            return res;
        }
        public bool updateDB(DataTable dt, string sql)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(dt);
                return true;
            }
            catch { return false; }
        }
    }
}
