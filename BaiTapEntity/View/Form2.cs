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
    public partial class Form2 : Form
    {
        public delegate void Mydel(string maLSH);
        public Mydel d;
        public string MSSV { get; set; }
        public Form2(string m)
        {
            MSSV = m;
            InitializeComponent();
            cbbLSH.Items.AddRange(BLLQLSV.Instance.GetCBB().ToArray());
            GUI();
        }
        public void GUI()
        {
            if (MSSV != "")
            {
                txtMSSV.Enabled = false;
                txtMSSV.Text = BLLQLSV.Instance.GetSVByMSSV(MSSV).MSSV.ToString();
                txtNameSV.Text = BLLQLSV.Instance.GetSVByMSSV(MSSV).NameSV;
                textBox_DTB.Text = BLLQLSV.Instance.GetSVByMSSV(MSSV).DTB.ToString();
                if (Convert.ToBoolean(BLLQLSV.Instance.GetSVByMSSV(MSSV).Gender)) radioButton_Female.Checked = true;
                else radioButton_Male.Checked = true;
                dateTimePicker.Value = Convert.ToDateTime(BLLQLSV.Instance.GetSVByMSSV(MSSV).NgaySinh);
                checkBox_Anh.Checked = Convert.ToBoolean(BLLQLSV.Instance.GetSVByMSSV(MSSV).Anh);
                checkBox_HB.Checked = Convert.ToBoolean(BLLQLSV.Instance.GetSVByMSSV(MSSV).HB);
                checkBox_CCNN.Checked = Convert.ToBoolean(BLLQLSV.Instance.GetSVByMSSV(MSSV).CCNN);
                foreach (CBBItem i in cbbLSH.Items)
                {
                    if (i.Value == BLLQLSV.Instance.GetSVByMSSV(MSSV).ID_Lop)
                    {
                        cbbLSH.SelectedItem = i;
                    }
                }
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            SV s = new SV
            {
                MSSV = txtMSSV.Text,
                NameSV = txtNameSV.Text,
                DTB = Convert.ToDouble(textBox_DTB.Text),
                Gender = radioButton_Female.Checked,
                NgaySinh = dateTimePicker.Value,
                Anh = checkBox_Anh.Checked,
                HB = checkBox_HB.Checked,
                CCNN = checkBox_CCNN.Checked,
                ID_Lop = ((CBBItem)cbbLSH.SelectedItem).Value,
            };
            BLLQLSV.Instance.Execute(s);
            d("0");
            this.Close();
        }
    }
}
