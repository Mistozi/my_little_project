using Client.Modal;
using New_Stroevaya_chast.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using static New_Stroevaya_chast.Modal.Shtat;

namespace New_Stroevaya_chast.ViewModal
{
    public class TCP
    {
        #region Переменные
        public List<string> FIO_VRIO = new List<string>();
        static ObservableCollection<Izmen> _izm_list = new ObservableCollection<Izmen>();
        Boolean g = false;
        string Dop = ".";
        #region ШТАТ
        static ObservableCollection<OpenLoad> list_shtat = new ObservableCollection<OpenLoad>();
        
        public ObservableCollection<OpenLoad> List_shtat
        {
            get
            {
                if (list_shtat == null)
                {
                   
                }
                return list_shtat;
            }
            set { list_shtat = value; }
        }
        
        #endregion
        #region ЗАШТАТ
        static ObservableCollection<OpenLoadZaShtat> list_Anshtat = new ObservableCollection<OpenLoadZaShtat>();
        public ObservableCollection<OpenLoadZaShtat> List_AnShtat
        {
            get { return list_Anshtat; }
            set { list_Anshtat = value; }
        }
        static ObservableCollection<OpenLoadZaShtat> list_Anshtat2 = new ObservableCollection<OpenLoadZaShtat>();
        public ObservableCollection<OpenLoadZaShtat> List_AnShtat2
        {
            get { return list_Anshtat2; }
            set { list_Anshtat2 = value; }
        }
        #endregion
        #region ПЕРЕВОД
        static ObservableCollection<OpenLoadPerevod> list_perev = new ObservableCollection<OpenLoadPerevod>();
        public ObservableCollection<OpenLoadPerevod> List_Perev
        {
            get { return list_perev; }
            set { list_perev = value; }
        }
        #endregion
        #region УВОЛЕННЫЕ
        static ObservableCollection<OpenLoadDel> list_del = new ObservableCollection<OpenLoadDel>();
        public ObservableCollection<OpenLoadDel> list_Del
        {
            get { return list_del; }
            set { list_del = value; }
        }
        #endregion
        #region ИСКЛЮЧЕННЫЕ
        static ObservableCollection<OpenLoadIskl> list_iskl = new ObservableCollection<OpenLoadIskl>();
        public ObservableCollection<OpenLoadIskl> list_Iskl
        {
            get { return list_iskl; }
            set { list_iskl = value; }
        }
        #endregion
        #region ПЕРЕСТАНОВКИ
        static List<string> list_perest = new List<string>();
        public List<string> list_Perest
        {
            get { return list_perest; }
            set { list_perest = value; }
        }

        #endregion
        #region Перестановочка
        static ObservableCollection<Perest> list_Per = new ObservableCollection<Perest>();
        public ObservableCollection<Perest> List_Per
        {
            get { return list_Per; }
            set { list_Per = value; }
        }
        #endregion
        #region РСЗ
        static ObservableCollection<lic_rsz> list_rsz_lic = new ObservableCollection<lic_rsz>();
        public ObservableCollection<lic_rsz> list_RSZ_lic
        {
            get { return list_rsz_lic; }
            set { list_rsz_lic = value; }
        }
        #endregion
        #region ВЫБОРКА
        static ObservableCollection<OpenLoad> list_Vibor = new ObservableCollection<OpenLoad>();
        public ObservableCollection<OpenLoad> List_Vibor
        {
            get { return list_Vibor; }
            set { list_Vibor = value; }
        }
        #endregion
        #region Наряд
        static ObservableCollection<Naryad> list_Naryad = new ObservableCollection<Naryad>();
        public ObservableCollection<Naryad> List_Naryad
        {
            get { return list_Naryad; }
            set { list_Naryad = value; }
        }
        #endregion
        #region Отобранные на контракт
        //Отобранные
        static ObservableCollection<Otbor_na_kontr> list_Otob = new ObservableCollection<Otbor_na_kontr>();
        public ObservableCollection<Otbor_na_kontr> List_Otob
        {
            get { return list_Otob; }
            set { list_Otob = value; }
        }
        //Переданные
        static ObservableCollection<Otbor_na_kontr> list_Pered = new ObservableCollection<Otbor_na_kontr>();
        public ObservableCollection<Otbor_na_kontr> List_Pered
        {
            get { return list_Pered; }
            set { list_Pered = value; }
        }

        //Состоялись
        static ObservableCollection<Otbor_na_kontr> list_Sost = new ObservableCollection<Otbor_na_kontr>();
        public ObservableCollection<Otbor_na_kontr> List_Sost
        {
            get { return list_Sost; }
            set { list_Sost = value; }
        }

        //Отказ
        static ObservableCollection<Otbor_na_kontr> list_Otkaz = new ObservableCollection<Otbor_na_kontr>();
        public ObservableCollection<Otbor_na_kontr> List_Otkaz
        {
            get { return list_Otkaz; }
            set { list_Otkaz = value; }
        }
        #endregion
        #region Котел
        static ObservableCollection<OpenLoadKotel> list_Kotel1 = new ObservableCollection<OpenLoadKotel>();
        public ObservableCollection<OpenLoadKotel> List_Kotel1
        {
            get { return list_Kotel1; }
            set { list_Kotel1 = value; }
        }

        static ObservableCollection<Kotel> list_Kotel2 = new ObservableCollection<Kotel>();
        public ObservableCollection<Kotel> List_Kotel2
        {
            get { return list_Kotel2; }
            set { list_Kotel2 = value; }
        }
        #endregion
        #region LD
        static ObservableCollection<zvanie> ld_zvanie = new ObservableCollection<zvanie>();

        public ObservableCollection<zvanie> LD_Zvanie
        {
            get
            {
                if (ld_zvanie == null)
                {

                }
                return ld_zvanie;
            }
            set { ld_zvanie = value; }
        }
        static ObservableCollection<Poslug> ld_poslug = new ObservableCollection<Poslug>();

        public ObservableCollection<Poslug> LD_Poslug
        {
            get
            {
                if (ld_poslug == null)
                {

                }
                return ld_poslug;
            }
            set { ld_poslug = value; }
        }
        static ObservableCollection<family> ld_fam = new ObservableCollection<family>();

        public ObservableCollection<family> LD_Fam
        {
            get
            {
                if (ld_fam == null)
                {

                }
                return ld_fam;
            }
            set { ld_fam = value; }
        }
        static ObservableCollection<Nagrad> ld_nag = new ObservableCollection<Nagrad>();

        public ObservableCollection<Nagrad> LD_Nag
        {
            get
            {
                if (ld_nag == null)
                {

                }
                return ld_nag;
            }
            set { ld_nag = value; }
        }
        static ObservableCollection<Docum> ld_doc = new ObservableCollection<Docum>();
        static ObservableCollection<Docum> ld_doc1 = new ObservableCollection<Docum>();
        public ObservableCollection<Docum> LD_Doc
        {
            get
            {
                if (ld_doc == null)
                {

                }
                return ld_doc;
            }
            set { ld_doc = value; }
        }
        public ObservableCollection<Docum> LD_Doc1
        {
            get
            {
                if (ld_doc1 == null)
                {

                }
                return ld_doc1;
            }
            set { ld_doc1 = value; }
        }
        static ObservableCollection<Kontrakt> ld_kon = new ObservableCollection<Kontrakt>();

        public ObservableCollection<Kontrakt> LD_Kon
        {
            get
            {
                if (ld_kon == null)
                {

                }
                return ld_kon;
            }
            set { ld_kon = value; }
        }
        public Naznach LD_Naz = new Naznach();
        public Photo_ LD_Ph = new Photo_();
        static Dannie LD_Dan = new Dannie();
        public Dannie LD_DAN
        {
            get
            {
                if (LD_Dan == null)
                {

                }
                return LD_Dan;
            }
            set { LD_Dan = value; }
        }
        public PriemDel LD_PD = new PriemDel();
        public Vizhivanie LD_Viz = new Vizhivanie();
        #endregion
        #region УБЫТИЕ
        static ObservableCollection<Ub> list_ub = new ObservableCollection<Ub>();
        public ObservableCollection<Ub> List_ub
        {
            get { return list_ub; }
            set { list_ub = value; }
        }
        #endregion
        #region УБЫТИЕ
        static ObservableCollection<Ub> list_preb = new ObservableCollection<Ub>();
        public ObservableCollection<Ub> List_preb
        {
            get { return list_preb; }
            set { list_preb = value; }
        }
        #endregion

        #endregion
        // Объект содержащий рабочий сокет клиентского приложения
        TcpClient _tcpСlient = new TcpClient();

        // Объект сетевого потока для приема и отправки сообщений
        NetworkStream ns;

        // Флаг для остановки потоков и завершения сетевой работы приложения
        bool _stopNetwork;

        #region Функциональная часть Сетевая работа

        // Попытка подключения к серверу
        string ip()
        {
            string s = ".";
            string fileName = Environment.CurrentDirectory + "\\Recources\\config.xml";
            XDocument doc = XDocument.Load(fileName);
            foreach(XElement el in doc.Root.Elements("track"))
            {
                s = el.Attribute("value").Value;
            }
            return s;
        }
        public void Connect()
        {
            try
            {

                _tcpСlient.Connect(ip(), 15000);

                ns = _tcpСlient.GetStream();

                Thread th = new Thread(ReceiveRun);
                th.Start();

            }
            catch
            {
                ErrorSound();
            }
        }
        public void CloseClient()
        {
            if (ns != null) ns.Close();
            if (_tcpСlient != null) _tcpСlient.Close();

            _stopNetwork = true;

        }


        // Отправка сообщений в блокирующем режиме,
        // поскольку обмен короткими сообщениями
        // не вызовет заметного блокирования интерфейса приложения. 
        public void SendMessage(string Text_cap)
        {
            if (ns != null)
            {
                byte[] buffer = Encoding.Default.GetBytes(Text_cap);
                ns.Write(buffer, 0, buffer.Length);
            }
        }


        // Цикл извлечения сообщений,
        // запускается в отдельном потоке.
        void ReceiveRun()
        {
            while (true)
            {
                try
                {
                    string s = null;
                    while (ns.DataAvailable == true)
                    {
                        // Определение необходимого размера буфера приема.
                        byte[] buffer = new byte[_tcpСlient.Available];

                        ns.Read(buffer, 0, buffer.Length);
                        s += Encoding.Default.GetString(buffer);
                    }

                    if (s != null)
                    {
                        ShowReceiveMessage(s);

                        //MessageBox.Show(s);
                        //s = String.Empty;
                    }


                    // Вынужденная строчка для экономия ресурсов процессора.
                    // Неизящный способ.
                    Thread.Sleep(100);
                }
                catch
                {
                    ErrorSound();
                }

                if (_stopNetwork == true) break;
            }
        }
        #endregion

