using New_Stroevaya_chast.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static New_Stroevaya_chast.Modal.Shtat;

namespace New_Stroevaya_chast.ViewModal
{
    public class TCP
    {
        static ObservableCollection<Izmen> _izm_list = new ObservableCollection<Izmen>();
        public ObservableCollection<Izmen> IzmenList
        {
            get { return _izm_list; }
            set { _izm_list = value; }
        }
        static ObservableCollection<Pol> _pol_list = new ObservableCollection<Pol>();
        public ObservableCollection<Pol> PolList
        {
            get { return _pol_list; }
            set { _pol_list = value; }
        }
        public string message_shtat = "";
        public string message_AnShtat = "";
        public string message_Del = "";
        public string message_Per = "";
        public string message_Isk = "";
        public string message_kotel = "";
        ObservableCollection<Dannie> MList_Kontrakt = new ObservableCollection<Dannie>();
        // Высокоуровневая надстройка для прослушивающего сокета
        TcpListener _server;

        // Количество принимаемых подключений к серверу
        const int MAXNUMCLIENTS = 5;

        // Высокоуровневая надстройка для сокетов обмена сообщений с 
        // клиентскими приложениями.
        TcpClient[] clients = new TcpClient[MAXNUMCLIENTS];

        // Счетчик подключенных клиентов
        int _countClient = 0;

        // Флаг мягкой остановки циклов и дополнительных потоков
        bool _stopNetwork;



        #region Управление серверным приложением

        private void buttonStart_Click(object sender, EventArgs e)
        {
            StartServer();
        }


        private void buttonSend_Click(object sender, EventArgs e)
        {
            //ВОТ ОТПРАВКА!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //string s = "сервер" + ": " + textBoxSend.Text;
            //SendToClients(s, -1);
        }


        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        /*private void TcpServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }*/

        #endregion


        #region Функциональная часть сетевой работы


        // Запуск сервера и вспомогательного потока акцептирования клиентских подключений
        // т.е. назначения сокетов ответственных за обмен сообщениями 
        // с соответствующим клиентским приложением
        public void StartServer()
        {
            // Предотвратим повторный запуск сервера
            if (_server == null)
            {
                // Блок перехвата исключений на случай запуска одновременно
                // двух серверных приложений с одинаковым портом.
                try
                {
                    _stopNetwork = false;
                    _countClient = 0;
                    //UpdateClientsDisplay();

                    int port = 15000;
                    _server = new TcpListener(IPAddress.Any, port);
                    _server.Start();


                    Thread acceptThread = new Thread(AcceptClients);
                    acceptThread.Start();
                    Zagruzchik();
                    // Визуальное оповещение, что сервер запущен
                    //this.BackColor = Color.FromArgb(150, 192, 255);

                }
                catch
                {
                    ErrorSound();
                }
            }
        }

        public void Zagruzchik()
        {
            OpenLoad t = new OpenLoad();
            message_shtat = t.OpenLoad1();
            OpenLoadZaShtat t1 = new OpenLoadZaShtat();
            message_AnShtat = t1.ColToString();
            OpenLoadDel t2 = new OpenLoadDel();
            message_Del = t2.ColToString();
            OpenLoadIskl t3 = new OpenLoadIskl();
            message_Isk = t3.ColToString();
            OpenLoadPerevod t4 = new OpenLoadPerevod();
            message_Per = t4.ColToString();
            Otbor_na_kontr t5 = new Otbor_na_kontr();
            message_Per = t5.D1();
            message_kotel = kotel();
            Pol n = new Pol();
            _pol_list = n.Sbor();
        }
        private string kotel()
        {
            string mes = "kotel|";
            OpenLoadKotel t5 = new OpenLoadKotel();
            ObservableCollection<OpenLoadKotel> t = new ObservableCollection<OpenLoadKotel>();
            ObservableCollection<OpenLoad> t2 = new ObservableCollection<OpenLoad>();
            OpenLoad t1 = new OpenLoad();
            t2 = t1.sbor();
            t = t5.Sbor_Del();
            foreach (OpenLoadKotel p in t)
            {
                foreach (OpenLoad pp in t2)
                {
                    if (p.Lnumber == pp.Lnumber)
                    {
                        string[] word = pp.Podr.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        p.Podr = word[0];
                        p.FIO = pp.Fio;
                        p.Types = pp.Types;
                    }
                }
            }
            foreach (OpenLoadKotel p in t)
            {
                mes += p.Lnumber + "|" + p.Podr + "|" + p.FIO + "|" + p.Types + "|";
            }
            return mes;
        }
        // Принудительная остановка сервера и запущенных потоков.
        public void StopServer()
        {
            if (_server != null)
            {
                _server.Stop();
                _server = null;
                _stopNetwork = true;

                for (int i = 0; i < MAXNUMCLIENTS; i++)
                {
                    if (clients[i] != null) clients[i].Close();
                }

                // Визуально оповещаем, что сервер остановлен.
                //this.BackColor = Color.FromName("Control");
            }
        }

