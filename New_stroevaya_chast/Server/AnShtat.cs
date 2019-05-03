using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace New_Stroevaya_chast.Modal
{
    class AnShtat
    {
    }
    public class Pol : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string ln_ = ".";
        string fio_ = ".";
        int level_ = 0;
        int num_ = 0;
        string status_ = ".";
        string login_ = ".";
        string pass_ = ".";

        public string LN
        {
            get { return ln_; }
            set
            {
                if (ln_ == value)
                    return;
                ln_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("LN"));
            }
        }
        public string FIO
        {
            get { return fio_; }
            set
            {
                if (fio_ == value)
                    return;
                fio_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("FIO"));
            }
        }
        public int Level
        {
            get { return level_; }
            set
            {
                if (level_ == value)
                    return;
                level_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("LVL"));
            }
        }
        public string Status
        {
            get { return status_; }
            set
            {
                if (status_ == value)
                    return;
                status_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }
        public string Login
        {
            get { return login_; }
            set
            {
                if (login_ == value)
                    return;
                login_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Login"));
            }
        }
        public string Pass
        {
            get { return pass_; }
            set
            {
                if (pass_ == value)
                    return;
                pass_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Pass"));
            }
        }
        public int Num
        {
            get { return num_; }
            set
            {
                if (num_ == value)
                    return;
                num_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Num"));
            }
        }
        public void ToXml()
        {

            XDocument doc;
            if (File.Exists(Environment.CurrentDirectory + "\\Recources\\prf\\" + Login + ".xml") == false)
            {
                string fileName = Environment.CurrentDirectory + "\\Recources\\prf\\" + Login + ".xml";
                doc = new XDocument(
                            new XElement("base",
                             new XElement("track",
                                new XAttribute("lnumber", LN),
                                new XAttribute("login", Login),
                                new XAttribute("pass", Pass),
                                new XAttribute("type_bottom", Level))));
                doc.Save(fileName);
            }
            else
            {
                string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\prf" + Login + ".xml";
                doc = XDocument.Load(fileName);
                doc.Descendants().Where(e => e.Name == "track").Remove();
                XElement track = new XElement("track");
                track.Add(new XAttribute("lnumber", LN));
                track.Add(new XAttribute("login", Login));
                track.Add(new XAttribute("pass", Pass));
                track.Add(new XAttribute("type_bottom", Level));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }
        public ObservableCollection<Pol> Sbor()
        {
            ObservableCollection<Pol> t = new ObservableCollection<Pol>();
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory + "\\Recources\\prf");
            foreach (var item in dir.GetFiles())
            {
                Pol n = new Pol();
                string filename = Environment.CurrentDirectory + "\\Recources\\prf\\" + item.Name;
                XDocument doc = XDocument.Load(filename);
                foreach (XElement el in doc.Root.Elements("track"))
                {
                    n.LN = el.Attribute("lnumber").Value;
                    n.Login = el.Attribute("login").Value;
                    n.Pass = el.Attribute("pass").Value;
                    n.Level = int.Parse(el.Attribute("type_bottom").Value);
                }
                if (File.Exists(filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + item.Name) == true)
                {
                    doc = XDocument.Load(filename);
                    foreach (XElement el in doc.Root.Elements("Lichnoe_delo"))
                    {
                        n.FIO = el.Attribute("fio").Value;

                    }
                }
                else n.FIO = "Не опознан";
                t.Add(n);
            }
            return t;
        }
    }
    public class Type_People : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string ring = "", lnumber_ = "", login_ = "", pass_ = "";
        private int id_ = 0;
        static int counter_ = 0;
        public string type_ = "";
        public string Level = "";
        public void Deap()
        {
            string[] seat = ring.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (seat[0] == "Ef9UH")
            {
                if (seat[1] == "KBPU7")//Всея Админ
                {
                    if (seat[2] == "78gb3v")//Полный доступ
                    {
                        TYPE_Bottom = "1";
                    }
                }
                if (seat[1] == "AnXhV") // НачАдмин
                {
                    if (seat[2] == "78gb3v")//Полный доступ
                    {
                        TYPE_Bottom = "2";
                    }
                }
                if (seat[1] == "gF5q3")// Офиц
                {
                    if (seat[2] == "78gb3v")//Полный доступ
                    {
                        TYPE_Bottom = "3";
                    }
                }
                if (seat[1] == "8Hyu9") // Прапор
                {
                    if (seat[2] == "78gb3v")//Полный доступ
                    {
                        TYPE_Bottom = "4";
                    }
                }
            }
            if (seat[0] == "9QglK")
            {
                if (seat[1] == "LISy5")//Серж
                {
                    if (seat[2] == "78gb3v")// ограниченный доступ
                    {
                        TYPE_Bottom = "5";
                    }
                }
                if (seat[1] == "JNDxs")//Солд
                {
                    if (seat[2] == "78gb3v")//ограниченный доступ
                    {
                        TYPE_Bottom = "6";
                    }
                }
                if (seat[1] == "0TPM4")//Сроч
                {
                    if (seat[2] == "78gb3v")//ограниченный доступ
                    {
                        TYPE_Bottom = "7";
                    }
                }
                if (seat[1] == "LISy5")//Приказ
                {
                    if (seat[2] == "78gb3v")//ограниченный доступ
                    {
                        TYPE_Bottom = "8";
                    }
                }
                if (seat[1] == "Hfy4A")//Наряд
                {
                    if (seat[2] == "56gH3a")//ограниченный доступ
                    {
                        TYPE_Bottom = "9";
                    }
                }
                if (seat[1] == "Hdy5A")//гражданский штат
                {
                    if (seat[2] == "52gZ3b")//ограниченный доступ
                    {
                        TYPE_Bottom = "10";
                    }
                }

            }
            else return;

        }//уровень доступа

        public Type_People()
        {
            id_ = counter_++;
        }
        public int Counter
        {
            get { return counter_; }
        }
        public int ID { get; set; }


        public Type_People(XElement element)
        {
            Lnumber = element.Attribute("lnumber").Value;
            LOGIN = element.Attribute("login").Value;
            PASSWORD = element.Attribute("pass").Value;
            TYPE_Bottom = element.Attribute("type_bottom").Value;
            if (int.Parse(element.Attribute("id").Value) > id_)
            {
                id_ = int.Parse(element.Attribute("id").Value);
                counter_ = Math.Max(counter_, id_ + 1);
            }
        }


        public string Lnumber { get; set; }
        public string TYPE
        {
            get { return ring; }
            set
            {
                if (ring == value)
                    return;
                ring = value;
            }
        }
        public string LOGIN { get; set; }
        public string PASSWORD { get; set; }
        public string TYPE_Bottom { get; set; }

        public void ToXml()
        {

            if (TYPE_Bottom != null)
            {
                string fileName = Environment.CurrentDirectory + "\\Recources\\Profile\\" + LOGIN + ".xml";
                XDocument doc;
                if (File.Exists(fileName) == false)
                {
                    doc = new XDocument(
                        new XElement("base",
                         new XElement("track",
                            new XAttribute("id", ID),
                            new XAttribute("lnumber", Lnumber),
                            new XAttribute("login", LOGIN),
                            new XAttribute("pass", PASSWORD),
                            new XAttribute("type_bottom", TYPE_Bottom))));
                    doc.Save(fileName);
                }
                else
                {
                    doc = XDocument.Load(fileName);
                    XElement based = new XElement("base");
                    XElement track = new XElement("track");
                    track.Add(new XAttribute("id", ID));
                    track.Add(new XAttribute("lnumber", Lnumber));
                    track.Add(new XAttribute("login", LOGIN));
                    track.Add(new XAttribute("pass", PASSWORD));
                    track.Add(new XAttribute("type_bottom", TYPE_Bottom));
                    based.Add(track);
                    doc.Root.Add(based);
                    doc.Save(fileName);
                }
            }


        }



    }
}