        public void Create_LD(ObservableCollection<zvanie> Z, ObservableCollection<Poslug> T, ObservableCollection<family> T1, ObservableCollection<Nagrad> T2, Photo_ T3, ObservableCollection<Docum> T4, Dannie T5, ObservableCollection<Kontrakt> T6, Naznach p, PriemDel p1, Vizhivanie p2)
        {
            foreach(zvanie z in Z)
            {
                ld_zvanie.Add(z);
            }
            foreach(Poslug z in T)
            {
                ld_poslug.Add(z);
            }
            foreach (family z in T1)
            {
                ld_fam.Add(z);
            }
            foreach (Nagrad z in T2)
            {
                ld_nag.Add(z);
            }
            LD_Ph = T3;
            foreach (Docum z in T4)
            {
                ld_doc.Add(z);
            }
            LD_Dan = T5;
            foreach (Kontrakt z in T6)
            {
                ld_kon.Add(z);
            }
            LD_Naz = p;
            LD_PD = p1;
            LD_Viz = p2;
            LD l = new LD();
            string m = l.Create_people_LD(Z, T, T1, T2, T3, T4, T5, T6, p, p1, p2);
            SendMessage(m);
        }

        #region Информации о сетевой работе

        // Код доступа к свойствам объектов главной формы  из других потоков
        delegate void UpdateReceiveDisplayDelegate(string message);
        void ShowReceiveMessage(string message) //Принятие сообщения
        {
            string[] wor = message.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
            if (wor.Count() > 1)
                if (wor[0] == "LD1") { _Pered_ld(wor[1]); }

            string[] word = message.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            if (word.Length > 1)
            {
                string[] word_dop = word[0].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string[] word_shtat = word[1].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string[] word_anshtat = word[2].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string[] word_del = word[3].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string[] word_iskl = word[4].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string[] word_kotel = word[5].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                shtat_pol(word_shtat, word_dop[1]);
                anshtat_pol(word_anshtat, word_dop[1]);
                kotel(word_kotel, word_dop[1]);
                Dop = word_dop[1];
            }
            if (word[0] == "log1")
            {
                MessageBox.Show(word[1]);
            }
            if (word[0] == "Data1")
            {
                MessageBox.Show(word[1]);
            }
            if (word[0] == "Shtat1")
            {
                //MessageBox.Show("Штат получен - "+word[3]);
                shtat_pol(word, "1");
            }
            if (word[0] == "AnShtat1")
            {
                //MessageBox.Show("ЗАШтат получен - "+word[3]);
                anshtat_pol(word, "1");
            }
            if (word[0] == "RSZ1")
            {
                MessageBox.Show("РСЗ получен - " + word[3]);
                lic_RSZ(word);
            }
            if (word[0] == "LD1")
            {
                _Pered_ld(word[1]);
            }
            if (word[0] == "izm")
            {
                Izmen T = new Izmen
                {
                    Types = word[1],
                    Row = int.Parse(word[2]),
                    Name_table = word[3],
                    Old_value = word[4],
                    New_value = word[5],
                    Col = word[6]
                };
                Izm_d(T);
                _izm_list.Add(T);
            }
            if (word[0] == "kot-preb")
            {
                Izmen T = new Izmen
                {
                    Types = word[1],
                    Row = int.Parse(word[2]),
                    Name_table = word[3],
                    Old_value = word[4],
                    New_value = word[5],
                    Col = word[6]
                };
                preb_kotel(T);
            }
        }
        public void Izm_d(Izmen t)
        {
            if (t.Types == "Удалить")
            {
                OpenLoadZaShtat p = new OpenLoadZaShtat();
                string[] word = t.Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                p.FIO = word[2];
                p.Last_dolg = word[0];
                p.Last_podr = word[1];
                p.Lnumber = t.Old_value;
                p.Zvanie = word[3];
                p.Types = word[4];
                list_Anshtat.Add(p);
                foreach(OpenLoad i in list_shtat)
                {
                    if(i.Lnumber == p.Lnumber)
                    {
                        i.Lnumber = ".";
                        i.Types = ".";
                        i.Zvanie = ".";
                        i.Otryv_pricaz = ".";
                        i.Otryv_primech = ".";
                        i.Otryv_status = ".";
                        i.Fio = "-В-";
                    }
                }
            }
            if (t.Types == "Добавить")
            {
                string t1 = "."; string t2 = "."; string t3 = ".";
                string[] word = t.Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (Ub p in list_ub)
                {
                    if (p.Lnumber == t.New_value)
                    {
                        t1 = p.Types_ub;
                        if (p.Date_ub != ".")
                            t2 += "_c_" + p.Date_ub;
                        if (p.Date_preb != ".")
                            t2 += "_по_" + p.Date_preb;
                        if (p.Mesto != ".")
                            t2 += "_с выездом в_" + p.Mesto;
                    }
                }
                foreach (OpenLoad p in list_shtat)
                {
                    if(p.Id == t.Row)
                    {
                        p.Lnumber = t.New_value;
                        p.Zvanie = word[2];
                        p.Types = word[1];
                        p.Fio = word[0];
                        p.Otryv_status = t1;
                        p.Otryv_primech = t2;
                        p.Otryv_pricaz = t3;
                    }
                }
                foreach(OpenLoadZaShtat p in list_Anshtat)
                {
                    if(p.Lnumber == t.New_value)
                    {
                        list_Anshtat.Remove(p);
                        break;
                    }
                }
                
            }
            if (t.Types == "Убытие")
            {
                string[] word = t.New_value.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                Ub t1 = new Ub();
                t1.Cel = word[0];
                t1.Date3 = word[1];
                t1.Date_preb = word[2];
                t1.Date_ub = word[3];
                t1.Dolg = word[4];
                t1.FIO = word[5];
                t1.Lnumber = word[6];
                t1.Mesto = word[7];
                t1.Osn = word[8];
                t1.Podr = word[9];
                t1.Types = word[10];
                t1.Types_ub = word[11];
                t1.VPD_ST1 = word[12];
                t1.VPD_ST2 = word[13];
                t1.VPD_ST3 = word[14];
                t1.VPD_ST4 = word[15];
                t1.VPD_ST5 = word[16];
                t1.VPD_ST6 = word[17];
                t1.VPD_ST7 = word[18];
                t1.Vrio = word[19];
                t1.Zvanie = word[20];
                list_ub.Add(t1);
                foreach(OpenLoad a in list_shtat)
                {
                    if(a.Lnumber == t1.Lnumber)
                    {
                        a.Otryv_status = word[11];
                        if (t1.Cel != ".")
                            a.Otryv_primech += t1.Cel;
                        if (t1.Date_ub != ".")
                            a.Otryv_primech += "_c_" + t1.Date_ub;
                        if (t1.Date_preb != ".")
                            a.Otryv_primech += "_по_" + t1.Date_preb;
                        if (t1.Mesto != ".")
                            a.Otryv_primech += "_с выездом в_" + t1.Mesto;
                    }
                }
            }
            if (t.Types == "Пребытие")
            {
                string[] word = t.New_value.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                Ub t1 = new Ub();
                t1.Cel = word[0];
                t1.Date3 = word[1];
                t1.Date_preb = word[2];
                t1.Date_ub = word[3];
                t1.Dolg = word[4];
                t1.FIO = word[5];
                t1.Lnumber = word[6];
                t1.Mesto = word[7];
                t1.Osn = word[8];
                t1.Podr = word[9];
                t1.Types = word[10];
                t1.Types_ub = word[11];
                t1.VPD_ST1 = word[12];
                t1.VPD_ST2 = word[13];
                t1.VPD_ST3 = word[14];
                t1.VPD_ST4 = word[15];
                t1.VPD_ST5 = word[16];
                t1.VPD_ST6 = word[17];
                t1.VPD_ST7 = word[18];
                t1.Vrio = word[19];
                t1.Zvanie = word[20];
                list_preb.Add(t1);
                foreach (OpenLoad a in list_shtat)
                {
                    if (a.Lnumber == t1.Lnumber)
                    {
                        a.Otryv_status = ".";
                        a.Otryv_primech = ".";
                        a.Otryv_pricaz = ".";
                    }
                }
            }
            if (t.Types == "Отбор на контракт")
            {
                if (t.Name_table == "Отобранные")
                {
                    Otbor_na_kontr tmp = new Otbor_na_kontr();
                    string[] word = t.Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                    if (word[0] == "true") tmp.Blank = true; else if (word[0] == "false") tmp.Blank = false;
                    if (word[7] == "true") tmp.FIZO = true; else if (word[7] == "false") tmp.FIZO = false;
                    if (word[8] == "true") tmp.Ic = true; else if (word[8] == "false") tmp.Ic = false;
                    if (word[12] == "true") tmp.Prof = true; else if (word[12] == "false") tmp.Prof = false;
                    if (word[13] == "true") tmp.Raport = true; else if (word[13] == "false") tmp.Raport = false;
                    if (word[15] == "true") tmp.Vvk = true; else if (word[15] == "false") tmp.Vvk = false;
                    tmp.Date1 = word[1];
                    tmp.Date2 = word[2];
                    tmp.Date3 = word[3];
                    tmp.Date4 = word[4];
                    tmp.Date_ic = word[5];
                    tmp.Dolg = word[6];
                    tmp.FIO = t.New_value;
                    tmp.Lnumber = word[9];
                    tmp.Podr = word[10];
                    tmp.Primec = word[11];
                    tmp.Types = word[14];
                    tmp.Zvanie = word[16];
                    list_Otob.Add(tmp);
                    foreach(Otbor_na_kontr i in list_Otkaz)
                    {
                        if(i.Lnumber == tmp.Lnumber)
                        {
                            list_Otkaz.Remove(i);
                            break;
                        }
                    }
                }
                if (t.Name_table == "Переданные")
                {
                    Otbor_na_kontr tmp = new Otbor_na_kontr();
                    string[] word = t.Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                    if (word[0] == "true") tmp.Blank = true; else if (word[0] == "false") tmp.Blank = false;
                    if (word[7] == "true") tmp.FIZO = true; else if (word[7] == "false") tmp.FIZO = false;
                    if (word[8] == "true") tmp.Ic = true; else if (word[8] == "false") tmp.Ic = false;
                    if (word[12] == "true") tmp.Prof = true; else if (word[12] == "false") tmp.Prof = false;
                    if (word[13] == "true") tmp.Raport = true; else if (word[13] == "false") tmp.Raport = false;
                    if (word[15] == "true") tmp.Vvk = true; else if (word[15] == "false") tmp.Vvk = false;
                    tmp.Date1 = word[1];
                    tmp.Date2 = word[2];
                    tmp.Date3 = word[3];
                    tmp.Date4 = word[4];
                    tmp.Date_ic = word[5];
                    tmp.Dolg = word[6];
                    tmp.FIO = t.New_value;
                    tmp.Lnumber = word[9];
                    tmp.Podr = word[10];
                    tmp.Primec = word[11];
                    tmp.Types = word[14];
                    tmp.Zvanie = word[16];
                    foreach (Otbor_na_kontr i in list_Otob)
                    {
                        if (i.Lnumber == tmp.Lnumber)
                        {
                            list_Otob.Remove(i);
                            break;
                        }
                    }
                }
                if (t.Name_table == "Состоялись")
                {
                    Otbor_na_kontr tmp = new Otbor_na_kontr();
                    string[] word = t.Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                    if (word[0] == "true") tmp.Blank = true; else if (word[0] == "false") tmp.Blank = false;
                    if (word[7] == "true") tmp.FIZO = true; else if (word[7] == "false") tmp.FIZO = false;
                    if (word[8] == "true") tmp.Ic = true; else if (word[8] == "false") tmp.Ic = false;
                    if (word[12] == "true") tmp.Prof = true; else if (word[12] == "false") tmp.Prof = false;
                    if (word[13] == "true") tmp.Raport = true; else if (word[13] == "false") tmp.Raport = false;
                    if (word[15] == "true") tmp.Vvk = true; else if (word[15] == "false") tmp.Vvk = false;
                    tmp.Date1 = word[1];
                    tmp.Date2 = word[2];
                    tmp.Date3 = word[3];
                    tmp.Date4 = word[4];
                    tmp.Date_ic = word[5];
                    tmp.Dolg = word[6];
                    tmp.FIO = t.New_value;
                    tmp.Lnumber = word[9];
                    tmp.Podr = word[10];
                    tmp.Primec = word[11];
                    tmp.Types = word[14];
                    tmp.Zvanie = word[16];
                    foreach (Otbor_na_kontr i in list_Pered)
                    {
                        if (i.Lnumber == tmp.Lnumber)
                        {
                            list_Pered.Remove(i);
                            break;
                        }
                    }
                    foreach(OpenLoad a in list_shtat)
                    {
                        if(a.Lnumber == tmp.Lnumber)
                        {
                            a.Types = "контр";
                        }
                    }
                    foreach (OpenLoadZaShtat a in list_Anshtat)
                    {
                        if (a.Lnumber == tmp.Lnumber)
                        {
                            a.Types = "контр";
                        }
                    }
                }
                if (t.Name_table == "Отказ")
                {
                    Otbor_na_kontr tmp = new Otbor_na_kontr();
                    string[] word = t.Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                    if (word[0] == "true") tmp.Blank = true; else if (word[0] == "false") tmp.Blank = false;
                    if (word[7] == "true") tmp.FIZO = true; else if (word[7] == "false") tmp.FIZO = false;
                    if (word[8] == "true") tmp.Ic = true; else if (word[8] == "false") tmp.Ic = false;
                    if (word[12] == "true") tmp.Prof = true; else if (word[12] == "false") tmp.Prof = false;
                    if (word[13] == "true") tmp.Raport = true; else if (word[13] == "false") tmp.Raport = false;
                    if (word[15] == "true") tmp.Vvk = true; else if (word[15] == "false") tmp.Vvk = false;
                    tmp.Date1 = word[1];
                    tmp.Date2 = word[2];
                    tmp.Date3 = word[3];
                    tmp.Date4 = word[4];
                    tmp.Date_ic = word[5];
                    tmp.Dolg = word[6];
                    tmp.FIO = t.New_value;
                    tmp.Lnumber = word[9];
                    tmp.Podr = word[10];
                    tmp.Primec = word[11];
                    tmp.Types = word[14];
                    tmp.Zvanie = word[16];
                    foreach (Otbor_na_kontr i in list_Pered)
                    {
                        if (i.Lnumber == tmp.Lnumber)
                        {
                            list_Pered.Remove(i);
                            break;
                        }
                    }
                    foreach (Otbor_na_kontr i in list_Otob)
                    {
                        if (i.Lnumber == tmp.Lnumber)
                        {
                            list_Otob.Remove(i);
                            break;
                        }
                    }
                    foreach (Otbor_na_kontr i in list_Sost)
                    {
                        if (i.Lnumber == tmp.Lnumber)
                        {
                            list_Sost.Remove(i);
                            break;
                        }
                    }
                }
            }
            if (t.Types == "Котел")
            {
                if (t.Name_table == "Добавить")
                {
                    string[] word = t.Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                    OpenLoadKotel tt = new OpenLoadKotel();
                    tt.Lnumber = t.New_value;
                    tt.Podr = word[1];
                    tt.Types = word[2];
                    tt.FIO = word[0];
                    list_Kotel1.Add(tt);
                    kotel2();
                }
                if (t.Name_table == "Убрать")
                {
                    foreach(OpenLoadKotel a in list_Kotel1)
                    {
                        if(a.Lnumber == t.New_value)
                        {
                            list_Kotel1.Remove(a);
                            break;
                        }
                    }
                }
                if (t.Name_table == "Убытие")
                {
                    foreach (OpenLoadKotel a in list_Kotel1)
                    {
                        if (a.Lnumber == t.New_value)
                        {
                            list_Kotel1.Remove(a);
                            break;
                        }
                    }
                }

            }
        }
        private void kotel(string[] word, string dop)
        {
            OpenLoadKotel tmp = new OpenLoadKotel();
            int ij = 1;
            for (int i = 1; i < word.Length; i++)
            {
                int j = i;
                while (j > 4)
                {
                    j -= 4;
                }
                if (j == 1)
                {
                    tmp = new OpenLoadKotel();
                    tmp.Lnumber = word[i];
                    //tmp.Id = ij;
                    ij++;
                }
                if (j == 2)
                {
                    tmp.Podr = word[i];
                }
                if (j == 3)
                {
                    tmp.FIO = word[i];
                }
                if (j == 4)
                {
                    tmp.Types = word[i];
                    list_Kotel1.Add(tmp);
                }
            }
            kotel2();
        }
        private void kotel2()
        {
            var list = list_Kotel1.Select(p => p.Podr).Distinct();
            foreach (string pp in list)
            {
                Kotel tmp = new Kotel();
                tmp.Podr = pp;
                foreach (OpenLoadKotel p in list_Kotel1)
                {
                    if (p.Podr == pp)
                    {
                        tmp.Col += 1;
                    }
                }

                list_Kotel2.Add(tmp);
            }
        }
        
