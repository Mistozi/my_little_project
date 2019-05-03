using System;
using System.Collections.Generic;
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
    public class Pol: INotifyPropertyChanged
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
                return;
            }
        }

    }
}
