
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace New_Stroevaya_chast.Modal
{
    public class LD
    {
        string Message = "";
        #region Открытие
        public string Load(string LNumber)
        {
            Message = "LD1>";
            if (Message_zv(LNumber) != "")
                Message += "Zvanie" + Message_zv(LNumber);
            if (Message_pos(LNumber) != "")
                Message += "|Poslug" + Message_pos(LNumber);
            if (Message_fm(LNumber) != "")
                Message += "|Fam" + Message_fm(LNumber);
            if (Message_ng(LNumber) != "")
                Message += "|Nagrad" + Message_ng(LNumber);
            if (Message_ph(LNumber) != "")
                Message += "|Foto" + Message_ph(LNumber);
            if (Message_docum(LNumber) != "")
                Message += "|Docum" + Message_docum(LNumber);
            if (Message_dan(LNumber) != "")
                Message += "|Dan" + Message_dan(LNumber);
            if (Message_kon(LNumber) != "")
                Message += "|Kontr" + Message_kon(LNumber);
            if (Message_nazn(LNumber) != "")
                Message += "|Naznac" + Message_nazn(LNumber);
            if (Message_dela(LNumber) != "")
                Message += "|Priem" + Message_dela(LNumber);
            if (Message_vizh(LNumber) != "")
                Message += "|Vizhiv" + Message_vizh(LNumber);
            if (Message_otr(LNumber) != "")
                Message += "|Otriv" + Message_otr(LNumber);
            return Message ;
        }
        #region Загрузка званий
        public string Message_zv(string ln)
        {
            ObservableCollection<zvanie> T = Sbor_Zv(ln);
            string s = "";
            foreach(zvanie p in T)
            {
                s += "|" + p.Name + "|" + p.Nomber + "|" + p.Pr + "|" + p.What + "|" + p.Date;
            }
            return s;
        }
        public ObservableCollection<zvanie> Sbor_Zv(string ln)
        {
            ObservableCollection<zvanie> T = new ObservableCollection<zvanie>();
            string filename = Environment.CurrentDirectory  +"\\Recources\\Active\\LD\\" + ln + ".xml";
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements("track_zv"))
            {
                T.Add(new zvanie(el));
            }
            return T;
        }
        #endregion
        #region Послужной
        public string Message_pos(string ln)
        {
            ObservableCollection<Poslug> T = Sbor_Pos(ln);
            string s = "";
            foreach (Poslug p in T)
            {
                s += "|" + p.Chast_ + "|" + p.Date_pricaz + "|" + p.Dolgnost_ + "|" + p.Name_pricaz + "|" + p.Nomber_pricaz + "|" + p.Podr_ + "|" + p.VUS_ ;
            }
            return s;
        }
        public ObservableCollection<Poslug> Sbor_Pos(string ln)
        {
            ObservableCollection<Poslug> T = new ObservableCollection<Poslug>();
            string filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + ln + ".xml";
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements("track_pos"))
            {
                T.Add(new Poslug(el));
            }
            return T;
        }
        #endregion
        #region Семья
        public string Message_fm(string ln)
        {
            ObservableCollection<family> T = Sbor_fam(ln);
            string s = "";
            foreach (family p in T)
            {
                s += "|"+ p.Name + "|" + p.types + "|" + p.Brd;
            }
            return s;
        }
        public ObservableCollection<family> Sbor_fam(string ln)
        {
            ObservableCollection<family> T = new ObservableCollection<family>();
            string filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + ln + ".xml";
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements("track_fm"))
            {
                T.Add(new family(el));
            }
            return T;
        }
        #endregion
        #region Награды
        public string Message_ng(string ln)
        {
            ObservableCollection<Nagrad> T = Sbor_ng(ln);
            string s = "";
            foreach (Nagrad p in T)
            {
                s += "|" + p.Type + "|" + p.Who_pricaz + "|" + p.Pricaz + "|" + p.Date_pricaz + "|" + p.Date_ ;
            }
            return s;
        }
        public ObservableCollection<Nagrad> Sbor_ng(string ln)
        {
            ObservableCollection<Nagrad> T = new ObservableCollection<Nagrad>();
            string filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + ln + ".xml";
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements("track_ng"))
            {
                T.Add(new Nagrad(el));
            }
            return T;
        }

        #endregion
        #region Фото
        public string Message_ph(string ln)
        {
            ObservableCollection<Photo_> T = Sbor_ph(ln);
            string s = "";
            foreach (Photo_ p in T)
            {
                s +="|Ph1*"+ p.Date1 + "|Ph2*" + p.Date2 + "|Ph3*" + p.Date3 + "|Ph4*" + p.Date4 + "|Ph5*" + p.Date5 + "|Ph6*"+p.Date6;
            }
            return s;
        }
        public ObservableCollection<Photo_> Sbor_ph(string ln)
        {
            ObservableCollection<Photo_> T = new ObservableCollection<Photo_>();
            string filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\ph\\" + ln + ".xml";
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements("track_ph"))
            {
                T.Add(new Photo_(el));
            }
            return T;
        }

        #endregion
        #region Документы
        public string Message_docum(string ln)
        {
            ObservableCollection<Docum> T = Sbor_docum(ln);
            string s = "";
            foreach (Docum p in T)
            {
                s += "|" + p.Kod + "|" + p.Seriya + "|" + p.Nomber + "|" + p.Who_vidal + "|" + p.Date_vid;
            }
            return s;
        }
        public ObservableCollection<Docum> Sbor_docum(string ln)
        {
            ObservableCollection<Docum> T = new ObservableCollection<Docum>();
            string filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + ln + ".xml";
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements("track_dm"))
            {
                T.Add(new Docum(el));
            }
            return T;
        }
        #endregion
        #region Данные
        public string Message_dan(string ln)
        {
            ObservableCollection<Dannie> T = Sbor_dn(ln);
            string s = "";
            foreach (Dannie p in T)
            {
                s += "|" + p.Lnumber + "|" + p.FIO + "|" + p.Bank_card + "|" + p.Date_brd + "|" + p.Gr_obr + "|"+ p.Home_adres+"|" + p.Mesto_brd + "|" + p.Nac + "|" + p.PAC + "|" + p.POL + "|" + p.Vod_prava + "|" + p.Voenkomat + "|" + p.Voen_obr + "|" + p.Types;
            }
            return s;
        }
        public ObservableCollection<Dannie> Sbor_dn(string ln)
        {
            ObservableCollection<Dannie> T = new ObservableCollection<Dannie>();
            string filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + ln + ".xml";
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements("track_dan"))
            {
                T.Add(new Dannie(el));
            }
            return T;
        }
        #endregion
        #region Контракт
        public string Message_kon(string ln)
        {
            ObservableCollection<Kontrakt> T = Sbor_kon(ln);
            string s = "";
            foreach (Kontrakt p in T)
            {
                s += "|" + p.data_zakl + "|" + p.data_okon + "|" + p.Date_pricaz + "|" + p.Nomber_pricaz + "|" + p.types + "|" + p.Who_pricaz;
            }
            return s;
        }
        public ObservableCollection<Kontrakt> Sbor_kon(string ln)
        {
            ObservableCollection<Kontrakt> T = new ObservableCollection<Kontrakt>();
            string filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + ln + ".xml";
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements("track_kon"))
            {
                T.Add(new Kontrakt(el));
            }
            return T;
        }
        #endregion
        #region Назначение
        public string Message_nazn(string ln)
        {
            ObservableCollection<Naznach> T = Sbor_nazn(ln);
            string s = "";
            foreach (Naznach p in T)
            {
                s += "|" + p.pricaz + "|" + p.who_pricaz + "|"  + p.date_pricaz;
            }
            return s;
        }
        public ObservableCollection<Naznach> Sbor_nazn(string ln)
        {
            ObservableCollection<Naznach> T = new ObservableCollection<Naznach>();
            string filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + ln + ".xml";
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements("track_naz"))
            {
                T.Add(new Naznach(el));
            }
            return T;
        }
        #endregion
        #region Прием дел
        public string Message_dela(string ln)
        {
            ObservableCollection<PriemDel> T = Sbor_dela(ln);
            string s = "";
            foreach (PriemDel p in T)
            {
                s += "|" + p.pricaz + "|" + p.who_pricaz + "|" + p.date_pricaz ;
            }
            return s;
        }
        public ObservableCollection<PriemDel> Sbor_dela(string ln)
        {
            ObservableCollection<PriemDel> T = new ObservableCollection<PriemDel>();
            string filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + ln + ".xml";
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements("track_pr_del"))
            {
                T.Add(new PriemDel(el));
            }
            return T;
        }
        #endregion
        #region Выживайка
        public string Message_vizh(string ln)
        {
            ObservableCollection<Vizhivanie> T = Sbor_vizh(ln);
            string s = "";
            foreach (Vizhivanie p in T)
            {
                s += "|" + p.Period + "|" + p.Mesto + "|" + p.Osnovanie;
            }
            return s;
        }
        public ObservableCollection<Vizhivanie> Sbor_vizh(string ln)
        {
            ObservableCollection<Vizhivanie> T = new ObservableCollection<Vizhivanie>();
            string filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + ln + ".xml";
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements("track_viz"))
            {
                T.Add(new Vizhivanie(el));
            }
            return T;
        }

        #endregion
        #region Отрыв
        public string Message_otr(string ln)
        {
            ObservableCollection<Otryv_LD> T = Sbor_otr(ln);
            string s = "";
            foreach (Otryv_LD p in T)
            {
                s += "|" + p.Types_ + "|" + p.S_data + "|" + p.Cel_ + "|" + p.Mesto + "|" + p.Osnovanie + "|"+p.Data_preb;
            }
            return s;
        }
        public ObservableCollection<Otryv_LD> Sbor_otr(string ln)
        {
            ObservableCollection<Otryv_LD> T = new ObservableCollection<Otryv_LD>();
            string filename = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + ln + ".xml";
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements("track_otr"))
            {
                T.Add(new Otryv_LD(el));
            }
            return T;
        }

        #endregion
        #endregion
        #region Сохранение
        public void Saveing_Zv(string LNumber, zvanie tmp)
        {
            tmp.ToXml(LNumber);
        }
        public void Saving_photo(string LNumber, Photo_ tmp)
        {
            tmp.ToXml(LNumber);
        }
        public void Saveing_Po(string LNumber, Poslug tmp)
        {
            tmp.ToXml(LNumber);
        }
        public void Saveing_Fm(string LNumber, family tmp)
        {
            tmp.ToXml(LNumber);
        }
        public void Saveing_Ng(string LNumber, Nagrad tmp)
        {
            tmp.ToXml(LNumber);
        }
        public void Saveing_Dc(string LNumber, Docum tmp)
        {
            tmp.ToXml(LNumber);
        }
        public void Saveing_Dn(string LNumber, Dannie tmp)
        {
            tmp.ToXml(LNumber);
        }
        public void Saveing_Kn(string LNumber, Kontrakt tmp)
        {
            tmp.ToXml(LNumber);
        }
        public void Saveing_Naz(string LNumber, Naznach tmp)
        {
            tmp.ToXml(LNumber);
        }
        public void Saveing_PD(string LNumber, PriemDel tmp)
        {
            tmp.ToXml(LNumber);
        }
        public void Saveing_Viz(string LNumber, Vizhivanie tmp)
        {
            tmp.ToXml(LNumber);
        }
        public void Saveing_OLD(string LNumber, Otryv_LD tmp)
        {
            tmp.ToXml(LNumber);
        }
        #endregion
    }
    public class zvanie : INotifyPropertyChanged
    {
        int nomber_ = 0;
        string name = ".";
        string date = ".";
        string what = ".";
        int pricaz = 0;

        public zvanie(){}
        public int Nomber
        {
            get { return nomber_; }
            set
            {
                if (nomber_ == value)
                    return;
                nomber_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Nomber"));
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (name == value)
                    return;
                name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public string Date
        {
            get { return date; }
            set
            {
                if (date == value)
                    return;
                date = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date"));
            }
        }
        public string What
        {
            get { return what; }
            set
            {
                if (what == value)
                    return;
                what = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public int Pr
        {
            get { return pricaz; }
            set
            {
                if (pricaz == value)
                    return;
                pricaz = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Pr"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public zvanie(XElement element)
        {
            if (element.Attribute("name").Value != null) Name = element.Attribute("name").Value;
            if (element.Attribute("date").Value != null) Date = element.Attribute("date").Value;
            if (element.Attribute("what").Value != null) What = element.Attribute("what").Value;

            if (int.Parse(element.Attribute("pr").Value) != 0)
            {
                Pr = int.Parse(element.Attribute("pr").Value);
            }
        }
        public void ToXml(string LOGIN)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + LOGIN + ".xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track_zv",
                        new XAttribute("name", Name),
                        new XAttribute("date", Date),
                        new XAttribute("what", What),
                        new XAttribute("pr", Pr))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                XElement track = new XElement("track_zv");
                track.Add(new XAttribute("name", Name));
                track.Add(new XAttribute("date", Date));
                track.Add(new XAttribute("what", What));
                track.Add(new XAttribute("pr", Pr));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }
    }//конец звания
    public class Poslug : INotifyPropertyChanged  /// Послужной лист
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string chast_ = "", podr_ = "", dolgnost_ = "", vus_ = "", name_pricaz_ = "", nomber_pricaz_ = "", date_pricaz_ = "";
        public Poslug() { }
        public Poslug(XElement element)
        {
            if (element.Attribute("chast_").Value != null) Chast_ = element.Attribute("chast_").Value;
            if (element.Attribute("podr_").Value != null) Podr_ = element.Attribute("podr_").Value;
            if (element.Attribute("dolgnost_").Value != null) Dolgnost_ = element.Attribute("dolgnost_").Value;
            if (element.Attribute("vus_").Value != null) VUS_ = element.Attribute("vus_").Value;
            if (element.Attribute("name_pricaz").Value != null) Name_pricaz = element.Attribute("name_pricaz").Value;
            if (element.Attribute("nomber_pricaz").Value != null) Nomber_pricaz = element.Attribute("nomber_pricaz").Value;
            if (element.Attribute("date_pricaz").Value != null) Date_pricaz = element.Attribute("date_pricaz").Value;
        }
        
        public string Chast_
        {
            get { return chast_; }
            set
            {
                if (chast_ == value)
                    return;
                chast_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("chast_"));
            }
        }
        public string Podr_
        {
            get { return podr_; }
            set
            {
                if (podr_ == value)
                    return;
                podr_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("podr_"));
            }
        }
        public string Dolgnost_
        {
            get { return dolgnost_; }
            set
            {
                if (dolgnost_ == value)
                    return;
                dolgnost_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("dolgnost_"));
            }
        }
        public string VUS_
        {
            get { return vus_; }
            set
            {
                if (vus_ == value)
                    return;
                vus_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("vus_"));
            }
        }
        public string Name_pricaz
        {
            get { return name_pricaz_; }
            set
            {
                if (name_pricaz_ == value)
                    return;
                name_pricaz_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("name_pricaz"));
            }
        }
        public string Nomber_pricaz
        {
            get { return nomber_pricaz_; }
            set
            {
                if (nomber_pricaz_ == value)
                    return;
                nomber_pricaz_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("nomber_pricaz"));
            }
        }
        public string Date_pricaz
        {
            get { return date_pricaz_; }
            set
            {
                if (date_pricaz_ == value)
                    return;
                date_pricaz_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("date_pricaz"));
            }
        }
        public void ToXml(string LOGIN)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + LOGIN + ".xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track_pos",
                        new XAttribute("chast_", Chast_),
                        new XAttribute("podr_", Podr_),
                        new XAttribute("dolg_", Dolgnost_),
                        new XAttribute("vus_", VUS_),
                        new XAttribute("name_pricaz", Name_pricaz),
                        new XAttribute("nomber_pricaz", Nomber_pricaz),
                        new XAttribute("date_pricaz", Date_pricaz))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                XElement track = new XElement("track_pos");
                track.Add(new XAttribute("chast_", Chast_));
                track.Add(new XAttribute("podr_", Podr_));
                track.Add(new XAttribute("dolg_", Dolgnost_));
                track.Add(new XAttribute("vus_", VUS_));
                track.Add(new XAttribute("name_pricaz", Name_pricaz));
                track.Add(new XAttribute("nomber_pricaz", Nomber_pricaz));
                track.Add(new XAttribute("date_pricaz", Date_pricaz));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }
        
    }//конец послужного
    public class family : INotifyPropertyChanged // Список семьи
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string name_ = ".";
        string types_ = ".";
        string brd_ = ".";
        public family() { }
        public family(XElement element)
        {
            if (element.Attribute("name").Value != null) Name = element.Attribute("name").Value;
            if (element.Attribute("type").Value != null) types = element.Attribute("type").Value;
            if (element.Attribute("brd").Value != null) Brd = element.Attribute("brd").Value;
        }
        public string Name
        {
            get { return name_; }
            set
            {
                if (name_ == value)
                    return;
                name_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public string types
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
        public string Brd
        {
            get { return brd_; }
            set
            {
                if (brd_ == value)
                    return;
                brd_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Brd"));
            }
        }
        public void ToXml(string LOGIN)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + LOGIN + ".xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track_fm",
                        new XAttribute("name", Name),
                        new XAttribute("type", types),
                        new XAttribute("brd", Brd))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                XElement track = new XElement("track_fm");
                track.Add(new XAttribute("name", Name));
                track.Add(new XAttribute("type", types));
                track.Add(new XAttribute("brd", Brd));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }
    }
    public class Nagrad: INotifyPropertyChanged // Наградной список
    {
        string types_ = ".";
        string date1_ = ".";
        string what_ = ".";
        string pr_ = ".";
        string date2_ = ".";
        public event PropertyChangedEventHandler PropertyChanged;
        public Nagrad() { }
        public Nagrad(XElement element)
        {
            if (element.Attribute("type").Value != null) Type = element.Attribute("type").Value;
            if (element.Attribute("date1_").Value != null) Date_ = element.Attribute("date_").Value;
            if (element.Attribute("what").Value != null) Who_pricaz = element.Attribute("what").Value;
            if (element.Attribute("pr").Value != null) Pricaz = element.Attribute("pr").Value;
            if (element.Attribute("date2_").Value != null) Date_pricaz = element.Attribute("date2_").Value;
        }
        public string Type
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
        public string Date_
        {
            get { return date1_; }
            set
            {
                if (date1_ == value)
                    return;
                date1_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date1"));
            }
        }
        public string Who_pricaz
        {
            get { return what_; }
            set
            {
                if (what_ == value)
                    return;
                what_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("What"));
            }
        }
        public string Pricaz
        {
            get { return pr_; }
            set
            {
                if (pr_ == value)
                    return;
                pr_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Pr"));
            }
        }
        public string Date_pricaz
        {
            get { return date2_; }
            set
            {
                if (date2_ == value)
                    return;
                date2_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date2"));
            }
        }
        public void ToXml(string LOGIN)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + LOGIN + ".xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track_ng",
                        new XAttribute("type", Type),
                        new XAttribute("date1_", Date_),
                        new XAttribute("what", Who_pricaz),
                        new XAttribute("pr", Pricaz),
                        new XAttribute("date2_", Date_pricaz))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                XElement track = new XElement("track_ng");
                track.Add(new XAttribute("type", Type));
                track.Add(new XAttribute("date1_", Date_));
                track.Add(new XAttribute("what", Who_pricaz));
                track.Add(new XAttribute("pr", Pricaz));
                track.Add(new XAttribute("date2_", Date_pricaz));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }
    }
    public class Photo_ //подгрузчик фотографий
    {
        BitmapImage photo1;
        BitmapImage photo2;
        BitmapImage photo3;
        BitmapImage photo4;
        BitmapImage photo5;
        BitmapImage photo6;
        public BitmapImage Date1
        {
            get { return photo1; }
            set
            {
                if (photo1 == value)
                    return;
                photo1 = value;
            }
        }
        public BitmapImage Date2
        {
            get { return photo2; }
            set
            {
                if (photo2 == value)
                    return;
                photo2 = value;
            }
        }
        public BitmapImage Date3
        {
            get { return photo3; }
            set
            {
                if (photo3 == value)
                    return;
                photo3 = value;
            }
        }
        public BitmapImage Date4
        {
            get { return photo4; }
            set
            {
                if (photo4 == value)
                    return;
                photo4 = value;

            }
        }
        public BitmapImage Date5
        {
            get { return photo5; }
            set
            {
                if (photo5 == value)
                    return;
                photo5 = value;

            }
        }
        public BitmapImage Date6
        {
            get { return photo6; }
            set
            {
                if (photo6 == value)
                    return;
                photo6 = value;

            }
        }
        public Photo_()
        {

        }
        public void ToXml(string LOGIN)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\ph\\" + LOGIN + ".xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track_ph",
                        new XAttribute("ph1", ByteArrayToString(getJPGFromImageControl(Date1))),
                        new XAttribute("ph2", ByteArrayToString(getJPGFromImageControl(Date2))),
                        new XAttribute("ph3", ByteArrayToString(getJPGFromImageControl(Date3))),
                        new XAttribute("ph4", ByteArrayToString(getJPGFromImageControl(Date4))),
                        new XAttribute("ph5", ByteArrayToString(getJPGFromImageControl(Date5))),
                        new XAttribute("ph6", ByteArrayToString(getJPGFromImageControl(Date6))))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                XElement track = new XElement("track_ph");
                track.Add(new XAttribute("ph1", ByteArrayToString(getJPGFromImageControl(Date1))));
                track.Add(new XAttribute("ph2", ByteArrayToString(getJPGFromImageControl(Date2))));
                track.Add(new XAttribute("ph3", ByteArrayToString(getJPGFromImageControl(Date3))));
                track.Add(new XAttribute("ph4", ByteArrayToString(getJPGFromImageControl(Date4))));
                track.Add(new XAttribute("ph5", ByteArrayToString(getJPGFromImageControl(Date5))));
                track.Add(new XAttribute("ph6", ByteArrayToString(getJPGFromImageControl(Date6))));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }
        public Photo_(XElement element)
        {
            if (element.Attribute("ph1").Value != null) Date1 = LoadImage(StringToByteArray(element.Attribute("ph1").Value));
            if (element.Attribute("ph2").Value != null) Date2 = LoadImage(StringToByteArray(element.Attribute("ph2").Value));
            if (element.Attribute("ph3").Value != null) Date3 = LoadImage(StringToByteArray(element.Attribute("ph3").Value));
            if (element.Attribute("ph4").Value != null) Date4 = LoadImage(StringToByteArray(element.Attribute("ph4").Value));
            if (element.Attribute("ph5").Value != null) Date5 = LoadImage(StringToByteArray(element.Attribute("ph5").Value));
            if (element.Attribute("ph6").Value != null) Date6 = LoadImage(StringToByteArray(element.Attribute("ph6").Value));
        }
        public byte[] getJPGFromImageControl(BitmapImage imageC) // Получение из изображения массива байт
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageC));
            encoder.Save(memStream);
            return memStream.ToArray();
        }
        public BitmapImage LoadImage(byte[] imageData) //получение из массива байт изображения
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        public string ByteArrayToString(byte[] ba) //Перевод массива байт в строку
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        public byte[] StringToByteArray(String hex) //перевод строки в массив байт
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        
    }
    public class Docum : INotifyPropertyChanged ///Документ 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string kod_ = ".";
        string ser_ = ".";
        string nomber_ = ".";
        string who_ = ".";
        string date_ = ".";
        public Docum() { }
        public Docum(XElement element)
        {
            if (element.Attribute("kod").Value != null) Kod = element.Attribute("kod").Value;
            if (element.Attribute("seriya").Value != null) Seriya = element.Attribute("seriya").Value;
            if (element.Attribute("nomber").Value != null) Nomber = element.Attribute("nomber").Value;
            if (element.Attribute("who_vidal").Value != null) Who_vidal = element.Attribute("who_vidal").Value;
            if (element.Attribute("date_vid").Value != null) Date_vid = element.Attribute("date_vid").Value;
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
        public string Seriya
        {
            get { return ser_; }
            set
            {
                if (ser_ == value)
                    return;
                ser_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Ser"));
            }
        }
        public string Nomber
        {
            get { return nomber_; }
            set
            {
                if (nomber_ == value)
                    return;
                nomber_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Nomber"));
            }
        }
        public string Who_vidal
        {
            get { return who_; }
            set
            {
                if (who_ == value)
                    return;
                who_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Who"));
            }
        }
        public string Date_vid
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
        public void ToXml(string LOGIN)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + LOGIN + ".xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track_dm",
                        new XAttribute("kod", Kod),
                        new XAttribute("seriya", Seriya),
                        new XAttribute("nomber", Nomber),
                        new XAttribute("who_vidal", Who_vidal),
                        new XAttribute("date_vid", Date_vid))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                XElement track = new XElement("track_dm");
                track.Add(new XAttribute("kod", Kod));
                track.Add(new XAttribute("seriya", Seriya));
                track.Add(new XAttribute("nomber", Nomber));
                track.Add(new XAttribute("who_vidal", Who_vidal));
                track.Add(new XAttribute("date_vid", Date_vid));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }

    }
    public class Dannie : INotifyPropertyChanged  //Личные данные
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string lnumber_ = ".";
        string fio_ = ".";
        string pol_ = ".";
        string mesto_ = ".";
        string date_ = ".";
        string nac_ = ".";
        string gr_ob_ = ".";
        string vo_ob_ = ".";
        string card_ = ".";
        string vod_pr_ = ".";
        string voen_ = ".";
        string home_ = ".";
        string pac_ = ".";
        string types_ = ".";
        public Dannie() { }
        public Dannie(XElement element)
        {
            if (element.Attribute("lnumber").Value != null) Lnumber = element.Attribute("lnumber").Value;
            if (element.Attribute("fio").Value != null) FIO = element.Attribute("fio").Value;
            if (element.Attribute("pol").Value != null) POL = element.Attribute("pol").Value;
            if (element.Attribute("mesto_brd").Value != null) Mesto_brd = element.Attribute("mesto_brd").Value;
            if (element.Attribute("date_brd").Value != null) Date_brd = element.Attribute("date_brd").Value;
            if (element.Attribute("nac").Value != null) Nac = element.Attribute("nac").Value;
            if (element.Attribute("gr_obr").Value != null) Gr_obr = element.Attribute("gr_obr").Value;
            if (element.Attribute("voen_obr").Value != null) Voen_obr = element.Attribute("voen_obr").Value;
            if (element.Attribute("bank_card").Value != null) Bank_card = element.Attribute("bank_card").Value;
            if (element.Attribute("vod_prava").Value != null) Vod_prava = element.Attribute("vod_prava").Value;
            if (element.Attribute("voenkomat").Value != null) Voenkomat = element.Attribute("voenkomat").Value;
            if (element.Attribute("home_adres").Value != null) Home_adres = element.Attribute("home_adres").Value;
            if (element.Attribute("pac").Value != null) PAC = element.Attribute("pac").Value;
            if (element.Attribute("types").Value != null) Types = element.Attribute("types").Value;
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
        public string POL
        {
            get { return pol_; }
            set
            {
                if (pol_ == value)
                    return;
                pol_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("POL"));
            }
        }
        public string Mesto_brd
        {
            get { return mesto_; }
            set
            {
                if (mesto_ == value)
                    return;
                mesto_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Mesto_brd"));
            }
        }
        public string Date_brd
        {
            get { return date_; }
            set
            {
                if (date_ == value)
                    return;
                date_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date_brd"));
            }
        }
        public string Nac
        {
            get { return nac_; }
            set
            {
                if (nac_ == value)
                    return;
                nac_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Nac"));
            }
        }
        public string Gr_obr
        {
            get { return gr_ob_; }
            set
            {
                if (gr_ob_ == value)
                    return;
                gr_ob_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Gr_ob"));
            }
        }
        public string Voen_obr
        {
            get { return vo_ob_; }
            set
            {
                if (vo_ob_ == value)
                    return;
                vo_ob_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Vo_ob"));
            }
        }
        public string Bank_card
        {
            get { return card_; }
            set
            {
                if (card_ == value)
                    return;
                card_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Bank_card"));
            }
        }
        public string Vod_prava
        {
            get { return vod_pr_; }
            set
            {
                if (vod_pr_ == value)
                    return;
                vod_pr_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Vod_pr"));
            }
        }
        public string Voenkomat
        {
            get { return voen_; }
            set
            {
                if (voen_ == value)
                    return;
                voen_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Voen"));
            }
        }
        public string Home_adres
        {
            get { return home_; }
            set
            {
                if (home_ == value)
                    return;
                home_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Home"));
            }
        }
        public string PAC
        {
            get { return pac_; }
            set
            {
                if (pac_ == value)
                    return;
                pac_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Pac"));
            }
        }
        public void ToXml(string LOGIN)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + LOGIN + ".xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track_dan",
                        new XAttribute("lnumber", Lnumber),
                        new XAttribute("fio", FIO),
                        new XAttribute("pol", POL),
                        new XAttribute("mesto_brd", Mesto_brd),
                        new XAttribute("date_brd", Date_brd),
                        new XAttribute("nac", Nac),
                        new XAttribute("gr_obr", Gr_obr),
                        new XAttribute("voen_obr", Voen_obr),
                        new XAttribute("bank_card", Bank_card),
                        new XAttribute("vod_prava", Vod_prava),
                        new XAttribute("voenkomat", Voenkomat),
                        new XAttribute("home_adres", Home_adres),
                        new XAttribute("types", Types),
                        new XAttribute("pac", PAC))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                XElement track = new XElement("track_dan");
                track.Add(new XAttribute("lnumber", Lnumber));
                track.Add(new XAttribute("fio", FIO));
                track.Add(new XAttribute("pol", POL));
                track.Add(new XAttribute("mesto_brd", Mesto_brd));
                track.Add(new XAttribute("date_brd", Date_brd));
                track.Add(new XAttribute("nac", Nac));
                track.Add(new XAttribute("gr_obr", Gr_obr));
                track.Add(new XAttribute("voen_obr", Voen_obr));
                track.Add(new XAttribute("bank_card", Bank_card));
                track.Add(new XAttribute("vod_prava", Vod_prava));
                track.Add(new XAttribute("voenkomat", Voenkomat));
                track.Add(new XAttribute("home_adres", Home_adres));
                track.Add(new XAttribute("types", Types));
                track.Add(new XAttribute("pac", PAC));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }
    }
    public class Kontrakt: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string types_ = ".";
        string date1_ = ".";
        string date2_ = ".";
        string who_ = ".";
        string nom_ = ".";
        string date3_ = ".";
        public Kontrakt() { }
        public Kontrakt(XElement element)
        {
            if (element.Attribute("types").Value != null) types = element.Attribute("types").Value;
            if (element.Attribute("data_zakl").Value != null) data_zakl = element.Attribute("data_zakl").Value;
            if (element.Attribute("data_okon").Value != null) data_okon = element.Attribute("data_okon").Value;
            if (element.Attribute("who_pricaz").Value != null) Who_pricaz = element.Attribute("who_pricaz").Value;
            if (element.Attribute("nomber_pricaz").Value != null) Nomber_pricaz = element.Attribute("nomber_pricaz").Value;
            if (element.Attribute("date_pricaz").Value != null) Date_pricaz = element.Attribute("date_pricaz").Value;
        }
        public string types
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
        public string data_zakl
        {
            get { return date1_; }
            set
            {
                if (date1_ == value)
                    return;
                date1_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date1"));
            }
        }
        public string data_okon
        {
            get { return date2_; }
            set
            {
                if (date2_ == value)
                    return;
                date2_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date2"));
            }
        }
        public string Who_pricaz
        {
            get { return who_; }
            set
            {
                if (who_ == value)
                    return;
                who_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Who"));
            }
        }
        public string Nomber_pricaz
        {
            get { return nom_; }
            set
            {
                if (nom_ == value)
                    return;
                nom_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Nom"));
            }
        }
        public string Date_pricaz
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
        public void ToXml(string LOGIN)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + LOGIN + ".xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track_kon",
                        new XAttribute("types", types),
                        new XAttribute("data_zakl", data_zakl),
                        new XAttribute("data_okon", data_okon),
                        new XAttribute("who_pricaz", Who_pricaz),
                        new XAttribute("nomber_pricaz", Nomber_pricaz),
                        new XAttribute("date_pricaz", Date_pricaz))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                doc.Descendants().Where(e => e.Name == "track_kon").Remove();
                XElement track = new XElement("track_kon");
                track.Add(new XAttribute("types", types));
                track.Add(new XAttribute("data_zakl", data_zakl));
                track.Add(new XAttribute("data_okon", data_okon));
                track.Add(new XAttribute("who_pricaz", Who_pricaz));
                track.Add(new XAttribute("nomber_pricaz", Nomber_pricaz));
                track.Add(new XAttribute("date_pricaz", Date_pricaz));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }
    }
    public class Naznach : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string who_ = ".";
        string pr_ = ".";
        string date1_ = ".";
        public Naznach() { }
        public Naznach(XElement element)
        {
            if (element.Attribute("who_pricaz").Value != null) who_pricaz = element.Attribute("who_pricaz").Value;
            if (element.Attribute("pricaz").Value != null) pricaz = element.Attribute("pricaz").Value;
            if (element.Attribute("date_pricaz").Value != null) date_pricaz = element.Attribute("date_pricaz").Value;
        }
        public string who_pricaz
        {
            get { return who_; }
            set
            {
                if (who_ == value)
                    return;
                who_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Who"));
            }
        }
        public string pricaz
        {
            get { return pr_; }
            set
            {
                if (pr_ == value)
                    return;
                pr_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Pr"));
            }
        }
        public string date_pricaz
        {
            get { return date1_; }
            set
            {
                if (date1_ == value)
                    return;
                date1_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date1"));
            }
        }
        public void ToXml(string LOGIN)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + LOGIN + ".xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track_naz",
                        new XAttribute("who_pricaz", who_pricaz),
                        new XAttribute("pricaz", pricaz),
                        new XAttribute("date_pricaz", date_pricaz))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                doc.Descendants().Where(e => e.Name == "track_naz").Remove();
                XElement track = new XElement("track_naz");
                track.Add(new XAttribute("who_pricaz", who_pricaz));
                track.Add(new XAttribute("pricaz", pricaz));
                track.Add(new XAttribute("date_pricaz", date_pricaz));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }

    }
    public class PriemDel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string who_ = ".";
        string pr_ = ".";
        string date1_ = ".";
        public PriemDel() { }
        public PriemDel(XElement element)
        {
            if (element.Attribute("who_pricaz").Value != null) who_pricaz = element.Attribute("who_pricaz").Value;
            if (element.Attribute("pricaz").Value != null) pricaz = element.Attribute("pricaz").Value;
            if (element.Attribute("date_pricaz").Value != null) date_pricaz = element.Attribute("date_pricaz").Value;
        }
        public string who_pricaz
        {
            get { return who_; }
            set
            {
                if (who_ == value)
                    return;
                who_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Who"));
            }
        }
        public string pricaz
        {
            get { return pr_; }
            set
            {
                if (pr_ == value)
                    return;
                pr_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Pr"));
            }
        }
        public string date_pricaz
        {
            get { return date1_; }
            set
            {
                if (date1_ == value)
                    return;
                date1_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date1"));
            }
        }
        public void ToXml(string LOGIN)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + LOGIN + ".xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track_pr_del",
                        new XAttribute("who_pricaz", who_pricaz),
                        new XAttribute("pricaz", pricaz),
                        new XAttribute("date_pricaz", date_pricaz))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                doc.Descendants().Where(e => e.Name == "track_pr_del").Remove();
                XElement track = new XElement("track_pr_del");
                track.Add(new XAttribute("who_pricaz", who_pricaz));
                track.Add(new XAttribute("pricaz", pricaz));
                track.Add(new XAttribute("date_pricaz", date_pricaz));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }
    }
    public class Vizhivanie: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string mesto_ = ".";
        string per_ = ".";
        string osn_ = ".";
        public Vizhivanie() { }
        public Vizhivanie(XElement element)
        {
            if (element.Attribute("mesto").Value != null) Mesto = element.Attribute("mesto").Value;
            if (element.Attribute("period").Value != null) Period = element.Attribute("period").Value;
            if (element.Attribute("osnovanie").Value != null) Osnovanie = element.Attribute("osnovanie").Value;
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
        public string Period
        {
            get { return per_; }
            set
            {
                if (per_ == value)
                    return;
                per_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Per"));
            }
        }
        public string Osnovanie
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
        public void ToXml(string LOGIN)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + LOGIN + ".xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track_viz",
                        new XAttribute("mesto", Mesto),
                        new XAttribute("period", Period),
                        new XAttribute("osnovanie", Osnovanie))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                doc.Descendants().Where(e => e.Name == "track_viz").Remove();
                XElement track = new XElement("track_viz");
                track.Add(new XAttribute("mesto", Mesto));
                track.Add(new XAttribute("period", Period));
                track.Add(new XAttribute("osnovanie", Osnovanie));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }
    }
    public class Otryv_LD: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string types_ = ".";
        string cel_ = ".";
        string mesto_ = ".";
        string date1_ = ".";
        string osn_ = ".";
        string date2_ = ".";
        string pr_ = ".";

        public Otryv_LD() { }
        public Otryv_LD(XElement element)
        {
            if (element.Attribute("mesto").Value != null) Mesto = element.Attribute("mesto").Value;
            if (element.Attribute("types_").Value != null) Types_ = element.Attribute("types_").Value;
            if (element.Attribute("cel_").Value != null) Cel_ = element.Attribute("cel_").Value;
            if (element.Attribute("s_data").Value != null) S_data = element.Attribute("s_data").Value;
            if (element.Attribute("data_preb").Value != null) Data_preb = element.Attribute("data_preb").Value;
            if (element.Attribute("osnovanie").Value != null) Osnovanie = element.Attribute("osnovanie").Value;
            if (element.Attribute("pr").Value != null) Osnovanie = element.Attribute("pr").Value;
        }
        public string Types_
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
        public string Cel_
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
        public string S_data
        {
            get { return date1_; }
            set
            {
                if (date1_ == value)
                    return;
                date1_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date1"));
            }
        }
        public string Osnovanie
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
        public string Data_preb
        {
            get { return date2_; }
            set
            {
                if (date2_ == value)
                    return;
                date2_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Date2"));
            }
        }
        public string Pr
        {
            get { return pr_; }
            set
            {
                if (pr_ == value)
                    return;
                pr_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Pr"));
            }
        }
        public void ToXml(string LOGIN)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\LD\\" + LOGIN + ".xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track_ub",
                        new XAttribute("mesto", Mesto),
                        new XAttribute("types_", Types_),
                        new XAttribute("cel_", Cel_),
                        new XAttribute("s_data", S_data),
                        new XAttribute("data_preb", Data_preb),
                        new XAttribute("pr", Pr),
                        new XAttribute("osnovanie", Osnovanie))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                doc.Descendants().Where(e => e.Name == "track_ub").Remove();
                XElement track = new XElement("track_ub");
                track.Add(new XAttribute("mesto", Mesto));
                track.Add(new XAttribute("types_", Types_));
                track.Add(new XAttribute("cel_", Cel_));
                track.Add(new XAttribute("s_data", S_data));
                track.Add(new XAttribute("data_preb", Data_preb));
                track.Add(new XAttribute("pr", Pr));
                track.Add(new XAttribute("osnovanie", Osnovanie));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }

    }
}
