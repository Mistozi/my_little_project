using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
//using System.Threading.Tasks;

namespace МиниЧат.Modal
{
    public class Messager: INotifyPropertyChanged
    {
        private string name = "";
        private string mes = "";
        private string ip_ = "";
        private string assnova_ = "";
        

        public Messager()
        {

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
        public string Text
        {
            get { return assnova_; }
            set
            {
                if (assnova_ == value)
                    return;
                assnova_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }
        
        public string Mes
        {
            get { return mes; }
            set
            {
                if (mes == value)
                    return;
                mes = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Mes"));
            }
        }
        public string IP
        {
            get { return ip_; }
            set
            {
                if (ip_ == value)
                    return;
                ip_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IP"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Messager(XElement el)
        {
            if (el.Attribute("name").Value != "") name = el.Attribute("name").Value;
            if (el.Attribute("mes").Value != "") mes = el.Attribute("mes").Value;
            if (el.Attribute("ip_").Value != "") ip_ = el.Attribute("ip_").Value;
            if (el.Attribute("assnova_").Value != "") assnova_ = el.Attribute("assnova_").Value;

        }

        public void ToXml()
        {

            XDocument doc;
            if (File.Exists(Environment.CurrentDirectory + "\\Recources\\dannie.xml") == false)
            {
                string fileName = Environment.CurrentDirectory + "\\Recources\\dannie.xml";
                doc = new XDocument(
                            new XElement("base",
                             new XElement("track",
                                new XAttribute("name", name),
                                new XAttribute("mes", mes),
                                new XAttribute("ip_", ip_),
                                new XAttribute("assnova_", assnova_))));
                doc.Save(fileName);
            }
            else
            {
                string fileName = Environment.CurrentDirectory + "\\Recources\\dannie.xml";
                doc = XDocument.Load(fileName);
                XElement track = new XElement("track");
                track.Add(new XAttribute("name", name));
                track.Add(new XAttribute("mes", mes));
                track.Add(new XAttribute("ip_", ip_));
                track.Add(new XAttribute("assnova_", assnova_));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }

    }
    public class Peoples : INotifyPropertyChanged
    {
        private int id_;
        private string name;
        private string ip_;
        private string assnova_;
        private int counter = -1;
        public Peoples()
        {

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
        public string Text
        {
            get { return assnova_; }
            set
            {
                if (assnova_ == value)
                    return;
                assnova_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }
        public string IP
        {
            get { return ip_; }
            set
            {
                if (ip_ == value)
                    return;
                ip_ = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("IP"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public int Counter
        {
            get { return counter; }
            set
            {
                if (counter == value)
                    return;
                counter = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Counter"));
            }
        }
    }
}
