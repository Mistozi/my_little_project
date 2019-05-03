using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace New_Stroevaya_chast.Modal
{
    public class Shtat
    {
        public class Izmen : INotifyPropertyChanged
        {
            public Izmen()
            {

            }
            public Izmen(XElement el)
            {
                if (el.Attribute("types").Value != ".") Types = el.Attribute("types").Value;
                if (el.Attribute("col").Value != ".") Col = el.Attribute("col").Value;
                if (el.Attribute("row").Value != ".") Row = int.Parse(el.Attribute("row").Value);
                if (el.Attribute("name").Value != ".") Name_table = el.Attribute("name").Value;
                if (el.Attribute("new_value").Value != ".") New_value = el.Attribute("new_value").Value;
                if (el.Attribute("old_value").Value != ".") Old_value = el.Attribute("old_value").Value;
            }
            private string types_ = ".";
            private string name_table_ = ".";
            private string old_value = ".";
            private int row_ = 0;
            private string col_ = ".";
            private string new_value = ".";
            public string Types
            {
                get { return types_; }
                set
                {
                    if (types_ == value)
                        return;
                    types_ = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Types"));
                }
            }
            public string Name_table
            {
                get { return name_table_; }
                set
                {
                    if (name_table_ == value)
                        return;
                    name_table_ = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name_table"));
                }
            }
            public string Old_value
            {
                get { return old_value; }
                set
                {
                    if (old_value == value)
                        return;
                    old_value = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Old_value"));
                }
            }
            public string New_value
            {
                get { return new_value; }
                set
                {
                    if (new_value == value)
                        return;
                    new_value = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("New_value"));
                }
            }
            public int Row
            {
                get { return row_; }
                set
                {
                    if (row_ == value)
                        return;
                    row_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Row"));
                }
            }
            public string Col
            {
                get { return col_; }
                set
                {
                    if (col_ == value)
                        return;
                    col_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Col"));
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
            private string Date()
            {
                string data = "";
                DateTime dt = DateTime.Today;
                if (dt.Day < 10)
                { data = "0" + dt.Day.ToString(); }
                else
                { data = dt.Day.ToString(); }
                if (dt.Month < 10)
                { data += ".0" + dt.Month.ToString(); }
                else
                { data += "." + dt.Month.ToString(); }
                data += "." + dt.Year.ToString();

                return data;
            }
            public void ToXml1()
            {

                XDocument doc;
                if (File.Exists(Environment.CurrentDirectory + "\\Recources\\Active\\izm.xml") == false)
                {
                    string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\izm.xml";
                    doc = new XDocument(
                                new XElement("base",
                                 new XElement("track",
                                    new XAttribute("types", Types),
                                    new XAttribute("col", Col),
                                    new XAttribute("row", Row),
                                    new XAttribute("name", Name_table),
                                    new XAttribute("new_value", New_value),
                                    new XAttribute("old_value", Old_value))));
                    doc.Save(fileName);
                }
                else
                {
                    string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\izm.xml";
                    doc = XDocument.Load(fileName);
                    XElement track = new XElement("track");
                    track.Add(new XAttribute("types", Types));
                    track.Add(new XAttribute("col", Col));
                    track.Add(new XAttribute("row", Row));
                    track.Add(new XAttribute("name", Name_table));
                    track.Add(new XAttribute("new_value", New_value));
                    track.Add(new XAttribute("old_value", Old_value));
                    doc.Root.Add(track);
                    doc.Save(fileName);
                }
            }
            public void ToXml()
            {
                string fileName;
                XDocument doc;
                if (File.Exists(Environment.CurrentDirectory + "\\Recources\\Active\\izm\\izm " + Date() + ".xml") == false)
                {
                    fileName = Environment.CurrentDirectory + "\\Recources\\Active\\izm\\izm " + Date() + ".xml";
                    doc = new XDocument(
                                new XElement("base",
                                 new XElement("track",
                                    new XAttribute("types", Types),
                                    new XAttribute("col", Col),
                                    new XAttribute("row", Row),
                                    new XAttribute("name", Name_table),
                                    new XAttribute("new_value", New_value),
                                    new XAttribute("old_value", Old_value))));
                    doc.Save(fileName);
                }
                else
                {
                    fileName = Environment.CurrentDirectory + "\\Recources\\Active\\izm\\izm " + Date() + ".xml";
                    doc = XDocument.Load(fileName);
                    XElement track = new XElement("track");
                    track.Add(new XAttribute("types", Types));
                    track.Add(new XAttribute("col", Col));
                    track.Add(new XAttribute("row", Row));
                    track.Add(new XAttribute("name", Name_table));
                    track.Add(new XAttribute("new_value", New_value));
                    track.Add(new XAttribute("old_value", Old_value));
                    doc.Root.Add(track);
                    doc.Save(fileName);
                }
                fileName = Environment.CurrentDirectory + "\\Recources\\Active\\izm.xml";
                File.Delete(fileName);
            }
            public void ToXmlPerest(OpenLoad tmp, string message)
            {
                string fileName;
                XDocument doc;
                if (Types == "Добавить")
                {
                    if (File.Exists(Environment.CurrentDirectory + "\\Recources\\Active\\peres\\peres " + Date() + ".xml") == false)
                    {
                        fileName = Environment.CurrentDirectory + "\\Recources\\Active\\peres\\peres " + Date() + ".xml";
                        doc = new XDocument(
                                    new XElement("base",
                                     new XElement("track",
                                        new XAttribute("lnumber", New_value),
                                        new XAttribute("new_podr", tmp.Podr),
                                        new XAttribute("vus", tmp.Vus),
                                        new XAttribute("kod", tmp.Kod),
                                        new XAttribute("new_dolg", tmp.Dolgnost))));
                        doc.Save(fileName);
                    }
                    else
                    {
                        fileName = Environment.CurrentDirectory + "\\Recources\\Active\\peres\\peres " + Date() + ".xml";
                        doc = XDocument.Load(fileName);
                        XElement track = new XElement("track");
                        track.Add(new XAttribute("lnumber", New_value));
                        track.Add(new XAttribute("new_podr", tmp.Podr));
                        track.Add(new XAttribute("vus", tmp.Vus));
                        track.Add(new XAttribute("kod", tmp.Kod));
                        track.Add(new XAttribute("new_dolg", tmp.Dolgnost));
                        doc.Root.Add(track);
                        doc.Save(fileName);
                    }
                    fileName = Environment.CurrentDirectory + "\\Recources\\Shtat\\51460.xml";
                    doc = XDocument.Load(fileName);
                    foreach(XElement i in doc.Root.Elements("track"))
                    {
                        if(int.Parse(i.Attribute("id").Value) == Row)
                        {
                            i.Attribute("lnumber").Value = New_value;
                        }
                    }
                    doc.Save(fileName);
                    fileName = Environment.CurrentDirectory + "\\Recources\\AnShtat\\51460.xml";
                    doc = XDocument.Load(fileName);
                    doc.Descendants().Where(e => e.Name == New_value).Remove();
                    doc.Save(fileName);
                }
                else
                    if (Types == "Удалить")
                {
                    if (File.Exists(Environment.CurrentDirectory + "\\Recources\\Active\\peres\\peres " + Date() + ".xml") == false)
                    {
                        fileName = Environment.CurrentDirectory + "\\Recources\\Active\\peres\\peres " + Date() + ".xml";
                        doc = new XDocument(
                            new XElement("base",
                             new XElement("track",
                                new XAttribute("lnumber", Old_value),
                                new XAttribute("old_podr", tmp.Podr),
                                new XAttribute("old_dolg", tmp.Dolgnost))));
                        doc.Save(fileName);
                    }
                    else
                    {
                        fileName = Environment.CurrentDirectory + "\\Recources\\Active\\peres\\peres " + Date() + ".xml";
                        doc = XDocument.Load(fileName);
                        XElement track = new XElement("track");
                        track.Add(new XAttribute("lnumber", Old_value));
                        track.Add(new XAttribute("old_podr", tmp.Podr));
                        track.Add(new XAttribute("old_dolg", tmp.Dolgnost));
                        doc.Root.Add(track);
                        doc.Save(fileName);
                    }
                    fileName = Environment.CurrentDirectory + "\\Recources\\Shtat\\51460.xml";
                    doc = XDocument.Load(fileName);
                    foreach (XElement i in doc.Root.Elements("track"))
                    {
                        if (int.Parse(i.Attribute("id").Value) == Row)
                        {
                            i.Attribute("lnumber").Value = "0";
                        }
                    }
                    doc.Save(fileName);
                    string[] word = Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                    OpenLoadZaShtat t = new OpenLoadZaShtat();
                    t.FIO = word[2];
                    t.Last_dolg = word[0];
                    t.Last_podr = word[1];
                    t.Zvanie = word[3];
                    t.Types = word[4];
                    t.ToXml();
                   
                }
            }
            public void ToXmlLD(OpenLoad tmp, string mes)
            {
                if (Types == "Добавить")
                {

                    XDocument doc;
                    if (Types == "Добавить")
                    {
                        string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + New_value + ".xml";
                        doc = XDocument.Load(fileName);
                        XElement track = new XElement("track_pos");
                        track.Add(new XAttribute("chast", "51460"));
                        track.Add(new XAttribute("podr", tmp.Podr));
                        track.Add(new XAttribute("dolg", tmp.Dolgnost));
                        track.Add(new XAttribute("vus", tmp.Vus));
                        track.Add(new XAttribute("data", Date()));
                        doc.Root.Add(track);
                        doc.Save(fileName);
                    }
                }

            }
            public void ToXmlUb(Ub tmp, string Types)
            {
                try
                {
                    string fileName;
                    XDocument doc;
                    XElement track;
                    if (Types == "Убытие")
                    {

                        if (File.Exists(Environment.CurrentDirectory + "\\Recources\\Active\\ub.xml") == false)
                        {
                            fileName = Environment.CurrentDirectory + "\\Recources\\Active\\ub.xml";
                            doc = new XDocument(
                                        new XElement("base",
                                         new XElement("track",
                                            new XAttribute("cel", tmp.Cel),
                                            new XAttribute("date2", tmp.Date_preb),
                                            new XAttribute("date1", tmp.Date_ub),
                                            new XAttribute("dolg", tmp.Dolg),
                                            new XAttribute("fio", tmp.FIO),
                                            new XAttribute("ln", tmp.Lnumber),
                                            new XAttribute("ms", tmp.Mesto),
                                            new XAttribute("osn", tmp.Osn),
                                            new XAttribute("podr", tmp.Podr),
                                            new XAttribute("type1", tmp.Types),
                                            new XAttribute("type2", tmp.Types_ub),
                                            new XAttribute("vrio", tmp.Vrio),
                                            new XAttribute("zv", tmp.Zvanie),
                                            new XAttribute("vp1", tmp.VPD_ST1),
                                            new XAttribute("vp2", tmp.VPD_ST2),
                                            new XAttribute("vp3", tmp.VPD_ST3),
                                            new XAttribute("vp4", tmp.VPD_ST4),
                                            new XAttribute("vp5", tmp.VPD_ST5),
                                            new XAttribute("vp6", tmp.VPD_ST6),
                                            new XAttribute("vp7", tmp.VPD_ST7))));
                            doc.Save(fileName);
                        }
                        else
                        {
                            fileName = Environment.CurrentDirectory + "\\Recources\\Active\\ub.xml";
                            doc = XDocument.Load(fileName);
                            track = new XElement("track");
                            track.Add(new XAttribute("cel", tmp.Cel));
                            track.Add(new XAttribute("date2", tmp.Date_preb));
                            track.Add(new XAttribute("date1", tmp.Date_ub));
                            track.Add(new XAttribute("dolg", tmp.Dolg));
                            track.Add(new XAttribute("fio", tmp.FIO));
                            track.Add(new XAttribute("ln", tmp.Lnumber));
                            track.Add(new XAttribute("ms", tmp.Mesto));
                            track.Add(new XAttribute("osn", tmp.Osn));
                            track.Add(new XAttribute("podr", tmp.Podr));
                            track.Add(new XAttribute("type1", tmp.Types));
                            track.Add(new XAttribute("type2", tmp.Types_ub));
                            track.Add(new XAttribute("vrio", tmp.Vrio));
                            track.Add(new XAttribute("zv", tmp.Zvanie));
                            track.Add(new XAttribute("vp1", tmp.VPD_ST1));
                            track.Add(new XAttribute("vp2", tmp.VPD_ST2));
                            track.Add(new XAttribute("vp3", tmp.VPD_ST3));
                            track.Add(new XAttribute("vp4", tmp.VPD_ST4));
                            track.Add(new XAttribute("vp5", tmp.VPD_ST5));
                            track.Add(new XAttribute("vp6", tmp.VPD_ST6));
                            track.Add(new XAttribute("vp7", tmp.VPD_ST7));
                            doc.Root.Add(track);
                            doc.Save(fileName);
                        }
                        fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + tmp.Lnumber + ".xml";
                        doc = XDocument.Load(fileName);
                        track = new XElement("track_ub");
                        track.Add(new XAttribute("date1", tmp.Date_ub));
                        track.Add(new XAttribute("date2", tmp.Date_preb));
                        track.Add(new XAttribute("cel", tmp.Cel));
                        track.Add(new XAttribute("ms", tmp.Mesto));
                        track.Add(new XAttribute("osn", tmp.Osn));
                        track.Add(new XAttribute("type1", tmp.Types_ub));
                        track.Add(new XAttribute("vrio", tmp.Vrio));
                        track.Add(new XAttribute("date3", tmp.Date3));
                        doc.Root.Add(track);
                        doc.Save(fileName);

                    }
                    if (tmp.Types_ub == "Пребытие")
                    {
                        fileName = Environment.CurrentDirectory + "\\Recources\\Active\\preb.xml";
                        doc = XDocument.Load(fileName);
                        track = new XElement("track");
                        track.Add(new XAttribute("fio", tmp.FIO));
                        track.Add(new XAttribute("ln", tmp.Lnumber));
                        track.Add(new XAttribute("type2", tmp.Types_ub));
                        doc.Root.Add(track);
                        doc.Save(fileName);
                        fileName = Environment.CurrentDirectory + "\\Recources\\Active\\ub.xml";
                        doc = XDocument.Load(fileName);
                        doc.Descendants().Where(e => e.Name == tmp.FIO).Remove();
                        doc.Save(fileName);
                        fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + tmp.Lnumber + ".xml";
                        doc = XDocument.Load(fileName);
                        track = new XElement("track_ub");
                        if (track.Attribute("date3").Value == ".")
                            track.Attribute("date3").Value = tmp.Date3;
                        doc.Save(fileName);
                    }
                }
                catch { MessageBox.Show("Ошибка сохранения убытия"); }
            }
                


        }
        public class OpenLoad : INotifyPropertyChanged //Штат
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public OpenLoad()
            {
            }
            ObservableCollection<OpenLoad> Shtat_List = new ObservableCollection<OpenLoad>();

            #region Обязательные переменные
            private int id = 1;
            int counter_ = 1;
            private string podr = "";
            private string podr1 = "";
            private string podr2 = "";
            private string podr3 = "";
            private string podr4 = "";
            private string dolgnost = "";
            private string shtat_type = "";
            private string vus = "";
            private string kod = "";
            private string lnumber = "";
            #endregion
            #region переменные для сборки
            private string zvanie = "";
            private string fio = "";
            private string types = "";
            private string otryv_status = "";
            private string otryv_primech = "";
            private string otryv_pricaz = "";
            #endregion
            #region конструтора (обязательные)
            public int Counter
            {
                get { return counter_; }
            }

            public OpenLoad(XElement element)
            {
                Lnumber = element.Attribute("lnumber").Value;
                Podr = element.Attribute("podr").Value;
                Dolgnost = element.Attribute("dolg").Value;
                Vus = element.Attribute("vus").Value;
                Kod = element.Attribute("kod").Value;
                Shtat_type = element.Attribute("shtat_type").Value;
                if (int.Parse(element.Attribute("id").Value) > Id)
                {
                    Id = int.Parse(element.Attribute("id").Value);
                }
            }
            public string Lnumber
            {
                get { return lnumber; }
                set
                {
                    if (lnumber == value)
                        return;
                    lnumber = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Lnumber"));
                }
            }
            public string Podr
            {
                get { return podr; }
                set
                {
                    if (podr == value)
                        return;
                    podr = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Podr"));
                }
            }
            public string Podr1
            {
                get { return podr1; }
                set
                {
                    if (podr1 == value)
                        return;
                    podr1 = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Podr1"));
                }
            }
            public string Podr2
            {
                get { return podr2; }
                set
                {
                    if (podr2 == value)
                        return;
                    podr2 = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Podr2"));
                }
            }
            public string Podr3
            {
                get { return podr3; }
                set
                {
                    if (podr3 == value)
                        return;
                    podr3 = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Podr3"));
                }
            }
            public string Podr4
            {
                get { return podr4; }
                set
                {
                    if (podr4 == value)
                        return;
                    podr4 = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Podr4"));
                }
            }
            public string Dolgnost
            {
                get { return dolgnost; }
                set
                {
                    if (dolgnost == value)
                        return;
                    dolgnost = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Dolgnost"));
                }
            }
            public string Shtat_type
            {
                get { return shtat_type; }
                set
                {
                    if (shtat_type == value)
                        return;
                    shtat_type = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Shtat_type"));
                }
            }
            public string Vus
            {
                get { return vus; }
                set
                {
                    if (vus == value)
                        return;
                    vus = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Vus"));
                }
            }
            public string Kod
            {
                get { return kod; }
                set
                {
                    if (kod == value)
                        return;
                    kod = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Kod"));
                }
            }
            #endregion
            #region конструктора (для сборки)
            public string Zvanie
            {
                get { return zvanie; }
                set
                {
                    if (zvanie == value)
                        return;
                    zvanie = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Zvanie"));
                }
            }
            public string Fio
            {
                get { return fio; }
                set
                {
                    if (fio == value)
                        return;
                    fio = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Fio"));
                }
            }
            public string Types
            {
                get { return types; }
                set
                {
                    if (types == value)
                        return;
                    types = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Types"));
                }
            }
            public string Otryv_status
            {
                get { return otryv_status; }
                set
                {
                    if (otryv_status == value)
                        return;
                    otryv_status = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Otryv_status"));
                }
            }
            public string Otryv_primech
            {
                get { return otryv_primech; }
                set
                {
                    if (otryv_primech == value)
                        return;
                    otryv_primech = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Otryv_primech"));
                }
            }
            public string Otryv_pricaz
            {
                get { return otryv_pricaz; }
                set
                {
                    if (otryv_pricaz == value)
                        return;
                    otryv_pricaz = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Otryv_pricaz"));
                }
            }
            public int Id
            {
                get { return id; }
                set
                {
                    if (id == value)
                        return;
                    id = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                }
            }
            #endregion
            public string OpenLoad1()
            {
                string Message;
                ObservableCollection<OpenLoad> List = new ObservableCollection<OpenLoad>();
                List = sbor();
                Message = Mass_shtat(List);
                //MessageBox.Show(Message);
                return Message;

            }
            public ObservableCollection<OpenLoad> sbor()
            {
                string filename = Environment.CurrentDirectory + "\\Recources\\Shtat\\51460.xml";
                XDocument doc = XDocument.Load(filename);
                foreach (XElement el in doc.Root.Elements("track"))
                {
                    Shtat_List.Add(new OpenLoad(el));
                }
                foreach (OpenLoad P in Shtat_List)
                {
                    if (P.Lnumber != "0")
                    {
                        if (File.Exists(Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + P.Lnumber + ".xml") == true)
                        {
                            filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + P.Lnumber + ".xml";
                            doc = XDocument.Load(filename);
                            foreach (XElement el in doc.Root.Elements("track_dan"))
                            {
                                P.fio = el.Attribute("fio").Value;
                                P.types = el.Attribute("types").Value;
                            }
                            foreach (XElement el in doc.Root.Elements("track_zv"))
                            {
                                P.Zvanie = el.Attribute("name").Value;
                            }
                            P.Otryv_pricaz = "0";
                            P.Otryv_primech = "0";
                            P.Otryv_status = "0";
                            foreach (XElement el in doc.Root.Elements("track_otr"))
                            {
                                if (el.Attribute("data_preb").Value == "0")
                                {
                                    P.Otryv_pricaz = el.Attribute("pr").Value;
                                    P.Otryv_primech = el.Attribute("mesto").Value + "_с_" + el.Attribute("s_data").Value + "_" + el.Attribute("cel_").Value;
                                    P.Otryv_status = el.Attribute("types_").Value;
                                }
                                else
                                {
                                    P.Otryv_pricaz = "0";
                                    P.Otryv_primech = "0";
                                    P.Otryv_status = "0";
                                }
                            }
                        }
                        else
                        if (File.Exists(Environment.CurrentDirectory + "\\Recources\\Archive\\LD\\" + P.Lnumber + ".xml") == true)
                        {
                            File.Move(Environment.CurrentDirectory + "\\Recources\\Archive\\LD\\" + P.Lnumber + ".xml", Environment.CurrentDirectory + "\\Recources\\Active\\LD\\");
                            filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + P.Lnumber + ".xml";
                            doc = XDocument.Load(filename);
                            foreach (XElement el in doc.Root.Elements("track_dan"))
                            {
                                P.fio = el.Attribute("fio").Value;
                                P.types = el.Attribute("types").Value;
                            }
                            foreach (XElement el in doc.Root.Elements("track_zv"))
                            {
                                P.Zvanie = el.Attribute("name").Value;
                            }
                            foreach (XElement el in doc.Root.Elements("track_otr"))
                            {
                                if (el.Attribute("data_preb").Value == "0")
                                {
                                    P.Otryv_pricaz = el.Attribute("pr").Value;
                                    P.Otryv_primech = el.Attribute("mesto").Value + "_с_" + el.Attribute("s_data").Value + "_" + el.Attribute("cel_").Value;
                                    P.Otryv_status = el.Attribute("types_").Value;
                                }
                                else
                                {
                                    P.Otryv_pricaz = "0";
                                    P.Otryv_primech = "0";
                                    P.Otryv_status = "0";
                                }
                            }
                        }
                    }
                    else
                    {
                        P.fio = "-В-";
                        P.types = "0";
                        P.zvanie = "0";
                        P.Otryv_pricaz = "0";
                        P.Otryv_primech = "0";
                        P.Otryv_status = "0";
                    }

                }
                return Shtat_List;
            }
            public string Mass_shtat(ObservableCollection<OpenLoad> List)
            {
                string s = "";
                foreach (OpenLoad P in List)
                {
                    s = s + "|" + P.id + "|" + P.Podr + "|" + P.dolgnost + "|" + P.Shtat_type + "|" + P.Vus + "|" + P.Kod + "|" + P.lnumber + "|" + P.Zvanie + "|" + P.Fio + "|" + P.Types + "|" + P.Otryv_status + "|" + P.Otryv_primech + "|" + P.Otryv_pricaz;
                }
                //shtat = Encoding.Default.GetBytes(s);
                return s;
            }
            public OpenLoad Search(int num)
            {
                ObservableCollection<OpenLoad> List = new ObservableCollection<OpenLoad>();
                List = sbor();
                foreach (OpenLoad P in List)
                {
                    if (P.id == num)
                    {
                        return P;
                    }
                }
                OpenLoad Pi = new OpenLoad();
                return Pi;
            }
        }//штат
        public class OpenLoadZaShtat : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public OpenLoadZaShtat() { }
            public string ColToString()
            {
                Sbor_AnShtat();
                string S = "";
                foreach (OpenLoadZaShtat N in AnShtat_List)
                {
                    S = S + "|" + N.Lnumber + "|" + N.FIO + "|" + N.Types + "|" + N.Zvanie + "|" + N.Last_dolg + "|" + N.Last_podr;
                }
                return S;
            }
            ObservableCollection<OpenLoadZaShtat> AnShtat_List = new ObservableCollection<OpenLoadZaShtat>();
            int id = 0;
            private string lnumber = "";
            private string fio = "";
            private string zvanie = "";
            private string types = "";
            private string last_podr = ".";
            private string last_dolg = ".";
            public int Id
            {
                get { return id; }
                set
                {
                    if (id == value)
                        return;
                    id = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                }
            }
            public string Lnumber
            {
                get { return lnumber; }
                set
                {
                    if (lnumber == value)
                        return;
                    lnumber = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Lnumber"));
                }
            }
            public string Last_podr
            {
                get { return last_podr; }
                set
                {
                    if (last_podr == value)
                        return;
                    last_podr = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Last_podr"));
                }
            }
            public string Last_dolg
            {
                get { return last_dolg; }
                set
                {
                    if (last_dolg == value)
                        return;
                    last_dolg = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Last_dolg"));
                }
            }
            public string FIO
            {
                get { return fio; }
                set
                {
                    if (fio == value)
                        return;
                    fio = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("FIO"));
                }
            }
            public string Zvanie
            {
                get { return zvanie; }
                set
                {
                    if (zvanie == value)
                        return;
                    zvanie = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Zvanie"));
                }
            }
            public string Types
            {
                get { return types; }
                set
                {
                    if (types == value)
                        return;
                    types = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Types"));
                }
            }
            public OpenLoadZaShtat(XElement element)
            {
                Lnumber = element.Attribute("lnumber").Value;
                FIO = element.Attribute("fio").Value;
                Zvanie = element.Attribute("tek_zvanie").Value;
                Types = element.Attribute("type").Value;
                Last_dolg = element.Attribute("last_dolg").Value;
                Last_podr = element.Attribute("last_podr").Value;
            }
            public ObservableCollection<OpenLoadZaShtat> Sbor_AnShtat()
            {
                string filename = Environment.CurrentDirectory + "\\Recources\\AnShtat\\51460.xml";
                XDocument doc = XDocument.Load(filename);
                foreach (XElement el in doc.Root.Elements("track"))
                {
                    AnShtat_List.Add(new OpenLoadZaShtat(el));
                }
                return AnShtat_List;
            }
            public void RemovePeople()
            {
                string fileName = Environment.CurrentDirectory + "\\Recources\\AnShtat\\51460.xml";
                XDocument doc;
                doc = XDocument.Load(fileName);
                doc.Descendants().Where(e => e.Name == Lnumber).Remove();
                doc.Save(fileName);
            }
            public void ToXml()
            {
                string fileName = Environment.CurrentDirectory + "\\Recources\\AnShtat\\51460.xml";
                XDocument doc;
                doc = XDocument.Load(fileName);
                XElement track = new XElement("track");
                track.Add(new XAttribute("lnumber", Lnumber));
                track.Add(new XAttribute("fio", FIO));
                track.Add(new XAttribute("tek_zvanie", Zvanie));
                track.Add(new XAttribute("type", Types));
                track.Add(new XAttribute("last_dolg", Last_dolg));
                track.Add(new XAttribute("last_podr", Last_podr));
                doc.Root.Add(track);
                doc.Save(fileName);
            }

        }//Загрузка заштатников
        public class OpenLoadDel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public OpenLoadDel() { }
            public string ColToString()
            {
                Sbor_Del();
                string S = "";
                foreach (OpenLoadDel N in Del_List)
                {
                    S = S + "|" + N.Lnumber + "|" + N.FIO + "|" + N.Types + "|" + N.Zvanie + "|" + N.Date;
                }
                return S;
            }
            ObservableCollection<OpenLoadDel> Del_List = new ObservableCollection<OpenLoadDel>();
            private string lnumber = "";
            private string fio = "";
            private string zvanie = "";
            private string types = "";
            private string date_ = "";

            public string Lnumber
            {
                get { return lnumber; }
                set
                {
                    if (lnumber == value)
                        return;
                    lnumber = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Lnumber"));
                }
            }
            public string FIO
            {
                get { return fio; }
                set
                {
                    if (fio == value)
                        return;
                    fio = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("FIO"));
                }
            }
            public string Zvanie
            {
                get { return zvanie; }
                set
                {
                    if (zvanie == value)
                        return;
                    zvanie = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Zvanie"));
                }
            }
            public string Types
            {
                get { return types; }
                set
                {
                    if (types == value)
                        return;
                    types = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Types"));
                }
            }
            public string Date
            {
                get { return date_; }
                set
                {
                    if (date_ == value)
                        return;
                    date_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Date"));
                }
            }
            public OpenLoadDel(XElement element)
            {
                Lnumber = element.Attribute("lnumber").Value;
                FIO = element.Attribute("fio").Value;
                Zvanie = element.Attribute("tek_zvanie").Value;
                Types = element.Attribute("type").Value;
                Date = element.Attribute("date_").Value;
            }
            public ObservableCollection<OpenLoadDel> Sbor_Del()
            {
                string filename = Environment.CurrentDirectory + "\\Recources\\idp\\d\\51460.xml";
                XDocument doc = XDocument.Load(filename);
                foreach (XElement el in doc.Root.Elements("track"))
                {
                    Del_List.Add(new OpenLoadDel(el));
                }
                return Del_List;
            }
        }//Загрузка уволенных
        public class OpenLoadIskl : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public OpenLoadIskl() { }
            public string ColToString()
            {
                Sbor_Del();
                string S = "";
                foreach (OpenLoadIskl N in Iskl_List)
                {
                    S = S + "|" + N.Lnumber + "|" + N.FIO + "|" + N.Types + "|" + N.Zvanie + "|" + N.Date;
                }
                return S;
            }
            ObservableCollection<OpenLoadIskl> Iskl_List = new ObservableCollection<OpenLoadIskl>();
            private string lnumber = "";
            private string fio = "";
            private string zvanie = "";
            private string types = "";
            private string date_ = "";

            public string Lnumber
            {
                get { return lnumber; }
                set
                {
                    if (lnumber == value)
                        return;
                    lnumber = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Lnumber"));
                }
            }
            public string FIO
            {
                get { return fio; }
                set
                {
                    if (fio == value)
                        return;
                    fio = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("FIO"));
                }
            }
            public string Zvanie
            {
                get { return zvanie; }
                set
                {
                    if (zvanie == value)
                        return;
                    zvanie = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Zvanie"));
                }
            }
            public string Types
            {
                get { return types; }
                set
                {
                    if (types == value)
                        return;
                    types = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Types"));
                }
            }
            public string Date
            {
                get { return date_; }
                set
                {
                    if (date_ == value)
                        return;
                    date_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Date"));
                }
            }
            public OpenLoadIskl(XElement element)
            {
                Lnumber = element.Attribute("lnumber").Value;
                FIO = element.Attribute("fio").Value;
                Zvanie = element.Attribute("tek_zvanie").Value;
                Types = element.Attribute("type").Value;
                Date = element.Attribute("date_").Value;
            }
            public ObservableCollection<OpenLoadIskl> Sbor_Del()
            {
                string filename = Environment.CurrentDirectory + "\\Recources\\idp\\i\\51460.xml";
                XDocument doc = XDocument.Load(filename);
                foreach (XElement el in doc.Root.Elements("track"))
                {
                    Iskl_List.Add(new OpenLoadIskl(el));
                }
                return Iskl_List;
            }
        }//Загрузка исключенных
        public class OpenLoadPerevod : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public OpenLoadPerevod() { }
            public string ColToString()
            {
                Sbor_Del();
                string S = "";
                foreach (OpenLoadPerevod N in Perev_List)
                {
                    S = S + "|" + N.Lnumber + "|" + N.FIO + "|" + N.Types + "|" + N.Zvanie + "|" + N.Date + "|" + "|";
                }
                return S;
            }
            ObservableCollection<OpenLoadPerevod> Perev_List = new ObservableCollection<OpenLoadPerevod>();
            private string lnumber = "";
            private string fio = "";
            private string zvanie = "";
            private string types = "";
            private string date_ = "";
            private string dolg_ = "";
            private string podr_ = "";
            public string Dolg
            {
                get { return dolg_; }
                set
                {
                    if (dolg_ == value)
                        return;
                    dolg_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Dolg"));
                }
            }
            public string Podr
            {
                get { return podr_; }
                set
                {
                    if (podr_ == value)
                        return;
                    podr_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Podr"));
                }
            }
            public string Lnumber
            {
                get { return lnumber; }
                set
                {
                    if (lnumber == value)
                        return;
                    lnumber = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Lnumber"));
                }
            }
            public string FIO
            {
                get { return fio; }
                set
                {
                    if (fio == value)
                        return;
                    fio = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("FIO"));
                }
            }
            public string Zvanie
            {
                get { return zvanie; }
                set
                {
                    if (zvanie == value)
                        return;
                    zvanie = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Zvanie"));
                }
            }
            public string Types
            {
                get { return types; }
                set
                {
                    if (types == value)
                        return;
                    types = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Types"));
                }
            }
            public string Date
            {
                get { return date_; }
                set
                {
                    if (date_ == value)
                        return;
                    date_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Date"));
                }
            }
            public OpenLoadPerevod(XElement element)
            {
                Lnumber = element.Attribute("lnumber").Value;
                FIO = element.Attribute("fio").Value;
                Zvanie = element.Attribute("tek_zvanie").Value;
                Types = element.Attribute("type").Value;
                Date = element.Attribute("date_").Value;
            }
            public ObservableCollection<OpenLoadPerevod> Sbor_Del()
            {
                string filename = Environment.CurrentDirectory + "\\Recources\\idp\\p\\51460.xml";
                XDocument doc = XDocument.Load(filename);
                foreach (XElement el in doc.Root.Elements("track"))
                {
                    Perev_List.Add(new OpenLoadPerevod(el));
                }
                return Perev_List;
            }
        }//Загрузка переведенных
        public class Naryad
        {
            int id_ = 0;
            string type_naryad = ".";
            string podr = ".";
            string dolg = ".";
            string lnumber = ".";
            string zvanie = ".";
            string fio = ".";
            string types = ".";
            string date1 = ".";
            string key = ".";
            public string Type_naryad
            {
                get { return type_naryad; }
                set
                {
                    if (type_naryad == value)
                        return;
                    type_naryad = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Types_naryad"));
                }
            }
            public string Podr
            {
                get { return podr; }
                set
                {
                    if (podr == value)
                        return;
                    podr = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Podr"));
                }
            }
            public string Dolg
            {
                get { return dolg; }
                set
                {
                    if (dolg == value)
                        return;
                    dolg = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Dolg"));
                }
            }
            public string Key
            {
                get { return key; }
                set
                {
                    if (key == value)
                        return;
                    key = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Key"));
                }
            }
            public string Lnumber
            {
                get { return lnumber; }
                set
                {
                    if (lnumber == value)
                        return;
                    lnumber = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Lnumber"));
                }
            }
            public string Zvanie
            {
                get { return zvanie; }
                set
                {
                    if (zvanie == value)
                        return;
                    zvanie = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Zvanie"));
                }
            }
            public string FIO
            {
                get { return fio; }
                set
                {
                    if (fio == value)
                        return;
                    fio = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("FIO"));
                }
            }
            public string Types
            {
                get { return types; }
                set
                {
                    if (types == value)
                        return;
                    types = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Types"));
                }
            }
            public string Date
            {
                get { return date1; }
                set
                {
                    if (date1 == value)
                        return;
                    date1 = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Date"));
                }
            }
            public int Id
            {
                get { return id_; }
                set
                {
                    if (id_ == value)
                        return;
                    id_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;

        }
        public class Kotel : INotifyPropertyChanged
        {
            string podr_ = ".";
            int col_ = 0;
            public string Podr
            {
                get { return podr_; }
                set
                {
                    if (podr_ == value)
                        return;
                    podr_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Podr"));
                }
            }
            public int Col
            {
                get { return col_; }
                set
                {
                    if (col_ == value)
                        return;
                    col_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Col"));
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }//Котел
        public class OpenLoadKotel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            int id_ = 0;
            private string lnumber = ".";
            private string types = ".";
            private string podr = ".";
            private string fio = ".";
            ObservableCollection<OpenLoadKotel> Kotel_List = new ObservableCollection<OpenLoadKotel>();
            public string Lnumber
            {
                get { return lnumber; }
                set
                {
                    if (lnumber == value)
                        return;
                    lnumber = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Lnumber"));
                }
            }
            public int Id
            {
                get { return id_; }
                set
                {
                    if (Id == value)
                        return;
                    Id = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                }
            }
            public string Podr
            {
                get { return podr; }
                set
                {
                    if (podr == value)
                        return;
                    podr = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Podr"));
                }
            }
            public string FIO
            {
                get { return fio; }
                set
                {
                    if (fio == value)
                        return;
                    fio = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("FIO"));
                }
            }
            public string Types
            {
                get { return types; }
                set
                {
                    if (types == value)
                        return;
                    types = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Types"));
                }
            }
            public OpenLoadKotel()
            {

            }
            public OpenLoadKotel(XElement element)
            {
                Lnumber = element.Attribute("lnumber").Value;
            }
            public ObservableCollection<OpenLoadKotel> Sbor_Del()
            {
                string filename = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel.xml";
                XDocument doc = XDocument.Load(filename);
                foreach (XElement el in doc.Root.Elements("track"))
                {
                    Kotel_List.Add(new OpenLoadKotel(el));
                }
                return Kotel_List;
            }


        }//Загрузка Котел
        public class Perest : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            string lnumber_ = ".";
            string fio_ = ".";
            string zvanie_ = ".";
            string types_ = ".";
            string oldPodr_ = ".";
            string oldDolg_ = ".";
            string newDolg_ = ".";
            string newPodr_ = ".";
            string vus_ = ".";
            string kod_ = ".";

            public string Lnumber
            {
                get { return lnumber_; }
                set
                {
                    if (lnumber_ == value)
                        return;
                    lnumber_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Lnumber"));
                }
            }
            public string Zvanie
            {
                get { return zvanie_; }
                set
                {
                    if (zvanie_ == value)
                        return;
                    zvanie_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Zvanie"));
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
            public string Types
            {
                get { return types_; }
                set
                {
                    if (types_ == value)
                        return;
                    types_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Types"));
                }
            }
            public string Old_podr
            {
                get { return oldPodr_; }
                set
                {
                    if (oldPodr_ == value)
                        return;
                    oldPodr_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Old_podr"));
                }
            }
            public string Old_dolg
            {
                get { return oldDolg_; }
                set
                {
                    if (oldDolg_ == value)
                        return;
                    oldDolg_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Old_dolg"));
                }
            }
            public string New_dolg
            {
                get { return newDolg_; }
                set
                {
                    if (newDolg_ == value)
                        return;
                    newDolg_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("New_dolg"));
                }
            }
            public string New_podr
            {
                get { return newPodr_; }
                set
                {
                    if (newPodr_ == value)
                        return;
                    newPodr_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("New_podr"));
                }
            }
            public string VUS
            {
                get { return vus_; }
                set
                {
                    if (vus_ == value)
                        return;
                    vus_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("VUS"));
                }
            }
            public string Kod
            {
                get { return kod_; }
                set
                {
                    if (kod_ == value)
                        return;
                    kod_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Kod"));
                }
            }
        }// Перестановочка
        class OpenLoadArhive
        {

        } //Загрузка архив
        class OpenLoadDouble
        {

        }//Загрузка Двойники
        class OpenLoadNaryad
        {

        }//Загрузка Наряд
        class OpenLoadDop
        {

        }//Загрузка Доп Документы
        class OpenLoadPer
        {

        }//Загрузка Перечень
        public class Otbor_na_kontr : INotifyPropertyChanged
        {
            public Otbor_na_kontr()
            { }
            public Otbor_na_kontr(XElement element)
            {
                Podr = element.Attribute("podr").Value;
                Dolg = element.Attribute("dolg").Value;
                Lnumber = element.Attribute("ln").Value;
                FIO = element.Attribute("fio").Value;
                Types = element.Attribute("per").Value;
                Zvanie = element.Attribute("zv").Value;
                Date_ic = element.Attribute("date_ic").Value;
                Date1 = element.Attribute("date1").Value;
                Date2 = element.Attribute("date2").Value;
                Date3 = element.Attribute("date3").Value;

                Date4 = element.Attribute("date4").Value;
                Primec = element.Attribute("prim").Value;
                if(element.Attribute("rap").Value == "true") Raport = true; else if (element.Attribute("rap").Value == "false") Raport = true;
                if (element.Attribute("prof").Value == "true") Prof = true; else if (element.Attribute("prof").Value == "false") Prof = true;
                if (element.Attribute("bla").Value == "true") Blank = true; else if (element.Attribute("bla").Value == "false") Blank = true;
                if (element.Attribute("fiz").Value == "true") FIZO = true; else if (element.Attribute("fiz").Value == "false") FIZO = true;
                if (element.Attribute("vvk").Value == "true") Vvk = true; else if (element.Attribute("vvk").Value == "false") Vvk = true;
                if (element.Attribute("ic").Value == "true") Ic = true; else if (element.Attribute("ic").Value == "false") Ic = true;
            }
            public event PropertyChangedEventHandler PropertyChanged;
            string podr_ = ".";
            string dolg_ = ".";
            string ln_ = ".";
            string fio_ = ".";
            string per_ = ".";
            string zv_ = ".";
            string date_ic = ".";
            string date1 = "."; //ДАта отбора
            string date2 = ".";//Дата передачи документов в кадры
            string date3 = "."; // Дата приказа
            string date4 = "."; // Дата отказа
            string prim_ = ".";

            bool rap_ = false;
            bool prof_ = false;
            bool bla_ = false;
            bool fiz_ = false;
            bool vvk_ = false;
            bool ic_ = false;
            string message = ".";
            public string D1()
            {
                ObservableCollection<Otbor_na_kontr> Ot = new ObservableCollection<Otbor_na_kontr>();
                string filename = Environment.CurrentDirectory + "\\Recources\\nk\\Otob.xml"; //Отобранные
                XDocument doc = XDocument.Load(filename);
                foreach (XElement el in doc.Root.Elements("track"))
                {
                    Ot.Add(new Otbor_na_kontr(el));
                }
                message = "O1";
                foreach(Otbor_na_kontr i in Ot)
                {
                    message += "|" + i.Blank + "|" + i.Date1 + "|" + i.Date2 + "|" + i.Date3 + "|" + i.Date4 + "|" + i.Date_ic + "|" + i.Dolg + "|" + i.FIO + "|" + i.FIZO + "|" + i.Ic + "|" + i.Lnumber + "|" + i.Podr + "|" + i.Primec + "|" + i.Prof + "|" + i.Raport + "|" + i.Types + "|" + i.Vvk + "|" + i.Zvanie;
                }
                Ot = new ObservableCollection<Otbor_na_kontr>();
                filename = Environment.CurrentDirectory + "\\Recources\\nk\\Pered.xml"; //Переданные
                doc = XDocument.Load(filename);
                foreach (XElement el in doc.Root.Elements("track"))
                {
                    Ot.Add(new Otbor_na_kontr(el));
                }
                message += "|O2";
                foreach (Otbor_na_kontr i in Ot)
                {
                    message += "|" + i.Blank + "|" + i.Date1 + "|" + i.Date2 + "|" + i.Date3 + "|" + i.Date4 + "|" + i.Date_ic + "|" + i.Dolg + "|" + i.FIO + "|" + i.FIZO + "|" + i.Ic + "|" + i.Lnumber + "|" + i.Podr + "|" + i.Primec + "|" + i.Prof + "|" + i.Raport + "|" + i.Types + "|" + i.Vvk + "|" + i.Zvanie;
                }
                Ot = new ObservableCollection<Otbor_na_kontr>();
                filename = Environment.CurrentDirectory + "\\Recources\\nk\\Sostoy.xml"; //Состоялись
                doc = XDocument.Load(filename);
                foreach (XElement el in doc.Root.Elements("track"))
                {
                    Ot.Add(new Otbor_na_kontr(el));
                }
                message += "|O3";
                foreach (Otbor_na_kontr i in Ot)
                {
                    message += "|" + i.Blank + "|" + i.Date1 + "|" + i.Date2 + "|" + i.Date3 + "|" + i.Date4 + "|" + i.Date_ic + "|" + i.Dolg + "|" + i.FIO + "|" + i.FIZO + "|" + i.Ic + "|" + i.Lnumber + "|" + i.Podr + "|" + i.Primec + "|" + i.Prof + "|" + i.Raport + "|" + i.Types + "|" + i.Vvk + "|" + i.Zvanie;
                }
                Ot = new ObservableCollection<Otbor_na_kontr>();
                filename = Environment.CurrentDirectory + "\\Recources\\nk\\Otkaz.xml"; //Отказ
                doc = XDocument.Load(filename);
                foreach (XElement el in doc.Root.Elements("track"))
                {
                    Ot.Add(new Otbor_na_kontr(el));
                }
                message += "|O4";
                foreach (Otbor_na_kontr i in Ot)
                {
                    message += "|" + i.Blank + "|" + i.Date1 + "|" + i.Date2 + "|" + i.Date3 + "|" + i.Date4 + "|" + i.Date_ic + "|" + i.Dolg + "|" + i.FIO + "|" + i.FIZO + "|" + i.Ic + "|" + i.Lnumber + "|" + i.Podr + "|" + i.Primec + "|" + i.Prof + "|" + i.Raport + "|" + i.Types + "|" + i.Vvk + "|" + i.Zvanie;
                }
                return message;
            }
            public string Podr
            {
                get { return podr_; }
                set
                {
                    if (podr_ == value)
                        return;
                    podr_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Podr"));
                }
            }
            public string Dolg
            {
                get { return dolg_; }
                set
                {
                    if (dolg_ == value)
                        return;
                    dolg_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Dolg"));
                }
            }
            public string Lnumber
            {
                get { return ln_; }
                set
                {
                    if (ln_ == value)
                        return;
                    ln_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Lnumber"));
                }
            }
            public string Zvanie
            {
                get { return zv_; }
                set
                {
                    if (zv_ == value)
                        return;
                    zv_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Zvanie"));
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
            public string Types
            {
                get { return per_; }
                set
                {
                    if (per_ == value)
                        return;
                    per_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Types"));
                }
            }
            public string Date_ic
            {
                get { return date_ic; }
                set
                {
                    if (date_ic == value)
                        return;
                    date_ic = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Date_ic"));
                }
            }
            public string Date1
            {
                get { return date1; }
                set
                {
                    if (date1 == value)
                        return;
                    date1 = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Date1"));
                }
            }
            public string Date2
            {
                get { return date2; }
                set
                {
                    if (date2 == value)
                        return;
                    date2 = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Date2"));
                }
            }
            public string Date3
            {
                get { return date3; }
                set
                {
                    if (date3 == value)
                        return;
                    date3 = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Date3"));
                }
            }
            public string Date4
            {
                get { return date4; }
                set
                {
                    if (date4 == value)
                        return;
                    date4 = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Date4"));
                }
            }
            public string Primec
            {
                get { return prim_; }
                set
                {
                    if (prim_ == value)
                        return;
                    prim_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Primec"));
                }
            }
            public bool Ic
            {
                get { return ic_; }
                set
                {
                    if (ic_ == value)
                        return;
                    ic_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Ic"));
                }
            }
            public bool Vvk
            {
                get { return vvk_; }
                set
                {
                    if (vvk_ == value)
                        return;
                    vvk_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Vvk"));
                }
            }
            public bool Prof
            {
                get { return prof_; }
                set
                {
                    if (prof_ == value)
                        return;
                    prof_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Prof"));
                }
            }
            public bool Blank
            {
                get { return bla_; }
                set
                {
                    if (bla_ == value)
                        return;
                    bla_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Blank"));
                }
            }
            public bool FIZO
            {
                get { return fiz_; }
                set
                {
                    if (fiz_ == value)
                        return;
                    fiz_ = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Fizo"));
                }
            }
            public bool Raport
            {
                get { return rap_; }
                set
                {
                    if (rap_ == value)
                        return;
                    rap_ = value;
                    MessageBox.Show(rap_.ToString());
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Raport"));
                }
            }
        }//Загрузка о должности

    }
    public class Ub : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string dolg_ = ".";
        string podr_ = ".";

        string lnumber_ = ".";
        string fio_ = ".";
        string zvanie_ = ".";
        string types_1 = ".";

        string types_2 = ".";
        string mesto_ = ".";
        string cel_ = ".";
        string date1_ = ".";
        string date2_ = ".";
        string date3_ = ".";

        string osn_ = ".";
        string vrio_ = ".";

        string vpd_st1 = ".";
        string vpd_st2 = ".";
        string vpd_st3 = ".";
        string vpd_st4 = ".";
        string vpd_st5 = ".";
        string vpd_st6 = ".";
        string vpd_st7 = ".";
        public string Osn
        {
            get { return osn_; }
            set
            {
                if (osn_ == value)
                    return;
                osn_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Osn"));
            }
        }
        public string Vrio
        {
            get { return vrio_; }
            set
            {
                if (vrio_ == value)
                    return;
                vrio_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Vrio"));
            }
        }
        public string Dolg
        {
            get { return dolg_; }
            set
            {
                if (dolg_ == value)
                    return;
                dolg_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Dolg"));
            }
        }
        public string Podr
        {
            get { return podr_; }
            set
            {
                if (podr_ == value)
                    return;
                podr_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Podr"));
            }
        }
        public string Lnumber
        {
            get { return lnumber_; }
            set
            {
                if (lnumber_ == value)
                    return;
                lnumber_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Lnumber"));
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
        public string Zvanie
        {
            get { return zvanie_; }
            set
            {
                if (zvanie_ == value)
                    return;
                zvanie_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Zvanie"));
            }
        }
        public string Types
        {
            get { return types_1; }
            set
            {
                if (types_1 == value)
                    return;
                types_1 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Types"));
            }
        }
        public string Cel
        {
            get { return cel_; }
            set
            {
                if (cel_ == value)
                    return;
                cel_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Cel"));
            }
        }
        public string Mesto
        {
            get { return mesto_; }
            set
            {
                if (mesto_ == value)
                    return;
                mesto_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Mesto"));
            }
        }
        public string Date_ub
        {
            get { return date1_; }
            set
            {
                if (date1_ == value)
                    return;
                date1_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date_ub"));
            }
        }
        public string Date_preb
        {
            get { return date2_; }
            set
            {
                if (date2_ == value)
                    return;
                date2_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date_preb"));
            }
        }
        public string Date3
        {
            get { return date3_; }
            set
            {
                if (date3_ == value)
                    return;
                date3_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date3"));
            }
        }
        public string Types_ub
        {
            get { return types_2; }
            set
            {
                if (types_2 == value)
                    return;
                types_2 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Types_ub"));
            }
        }


        public string VPD_ST1
        {
            get { return vpd_st1; }
            set
            {
                if (vpd_st1 == value)
                    return;
                vpd_st1 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("VPD_ST1"));
            }
        }
        public string VPD_ST2
        {
            get { return vpd_st2; }
            set
            {
                if (vpd_st2 == value)
                    return;
                vpd_st2 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("VPD_ST2"));
            }
        }
        public string VPD_ST3
        {
            get { return vpd_st3; }
            set
            {
                if (vpd_st3 == value)
                    return;
                vpd_st3 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("VPD_ST3"));
            }
        }
        public string VPD_ST4
        {
            get { return vpd_st4; }
            set
            {
                if (vpd_st4 == value)
                    return;
                vpd_st4 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("VPD_ST4"));
            }
        }
        public string VPD_ST5
        {
            get { return vpd_st5; }
            set
            {
                if (vpd_st5 == value)
                    return;
                vpd_st5 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("VPD_ST5"));
            }
        }
        public string VPD_ST6
        {
            get { return vpd_st6; }
            set
            {
                if (vpd_st6 == value)
                    return;
                vpd_st6 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("VPD_ST6"));
            }
        }
        public string VPD_ST7
        {
            get { return vpd_st7; }
            set
            {
                if (vpd_st7 == value)
                    return;
                vpd_st7 = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("VPD_ST7"));
            }
        }
    }
    class CheckedListItem<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isChecked;
        private T item;

        public CheckedListItem()
        { }

        public CheckedListItem(T item, bool isChecked = false)
        {
            this.item = item;
            this.isChecked = isChecked;
        }

        public T Item
        {
            get { return item; }
            set
            {
                item = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Item"));
            }
        }


        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
            }
        }
    }

    public class List_new
    {
        List<string> list_Peres = new List<string>();
        List<string> list_Del = new List<string>();
        List<string> list_Perev = new List<string>();
        List<string> list_Iskl = new List<string>();
        List<string> list_Naryad = new List<string>();
        List<string> list_Izmen = new List<string>();
        List<string> list_LD = new List<string>();

        public string message()
        {
            string message_ = "Lists|Peres*";
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory + "\\Recources\\Active\\peres");
            foreach (var item in dir.GetFiles())
            {
                list_Peres.Add(item.Name);
            }
            dir = new DirectoryInfo(Environment.CurrentDirectory + "\\Recources\\Active\\LD");
            foreach (var item in dir.GetFiles())
            {
                list_LD.Add(item.Name);
            }
            dir = new DirectoryInfo(Environment.CurrentDirectory + "\\Recources\\Active\\izm");
            foreach (var item in dir.GetFiles())
            {
                list_Izmen.Add(item.Name);
            }
            dir = new DirectoryInfo(Environment.CurrentDirectory + "\\Recources\\idp\\i");
            foreach (var item in dir.GetFiles())
            {
                list_Iskl.Add(item.Name);
            }
            dir = new DirectoryInfo(Environment.CurrentDirectory + "\\Recources\\idp\\d");
            foreach (var item in dir.GetFiles())
            {
                list_Del.Add(item.Name);
            }
            dir = new DirectoryInfo(Environment.CurrentDirectory + "\\Recources\\idp\\p");
            foreach (var item in dir.GetFiles())
            {
                list_Perev.Add(item.Name);
            }
            dir = new DirectoryInfo(Environment.CurrentDirectory + "\\Recources\\nd");
            foreach (var item in dir.GetFiles())
            {
                list_Naryad.Add(item.Name);
            }
            foreach (string i in list_Peres)
                message_ +=  i + ">";
            message_ += "LD*";
            foreach (string i in list_LD)
                message_ += i + ">";
            message_ += "Izm*";
            foreach (string i in list_Izmen)
                message_ += i + ">";
            message_ += "Iskl*";
            foreach (string i in list_Iskl)
                message_ += i + ">";
            message_ += "Del*";
            foreach (string i in list_Del)
                message_ += i + ">";
            message_ += "Perev*";
            foreach (string i in list_Perev)
                message_ += i + ">";
            message_ += "ND*";
            foreach (string i in list_Naryad)
                message_ += i + ">";
            return message_;
        }
    }
}