        public void clear_viborka()
        {
            list_Vibor.Clear();
        }
        public void Delete_people(OpenLoadZaShtat N)
        {
            if(Dop == "7")
            {
                if(N.Types != "контр" && N.Types != "жен")
                {
                    OpenLoadDel t = new OpenLoadDel();
                    t.Date = Date();
                    t.Dolg = N.Last_dolg;
                    t.Podr = N.Last_podr;
                    t.Types = N.Types;
                    t.Zvanie = N.Zvanie;
                    t.Lnumber = N.Lnumber;
                    t.FIO = N.FIO;
                    list_del.Add(t);
                    list_Anshtat.Remove(N);
                    list_Anshtat2.Remove(N);
                    Izmen td = new Izmen();
                    td.Types = "Увольнение";
                    td.New_value = "|" + t.Lnumber + "|" + t.FIO + "|" + t.Zvanie + "|" + t.Types + "|" + t.Dolg + "|" + t.Podr + "|" + t.Date;
                    SendMessage("izm|" + td.Types + "|" + td.Row + "|" + td.Name_table + "|" + td.Old_value + "|" + td.New_value + "|" + td.Col);
                }
            }
            if (Dop == "5" || Dop == "6")
            {
                if (N.Zvanie == "рядовой" && N.Types =="контр" || N.Zvanie == "рядовой" && N.Types == "жен" || N.Zvanie == "ефрейтор" && N.Types == "контр" || N.Zvanie == "ефрейтор" && N.Types == "жен" || N.Zvanie == "младший сержант" && N.Types == "контр" || N.Zvanie == "младший сержант" && N.Types == "жен" || N.Zvanie == "сержант" && N.Types == "контр" || N.Zvanie == "сержант" && N.Types == "жен" || N.Zvanie == "старший сержант" && N.Types == "контр" || N.Zvanie == "старший сержант" && N.Types == "жен" || N.Zvanie == "старшина" && N.Types == "контр" || N.Zvanie == "старшина" && N.Types == "жен")
                {
                    OpenLoadDel t = new OpenLoadDel();
                    t.Date = Date();
                    t.Dolg = N.Last_dolg;
                    t.Podr = N.Last_podr;
                    t.Types = N.Types;
                    t.Zvanie = N.Zvanie;
                    t.Lnumber = N.Lnumber;
                    t.FIO = N.FIO;
                    list_del.Add(t);
                    list_Anshtat.Remove(N);
                    list_Anshtat2.Remove(N);
                    Izmen td = new Izmen();
                    td.Types = "Увольнение";
                    td.New_value = "|" + t.Lnumber + "|" + t.FIO + "|" + t.Zvanie + "|" + t.Types + "|" + t.Dolg + "|" + t.Podr + "|" + t.Date;
                    SendMessage("izm|" + td.Types + "|" + td.Row + "|" + td.Name_table + "|" + td.Old_value + "|" + td.New_value + "|" + td.Col);
                }
            }
            if (Dop == "4")
            {
                if (N.Zvanie == "старшина" || N.Zvanie == "прапорщик" || N.Zvanie == "старший прапорщик")
                {
                    OpenLoadDel t = new OpenLoadDel();
                    t.Date = Date();
                    t.Dolg = N.Last_dolg;
                    t.Podr = N.Last_podr;
                    t.Types = N.Types;
                    t.Zvanie = N.Zvanie;
                    t.Lnumber = N.Lnumber;
                    t.FIO = N.FIO;
                    list_del.Add(t);
                    list_Anshtat.Remove(N);
                    list_Anshtat2.Remove(N);
                    Izmen td = new Izmen();
                    td.Types = "Увольнение";
                    td.New_value = "|" + t.Lnumber + "|" + t.FIO + "|" + t.Zvanie + "|" + t.Types + "|" + t.Dolg + "|" + t.Podr + "|" + t.Date;
                    SendMessage("izm|" + td.Types + "|" + td.Row + "|" + td.Name_table + "|" + td.Old_value + "|" + td.New_value + "|" + td.Col);
                }
            }
            if (Dop == "3")
            {
                if (N.Zvanie == "лейтенант" || N.Zvanie == "старший лейтенант" || N.Zvanie == "капитан" || N.Zvanie == "майор" || N.Zvanie == "подполковник" || N.Zvanie == "полковник" || N.Zvanie == "генерал-лейтенант")
                {
                    OpenLoadDel t = new OpenLoadDel();
                    t.Date = Date();
                    t.Dolg = N.Last_dolg;
                    t.Podr = N.Last_podr;
                    t.Types = N.Types;
                    t.Zvanie = N.Zvanie;
                    t.Lnumber = N.Lnumber;
                    t.FIO = N.FIO;
                    list_del.Add(t);
                    list_Anshtat.Remove(N);
                    list_Anshtat2.Remove(N);
                    Izmen td = new Izmen();
                    td.Types = "Увольнение";
                    td.New_value = "|" + t.Lnumber + "|" + t.FIO + "|" + t.Zvanie + "|" + t.Types + "|" + t.Dolg + "|" + t.Podr + "|" + t.Date;
                    SendMessage("izm|" + td.Types + "|" + td.Row + "|" + td.Name_table + "|" + td.Old_value + "|" + td.New_value + "|" + td.Col);
                }
            }
        }
        public void Perevod_people(OpenLoadZaShtat N)
        {
            if (Dop == "7")
            {
                if (N.Types != "контр" && N.Types != "жен")
                {
                    OpenLoadPerevod t = new OpenLoadPerevod();
                    t.Podr = N.Last_podr;
                    t.Dolg = N.Last_dolg;
                    t.FIO = N.FIO;
                    t.Types = N.Types;
                    t.Zvanie = N.Zvanie;
                    t.Lnumber = N.Lnumber;
                    t.Date = Date();
                    list_perev.Add(t);
                    list_Anshtat.Remove(N);
                    list_Anshtat2.Remove(N);
                    Izmen td = new Izmen();
                    td.Types = "Перевод";
                    td.New_value = "|" + t.Lnumber + "|" + t.FIO + "|" + t.Zvanie + "|" + t.Types + "|" + t.Dolg + "|" + t.Podr + "|" + t.Date;
                    SendMessage("izm|" + td.Types + "|" + td.Row + "|" + td.Name_table + "|" + td.Old_value + "|" + td.New_value + "|" + td.Col);
                }
            }
            if (Dop == "5" || Dop == "6")
            {
                if (N.Zvanie == "рядовой" && N.Types == "контр" || N.Zvanie == "рядовой" && N.Types == "жен" || N.Zvanie == "ефрейтор" && N.Types == "контр" || N.Zvanie == "ефрейтор" && N.Types == "жен" || N.Zvanie == "младший сержант" && N.Types == "контр" || N.Zvanie == "младший сержант" && N.Types == "жен" || N.Zvanie == "сержант" && N.Types == "контр" || N.Zvanie == "сержант" && N.Types == "жен" || N.Zvanie == "старший сержант" && N.Types == "контр" || N.Zvanie == "старший сержант" && N.Types == "жен" || N.Zvanie == "старшина" && N.Types == "контр" || N.Zvanie == "старшина" && N.Types == "жен")
                {
                    OpenLoadPerevod t = new OpenLoadPerevod();
                    t.Podr = N.Last_podr;
                    t.Dolg = N.Last_dolg;
                    t.FIO = N.FIO;
                    t.Types = N.Types;
                    t.Zvanie = N.Zvanie;
                    t.Lnumber = N.Lnumber;
                    t.Date = Date();
                    list_perev.Add(t);
                    list_Anshtat.Remove(N);
                    list_Anshtat2.Remove(N);
                    Izmen td = new Izmen();
                    td.Types = "Перевод";
                    td.New_value = "|" + t.Lnumber + "|" + t.FIO + "|" + t.Zvanie + "|" + t.Types + "|" + t.Dolg + "|" + t.Podr + "|" + t.Date;
                    SendMessage("izm|" + td.Types + "|" + td.Row + "|" + td.Name_table + "|" + td.Old_value + "|" + td.New_value + "|" + td.Col);
                }
            }
            if (Dop == "4")
            {
                if (N.Zvanie == "старшина" || N.Zvanie == "прапорщик" || N.Zvanie == "старший прапорщик")
                {
                    OpenLoadPerevod t = new OpenLoadPerevod();
                    t.Podr = N.Last_podr;
                    t.Dolg = N.Last_dolg;
                    t.FIO = N.FIO;
                    t.Types = N.Types;
                    t.Zvanie = N.Zvanie;
                    t.Lnumber = N.Lnumber;
                    t.Date = Date();
                    list_perev.Add(t);
                    list_Anshtat.Remove(N);
                    list_Anshtat2.Remove(N);
                    Izmen td = new Izmen();
                    td.Types = "Перевод";
                    td.New_value = "|" + t.Lnumber + "|" + t.FIO + "|" + t.Zvanie + "|" + t.Types + "|" + t.Dolg + "|" + t.Podr + "|" + t.Date;
                    SendMessage("izm|" + td.Types + "|" + td.Row + "|" + td.Name_table + "|" + td.Old_value + "|" + td.New_value + "|" + td.Col);
                }
            }
            if (Dop == "3")
            {
                if (N.Zvanie == "лейтенант" || N.Zvanie == "старший лейтенант" || N.Zvanie == "капитан" || N.Zvanie == "майор" || N.Zvanie == "подполковник" || N.Zvanie == "полковник" || N.Zvanie == "генерал-лейтенант")
                {
                    OpenLoadPerevod t = new OpenLoadPerevod();
                    t.Podr = N.Last_podr;
                    t.Dolg = N.Last_dolg;
                    t.FIO = N.FIO;
                    t.Types = N.Types;
                    t.Zvanie = N.Zvanie;
                    t.Lnumber = N.Lnumber;
                    t.Date = Date();
                    list_perev.Add(t);
                    list_Anshtat.Remove(N);
                    list_Anshtat2.Remove(N);
                    Izmen td = new Izmen();
                    td.Types = "Перевод";
                    td.New_value = "|" + t.Lnumber + "|" + t.FIO + "|" + t.Zvanie + "|" + t.Types + "|" + t.Dolg + "|" + t.Podr + "|" + t.Date;
                    SendMessage("izm|" + td.Types + "|" + td.Row + "|" + td.Name_table + "|" + td.Old_value + "|" + td.New_value + "|" + td.Col);
                }
            }
        }
        public void New_otbor(Otbor_na_kontr tmp)
        {
            if (Dop == "7")
            {
                list_Otob.Add(tmp);
                Izmen T = new Izmen();
                T.Types = "Отбор на контракт";
                T.Name_table = "Отобранные";
                T.New_value = tmp.FIO;
                T.Col = tmp.Blank + ">" + tmp.Date1 + ">" + tmp.Date2 + ">" + tmp.Date3 + ">" + tmp.Date4 + ">" + tmp.Date_ic + ">" + tmp.Dolg + ">" + tmp.FIZO + ">" + tmp.Ic + ">" + tmp.Lnumber + ">" + tmp.Podr + ">" + tmp.Primec + ">" + tmp.Prof + ">" + tmp.Raport + ">" + tmp.Types + ">" + tmp.Vvk + ">" + tmp.Zvanie;
                SendMessage("izm|" + T.Types + "|" + T.Row + "|" + T.Name_table + "|" + T.Old_value + "|" + T.New_value + "|" + T.Col);
            }
        }
        public void New_Naryad(Naryad tmp) //СОЗДАНИЕ НАРЯДА
        {
            if(Dop == "9")
            {
                foreach (Naryad d in list_Naryad)
                {
                    if (d.Lnumber == tmp.Lnumber) return;
                }
                list_Naryad.Add(tmp);
                Izmen tmpp = new Izmen();
                tmpp.Types = "Добавить наряд";
                tmpp.Name_table = "Наряд";
                tmpp.New_value = tmp.Lnumber + "/" + tmp.Podr + "/" + tmp.Types + "/" + tmp.FIO + "/" + tmp.Zvanie + "/" + tmp.Dolg + "/" + tmp.Key;
                SendMessage("izm|" + tmpp.Types + "|" + tmpp.Row + "|" + tmpp.Name_table + "|" + tmpp.Old_value + "|" + tmpp.New_value + "|" + tmpp.Col);
            }
            
        }
        public void Create_viborka(OpenLoad tmp, string num)//ФУНКЦИЯ ВЫБОРА ИЗ ШТАТА
        {
            bool q = false;
            if (num == "+")
            {
                foreach (OpenLoad m in list_Vibor)
                {
                    if (tmp.Lnumber != m.Lnumber)
                    {
                        q = true;
                    }
                    else
                    {
                        q = false;
                        return;
                    }
                }
                if (list_Vibor.Count == 0) q = true;
                if (q == true)
                    list_Vibor.Add(tmp);
            }
            else
            {
                if (num == "-")
                {
                    foreach (OpenLoad m in list_Vibor)
                    {
                        if (tmp.Lnumber == m.Lnumber)
                        {
                            list_Vibor.Remove(m);
                            return;
                        }
                    }
                }
            }

        }
        public string Date() 
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
        }//Функции ДАТ
        public void Perevod(OpenLoad tmp) 
        {
            OpenLoadPerevod t = new OpenLoadPerevod();
            t.Dolg = tmp.Dolgnost;
            t.Podr = tmp.Podr;
            t.Lnumber = tmp.Lnumber;
            t.FIO = tmp.Fio;
            t.Zvanie = tmp.Zvanie;
            t.Types = tmp.Types;
            t.Date = Date();
            Izmen tmpp = new Izmen();
            tmpp.Types = "Перевод";
            tmpp.New_value = t.Podr + "|" + t.Dolg + "|" + t.Lnumber + "|" + t.FIO + "|" + t.Zvanie + "|" + t.Types + "|" + t.Date;
            SendMessage("izm|" + tmpp.Types + "|" + tmpp.Row + "|" + tmpp.Name_table + "|" + tmpp.Old_value + "|" + tmpp.New_value + "|" + tmpp.Col);
        }//Отработка перевода
        private void lic_RSZ(string[] word)
        {
            lic_rsz tmp = new lic_rsz();
            int g = 0;
            for (int i = 1; i < word.Length; i++)
            {

                int j = i;
                while (j > 41)
                {
                    j -= 41;
                }
                if (j == 1)
                {
                    tmp = new lic_rsz();
                    tmp.Podr = word[i];
                }
                if (j == 2)
                {
                    tmp.shtat_of = int.Parse(word[i]);
                }
                if (j == 3)
                {
                    tmp.shtat_prap = int.Parse(word[i]);
                }
                if (j == 4)
                {
                    tmp.shtat_ser = int.Parse(word[i]);
                }
                if (j == 5)
                {
                    tmp.shtat_sold = int.Parse(word[i]);
                }
                if (j == 6)
                {
                    tmp.shtat_vsego = int.Parse(word[i]);
                }
                if (j == 7)
                {
                    tmp.spisok_of = int.Parse(word[i]);
                }
                if (j == 8)
                {
                    tmp.spisok_prap = int.Parse(word[i]);
                }
                if (j == 9)
                {
                    tmp.spisok_serg_kontr = int.Parse(word[i]);
                }
                if (j == 10)
                {
                    tmp.spisok_serg_priz = int.Parse(word[i]);
                }
                if (j == 11)
                {
                    tmp.spisok_serg_vsego = int.Parse(word[i]);
                }
                if (j == 12)
                {
                    tmp.spisok_sold_kontr = int.Parse(word[i]);
                }
                if (j == 13)
                {
                    tmp.spisok_sold_priz = int.Parse(word[i]);
                }
                if (j == 14)
                {
                    tmp.spisok_sold_vsego = int.Parse(word[i]);
                }
                if (j == 15)
                {
                    tmp.spisok_serg_sold_kontr = int.Parse(word[i]);
                }
                if (j == 16)
                {
                    tmp.spisok_serg_sold_priz = int.Parse(word[i]);
                }
                if (j == 17)
                {
                    tmp.spisok_serg_sold_vsego = int.Parse(word[i]);
                }
                if (j == 18)
                {
                    tmp.spisok_ukompl = int.Parse(word[i]);
                }
                if (j == 19)
                {
                    tmp.spisok_vsego = int.Parse(word[i]);
                }
                if (j == 20)
                {
                    tmp.lico_of = int.Parse(word[i]);
                }
                if (j == 21)
                {
                    tmp.lico_prap = int.Parse(word[i]);
                }
                if (j == 22)
                {
                    tmp.lico_serg_kontr = int.Parse(word[i]);
                }
                if (j == 23)
                {
                    tmp.lico_serg_priz = int.Parse(word[i]);
                }
                if (j == 24)
                {
                    tmp.lico_serg_vsego = int.Parse(word[i]);
                }
                if (j == 25)
                {
                    tmp.lico_sold_kontr = int.Parse(word[i]);
                }
                if (j == 26)
                {
                    tmp.lico_sold_priz = int.Parse(word[i]);
                }
                if (j == 27)
                {
                    tmp.lico_sold_vsego = int.Parse(word[i]);
                }
                if (j == 28)
                {
                    tmp.lico_serg_sold_kontr = int.Parse(word[i]);
                }
                if (j == 29)
                {
                    tmp.lico_serg_sold_priz = int.Parse(word[i]);
                }
                if (j == 30)
                {
                    tmp.lico_serg_sold_vsego = int.Parse(word[i]);
                }
                if (j == 31)
                {
                    tmp.lico_vsego = int.Parse(word[i]);
                }
                if (j == 32)
                {
                    tmp.ots_arest = int.Parse(word[i]);
                }
                if (j == 33)
                {
                    tmp.ots_dr_pr = int.Parse(word[i]);
                }
                if (j == 34)
                {
                    tmp.ots_hospital = int.Parse(word[i]);
                }
                if (j == 35)
                {
                    tmp.ots_kom = int.Parse(word[i]);
                }
                if (j == 36)
                {
                    tmp.ots_med = int.Parse(word[i]);
                }
                if (j == 37)
                {
                    tmp.ots_naryad = int.Parse(word[i]);
                }
                if (j == 38)
                {
                    tmp.ots_otpusk = int.Parse(word[i]);
                }
                if (j == 39)
                {
                    tmp.ots_soch = int.Parse(word[i]);
                }
                if (j == 40)
                {
                    tmp.ots_vsego = int.Parse(word[i]);
                    list_rsz_lic.Add(tmp);
                }

            }
        }//Лицевая сторона РСЗ
        private void shtat_pol(string[] word, string dop) //Получение штата
        {
            OpenLoad tmp = new OpenLoad();
            int g = 0;
            for (int i = 1; i < word.Length; i++)
            {

                int j = i;
                while (j > 13)
                {
                    j -= 13;
                }
                if (j == 1)
                {
                    tmp = new OpenLoad();
                    tmp.Id = int.Parse(word[i]);
                    tmp.Counter = int.Parse(word[i]);
                }
                if (j == 2)
                {
                    tmp.Podr = word[i];
                    string[] word1 = word[i].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    tmp.Podr1 = word1[0];
                    if (word1.Length == 1) { tmp.Podr2 = "управ"; } else { tmp.Podr2 = word1[1]; }
                }
                if (j == 3)
                {
                    tmp.Dolgnost = word[i];
                }
                if (j == 4)
                {
                    tmp.Shtat_type = word[i];
                }
                if (j == 5)
                {
                    tmp.Vus = word[i];
                }
                if (j == 6)
                {
                    tmp.Kod = word[i];
                }
                if (j == 7)
                {
                    if (word[i] == "0") tmp.Lnumber = ".";
                    else
                        tmp.Lnumber = word[i];
                }
                if (j == 8)
                {
                    if (word[i] == "0") tmp.Zvanie = ".";
                    else
                        tmp.Zvanie = word[i];
                }
                if (j == 9)
                {
                    if (word[i] == "0") tmp.Fio = "-В-";
                    else
                        tmp.Fio = word[i];
                }
                if (j == 10)
                {
                    if (word[i] == "0") tmp.Types = ".";
                    else
                        tmp.Types = word[i];
                }
                if (j == 11)
                {
                    if (word[i] == "0") tmp.Otryv_status = ".";
                    else
                        tmp.Otryv_status = word[i];
                }
                if (j == 12)
                {
                    if (word[i] == "0") tmp.Otryv_primech = ".";
                    else
                        tmp.Otryv_primech = word[i];
                }
                if (j == 13)
                {
                    if (word[i] == "0") tmp.Otryv_pricaz = ".";
                    else
                        tmp.Otryv_pricaz = word[i];
                    list_shtat.Add(tmp);
                }

            }
            if (dop == "8") //Приказ по допуску
            {
                int ji = 0, nn = 0;
                int N = list_shtat.Count;
                for (int i = 0; i < N; i++)
                {
                    if (list_shtat[nn].Lnumber == ".")
                    {
                        list_shtat.Remove(list_shtat[nn]);
                    }
                    else { nn++; }
                }
                foreach (OpenLoad p in list_shtat)
                {
                    p.Counter = ji + 1;
                    ji += 1;
                }
            }
            if (dop == "9") //Наряд по допуску
            {
                int ji = 0, nn = 0;
                int N = list_shtat.Count;
                for (int i = 0; i < N; i++)
                {
                    if (list_shtat[nn].Lnumber == ".")
                    {
                        list_shtat.Remove(list_shtat[nn]);

                    }
                    else
                    if (list_shtat[nn].Otryv_status != ".")
                    {
                        list_shtat.Remove(list_shtat[nn]);
                    }
                    else { nn++; }
                }
                foreach (OpenLoad p in list_shtat)
                {
                    p.Counter = ji + 1;
                    ji += 1;
                }
            }
            if (dop == "4") //Прапорский штат по допуску
            {
                int ji = 0, nn = 0;
                int N = list_shtat.Count;
                for (int i = 0; i < N; i++)
                {
                    if (list_shtat[nn].Shtat_type != "пр-к")
                    {
                        list_shtat.Remove(list_shtat[nn]);
                    }
                    else { nn++; }
                }
                foreach (OpenLoad p in list_shtat)
                {
                    p.Counter = ji + 1;
                    ji += 1;
                }
            }
            if (dop == "3") //офицерский штат по допуску
            {
                int ji = 0, nn = 0;
                int N = list_shtat.Count;
                for (int i = 0; i < N; i++)
                {
                    if (list_shtat[nn].Shtat_type != "О")
                    {
                        list_shtat.Remove(list_shtat[nn]);
                    }
                    else { nn++; }
                }
                foreach (OpenLoad p in list_shtat)
                {
                    p.Counter = ji + 1;
                    ji += 1;
                }
            }
            if (dop == "5" || dop == "6") //сер и ряд штат по допуску
            {
                int ji = 0, nn = 0;
                int N = list_shtat.Count;
                for (int i = 0; i < N; i++)
                {
                    if (list_shtat[nn].Shtat_type != "ряд" && list_shtat[nn].Shtat_type != "сер")
                    {
                        list_shtat.Remove(list_shtat[nn]);
                    }
                    else { nn++; }
                }
                foreach (OpenLoad p in list_shtat)
                {
                    p.Counter = ji + 1;
                    ji += 1;
                }
            }
            if (dop == "10") //гражданский штат по допуску
            {
                int ji = 0, nn = 0;
                int N = list_shtat.Count;
                for (int i = 0; i < N; i++)
                {
                    if (list_shtat[nn].Shtat_type != "г/п")
                    {
                        list_shtat.Remove(list_shtat[nn]);
                    }
                    else { nn++; }
                }
                foreach (OpenLoad p in list_shtat)
                {
                    p.Counter = ji + 1;
                    ji += 1;
                }
            }
            var list = list_shtat.Select(e => e.Fio).Distinct();
            foreach(string n in list)
            {
                FIO_VRIO.Add(n);
            }
        }
        private void anshtat_pol(string[] word, string dop)
        {

            OpenLoadZaShtat tmp = new OpenLoadZaShtat();
            int ij = 1;
            for (int i = 1; i < word.Length; i++)
            {

                int j = i;
                while (j > 6)
                {
                    j -= 6;
                }
                if (j == 1)
                {
                    tmp = new OpenLoadZaShtat();
                    tmp.Lnumber = word[i];
                    tmp.Id = ij;
                    ij++;
                }
                if (j == 2)
                {
                    tmp.FIO = word[i];
                }
                if (j == 3)
                {
                    tmp.Types = word[i];
                }
                if (j == 4)
                {
                    tmp.Zvanie = word[i];
                }
                if (j == 5)
                {
                    tmp.Last_dolg = word[i];
                }
                if (j == 6)
                {
                    tmp.Last_podr = word[i];
                    list_Anshtat.Add(tmp);
                }

            }
            if (dop == "4") //Прапорский штат по допуску
            {
                foreach(OpenLoadZaShtat t in list_Anshtat)
                {
                    if(t.Zvanie == "старшина" || t.Zvanie == "прапорщик" || t.Zvanie == "старший прапорщик")
                    {
                        list_Anshtat2.Add(t);
                    }
                }
            }
            if (dop == "3") //офицерский штат по допуску
            {
                foreach (OpenLoadZaShtat t in list_Anshtat)
                {
                    if (t.Zvanie == "лейтенант" || t.Zvanie == "старший лейтенант" || t.Zvanie == "капитан" || t.Zvanie == "майор" || t.Zvanie == "подполковник" || t.Zvanie == "полковник" || t.Zvanie == "генерал-лейтенант" || t.Zvanie == "генерал-майор" )
                    {
                        list_Anshtat2.Add(t);
                    }
                }
            }
            if (dop == "5" || dop == "6") //сержанты и солдаты по допуску
            {
                foreach (OpenLoadZaShtat t in list_Anshtat)
                {
                    if (t.Zvanie == "рядовой" && t.Types == "контр" || t.Zvanie == "рядовой" && t.Types == "жен" || t.Zvanie == "ефрейтор" && t.Types == "контр" || t.Zvanie == "ефрейтор" && t.Types == "жен" || t.Zvanie == "младший сержант" && t.Types == "контр" || t.Zvanie == "младший сержант" && t.Types == "жен" || t.Zvanie == "сержант" && t.Types == "контр" || t.Zvanie == "сержант" && t.Types == "жен" || t.Zvanie == "старший сержант" && t.Types == "контр" || t.Zvanie == "старший сержант" && t.Types == "жен" || t.Zvanie == "старшина" && t.Types == "контр" || t.Zvanie == "старшина" && t.Types == "жен")
                    {
                        list_Anshtat2.Add(t);
                    }
                }
            }
            if (dop == "7") //срочники по допуску
            {
                foreach (OpenLoadZaShtat t in list_Anshtat)
                {
                    if (t.Zvanie == "рядовой" && t.Types != "контр" || t.Zvanie == "ефрейтор" && t.Types != "контр" || t.Zvanie == "младший сержант" && t.Types != "контр")
                    {
                        list_Anshtat2.Add(t);
                    }
                }
            }
        }//Получение заштатников
        // Звуковое оповещение о перехваченной ошибке.
        void ErrorSound()
        {
            Console.Beep(2000, 80);
            Console.Beep(3000, 120);
        }

