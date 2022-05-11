using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _2022_5_4_EntityFrameWork.DTO;
using _2022_5_4_EntityFrameWork.BLL;

namespace _2022_5_4_EntityFrameWork.View
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetCBB();
        }
        public void SetCBB()
        {
            QLSV db = new QLSV();
            cbbLSH.Items.Add(new CBBItem { Value = 0, Text = "All" });
            foreach (LopSH i in db.LopSHes)
            {
                cbbLSH.Items.Add(new CBBItem
                {
                    Value = i.ID_Lop,
                    Text = i.NameLop
                });
            }
        }

        private void butShow_Click(object sender, EventArgs e)
        {
            QLSV db = new QLSV();
            //select *
            //var l1 = from p in db.SVs select p;
            var l2 = db.SVs.Select(p => p);
            //var l1 = from p in db.SVs
            //           select new {p.MSSV, p.NameSV, p.LopSH.NameLop };
            //var l2 = db.SVs.Select(p => new{ p.MSSV, p.NameSV, p.LopSH.NameLop});
            //select ... where
            int ID_Lop = ((CBBItem)cbbLSH.SelectedItem).Value;
            //var l1 = from p in db.SVs
            //         where p.ID_Lop == ID_Lop
            //         select p;
            //var l2 = db.SVs.Where(p => p.ID_Lop == ID_Lop);
            var l1 = from p in db.SVs
                     where p.ID_Lop == ID_Lop
                     select new
                     {
                         p.MSSV,
                         p.NameSV,
                         p.LopSH.NameLop
                     };
            //var l2 = db.SVs.Where(p => p.ID_Lop == ID_Lop)
            //    .Select(p => new { p.MSSV, p.NameSV, p.LopSH.NameLop });
            var l3 = db.SVs.Where(p => p.ID_Lop == ID_Lop).Select(p =>
            new { p.MSSV, p.NameSV, p.DTB, p.NgaySinh, p.Gender, p.Anh, p.HB, p.CCNN, p.LopSH.NameLop });

            dataGridView1.DataSource = l3.ToList();
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            QLSV db = new QLSV();
            SV s = new SV
            {
                MSSV = "110",
                NameSV = "Khang",
                DTB = 7,
                Gender = true,

                Anh = true,
                HB = true,
                CCNN = false,
            };
            db.SVs.Add(s);
            db.SaveChanges();
            var l2 = db.SVs.Select(p =>
            new { p.MSSV, p.NameSV, p.DTB, p.NgaySinh, p.Gender, p.Anh, p.HB, p.CCNN, p.LopSH.NameLop });
            dataGridView1.DataSource = l2.ToList();
        }

        private void butUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                QLSV db = new QLSV();
                string MSSV = dataGridView1.SelectedRows[0].Cells["MSSV"].Value.ToString();
                //SV s = db.SVs.Where(p => p.MSSV == MSSV).FirstOrDefault();  // 1
                SV s = db.SVs.Find(MSSV);
                s.NameSV = "Update";
                db.SaveChanges();
                var l2 = db.SVs.Select(p => new { p.MSSV, p.NameSV, p.LopSH.NameLop });
                dataGridView1.DataSource = l2.ToList();
            }
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                QLSV db = new QLSV();
                foreach (DataGridViewRow i in dataGridView1.SelectedRows)
                {
                    string MSSV = i.Cells["MSSV"].Value.ToString();
                    //SV s = db.SVs.Where(p => p.MSSV == MSSV).FirstOrDefault();  // 1
                    SV s = db.SVs.Find(MSSV);
                    db.SVs.Remove(s);
                    db.SaveChanges();
                }

                var l2 = db.SVs.Select(p => new { p.MSSV, p.NameSV, p.LopSH.NameLop });
                dataGridView1.DataSource = l2.ToList();
            }

        }

        private void butSort_Click(object sender, EventArgs e)
        {

        }

        private void butSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
