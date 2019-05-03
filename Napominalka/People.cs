using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Whatch
{
    public class People : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id = 0;
        private int id1 = 1;
        private static int counter = 1;
        private string ln = ".";
        private string zvanie = ".";
        private string dolg = ".";
        private string fio = ".";
        private string nomber = ".";
        private string date_brd = ".";
        public People()
        {
            counter++;
            id = counter;
            ln = "pg-" + counter;
        }
        public int IDD
        {
            get { return id1; }
            set
            {
                id1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
            }
        }
        public int ID
        {
            get { return id; }
        }
        public string Dolg
        {
            get { return dolg; }
            set
            {
                if (dolg == value)
                    return;
                dolg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Dolg"));
            }
        }
        public string LN
        {
            get { return ln; }
            set
            {
                if (ln == value)
                    return;
                ln = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LN"));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Zvanie"));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Fio"));
            }
        }
        public string Nomber
        {
            get { return nomber; }
            set
            {
                if (nomber == value)
                    return;
                nomber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nomber"));
            }
        }
        public string Date_brd
        {
            get { return date_brd; }
            set
            {
                if (date_brd == value)
                    return;
                date_brd = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date_brd"));
            }
        }

        public People(XElement el)
        {
            if (el.Attribute("id").Value != "0") id = int.Parse(el.Attribute("id").Value);
            if (el.Attribute("ln").Value != ".") LN = el.Attribute("ln").Value;
            if (el.Attribute("dg").Value != ".") Dolg = el.Attribute("dg").Value;
            if (el.Attribute("zv").Value != ".") Zvanie = el.Attribute("zv").Value;
            if (el.Attribute("fio").Value != ".") Fio = el.Attribute("fio").Value;
            if (el.Attribute("nt").Value != ".") Nomber = el.Attribute("nt").Value;
            if (el.Attribute("db").Value != ".") Date_brd = el.Attribute("db").Value;
            if(counter < int.Parse(el.Attribute("id").Value))
                counter = int.Parse(el.Attribute("id").Value);
            IDD++;
        }

        public string Date(DateTime dt)
        {
            string data = "";
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

        public void ToXml()
        {

            XDocument doc;
            if (File.Exists(Environment.CurrentDirectory + "\\dannie.xml") == false)
            {
                string fileName = Environment.CurrentDirectory + "\\dannie.xml";
                doc = new XDocument(
                            new XElement("base",
                             new XElement("track",
                                new XAttribute("id", ID.ToString()),
                                new XAttribute("ln", LN),
                                new XAttribute("dg", Dolg),
                                new XAttribute("zv", Zvanie),
                                new XAttribute("fio", Fio),
                                new XAttribute("nt", Nomber),
                                new XAttribute("db", Date_brd))));
                doc.Save(fileName);
            }
            else
            {
                string fileName = Environment.CurrentDirectory + "\\dannie.xml";
                doc = XDocument.Load(fileName);
                doc.Descendants("track")
                    .Where(x => (string)x.Attribute("ln") == LN)
                    .Remove();
                XElement track = new XElement("track");
                track.Add(new XAttribute("id", ID.ToString()));
                track.Add(new XAttribute("ln", LN));
                track.Add(new XAttribute("dg", Dolg));
                track.Add(new XAttribute("zv", Zvanie));
                track.Add(new XAttribute("fio", Fio));
                track.Add(new XAttribute("nt", Nomber));
                track.Add(new XAttribute("db", Date_brd));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }

    }
}