        #endregion
        #region Набор на контракт
        public void Pered_vkadry(Otbor_na_kontr tmp)
        {
            tmp.Date2 = Date();
            list_Pered.Add(tmp);
            Izmen T = new Izmen();
            T.Types = "Отбор на контракт";
            T.Name_table = "Переданные";
            T.New_value = tmp.FIO;
            T.Col = tmp.Blank + ">" + tmp.Date1 + ">" + tmp.Date2 + ">" + tmp.Date3 + ">" + tmp.Date4 + ">" + tmp.Date_ic + ">" + tmp.Dolg + ">" + tmp.FIZO + ">" + tmp.Ic + ">" + tmp.Lnumber + ">" + tmp.Podr + ">" + tmp.Primec + ">" + tmp.Prof + ">" + tmp.Raport + ">" + tmp.Types + ">" + tmp.Vvk + ">" + tmp.Zvanie;
            SendMessage("izm|" + T.Types + "|" + T.Row + "|" + T.Name_table + "|" + T.Old_value + "|" + T.New_value + "|" + T.Col);
            list_Otob.Remove(tmp);
        }
        public void OK_sost(Otbor_na_kontr tmp)
        {
            tmp.Date3 = Date();
            list_Sost.Add(tmp);
            Izmen T = new Izmen();
            T.Types = "Отбор на контракт";
            T.Name_table = "Состоялись";
            T.New_value = tmp.FIO;
            T.Col = tmp.Blank + ">" + tmp.Date1 + ">" + tmp.Date2 + ">" + tmp.Date3 + ">" + tmp.Date4 + ">" + tmp.Date_ic + ">" + tmp.Dolg + ">" + tmp.FIZO + ">" + tmp.Ic + ">" + tmp.Lnumber + ">" + tmp.Podr + ">" + tmp.Primec + ">" + tmp.Prof + ">" + tmp.Raport + ">" + tmp.Types + ">" + tmp.Vvk + ">" + tmp.Zvanie;
            SendMessage("izm|" + T.Types + "|" + T.Row + "|" + T.Name_table + "|" + T.Old_value + "|" + T.New_value + "|" + T.Col);
            list_Pered.Remove(tmp);
        }
        public void OK_otkaz(Otbor_na_kontr tmp, int num)
        {

            if (num == 1)
            {
                tmp.Date4 = Date();
                list_Otkaz.Add(tmp);
                Izmen T = new Izmen();
                T.Types = "Отбор на контракт";
                T.Name_table = "Отказ";
                T.New_value = tmp.FIO;
                T.Col = tmp.Blank + ">" + tmp.Date1 + ">" + tmp.Date2 + ">" + tmp.Date3 + ">" + tmp.Date4 + ">" + tmp.Date_ic + ">" + tmp.Dolg + ">" + tmp.FIZO + ">" + tmp.Ic + ">" + tmp.Lnumber + ">" + tmp.Podr + ">" + tmp.Primec + ">" + tmp.Prof + ">" + tmp.Raport + ">" + tmp.Types + ">" + tmp.Vvk + ">" + tmp.Zvanie;
                SendMessage("izm|" + T.Types + "|" + T.Row + "|" + T.Name_table + "|" + T.Old_value + "|" + T.New_value + "|" + T.Col);
                list_Otob.Remove(tmp);
            }
            else
                if (num == 2)
            {
                tmp.Date4 = Date();
                list_Otkaz.Add(tmp);
                Izmen T = new Izmen();
                T.Types = "Отбор на контракт";
                T.Name_table = "Отказ";
                T.New_value = tmp.FIO;
                T.Col = tmp.Blank + ">" + tmp.Date1 + ">" + tmp.Date2 + ">" + tmp.Date3 + ">" + tmp.Date4 + ">" + tmp.Date_ic + ">" + tmp.Dolg + ">" + tmp.FIZO + ">" + tmp.Ic + ">" + tmp.Lnumber + ">" + tmp.Podr + ">" + tmp.Primec + ">" + tmp.Prof + ">" + tmp.Raport + ">" + tmp.Types + ">" + tmp.Vvk + ">" + tmp.Zvanie;
                SendMessage("izm|" + T.Types + "|" + T.Row + "|" + T.Name_table + "|" + T.Old_value + "|" + T.New_value + "|" + T.Col);
                list_Pered.Remove(tmp);
            }

        }
        public void OK_Snowa_otbor(Otbor_na_kontr tmp)
        {

            tmp.Date1 = Date();
            list_Otob.Add(tmp);
            Izmen T = new Izmen();
            T.Types = "Отбор на контракт";
            T.Name_table = "Вновь отобранные";
            T.New_value = tmp.FIO;
            T.Col = tmp.Blank + ">" + tmp.Date1 + ">" + tmp.Date2 + ">" + tmp.Date3 + ">" + tmp.Date4 + ">" + tmp.Date_ic + ">" + tmp.Dolg + ">" + tmp.FIZO + ">" + tmp.Ic + ">" + tmp.Lnumber + ">" + tmp.Podr + ">" + tmp.Primec + ">" + tmp.Prof + ">" + tmp.Raport + ">" + tmp.Types + ">" + tmp.Vvk + ">" + tmp.Zvanie;
            SendMessage("izm|" + T.Types + "|" + T.Row + "|" + T.Name_table + "|" + T.Old_value + "|" + T.New_value + "|" + T.Col);
            list_Otkaz.Remove(tmp);
        }
        #endregion
        #region Работа со штатом
        public void Create_Shtat_People(int id, string lnumber)
        {
            Izmen T = new Izmen();
            foreach (OpenLoad N in List_shtat)
            {
                if (N.Id == id)
                {
                    foreach (OpenLoadZaShtat a in List_AnShtat)
                    {
                        if (a.Lnumber == lnumber)
                        {
                            if (Dop == "5" || Dop == "4" || Dop == "6")
                            {
                                if (a.Types != "контр" && a.Types != "жен") return;
                            }
                            if (Dop == "3")
                            {
                                if (a.Types != "О") return;
                            }
                            if (Dop == "7")
                            {
                                if (a.Types == "О" || a.Types == "контр" || a.Types == "жен") return;
                            }
                            if (Dop == "8" || Dop == "9" || Dop == "10") return;
                            T.Types = "Добавить";
                            T.Name_table = "Штатка";
                            T.Row = N.Id;
                            T.Old_value = N.Lnumber;
                            T.New_value = a.Lnumber;
                            T.Col = a.FIO + ">" + a.Types + ">" + a.Zvanie;

                            N.Lnumber = a.Lnumber;
                            N.Zvanie = a.Zvanie;
                            N.Fio = a.FIO;
                            N.Types = a.Types;

                            foreach (OpenLoadKotel K in list_Kotel1)
                            {
                                if (K.Lnumber == a.Lnumber)
                                {
                                    K.Podr = N.Podr1;
                                    kotel2();
                                }

                            }
                            if (list_Per.Count != 0)
                            {
                                foreach (Perest Z in list_Per)
                                {
                                    if (Z.Lnumber == a.Lnumber)
                                    {
                                        if (Z.Old_dolg != "")
                                        {
                                            if (Z.New_dolg != "")
                                            {
                                                Perest tmp = new Perest();
                                                tmp.FIO = a.FIO;
                                                tmp.Lnumber = a.Lnumber;
                                                tmp.Zvanie = a.Zvanie;
                                                tmp.New_dolg = N.Dolgnost;
                                                tmp.New_podr = N.Podr;
                                                tmp.VUS = N.Vus;
                                                tmp.Kod = N.Kod;
                                                list_Per.Add(tmp);
                                                break;
                                            }
                                            else
                                            {
                                                Z.New_dolg = N.Dolgnost;
                                                Z.New_podr = N.Podr;
                                                Z.Kod = N.Kod;
                                                Z.VUS = N.Vus;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Perest tmp = new Perest();
                                        tmp.FIO = a.FIO;
                                        tmp.Lnumber = a.Lnumber;
                                        tmp.Zvanie = a.Zvanie;
                                        tmp.New_dolg = N.Dolgnost;
                                        tmp.New_podr = N.Podr;
                                        tmp.VUS = N.Vus;
                                        tmp.Kod = N.Kod;
                                        list_Per.Add(tmp);
                                        break;
                                    }
                                    // break;
                                }
                            }
                            else
                            {
                                Perest tmp = new Perest();
                                tmp.FIO = a.FIO;
                                tmp.Lnumber = a.Lnumber;
                                tmp.Zvanie = a.Zvanie;
                                tmp.New_dolg = N.Dolgnost;
                                tmp.New_podr = N.Podr;
                                tmp.VUS = N.Vus;
                                tmp.Kod = N.Kod;
                                list_Per.Add(tmp);
                            }



                            List_AnShtat.Remove(a);
                            list_Anshtat2.Remove(a);
                            int ii = 1;
                            foreach(OpenLoadZaShtat p in list_Anshtat2)
                            {
                                p.Id = ii;
                                ii++;
                            }
                            ii = 1;
                            foreach (OpenLoadZaShtat p in list_Anshtat)
                            {
                                p.Id = ii;
                                ii++;
                            }
                            break;
                        }
                    }
                    int i = 1;
                    foreach(OpenLoadZaShtat t in List_AnShtat)
                    {
                        t.Id = i;
                        i++;
                    }
                    break;
                }
            }

            SendMessage("izm|" + T.Types + "|" + T.Row + "|" + T.Name_table + "|" + T.Old_value + "|" + T.New_value + "|" + T.Col);
            //Тут мы ставим в штат
            //Тут мы записываем перестановку
            //Тут мы проверяем суточку
            //Тут мы переставляем рсз
            //тут мы записываем весь шлак в формат изменений
            //Тут мы записываем в личное дело

        }
        public void _Delete_people(OpenLoad NN)
        {
            Naryad tmp = new Naryad();
            tmp.Lnumber = NN.Lnumber;
            tmp.Podr = NN.Podr1;
            tmp.Types = NN.Types;
            tmp.FIO = NN.Fio;
            tmp.Zvanie = NN.Zvanie;
            tmp.Dolg = NN.Dolgnost;
            New_Naryad(tmp);
            Izmen tmpp = new Izmen();
            tmpp.Types = "Добавить наряд";
            tmpp.Name_table = "Наряд";
            tmpp.New_value = NN.Lnumber + "/" + NN.Podr + "/" + NN.Types + "/" + NN.Fio + "/" + NN.Zvanie + "/" + NN.Dolgnost + "/" + tmp.Id;
            SendMessage("izm|" + tmpp.Types + "|" + tmpp.Row + "|" + tmpp.Name_table + "|" + tmpp.Old_value + "|" + tmpp.New_value + "|" + tmpp.Col);
        }
        public void _Pered_ld(string message)
        {
            int j = 0;
            int i = 0;
            try
            {
                LD_Dan = new Dannie();
                ld_doc = new ObservableCollection<Docum>();
                ld_doc1 = new ObservableCollection<Docum>();
                ld_fam = new ObservableCollection<family>();
                ld_kon = new ObservableCollection<Kontrakt>();
                ld_nag = new ObservableCollection<Nagrad>();
                LD_Naz = new Naznach();
                LD_PD = new PriemDel();
                LD_Ph = new Photo_();
                ld_poslug = new ObservableCollection<Poslug>();
                LD_Viz = new Vizhivanie();
                ld_zvanie = new ObservableCollection<zvanie>();
                zvanie t_z = new zvanie();
                Docum t_d = new Docum();
                family t_f = new family();
                Kontrakt t_k = new Kontrakt();
                Nagrad t_n = new Nagrad();
                Poslug t_p = new Poslug();
                int types = 0;
                string[] word = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                for (i = 0; i < word.Count(); i++)
                {
                    if (word[i] == "Zvanie")
                    {
                        j = 0;
                        types = 1;
                    }
                    if (word[i] == "Poslug")
                    {
                        j = 0;
                        types = 2;
                    }
                    if (word[i] == "Fam")
                    {
                        j = 0;
                        types = 3;
                    }
                    if (word[i] == "Nagrad")
                    {
                        j = 0;
                        types = 4;
                    }
                    if (word[i] == "Foto")
                    {
                        j = 0;
                        types = 5;
                        //if (word[i + 1] != ".") LD_Ph.Date1 = LD_Ph.LoadImage(LD_Ph.StringToByteArray(word[i + 1]));
                        //if (word[i + 2] != ".") LD_Ph.Date2 = LD_Ph.LoadImage(LD_Ph.StringToByteArray(word[i + 2]));
                        //if (word[i + 3] != ".") LD_Ph.Date3 = LD_Ph.LoadImage(LD_Ph.StringToByteArray(word[i + 3]));
                        //if (word[i + 4] != ".") LD_Ph.Date4 = LD_Ph.LoadImage(LD_Ph.StringToByteArray(word[i + 4]));
                        //if (word[i + 5] != ".") LD_Ph.Date5 = LD_Ph.LoadImage(LD_Ph.StringToByteArray(word[i + 5]));
                    }
                    if (word[i] == "Docum")
                    {
                        j = 0;
                        types = 6;
                    }
                    if (word[i] == "Dan")
                    {
                        j = 0;
                        types = 7;
                        if (word[i + 1] != ".") LD_Dan.Lnumber = word[i + 1];
                        if (word[i + 2] != ".") LD_Dan.FIO = word[i + 2];
                        if (word[i + 3] != ".") LD_Dan.Bank_card = word[i + 3];
                        if (word[i + 4] != ".") LD_Dan.Date_brd = word[i + 4];
                        if (word[i + 5] != ".") LD_Dan.Gr_obr = word[i + 5];
                        if (word[i + 6] != ".") LD_Dan.Home_adres = word[i + 6];
                        if (word[i + 7] != ".") LD_Dan.Mesto_brd = word[i + 7];
                        if (word[i + 8] != ".") LD_Dan.Nac = word[i + 8];
                        if (word[i + 9] != ".") LD_Dan.PAC = word[i + 9];
                        if (word[i + 10] != ".") LD_Dan.POL = word[i + 10];
                        if (word[i + 11] != ".") LD_Dan.Vod_prava = word[i + 11];
                        if (word[i + 12] != ".") LD_Dan.Voenkomat = word[i + 12];
                        if (word[i + 13] != ".") LD_Dan.Voen_obr = word[i + 13];
                        if (word[i + 14] != ".") LD_Dan.Types = word[i + 14];
                    }
                    if (word[i] == "Kontr")
                    {
                        j = 0;
                        types = 8;
                    }
                    if (word[i] == "Naznac")
                    {
                        j = 0;
                        types = 9;
                        if (word[i + 1] != ".") LD_Naz.pricaz = word[i + 1];
                        if (word[i + 2] != ".") LD_Naz.who_pricaz = word[i + 2];
                        if (word[i + 3] != ".") LD_Naz.date_pricaz = word[i + 3];
                    }
                    if (word[i] == "Priem")
                    {
                        j = 0;
                        types = 10;
                        if (word[i + 1] != ".") LD_PD.pricaz = word[i + 1];
                        if (word[i + 2] != ".") LD_PD.who_pricaz = word[i + 2];
                        if (word[i + 3] != ".") LD_PD.date_pricaz = word[i + 3];
                    }
                    if (word[i] == "Vizhiv")
                    {
                        j = 0;
                        types = 11;
                        if (word[i + 2] != ".") LD_Viz.Mesto = word[i + 2];
                        if (word[i + 1] != ".") LD_Viz.Period = word[i + 1];
                        if (word[i + 3] != ".") LD_Viz.Osnovanie = word[i + 3];
                    }
                    switch (types)
                    {
                        case 1: // zvanie
                            {
                                if (j == 0)
                                {
                                    j++;
                                    break;
                                }
                                if (j == 1)
                                {
                                    t_z = new zvanie();
                                    if (word[i] != ".") t_z.Name = word[i];
                                    j++; break;
                                }
                                if (j == 2)
                                {
                                    if (word[i] != ".") t_z.Nomber = int.Parse(word[i]);
                                    j++; break;
                                }
                                if (j == 3)
                                {
                                    if (word[i] != ".") t_z.Pr = int.Parse(word[i]);
                                    j++; break;
                                }
                                if (j == 4)
                                {
                                    if (word[i] != ".") t_z.What = word[i];
                                    j++; break;
                                }
                                if (j == 5)
                                {
                                    if (word[i] != ".") t_z.Date = word[i];
                                    j = 1;
                                    ld_zvanie.Add(t_z);
                                }
                                break;
                            }
                        case 2: //poslugnoy
                            {
                                if (j == 0)
                                {
                                    j++;
                                    break;
                                }
                                if (j == 1)
                                {
                                    t_p = new Poslug();
                                    if (word[i] != ".") t_p.Chast_ = word[i];
                                    j++; break;
                                }
                                if (j == 2)
                                {
                                    if (word[i] != ".") t_p.Date_pricaz = word[i];
                                    j++; break;
                                }
                                if (j == 3)
                                {
                                    if (word[i] != ".") t_p.Dolgnost_ = word[i];
                                    j++; break;
                                }
                                if (j == 4)
                                {
                                    if (word[i] != ".") t_p.Name_pricaz = word[i];
                                    j++; break;
                                }
                                if (j == 5)
                                {
                                    if (word[i] != ".") t_p.Nomber_pricaz = word[i];
                                    j++; break;
                                }
                                if (j == 6)
                                {
                                    if (word[i] != ".") t_p.Podr_ = word[i];
                                    j++; break;
                                }
                                if (j == 7)
                                {
                                    if (word[i] != ".") t_p.VUS_ = word[i];
                                    j = 1;
                                    ld_poslug.Add(t_p);
                                }
                                break;
                            }
                        case 3: //family
                            {
                                if (j == 0)
                                {
                                    j++;
                                    break;
                                }
                                if (j == 1)
                                {
                                    t_f = new family();
                                    if (word[i] != ".") t_f.Name = word[i];
                                    j++; break;
                                }
                                if (j == 2)
                                {
                                    if (word[i] != ".") t_f.types = word[i];
                                    j++; break;
                                }
                                if (j == 3)
                                {
                                    if (word[i] != ".") t_f.Brd = word[i];
                                    j = 1;
                                    ld_fam.Add(t_f);
                                }
                                break;
                            }
                        case 4: //nagrad
                            {
                                if (j == 0)
                                {
                                    j++;
                                    break;
                                }
                                if (j == 1)
                                {
                                    t_n = new Nagrad();
                                    if (word[i] != ".") t_n.Type = word[i];
                                    j++; break;
                                }
                                if (j == 2)
                                {
                                    if (word[i] != ".") t_n.Who_pricaz = word[i];
                                    j++; break;
                                }
                                if (j == 3)
                                {
                                    if (word[i] != ".") t_n.Pricaz = word[i];
                                    j++; break;
                                }
                                if (j == 4)
                                {
                                    if (word[i] != ".") t_n.Date_pricaz = word[i];
                                    j++; break;
                                }
                                if (j == 5)
                                {
                                    if (word[i] != ".") t_n.Date_ = word[i];
                                    j = 1;
                                    ld_nag.Add(t_n);
                                }
                                break;
                            }
                        case 6: //docum
                            {
                                if (j == 0)
                                {
                                    j++;
                                    break;
                                }
                                if (j == 1)
                                {
                                    t_d = new Docum();
                                    if (word[i] != ".") t_d.Kod = word[i];
                                    j++; break;
                                }
                                if (j == 2)
                                {
                                    if (word[i] != ".") t_d.Seriya = word[i];
                                    j++; break;
                                }
                                if (j == 3)
                                {
                                    if (word[i] != ".") t_d.Nomber = word[i];
                                    j++; break;
                                }
                                if (j == 4)
                                {
                                    if (word[i] != ".") t_d.Who_vidal = word[i];
                                    j++; break;
                                }
                                if (j == 5)
                                {
                                    if (word[i] != ".") t_d.Date_vid = word[i];
                                    j = 1;
                                    if(t_d.Kod == "01")
                                        ld_doc.Add(t_d);
                                    if (t_d.Kod == "04")
                                        ld_doc1.Add(t_d);
                                }
                                break;
                            }
                        case 8: //kontrakt
                            {
                                if (j == 0)
                                {
                                    j++;
                                    break;
                                }
                                if (j == 1)
                                {
                                    t_k = new Kontrakt();
                                    if (word[i] != ".") t_k.data_zakl = word[i];
                                    j++; break;
                                }
                                if (j == 2)
                                {
                                    if (word[i] != ".") t_k.data_okon = word[i];
                                    j++; break;
                                }
                                if (j == 3)
                                {
                                    if (word[i] != ".") t_k.Date_pricaz = word[i];
                                    j++; break;
                                }
                                if (j == 4)
                                {
                                    if (word[i] != ".") t_k.Nomber_pricaz = word[i];
                                    j++; break;
                                }
                                if (j == 5)
                                {
                                    if (word[i] != ".") t_k.types = word[i];
                                    j++; break;
                                }
                                if (j == 6)
                                {
                                    if (word[i] != ".") t_k.Who_pricaz = word[i];
                                    j = 1;
                                    ld_kon.Add(t_k);
                                }
                                break;
                            }
                    }
                    
                }               
            }
            catch { MessageBox.Show("Ошибка на шаге i=" + i + " и шаге j =" + j); }
        }
        public void Viborka() //Выборка для наряда
        {
            if (Dop == "9")
            {
                foreach (OpenLoad B in list_Vibor)
                {
                    Naryad t = new Naryad();
                    t.FIO = B.Fio;
                    t.Lnumber = B.Lnumber;
                    t.Zvanie = B.Zvanie;
                    t.Types = B.Types;
                    t.Podr = B.Podr;
                    t.Dolg = B.Dolgnost;
                    list_Naryad.Add(t);
                }

            }
        }
        public void zashtatLD(OpenLoadZaShtat t)
        {
            List_AnShtat.Add(t);
        }
        public void Del_Shtat_People(int id) //Удаление из штата в заштатники
        {
            Izmen T = new Izmen();
            foreach (OpenLoad N in List_shtat)
            {
                if (N.Id == id)
                {
                    if (Dop == "5" || Dop == "4" || Dop == "6")
                    {
                        if (N.Types != "контр" || N.Types != "жен") return;
                    }
                    if (Dop == "3")
                    {
                        if (N.Types != "О") return;
                    }
                    if (Dop == "7")
                    {
                        if (N.Types == "О" || N.Types == "контр" || N.Types == "жен") return;
                    }
                    if (Dop == "8" || Dop == "9" || Dop == "10") return;
                    foreach (OpenLoadKotel K in list_Kotel1)
                    {
                        if (K.Lnumber == N.Lnumber)
                        {
                            K.Podr = ".";
                            kotel2();
                        }
                            
                    }

                    T.Types = "Удалить";
                    T.Name_table = "Штатка";
                    T.Row = N.Id;
                    T.Old_value = N.Lnumber;
                    T.New_value = ".";
                    T.Col = N.Dolgnost + ">" + N.Podr + ">" + N.Fio + ">" + N.Zvanie + ">" + N.Types;

                    OpenLoadZaShtat tmp = new OpenLoadZaShtat();
                    tmp.Lnumber = N.Lnumber;
                    tmp.Zvanie = N.Zvanie;
                    tmp.FIO = N.Fio;
                    tmp.Types = N.Types;
                    tmp.Last_dolg = N.Dolgnost;
                    tmp.Last_podr = N.Podr;
                    List_AnShtat.Add(tmp);


                    Perest tmp1 = new Perest();
                    tmp1.FIO = N.Fio;
                    tmp1.Lnumber = N.Lnumber;
                    tmp1.Zvanie = N.Zvanie;
                    tmp1.Types = N.Types;
                    tmp1.Old_dolg = N.Dolgnost;
                    tmp1.Old_podr = N.Podr;
                    list_Per.Add(tmp1);


                    N.Zvanie = ".";
                    N.Types = ".";
                    N.Lnumber = ".";
                    N.Fio = "-В-";
                    N.Otryv_status = ".";
                    N.Otryv_primech = ".";
                    N.Otryv_pricaz = ".";
                    int ii = 1;
                    foreach (OpenLoadZaShtat p in list_Anshtat2)
                    {
                        p.Id = ii;
                        ii++;
                    }
                    ii = 1;
                    foreach (OpenLoadZaShtat p in list_Anshtat)
                    {
                        p.Id = ii;
                        ii++;
                    }
                    break;

                }
            }

            SendMessage("izm|" + T.Types + "|" + T.Row + "|" + T.Name_table + "|" + T.Old_value + "|" + T.New_value + "|" + T.Col);
            //Тут мы ставим в штат
            //Тут мы записываем перестановку
            //Тут мы проверяем суточку
            //Тут мы переставляем рсз
            //тут мы записываем весь шлак в формат изменений
            //Тут мы записываем в личное дело

        }
        public void new_ub(Ub tmp) //Новый экземпляр убытия
        {
            if(tmp.Date3 != ".")
            {
                Creat_kotel2(tmp, "Пребытие");
                Izmen tmpp = new Izmen();
                tmpp.Types = "Пребытие";
                list_preb.Add(tmp);
                tmpp.New_value = tmp.Cel + "*" + tmp.Date3 + "*" + tmp.Date_preb + "*" + tmp.Date_ub + "*" + tmp.Dolg + "*" + tmp.FIO + "*" + tmp.Lnumber + "*" + tmp.Mesto + "*" + tmp.Osn + "*" + tmp.Podr + "*" + tmp.Types + "*" + tmp.Types_ub + "*" + tmp.VPD_ST1 + "*" + tmp.VPD_ST2 + "*" + tmp.VPD_ST3 + "*" + tmp.VPD_ST4 + "*" + tmp.VPD_ST5 + "*" + tmp.VPD_ST6 + "*" + tmp.VPD_ST7 + "*" + tmp.Vrio + "*" + tmp.Zvanie;
                SendMessage("izm|" + tmpp.Types + "|" + tmpp.Row + "|" + tmpp.Name_table + "|" + tmpp.Old_value + "|" + tmpp.New_value + "|" + tmpp.Col);
                list_ub.Remove(tmp);
            }//Пребытие
            else
            {
                Creat_kotel2(tmp, "Убытие");
                Izmen tmpp = new Izmen();
                tmpp.Types = "Убытие";
                list_ub.Add(tmp);
                tmpp.New_value = tmp.Cel + "*" + tmp.Date3 + "*" + tmp.Date_preb + "*" + tmp.Date_ub + "*" + tmp.Dolg + "*" + tmp.FIO + "*" + tmp.Lnumber + "*" + tmp.Mesto + "*" + tmp.Osn + "*" + tmp.Podr + "*" + tmp.Types + "*" + tmp.Types_ub + "*" + tmp.VPD_ST1 + "*" + tmp.VPD_ST2 + "*" + tmp.VPD_ST3 + "*" + tmp.VPD_ST4 + "*" + tmp.VPD_ST5 + "*" + tmp.VPD_ST6 + "*" + tmp.VPD_ST7 + "*" + tmp.Vrio + "*" + tmp.Zvanie;
                SendMessage("izm|" + tmpp.Types + "|" + tmpp.Row + "|" + tmpp.Name_table + "|" + tmpp.Old_value + "|" + tmpp.New_value + "|" + tmpp.Col);
            }
        }


        #endregion
        public void otrab(string s)
        {
            if (s == "Наряд")
            {
                foreach(OpenLoad a in list_Vibor)
                {
                    if (a.Otryv_pricaz == ".")
                    {
                        Naryad t = new Naryad();
                        t.Dolg = a.Dolgnost;
                        t.FIO = a.Fio;
                        t.Lnumber = a.Lnumber;
                        t.Podr = a.Podr;
                        t.Types = a.Types;
                        t.Zvanie = a.Zvanie;
                        list_Naryad.Add(t);
                    }
                }
            }
            if (s == "Пребытие")
            {
                
            }
            if (s == "Убытие")
            {

            }
            if (s == "Отбор на контракт")
            {
                foreach(OpenLoad a in list_Vibor)
                {
                    if (a.Types != "0" && a.Types != "контр" && a.Types != "жен")
                    {
                        Otbor_na_kontr t = new Otbor_na_kontr();
                        t.Dolg = a.Dolgnost;
                        t.FIO = a.Fio;
                        t.Zvanie = a.Zvanie;
                        t.Types = a.Types;
                        t.Lnumber = a.Lnumber;
                        t.Podr = a.Podr;
                        list_Otob.Add(t);
                    }
                }

            }
        }
        public void Creat_kotel2(Ub tmp, string D)
        {
            string[] word = tmp.Podr.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string podr = word[0];
            Izmen izm = new Izmen();
            
            if(D == "Убытие")
            {
                foreach (OpenLoadKotel t in List_Kotel1)
                {
                    if (t.Lnumber == tmp.Lnumber)
                    {
                        izm.Types = "Котел";
                        izm.Name_table = "Убытие";
                        izm.New_value = tmp.Lnumber;
                        izm.Col = tmp.FIO + ">" + podr + ">" + tmp.Types;
                        List_Kotel1.Remove(t);
                        kotel2();
                        break;
                    }
                }
            }
            if (D == "Пребытие")
            {
                izm.Types = "Котел";
                izm.Name_table = "Пребытие";
                izm.New_value = tmp.Lnumber;
                izm.Col = tmp.FIO + ">" + podr + ">" + tmp.Types;
            }
            SendMessage("izm|" + izm.Types + "|" + izm.Row + "|" + izm.Name_table + "|" + izm.Old_value + "|" + izm.New_value + "|" + izm.Col);

        }
        public void Creat_kotel(OpenLoad tmp, string D)
        {
            
            Izmen izm = new Izmen();
            if (D == "Добавить")
            {
                foreach (OpenLoadKotel t in List_Kotel1)
                {
                    if (t.Lnumber == tmp.Lnumber) return;
                }
                izm.Types = "Котел";
                izm.Name_table = "Добавить";
                izm.New_value = tmp.Lnumber;
                izm.Col = tmp.Fio + ">" + tmp.Podr1 + ">" + tmp.Types;
                OpenLoadKotel tmpp = new OpenLoadKotel();
                tmpp.FIO = tmp.Fio;
                tmpp.Lnumber = tmp.Lnumber;
                tmpp.Podr = tmp.Podr1;
                tmpp.Types = tmp.Types;
                list_Kotel1.Add(tmpp);
                kotel2();
            }
            if (D == "Убрать")
            {
                foreach (OpenLoadKotel t in List_Kotel1)
                {
                    if (t.Lnumber == tmp.Lnumber)
                    {
                        izm.Types = "Котел";
                        izm.Name_table = "Убрать";
                        izm.New_value = tmp.Lnumber;
                        izm.Col = tmp.Fio + ">" + tmp.Podr1 + ">" + tmp.Types;
                        List_Kotel1.Remove(t);
                        kotel2();
                        break;
                    }
                }
            }
            
            SendMessage("izm|" + izm.Types + "|" + izm.Row + "|" + izm.Name_table + "|" + izm.Old_value + "|" + izm.New_value + "|" + izm.Col);

        }
        void preb_kotel(Izmen I)
        {
            string[] word = I.Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
            OpenLoadKotel k = new OpenLoadKotel();
            k.FIO = word[0];
            k.Lnumber = I.New_value;
            k.Types = I.Types;
            foreach(OpenLoad p in list_shtat)
            {
                if(p.Lnumber == I.New_value)
                {
                    k.Podr = p.Podr1;
                }
            }
            list_Kotel1.Add(k);
            kotel2();
        }

    }


}
