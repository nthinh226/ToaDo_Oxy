using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;

namespace ToaDo_Oxy
{
    class Program
    {
        class ToaDo
        {
            private int Ox;
            private int Oy;
            private double r;
            
            public ToaDo()
            {
                this.Ox = 0;
                this.Oy = 0;
                this.r = 0;
            }
            public int OX
            {
                get { return Ox; }
                set { Ox = value; }
            }
            public int OY
            {
                get { return Oy; }
                set { Oy = value; }
            }
            public double R
            {
                get { return r = Math.Round(Math.Sqrt(Math.Pow(OX, 2) + Math.Pow(OY, 2)), 2); }
            }
            public void input()
            {
                Console.WriteLine("Nhập toạ độ:");
                Console.Write("Ox: ");
                OX = int.Parse(Console.ReadLine());
                Console.Write("Oy: ");
                OY = int.Parse(Console.ReadLine());
            }
            public void Output()
            {
                Console.WriteLine($"Hoành độ: {OX}\tTung độ: {OY}\tKhoảng cách đến tâm O là: {R}");
            }
        }
        class DSToaDo : ToaDo
        {
            private List<ToaDo> listToaDo = null;
            public DSToaDo()
            {
                listToaDo = new List<ToaDo>();
            }
            public int soLuongDiem()
            {
                int Count = 0;
                if (listToaDo != null)
                {
                    Count = listToaDo.Count();
                }
                return Count;
            }
            public void docfile(String file)
            {
                XmlDocument read = new XmlDocument();
                read.Load(file);
                XmlNodeList nodelist = read.SelectNodes("/OXY/ToaDo");

                foreach (XmlNode node in nodelist)
                {
                    ToaDo td = new ToaDo();
                    td.OX = int.Parse(node["Ox"].InnerText);
                    td.OY = int.Parse(node["Oy"].InnerText);
                    listToaDo.Add(td);
                }
            }
            public void inputTD()
            {
                ToaDo td = new ToaDo();
                td.input();
                listToaDo.Add(td);
            }
            public void outputTD(ToaDo td)
            {
                td.Output();
            }
            public void sortByKhoangCach()
            {
                listToaDo.Sort(delegate (ToaDo td1, ToaDo td2) {
                    return td1.R.CompareTo(td2.R);
                });
                listToaDo.Reverse();
            }
            public void diemTrenDuongTron(float a)
            {
                var b = from ToaDo tda in listToaDo where tda.R == a select tda;
                Console.WriteLine($"Danh sách các điểm nằm trên đường tròn có bán kính {a}");

                foreach (ToaDo td in b)
                {
                    Console.WriteLine($"Diem: ({td.OX},{td.OY})");
                }
            }
            public void diemTrenGocPhanTuI()
            {
                var x = from ToaDo tda in listToaDo where tda.OX > 0 && tda.OY > 0 select tda;
                Console.WriteLine("Danh sách các điểm nằm trong góc phần tư thứ nhất!");
                foreach (ToaDo td in x)
                {
                    Console.WriteLine($"Điểm: ({td.OX}:{td.OY})");
                }
            }