        // Принимаем запросы клиентов на подключение и
        // привязываем к каждому подключившемуся клиенту 
        // сокет (в данном случае объект класса TcpClient)
        // для обменом сообщений.
        void AcceptClients()
        {
            while (true)
            {
                try
                {
                    this.clients[_countClient] = _server.AcceptTcpClient();
                    Thread readThread = new Thread(ReceiveRun);
                    readThread.Start(_countClient);
                    _countClient++;


                    // Данный метод, хотя и вызывается в отдельном потоке (не в главном),
                    // но находит родительский поток и выполняет делегат указанный в качестве параметра 
                    // в главном потоке, безопасно обновляя интерфейс формы.
                    //Invoke(new UpdateClientsDisplayDelegate(UpdateClientsDisplay));
                }
                catch
                {
                    // Перехватим возможные исключения
                    ErrorSound();
                }


                if (_countClient == MAXNUMCLIENTS || _stopNetwork == true)
                {
                    break;
                }

            }
        }

        //Маршрутизатор для запросов
        void MashrutZapros(string message_, int num)
        {
            string[] wor = message_.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
            if (wor.Count() > 1)
                if (wor[0] == "nLD")
                    _Pered_ld(wor[1]);
            string[] word = message_.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (word[0] == "log")
            {
                Type_People Type_people = new Type_People();
                if (File.Exists(Environment.CurrentDirectory + "\\Recources\\prf\\" + word[1] + ".xml"))
                {
                    string fileName = Environment.CurrentDirectory + "\\Recources\\prf\\" + word[1] + ".xml";
                    XDocument doc = XDocument.Load(fileName);
                    foreach (XElement el in doc.Root.Elements())
                    {
                        Type_people = new Type_People(el);
                    }
                    if (Type_people.PASSWORD == word[2])
                    {
                        SendToClients("log1|" + Type_people.TYPE_Bottom + "*" + "Shtat1|" + message_shtat + "*" + "AnShtat1|" + message_AnShtat + "*" + "Del1|" + message_Del + "*" + "Isk1|" + message_Isk + "*" + message_kotel, num);
                    }
                    else MessageBox.Show("Неверно введен логин или пароль");


                }

            }
            if (word[0] == "Reg")
            {
                Pol tmp = new Pol();
                tmp.LN = word[1];
                tmp.Login = word[1];
                tmp.Pass = word[1];
                tmp.ToXml();
            }
            if (word[0] == "RSZ-бригадная")
            {
                //OpenLoad T = new OpenLoad();
                //ObservableCollection<OpenLoad> List = new ObservableCollection<OpenLoad>();
                //List = T.sbor();
                //RSZ Ti = new RSZ();
                //Ti.RSZ_(List, word[0], num); ///Выход за пределы
            }
            if (word[0] == "Search") //Открытие ЛД
            {
                LD a = new LD();
                string message = a.Load(word[1]);
                SendToClients(message, num);
            }
            if (word[0] == "Shtat")
            {
                SendToClients("Shtat1|" + message_shtat, num);
            }
            if (word[0] == "AnShtat")
            {
                SendToClients("AnShtat1|" + message_AnShtat, num);
            }
            if (word[0] == "Del")
            {
                SendToClients("Del1|" + message_Del, num);
            }
            if (word[0] == "Isk")
            {
                SendToClients("Isk1|" + message_Isk, num);
            }
            if (word[0] == "Per")
            {
                SendToClients("Per1|" + message_Per, num);
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
                T.ToXml1();
                _izm_list.Add(T);
                if(T.Types == "Котел" && T.Name_table == "Пребытие")
                {
                    string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\izm.xml";
                    XDocument doc = XDocument.Load(fileName);
                    foreach(XElement el in doc.Root.Elements("track"))
                    {
                        if (el.Attribute("lnumber").Value == T.New_value)
                        {
                            SendToClients("kot-preb|" + T.Types + "|" + T.Row + "|" + T.Name_table + "|" + T.Old_value + "|" + T.New_value + "|" + T.Col, num);
                            break;
                        }
                    }
                    
                }
                SendToClients2("izm|" + T.Types + "|" + T.Row + "|" + T.Name_table + "|" + T.Old_value + "|" + T.New_value + "|" + T.Col, num);

            }
        }
        Dannie LD_Dan = new Dannie();
        ObservableCollection<Docum> ld_doc = new ObservableCollection<Docum>();
        ObservableCollection<family> ld_fam = new ObservableCollection<family>();
        ObservableCollection<Kontrakt> ld_kon = new ObservableCollection<Kontrakt>();
        ObservableCollection<Nagrad> ld_nag = new ObservableCollection<Nagrad>();
        Naznach LD_Naz = new Naznach();
        PriemDel LD_PD = new PriemDel();
        Photo_ LD_Ph = new Photo_();
        ObservableCollection<Poslug> ld_poslug = new ObservableCollection<Poslug>();
        Vizhivanie LD_Viz = new Vizhivanie();
        ObservableCollection<zvanie> ld_zvanie = new ObservableCollection<zvanie>();
        public void _Pered_ld(string message)
        {
            int j = 0;
            int i = 0;
            try
            {
                LD_Dan = new Dannie();
                ld_doc = new ObservableCollection<Docum>();
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
                                    ld_doc.Add(t_d);
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
                Save_port();
            }
            catch { MessageBox.Show("Ошибка на шаге i=" + i + " и шаге j =" + j); }
        }
        void Save_port()
        {
            try
            {
                foreach (Kontrakt a in ld_kon)
                {
                    a.ToXml(LD_Dan.Lnumber);
                }
                foreach (zvanie a in ld_zvanie)
                {
                    a.ToXml(LD_Dan.Lnumber);
                }
                foreach (Poslug a in ld_poslug)
                {
                    a.ToXml(LD_Dan.Lnumber);
                }
                foreach (Docum a in ld_doc)
                {
                    a.ToXml(LD_Dan.Lnumber);
                }
                foreach (Nagrad a in ld_nag)
                {
                    a.ToXml(LD_Dan.Lnumber);
                }
                foreach (family a in ld_fam)
                {
                    a.ToXml(LD_Dan.Lnumber);
                }
                LD_Dan.ToXml(LD_Dan.Lnumber);
                LD_Naz.ToXml(LD_Dan.Lnumber);
                LD_PD.ToXml(LD_Dan.Lnumber);
                LD_Viz.ToXml(LD_Dan.Lnumber);
            }
            catch { MessageBox.Show("Оцибка №1. Личное дело"); }
        }
        public void SaveIzm()
        {
            foreach (Izmen T in _izm_list)
            {
                if (T.Types == "Удалить")
                {
                    OpenLoadZaShtat p = new OpenLoadZaShtat();
                    string[] word = T.Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                    p.FIO = word[2];
                    p.Last_dolg = word[0];
                    p.Last_podr = word[1];
                    p.Lnumber = T.Old_value;
                    p.Zvanie = word[3];
                    p.Types = word[4];
                    p.ToXml();
                }
                if (T.Types == "Добавить")
                {
                    OpenLoadZaShtat p = new OpenLoadZaShtat();
                    p.Lnumber = T.New_value;
                    p.RemovePeople();
                }
                if (T.Types == "Убытие") { T.ToXmlUb(taktic(T.New_value), "Убытие"); }
                if (T.Types == "Пребытие") { T.ToXmlUb(taktic(T.New_value), "Пребытие"); }
                if (T.Types == "Отбор на контракт")
                {
                    if (T.Name_table == "Отобранные")
                    {
                        Otbor_na_kontr tmp = PrevOtobSave(T.Col, T.New_value);
                        SaveOtbor(T.Name_table, tmp);
                    }
                    if (T.Name_table == "Переданные")
                    {
                        Otbor_na_kontr tmp = PrevOtobSave(T.Col, T.New_value);
                        SaveOtbor(T.Name_table, tmp);
                    }
                    if (T.Name_table == "Состоялись")
                    {
                        Otbor_na_kontr tmp = PrevOtobSave(T.Col, T.New_value);
                        SaveOtbor(T.Name_table, tmp);
                    }
                    if (T.Name_table == "Отказ")
                    {
                        Otbor_na_kontr tmp = PrevOtobSave(T.Col, T.New_value);
                        SaveOtbor(T.Name_table, tmp);
                    }
                }
                if (T.Types == "Котел")
                {
                    if (T.Name_table == "Добавить")
                    {
                        string[] word = T.Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                        string fileName = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel-del.xml";
                        XDocument doc = XDocument.Load(fileName);
                        doc.Descendants().Where(e => e.Name == T.New_value).Remove();
                        doc.Save(fileName);
                        fileName = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel-ub.xml";
                        doc = XDocument.Load(fileName);
                        doc.Descendants().Where(e => e.Name == T.New_value).Remove();
                        doc.Save(fileName);
                        if (File.Exists(Environment.CurrentDirectory + "\\Recources\\dokum\\kotel.xml") == false)
                        {
                            fileName = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel.xml";
                            doc = new XDocument(
                                        new XElement("base",
                                         new XElement("track",
                                            new XAttribute("lnumber", T.New_value))));
                            doc.Save(fileName);
                        }
                        else
                        {
                            fileName = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel.xml";
                            doc = XDocument.Load(fileName);
                            XElement track = new XElement("track");
                            track.Add(new XAttribute("lnumber", T.New_value));                            
                            doc.Root.Add(track);
                            doc.Save(fileName);
                        }
                    }
                    if (T.Name_table == "Убрать")
                    {
                        string[] word = T.Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                        string fileName = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel.xml";
                        XDocument doc = XDocument.Load(fileName);
                        doc.Descendants().Where(e => e.Name == T.New_value).Remove();
                        doc.Save(fileName);
                        fileName = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel-ub.xml";
                        doc = XDocument.Load(fileName);
                        doc.Descendants().Where(e => e.Name == T.New_value).Remove();
                        doc.Save(fileName);
                        if (File.Exists(Environment.CurrentDirectory + "\\Recources\\dokum\\kotel-del.xml") == false)
                        {
                            fileName = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel-del.xml";
                            doc = new XDocument(
                                        new XElement("base",
                                         new XElement("track",
                                            new XAttribute("lnumber", T.New_value))));
                            doc.Save(fileName);
                        }
                        else
                        {
                            fileName = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel-del.xml";
                            doc = XDocument.Load(fileName);
                            XElement track = new XElement("track");
                            track.Add(new XAttribute("lnumber", T.New_value));
                            doc.Root.Add(track);
                            doc.Save(fileName);
                        }
                    }
                    if (T.Name_table == "Убытие")
                    {
                        string[] word = T.Col.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                        string fileName = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel.xml";
                        XDocument doc = XDocument.Load(fileName);
                        doc.Descendants().Where(e => e.Name == T.New_value).Remove();
                        doc.Save(fileName);
                        fileName = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel-del.xml";
                        doc = XDocument.Load(fileName);
                        doc.Descendants().Where(e => e.Name == T.New_value).Remove();
                        doc.Save(fileName);
                        if (File.Exists(Environment.CurrentDirectory + "\\Recources\\dokum\\kotel-ub.xml") == false)
                        {
                            fileName = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel-ub.xml";
                            doc = new XDocument(
                                        new XElement("base",
                                         new XElement("track",
                                            new XAttribute("lnumber", T.New_value))));
                            doc.Save(fileName);
                        }
                        else
                        {
                            fileName = Environment.CurrentDirectory + "\\Recources\\dokum\\kotel-ub.xml";
                            doc = XDocument.Load(fileName);
                            XElement track = new XElement("track");
                            track.Add(new XAttribute("lnumber", T.New_value));
                            doc.Root.Add(track);
                            doc.Save(fileName);
                        }
                    }
                    
                }
                OpenLoad P = new OpenLoad();
                OpenLoad PP = new OpenLoad();
                PP = P.Search(T.Row);
                T.ToXml();
                T.ToXmlPerest(PP, "");
                T.ToXmlLD(PP, "");
            }
            _izm_list = new ObservableCollection<Izmen>();
        }
        void SaveOtbor(string w, Otbor_na_kontr tmp)
        {
            string fileName = ".";
            if (w == "Отобранные")
            {
                fileName = Environment.CurrentDirectory + "\\Recources\\Active\\nk\\Otob.xml";
            }
            if (w == "Переданные")
            {
                fileName = Environment.CurrentDirectory + "\\Recources\\Active\\nk\\Pered.xml";
            }
            if (w == "Состоялись")
            {
                fileName = Environment.CurrentDirectory + "\\Recources\\Active\\nk\\Sostoy.xml";
            }
            if (w == "Отказ")
            {
                fileName = Environment.CurrentDirectory + "\\Recources\\Active\\nk\\Otkaz.xml";
            }
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track",
                        new XAttribute("podr", tmp.Podr),
                        new XAttribute("dolg", tmp.Dolg),
                        new XAttribute("ln", tmp.Lnumber),
                        new XAttribute("fio", tmp.FIO),
                        new XAttribute("per", tmp.Types),
                        new XAttribute("zv", tmp.Zvanie),
                        new XAttribute("date_ic", tmp.Date_ic),
                        new XAttribute("date1", tmp.Date1),
                        new XAttribute("date2", tmp.Date2),
                        new XAttribute("date3", tmp.Date3),
                        new XAttribute("date4", tmp.Date4),
                        new XAttribute("prim", tmp.Primec),
                        new XAttribute("rap", tmp.Raport),
                        new XAttribute("prof", tmp.Prof),
                        new XAttribute("bla", tmp.Blank),
                        new XAttribute("fiz", tmp.FIZO),
                        new XAttribute("vvk", tmp.Vvk),
                        new XAttribute("ic", tmp.Ic))));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                doc.Descendants().Where(e => e.Name == tmp.Lnumber).Remove();
                XElement track = new XElement("track");
                track.Add(new XAttribute("podr", tmp.Podr));
                track.Add(new XAttribute("dolg", tmp.Dolg));
                track.Add(new XAttribute("ln", tmp.Lnumber));
                track.Add(new XAttribute("fio", tmp.FIO));
                track.Add(new XAttribute("per", tmp.Types));
                track.Add(new XAttribute("zv", tmp.Zvanie));
                track.Add(new XAttribute("date_ic", tmp.Date_ic));
                track.Add(new XAttribute("date1", tmp.Date1));
                track.Add(new XAttribute("date2", tmp.Date2));
                track.Add(new XAttribute("date3", tmp.Date3));
                track.Add(new XAttribute("date4", tmp.Date4));
                track.Add(new XAttribute("prim", tmp.Primec));
                track.Add(new XAttribute("rap", tmp.Raport));
                track.Add(new XAttribute("prof", tmp.Prof));
                track.Add(new XAttribute("bla", tmp.Blank));
                track.Add(new XAttribute("fiz", tmp.FIZO));
                track.Add(new XAttribute("vvk", tmp.Vvk));
                track.Add(new XAttribute("ic", tmp.Ic));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }
        Otbor_na_kontr PrevOtobSave(string W, string P)
        {
            Otbor_na_kontr tmp = new Otbor_na_kontr();
            string[] word = W.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
            if(word[0] == "true") tmp.Blank = true; else if (word[0] == "false") tmp.Blank = false;
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
            tmp.FIO = P;
            tmp.Lnumber = word[9];
            tmp.Podr = word[10];
            tmp.Primec = word[11];
            tmp.Types = word[14];
            tmp.Zvanie = word[16];
            return tmp;
        }
        Ub taktic(string s)
        {
            string[] word = s.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries );
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
            return t1; 
        }
        /// <summary>
        /// Отправка сообщений клиентам
        /// </summary>
        /// <param name="text">текст сообщения</param>
        /// <param name="skipindex">индекс клиента которому не посылается сообщение</param>
        public void SendToClients(string text, int skipindex)
        {
            //for (int i = 0; i < MAXNUMCLIENTS; i++)
            //{
            // if (clients[i] != null)
            //{
            //if (i == skipindex) continue;


            // Подготовка и запуск асинхронной отправки сообщения.
            NetworkStream ns = clients[skipindex].GetStream();
            byte[] myReadBuffer = Encoding.Default.GetBytes(text);
            ns.BeginWrite(myReadBuffer, 0, myReadBuffer.Length,
                                                         new AsyncCallback(AsyncSendCompleted), ns);
            //continue;

            //}
            //}
        }
        public void SendToClients2(string text, int skipindex)
        {
            for (int i = 0; i < clients.Count(); i++)
            {
                if (clients[i] != null)
                {
                    if (i == skipindex) continue;


            // Подготовка и запуск асинхронной отправки сообщения.
            NetworkStream ns = clients[skipindex].GetStream();
            byte[] myReadBuffer = Encoding.Default.GetBytes(text);
            ns.BeginWrite(myReadBuffer, 0, myReadBuffer.Length,
                                                         new AsyncCallback(AsyncSendCompleted), ns);
            continue;

                    }
                }
        }
        // Асинхронная отправка сообщения клиенту.
        public void AsyncSendCompleted(IAsyncResult ar)
        {
            NetworkStream ns = (NetworkStream)ar.AsyncState;
            ns.EndWrite(ar);
        }


        // Извлечение сообщения от клиента и ретрансляция полученного 
        // сообщения другим клиентам
        void ReceiveRun(object num)
        {
            while (true)
            {
                try
                {
                    string s = null;
                    NetworkStream ns = clients[(int)num].GetStream();

                    // Раскомментировав строчку ниже, тем самым уменьшив размер приемного буфера, можно убедиться,
                    // что прием данных будет все равно осуществляться полностью.
                    //clients[(int)num].ReceiveBufferSize = 2;
                    while (ns.DataAvailable == true)
                    {
                        // Определить точный размер буфера приема позволяет свойство класса TcpClient - Available
                        byte[] buffer = new byte[clients[(int)num].Available];

                        ns.Read(buffer, 0, buffer.Length);
                        s += Encoding.Default.GetString(buffer);
                        //MessageBox.Show(s);
                        MashrutZapros(s, (int)num);
                    }

                    /*if (s != null)
                    {
                        // Данный метод, хотя и вызывается в отдельном потоке (не в главном),
                        // но находит родительский поток и выполняет делегат указанный в качестве параметра 
                        // в главном потоке, безопасно обновляя интерфейс формы.
                        //Invoke(new UpdateReceiveDisplayDelegate(UpdateReceiveDisplay), new object[] { (int)num, s });

                        // Принятое сообщение от клиента перенаправляем всем клиентам
                        // кроме текущего.
                        s = "№" + ((int)num).ToString() + ": " + s;
                        SendToClients(s, (int)num);
                        s = String.Empty;
                    }*/

                    // Вынужденная строчка для экономия ресурсов процессора.
                    // Неизящный способ.
                    Thread.Sleep(100);
                }
                catch
                {
                    // Перехватим возможные исключения
                    ErrorSound();
                }


                if (_stopNetwork == true) break;

            }
        }



        #endregion


        #region Визуализация сетевой работы

        // Получение сообщений от клиентов
        public void UpdateReceiveDisplay(int clientnum, string message)
        {
            //listBox1.Items.Add("№" + clientnum.ToString() + ": " + message);
        }
        // Делегат доступа к элементу формы listBox1 из вспомогательного потока.
        protected delegate void UpdateReceiveDisplayDelegate(int clientcount, string message);

        // Делегат доступа к элементу формы labelCountClient из вспомогательного потока.
        protected delegate void UpdateClientsDisplayDelegate();

        // Звуковое оповещение о перехваченной ошибке.
        void ErrorSound()
        {
            Console.Beep(3000, 80);
            Console.Beep(1000, 100);
        }

        #endregion

        #region Загрузка несохраненного
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
        public void PrevLoad()
        {
            if (File.Exists(Environment.CurrentDirectory + "\\Recources\\Active\\izm.xml") == true)
            {
                string fileName = Environment.CurrentDirectory + "\\Recources\\Active\\izm.xml";
                XDocument X = XDocument.Load(fileName);
                foreach(XElement t in X.Root.Elements("track"))
                {
                    _izm_list.Add(new Izmen(t));

                }
            }
        }
        #endregion
    }


}
