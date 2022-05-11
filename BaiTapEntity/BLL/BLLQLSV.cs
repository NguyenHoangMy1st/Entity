using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2022_5_4_EntityFrameWork.DTO;

namespace _2022_5_4_EntityFrameWork.BLL
{
    public class BLLQLSV
    {
        private static BLLQLSV _Instance;
        public static BLLQLSV Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLLQLSV();
                }
                return _Instance;
            }
            private set { }
        }

        private BLLQLSV()
        {

        }
        
        public List<CBBItem> GetCBB()
        {
            QLSV db = new QLSV();
            List<CBBItem> data = new List<CBBItem>();
            foreach (LopSH i in db.LopSHes)
            {
                data.Add(new CBBItem { 
                    Value = i.ID_Lop,
                    Text = i.NameLop
                });
            }
            return data;
        }

        public List<SV_View> GetSVViewByIDLop(int ID_Lop)
        {
            QLSV db = new QLSV();
            List<SV_View> data = new List<SV_View>();
            if(ID_Lop == 0)
            {
                data = (from p in db.SVs select
                        new SV_View
                        {
                            MSSV = p.MSSV,
                            NameSV = p.NameSV,
                            NameLop = p.LopSH.NameLop,
                            DTB = (double)p.DTB,
                            Gender = (bool)p.Gender,
                            NgaySinh = (DateTime)p.NgaySinh,
                            Anh = (bool)p.Anh,
                            HB = (bool)p.HB,
                            CCNN = (bool)p.CCNN,
                        }).ToList();
            }
            else
            {
                data = (db.SVs.Where(p => p.ID_Lop == ID_Lop).Select(p =>
                new SV_View
                {
                    MSSV = p.MSSV,
                    NameSV = p.NameSV,
                    NameLop = p.LopSH.NameLop,
                    DTB = (double)p.DTB,
                    Gender = (bool)p.Gender,
                    NgaySinh = (DateTime)p.NgaySinh,
                    Anh = (bool)p.Anh,
                    HB = (bool)p.HB,
                    CCNN = (bool)p.CCNN,
                })).ToList();
            }
            return data;
        }
        public SV GetSVByMSSV(string MSSV)
        {
            QLSV db = new QLSV();
            SV s = new SV();
            foreach (SV i in db.SVs)
            {
                if (i.MSSV.ToString() == MSSV) return s = i;
            }
            return s;
        }


        

        public void Execute(SV s)
        {
            QLSV db = new QLSV();
            SV sv = db.SVs.Find(s.MSSV);
            if (sv != null)
            {
                sv.NameSV = s.NameSV;
                sv.DTB = Convert.ToDouble(s.DTB);
                sv.Gender = Convert.ToBoolean(s.Gender);
                sv.NgaySinh = Convert.ToDateTime(s.NgaySinh);
                sv.Anh = Convert.ToBoolean(s.Anh);
                sv.HB = Convert.ToBoolean(s.HB);
                sv.CCNN = Convert.ToBoolean(s.CCNN);
                sv.ID_Lop = s.ID_Lop;
            }
            else
            {
                db.SVs.Add(s);
            }
            db.SaveChanges();
        }

        public void DelSV(string MSSV)
        {
            QLSV db = new QLSV();
            SV s = db.SVs.Find(MSSV);
            db.SVs.Remove(s);
            db.SaveChanges();
        }
        public List<SV_View> Search(int ID_Lop, string txt)
        {
            List<SV_View> data = GetSVViewByIDLop(ID_Lop).Where(p => p.NameSV.Contains(txt)).ToList();
            return data;
        }
        public List<SV_View> Sort(int ID_Lop, string txt, string colName)
        {
            return Search(ID_Lop, txt).OrderBy(p => p.GetType().GetProperty(colName).GetValue(p)).ToList();
        }

    }
}
