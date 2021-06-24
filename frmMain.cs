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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void khốiLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form ff in Application.OpenForms)
                if (ff.Name == "frmKhoiLop")
                {
                    ff.Activate();
                    return;
                }

            frmKhoiLop f = new frmKhoiLop();
            f.MdiParent = this;
            f.Show();
        }

        private void sinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form ff in Application.OpenForms)
                if (ff.Name == "frmSinhVien")
                {
                    ff.Activate();
                    return;
                }

            frmSinhVien f = new frmSinhVien();
            f.MdiParent = this;
            f.Show();
        }
    }
}