            public void xoaToaDo_YeuCau()
            {
                int dem = listToaDo.Count();
                for (int i = 0; i < dem; i++)
                {
                    if (listToaDo[i].OX > 5 && listToaDo[i].OX < 8)
                    {
                        listToaDo.Remove(listToaDo[i]);
                        dem = dem - 1;
                        i = i - 1;
                    }
                }
            }
            public int demSoHoanhDoDuong()
            {
                int dem = 0;
                foreach(ToaDo td in listToaDo)
                {
                    if (td.OX > 0)
                        dem += 1;
                }
                return dem;
            }
            public void ShowToaDo(List<ToaDo> listTD)
            {
                Console.WriteLine("{0, -8} {1, 12} {2, 26}", "Hoành độ", "Tung độ", "Khoảng cách đến tâm O");
                if (listToaDo != null && listToaDo.Count() > 0)
                {
                    foreach (ToaDo sv in listToaDo)
                    {
                        Console.WriteLine("{0, -8} {1, 12} {2, 26}", sv.OX, sv.OY, sv.R);
                    }
                }
                Console.WriteLine();
            }
            public List<ToaDo> getListTD()
            {
                return listToaDo;
            }
        }
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.UTF8;
            DSToaDo ds = new DSToaDo();
            int option;
            do
            {
                Console.WriteLine("************************************MENU*************************************");
                Console.WriteLine("**  1. Nhập toạ độ từ bàn phím.                                            **");
                Console.WriteLine("**  2. Nhập toạ độ từ file xml.                                            **");
                Console.WriteLine("**  3. Hiển thị danh sách các điểm có R->O giảm dần.                       **");
                Console.WriteLine("**  4. Danh sách các điểm nằm trong đường tròn tâm O bán kính R>0.         **");
                Console.WriteLine("**  5. Hiển thị danh sách tất cả các điểm nằm trong góc phần tư thứ nhất.  **");
                Console.WriteLine("**  6. Xóa khỏi danh sách các điểm có hoành độ lớn hơn 5 nhưng nhỏ hơn 8.  **");
                Console.WriteLine("**  7. Đếm xem danh sách có bao nhiêu điểm có hoành độ là số dương.        **");
                Console.WriteLine("**  0. Thoát.                                                              **");
                Console.WriteLine("*****************************************************************************");
                Console.Write("Nhập lựa chọn: ");
                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.WriteLine("\n1. Bạn đã chọn nhập toạ độ từ bàn phím.");
                        ds.inputTD();
                        Console.WriteLine("\nThêm toạ độ thành công!");
                        break;
                    case 2:
                        Console.WriteLine("\n2. Bạn đã chọn nhập toạ độ từ file xml.");
                        ds.docfile("C:/Users/ngoct/Desktop/Lap Trinh Nang Cao/Buoi14/Tran Ngoc Thinh - ToaDo_Oxy/ToaDo_Oxy/toado.xml");
                        Console.WriteLine("Đọc dữ liệu thành công.");
                        break;
                    case 3:
                        if (ds.soLuongDiem() > 0)
                        {
                            Console.WriteLine("\n3. Hiển thị danh sách các điểm có R->O giảm dần.");
                            ds.sortByKhoangCach();
                            ds.ShowToaDo(ds.getListTD());
                        }
                        else
                        {
                            Console.WriteLine("\nDanh sách toạ độ rỗng!!");
                        }
                        break;
                    case 4:
                        if (ds.soLuongDiem() > 0)
                        {
                            Console.WriteLine("\n4. Danh sách các điểm nằm trong đường tròn tâm O bán kính R>0.");
                            Console.Write("Nhập bán kính đường tròn R = ");
                            float bk = float.Parse(Console.ReadLine());
                            ds.diemTrenDuongTron(bk);
                        }
                        else
                        {
                            Console.WriteLine("\nDanh sách toạ độ rỗng!!");
                        }
                        break;
                    case 5:
                        if (ds.soLuongDiem() > 0)
                        {
                            Console.WriteLine("\n5. Hiển thị danh sách tất cả các điểm nằm trong góc phần tư thứ nhất.");
                            ds.diemTrenGocPhanTuI();
                        }
                        else
                        {
                            Console.WriteLine("\nDanh sách toạ độ rỗng!!");
                        }
                        break;
                    case 6:
                        if (ds.soLuongDiem() > 0)
                        {
                            Console.WriteLine("\n6. Xóa khỏi danh sách các điểm có hoành độ lớn hơn 5 nhưng nhỏ hơn 8.");
                            ds.xoaToaDo_YeuCau();
                            ds.ShowToaDo(ds.getListTD());
                        }
                        else
                        {
                            Console.WriteLine("\nDanh sách toạ độ rỗng!!");
                        }
                        break;
                    case 7:
                        if (ds.soLuongDiem() > 0)
                        {
                            Console.WriteLine("\n7. Đếm xem danh sách có bao nhiêu điểm có hoành độ là số dương. ");
                            Console.WriteLine($"Danh sách có {ds.demSoHoanhDoDuong()} điểm có hoành độ là số dương. ");

                        }
                        else
                        {
                            Console.WriteLine("\nDanh sách toạ độ rỗng!!");
                        }
                        break;
                    default:
                        if (!option.Equals(0))
                            Console.WriteLine("Lựa chọn của bạn không có sẵn!!");
                        break;
                }
            } while (!option.Equals(0));
        }
    }
}
