using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{
    
    
    public class Goods
    {
        public string Group { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Type { get; set; }

    }
    public class Program
    {
        static void Main()
        {
            Console.Clear();
            string path = "";
            List<Goods> goods = new List<Goods>();
            Console.WriteLine("Введiть шлях до файлу '' або натиснiть будь-яку клавiшу, щоб створити новий");
            path = Console.ReadLine();
            try
            {
                goods = ReadData(path);
            }
            catch
            {
                path = "Data.txt";
            }
            
            while (true)
            {
                Console.Clear();
                ShowTable(goods);
                var press = Console.ReadKey().Key;
                if (press.ToString() == "Enter")
                {
                    Main();
                }
                if (press.ToString() == "P")
                {
                    Console.WriteLine();
                    ChangeData(goods);
                    SaveData(goods,path);
                }
                if (press.ToString() == "N")
                {
                    Console.WriteLine();
                    Seach(goods);
                    SaveData(goods, path);
                }
                if (press.ToString() == "D")
                {
                    Console.WriteLine();
                    AddNew(goods);
                    SaveData(goods, path);
                }
                if (press.ToString() == "S")
                {
                    Console.WriteLine();
                    Sort(goods);
                    SaveData(goods, path);
                }                
            }

        }
        static string PS(int count)
        {
            string s = "";
            for(int i = 0;i < count; i++)
            {
                s += " ";
            }
            return s;
        }
        static void ShowTable(List<Goods> Goods)
        {
            int MaxI = 8;
            int MaxN = 12;
            int MaxW = 8;
            int MaxC = 7;
            Console.WriteLine("| Група |Прiзвище\t| Обл. зап.\t\t| Тип\t|");
            foreach(Goods g in Goods)
            {
                int ni = MaxI - g.Group.Count();
                int nn = MaxN - g.Name.Count();
                int nw = MaxW - g.Mail.Count();
                int nc = MaxC - g.Type.Count();
                Console.WriteLine("|" + Convert.ToString(g.Group) + PS(ni) + "|" + g.Name + PS(nn) + "\t|" +
                    Convert.ToString(g.Mail) + PS(nw) + "\t\t|" + Convert.ToString(g.Type) + PS(nc) + "|");
            }
            Console.WriteLine(" p - Ред./Видалити,\n d - Створити\n s- Сортувати за типом \n n - Пошук,\n Enter - Вихід");
        }
        static List<Goods> ReadData(string path)
        {
            List<Goods> g = new List<Goods>();
            string text = "";
            using (StreamReader sr = new StreamReader(path))
            {
                text = sr.ReadToEnd();
            }
            string[] Dates = text.Split('/');
            foreach(string s in Dates)
            {
                string[] MetaDete = s.Split('|');
                if(MetaDete.Length > 3)
                {
                    Goods d = new Goods
                    {
                        Group = MetaDete[0],
                        Name = MetaDete[1],
                        Mail = MetaDete[2],
                        Type = MetaDete[3],
                    };
                    g.Add(d);
                }
            }
            return g;
        }
        static void SaveData(List<Goods> Data,string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (Goods g in Data)
                {

                    sw.WriteLine(g.Group + "|" + g.Name + "|" + g.Mail + "|" + g.Type + "/");

                }
            }
        }
        static void ChangeData(List<Goods> Data)
        {
            Console.WriteLine("Введiть прiзвище користувача для зміни");
            string Nam = Console.ReadLine();
            Goods Choosen = new Goods();
            foreach(Goods g in Data)
            {
                if(g.Name == Nam)
                {
                    Choosen = g;
                }
            }
            if(Choosen.Name != "")
            {
                Console.WriteLine();
                Console.WriteLine("1)Змiнити групу\n2)Змiнити прiзвище\n3)Змiнити обл.зап.\n4)Змiнити тип\n5)Delete");
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine("Введiть нове значення:");
                try
                {
                    if (key == '1')
                    {

                        Choosen.Group = Console.ReadLine();
                    }
                    if (key == '2')
                    {

                        Choosen.Name = Console.ReadLine();
                    }
                    if (key == '3')
                    {

                        Choosen.Mail = Console.ReadLine();
                    }
                    if (key == '4')
                    {
                        Choosen.Type = Console.ReadLine();
                    }
                    if (key == '5')
                    {
                        Data.Remove(Choosen);
                    }
                }
                catch
                {
                    Console.WriteLine("Помилка");
                }
               
            }
            else
            {
                Console.WriteLine("не знайдено");
            }
            
        }
        static void AddNew(List<Goods> Data)
        {
            
            Console.WriteLine("Введiть прiзвище");
            Goods neww = new Goods();
            neww.Name = Console.ReadLine();
            Console.WriteLine("Введiть групу");
            neww.Group = Console.ReadLine();
            Console.WriteLine("Введiть обл.зап.");
            neww.Mail = Console.ReadLine();
            Console.WriteLine("Введiть тип");
            neww.Type = Console.ReadLine();
            Data.Add(neww);
        }
        static void Seach(List<Goods> Data)
        {
            int MaxI = 8;
            int MaxN = 12;
            int MaxW = 8;
            int MaxC = 7;
            Console.Clear();
            Console.WriteLine("Введiть прiзвище, по якому будете шукати");
            string s = Console.ReadLine();
            foreach (Goods g in Data)
            {
                
                if (g.Name == s)
                {
                    int ni = MaxI - Convert.ToString(g.Group).Length;
                    int nn = MaxN - g.Name.Count();
                    int nw = MaxW - Convert.ToString(g.Mail).Length;
                    int nc = MaxC - Convert.ToString(g.Type).Length;
                    Console.WriteLine("| Група |Прiзвище\t| Обл. зап.\t\t| Тип\t|");
                    Console.WriteLine("|" + Convert.ToString(g.Group) + PS(ni) + "|" + g.Name + PS(nn) + "\t|" +
                    Convert.ToString(g.Mail) + PS(nw) + "\t\t|" + Convert.ToString(g.Type) + PS(nc) + "|");
                }
            }
            Console.WriteLine("Натиснiть будь-яку клавiшу");
            Console.ReadLine();
        }
        static void Sort(List<Goods> Data)
        {
            string[] a = new string[Data.Count];
            for(int i = 0; i < a.Length; i++)
            {
                a[i] = Data[i].Type;
              }
        }
    }
}