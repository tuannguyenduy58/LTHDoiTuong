using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
namespace Laptrinhhuongdoituong
{
    class Sinhvien
    {
        private string sbd, kv, uutien, dantoc, ten, ngaysinh;
        private string[,] nguyenvong = new string[4, 2];
        private double[] diemthi = new double[13];
       private double[] ketqua = new double[4];
       public double[] Ketqua
       {
           get { return ketqua; }
       }
       public string SBd
       {
           get { return sbd; }
       }
       public string[,] Nguyenvong
           
       {
           get { return nguyenvong; }
       }

        public void laygiatri( int stt)
        {
            docdulieu sv = new docdulieu();
            sv.dangkynv(stt);
            sbd = sv.Sbd;
            kv = sv.Khuvuc;
            uutien = sv.Uutien;
            dantoc = sv.Dantoc;
            ten = sv.Ten;
            ngaysinh = sv.Ngaysinh;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 2; j++)
                    nguyenvong[i, j] = sv.Nguyenvong[i, j];
                for (int i = 0; i < 13; i++)
                {

                    if (sv.Dienthi[i] == "NA") diemthi[i] = -1;
                    else diemthi[i] = double.Parse(sv.Dienthi[i]);
                }
           
        }
        private double diem;
       private double diemcacmon( string str)
        {
            
            switch (str)
            {
                case "Toan": diem = diemthi[0]; break;
                case "Van": diem = diemthi[1]; break;
                case "Ly": diem = diemthi[2]; break;
                case "Hoa": diem = diemthi[3]; break;
                case "Sinh": diem = diemthi[4]; break;
                case "Su": diem = diemthi[5]; break;
                case "Dia": diem = diemthi[6]; break;
                case "Anh": diem = diemthi[7]; break;
                case "Nga": diem = diemthi[8]; break;
                case "Phap": diem = diemthi[9]; break;
                case "Trung": diem = diemthi[10]; break;
                case "Duc": diem = diemthi[11]; break;
                case "Nhat": diem = diemthi[12]; break;
            }
            return diem;
        }
        private double diemkv;
        private double diemkhuvuc(string s)
        {
            switch (s)
            {
                case "\"KV1\"": diemkv = 1.5; break;
                case "\"KV2-NT\"": diemkv = 1; break;
                case "\"KV2\"": diemkv = 0.5; break;
                case "\"KV3\"": diemkv = 0; break;
            }
            return diemkv;
        }
        private double diemUT;
        private double diemuutien(string s)
        {
            if (s == "UT") diemUT = 1;
            else diemUT = 0;
            return diemUT;
        }

        public void tinhtoan()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 1; j < 2; j++)
                {
                    if (nguyenvong[i, j] ==null) ketqua[i]=-1;
                    else
                    {
                        string[] s = nguyenvong[i, j].Split(',');
                        if (s[3] == "1")
                        {
                            ketqua[i] = (diemcacmon(s[0]) * 2 + diemcacmon(s[1]) + diemcacmon(s[2]) + diemkhuvuc(kv)) / 4 + diemuutien(uutien);
                        }
                        else
                            ketqua[i] = (diemcacmon(s[0]) + diemcacmon(s[1]) + diemcacmon(s[2]) + diemkhuvuc(kv)) / 3 + diemuutien(uutien);
                    }
                    
                }
        }
        public void Getdata(string sql)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source = C:\Users\Thang\Desktop\bangdiem.db");
            conn.Open();


            SQLiteCommand cmd; //Đối tượng thuộc lớp SqlCommand
            cmd = new SQLiteCommand();
            cmd.Connection = conn; //Gán kết nối
            //string sql = "INSERT INTO ketqua VALUES('test2',1)";


            cmd.CommandText = sql; //Gán lệnh SQL
            try
            {
                cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL
            }
            catch (Exception ex)
            {
                Console.Write("Loi");
            }
            cmd.Dispose();//Giải phóng bộ nhớ
            cmd = null;
        }
        

    }
}
