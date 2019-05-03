using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Linq;

namespace �������.Modal
{
    public class TCP_Client
    {
        bool Minimazed_ = false;
        string NameS, _ip_client, _ip_server;
        Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;
        static ObservableCollection<Messager> list_shtat = new ObservableCollection<Messager>();
        public ObservableCollection<Messager> Humans
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
        static ObservableCollection<Peoples> list_people = new ObservableCollection<Peoples>();
        public ObservableCollection<Peoples> People
        {
            get
            {
                if (list_people == null)
                {

                }
                return list_people;
            }
            set { list_people = value; }
        }


        // ������ ���������� ������� ����� ����������� ����������
        TcpClient _tcp�lient = new TcpClient();

        // ������ �������� ������ ��� ������ � �������� ���������
        NetworkStream ns;

        // ���� ��� ��������� ������� � ���������� ������� ������ ����������
        bool _stopNetwork;

        #region �������������� ����� ������� ������

        public void Minimazed (bool t)
        {
            Minimazed_ = t;
        }
        // ������� ����������� � �������
        void ip()
        {         
            string fileName = Environment.CurrentDirectory + "\\Recources\\config.xml";
            XDocument doc = XDocument.Load(fileName);
            foreach (XElement el in doc.Root.Elements("track"))
            {
                _ip_server = el.Attribute("value").Value;
            }
            foreach (XElement el in doc.Root.Elements("Name"))
            {
                NameS = el.Attribute("value").Value;
            }
            foreach (XElement el in doc.Root.Elements("Ip"))
            {
                _ip_client = el.Attribute("value").Value;
            }
        }

        public void Connect()
        {
            try
            {

                ip();
                if (_ip_server == _ip_client) return;
                _tcp�lient.Connect(_ip_server, 15000);

                ns = _tcp�lient.GetStream();
                
                Thread th = new Thread(ReceiveRun_client);
                th.Start();
                if (File.Exists(Environment.CurrentDirectory + "\\Recources\\dannie.xml") == true)
                {
                    string fileName = Environment.CurrentDirectory + "\\Recources\\dannie.xml";
                    XDocument doc = XDocument.Load(fileName);
                    foreach (XElement el in doc.Root.Elements("track"))
                    {
                        list_shtat.Add(new Messager(el));
                    }
                }
            }
            catch
            {
                ErrorSound();
            }
        }
        public void CloseClient()
        {
            
            if (ns != null) ns.Close();
            if (_tcp�lient != null) _tcp�lient.Close();
            
            _stopNetwork = true;

        }

        // �������� ��������� � ����������� ������,
        // ��������� ����� ��������� �����������
        // �� ������� ��������� ������������ ���������� ����������. 
        public void SendMessage(string Text_cap)
        {
                if (ns != null)
                {
                    byte[] buffer = Encoding.Default.GetBytes(Text_cap);
                    ns.Write(buffer, 0, buffer.Length);
                }                   
        }

        public void ADD(string id, string mes, string Name)
        {
            DateTime T = DateTime.Now;
            Messager t = new Messager();
            t.IP = id;
            t.Mes = mes;
            t.Name = Name;
            t.Text = "[" + T.Hour + ":" + T.Minute + ":" + T.Second + "] �: " + mes;
            list_shtat.Add(t);
            t.ToXml();
            if (Minimazed_)
            {
                System.Windows.Window W = new System.Windows.Window();
                TextBlock TB = new TextBlock();
                TB.Text = mes;
                W.Width = 300;
                W.Height = 100;
                double screenHeight = SystemParameters.FullPrimaryScreenHeight;
                double screenWidth = SystemParameters.FullPrimaryScreenWidth;


                W.Top = (screenHeight - W.Height);
                W.Left = (screenWidth - W.Width);

                W.Name = "���������";
                W.Title = "�������� ��������� �� " + Name;
                Grid A = new Grid();
                W.Content = A;
                A.Children.Add(TB);
                //W..Children.Add(TB);
                W.Show();
            }

        }
        // ���� ���������� ���������,
        // ����������� � ��������� ������.
        void ReceiveRun_client()
        {
            
            while (true)
            {
                try
                {
                    string s = null;
                    while (ns.DataAvailable == true)
                    {
                        // ����������� ������������ ������� ������ ������.
                        byte[] buffer = new byte[_tcp�lient.Available];

                        ns.Read(buffer, 0, buffer.Length);
                        s += Encoding.Default.GetString(buffer);
                    }

                    if (s != null)
                    {
                        ShowReceiveMessage(s);

                        //MessageBox.Show(s);
                        //s = String.Empty;
                    }


                    // ����������� ������� ��� �������� �������� ����������.
                    // ��������� ������.
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


        #region ���������� � ������� ������

        // ��� ������� � ��������� �������� ������� �����  �� ������ �������
        delegate void UpdateReceiveDisplayDelegate(string message);
        void ShowReceiveMessage(string message) //�������� ���������
        {
            string[] wor = message.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
            if(wor[0] == "�����������")
            {
                Peoples t = new Peoples();
                t.IP = wor[1];
                t.Name = wor[2];          
                _dispatcher.Invoke(new Action(() =>
                {
                    list_people.Add(t);
                }));

            }else
            if (wor[0] == "����������_������")
            {
                _tcp�lient.Close();
                MessageBox.Show(" �������� ���������� � ��������. ������������� ������� ����������");
                Environment.Exit(0);
            }else
            if (wor[0] == "������������")
            {
                list_people = new ObservableCollection<Peoples>();
                int k = 1;
                Peoples tmp = new Peoples();
                for (int i = 1; i < wor.Length; i++)
                {
                    if (k == 1)
                    {
                        tmp = new Peoples();
                        tmp.Name = wor[i];
                        k++;
    
                }
                    if (k == 2)
                    {
                        tmp.IP = wor[i];
                        k++;
    
                }
                    if (k == 3)
                    {
                        tmp.Text = wor[i];
                        k++;
                    }
                    if (k == 4)
                    {
                        tmp.Counter = int.Parse(wor[i]);
                        list_people.Add(tmp);
                        k = 1;
                    }
                }
            }

            else
            {
                DateTime T = DateTime.Now;
                Messager tmp = new Messager();
                tmp.IP = wor[0];
                //MessageBox.Show("Ip:" + wor[0]);
                tmp.Mes = wor[1];
                //MessageBox.Show("Mes:" + wor[1]);
                tmp.Name = wor[2];
                //MessageBox.Show("Name:" + wor[2]);
                tmp.Text = "["+T.Hour+":"+ T.Minute+ ":"+T.Second+ "]"+ wor[2] + ": " + wor[1];
                _dispatcher.Invoke(new Action(() =>
                {
                    list_shtat.Add(tmp);
                }));
                tmp.ToXml();
                //MessageBox.Show(message);
            }

            //SendMessage("izm|" + td.Types + "|" + td.Row + "|" + td.Name_table + "|" + td.Old_value + "|" + td.New_value + "|" + td.Col);

        }



        // �������� ���������� � ������������� ������.
        void ErrorSound()
        {
            Console.Beep(2000, 80);
            Console.Beep(3000, 120);
        }

        #endregion







        //public IEnumerable<Messager> Humans { get; set; }
        // ��������������� ���������� ��� ��������������� ������
        TcpListener _server;

        // ���������� ����������� ����������� � �������
        const int MAXNUMCLIENTS = 15;

        // ��������������� ���������� ��� ������� ������ ��������� � 
        // ����������� ������������.
        TcpClient[] clients = new TcpClient[MAXNUMCLIENTS];

        // ������� ������������ ��������
        int _countClient = 0;

        // ���� ������ ��������� ������ � �������������� �������
        //bool _stopNetwork;

        #region �������������� ����� ������� ������


        // ������ ������� � ���������������� ������ �������������� ���������� �����������
        // �.�. ���������� ������� ������������� �� ����� ����������� 
        // � ��������������� ���������� �����������
        public void StartServer()
        {
            // ������������ ��������� ������ �������
            if (_server == null)
            {
                // ���� ��������� ���������� �� ������ ������� ������������
                // ���� ��������� ���������� � ���������� ������.
                try
                {
                    ip();
                    if (_ip_server != _ip_client) return;
                    Peoples t = new Peoples();
                    t.Name = NameS;
                    t.IP = _ip_client;
                    list_people.Add(t);

                    _stopNetwork = false;
                    _countClient = 0;
                    //UpdateClientsDisplay();

                    int port = 15000;
                    _server = new TcpListener(IPAddress.Any, port);
                    _server.Start();


                    Thread acceptThread = new Thread(AcceptClients);
                    acceptThread.Start();

                    if (File.Exists(Environment.CurrentDirectory + "\\Recources\\dannie.xml") == true)
                    {
                        string fileName = Environment.CurrentDirectory + "\\Recources\\dannie.xml";
                        XDocument doc = XDocument.Load(fileName);
                        foreach (XElement el in doc.Root.Elements("track"))
                        {
                            list_shtat.Add(new Messager(el));
                        }
                    }
                    //MessageBox.Show("������ �������");
                    // ���������� ����������, ��� ������ �������
                    //this.BackColor = Color.FromArgb(150, 192, 255);

                }
                catch
                {
                    SaveError("Start Server");
                    ErrorSound();
                }
            }
        }


        public void Stop()
        {
            SendToClients2("����������_������", -1);
            StopServer();
        }

        // �������������� ��������� ������� � ���������� �������.
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

                // ��������� ���������, ��� ������ ����������.
                //this.BackColor = Color.FromName("Control");
            }
        }

        // ��������� ������� �������� �� ����������� �
        // ����������� � ������� ��������������� ������� 
        // ����� (� ������ ������ ������ ������ TcpClient)
        // ��� ������� ���������.
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


                    // ������ �����, ���� � ���������� � ��������� ������ (�� � �������),
                    // �� ������� ������������ ����� � ��������� ������� ��������� � �������� ��������� 
                    // � ������� ������, ��������� �������� ��������� �����.
                    //Invoke(new UpdateClientsDisplayDelegate(UpdateClientsDisplay));
                }
                catch
                {
                    // ���������� ��������� ����������
                    SaveError("AcceptClients");
                    ErrorSound();
                }


                if (_countClient == MAXNUMCLIENTS || _stopNetwork == true)
                {
                    break;
                }

            }
        }

        //������������� ��� ��������
        void MashrutZapros(string message_, int num)
        {
            DateTime T = DateTime.Now;
            string[] wor = message_.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);

            if (wor[0] == "�����������")
            {
                Peoples t = new Peoples();
                t.IP = wor[1];
                t.Name = wor[2];
                t.Counter = num;
                _dispatcher.Invoke(new Action(() =>
                {
                    list_people.Add(t);
                }));
                SendToClients2(message_+">"+num, -1);
            }
            else
         if (wor[0] == "����������_������")
            {
                Peoples t = new Peoples();
                t.IP = wor[2];
                t.Name = wor[1];
                list_people.Remove(t);
                SendToClients2(Message_create(list_people), num);
            }
            else
            {
                //_Pered_ld(wor[1]);
                SendToClients2(message_, num);
                Messager tmp = new Messager();
                tmp.IP = wor[0];
                //MessageBox.Show("Ip:" + wor[0]);
                tmp.Mes = wor[1];
                //MessageBox.Show("Mes:" + wor[1]);
                tmp.Name = wor[2];
                tmp.Text = "[" + T.Hour + ":" + T.Minute + ":" + T.Second + "]" + wor[2] + ": " + wor[1];
                //MessageBox.Show("Name:" + wor[2]);
                _dispatcher.Invoke(new Action(() =>
                {
                    list_shtat.Add(tmp);
                }));
                //MessageBox.Show(message_);
            }

        }

        public string Message_create(ObservableCollection<Peoples> t)
        {
            string text = "������������>";
            foreach (Peoples a in t)
            {
                text += a.Name + ">" + a.IP + ">" + a.Text + ">" + a.Counter + ">";
    
        }
            return text;

        }


        /// <summary>
        /// �������� ��������� ��������
        /// </summary>
        /// <param name="text">����� ���������</param>
        /// <param name="skipindex">������ ������� �������� �� ���������� ���������</param>
        public void SendToClients2(string text, int skipindex)
        {
            for (int i = 0; i < clients.Count(); i++)
            {
                if (clients[i] != null)
                {
                    if (i == skipindex) continue;


                    // ���������� � ������ ����������� �������� ���������.
                    NetworkStream ns = clients[i].GetStream();
                    byte[] myReadBuffer = Encoding.Default.GetBytes(text);
                    ns.BeginWrite(myReadBuffer, 0, myReadBuffer.Length,
                                                                 new AsyncCallback(AsyncSendCompleted), ns);
                    //byte[] buffer = Encoding.Default.GetBytes(text);
                    //ns.Write(buffer, 0, buffer.Length);
                    continue;

                }
            }
        }
        // ����������� �������� ��������� �������.
        public void AsyncSendCompleted(IAsyncResult ar)
        {
            try
            {
                NetworkStream ns = (NetworkStream)ar.AsyncState;
                ns.EndWrite(ar);
            }
            catch (Exception e)
            {

            }
        }


        // ���������� ��������� �� ������� � ������������ ����������� 
        // ��������� ������ ��������
        void ReceiveRun(object num)
        {
            while (true)
            {
                try
                {
                    string s = null;
                    NetworkStream ns = clients[(int)num].GetStream();
                
                    

                    // ���������������� ������� ����, ��� ����� �������� ������ ��������� ������, ����� ���������,
                    // ��� ����� ������ ����� ��� ����� �������������� ���������.
                    //clients[(int)num].ReceiveBufferSize = 2;
                    while (ns.DataAvailable == true)
                    {
                        // ���������� ������ ������ ������ ������ ��������� �������� ������ TcpClient - Available
                        byte[] buffer = new byte[clients[(int)num].Available];

                        ns.Read(buffer, 0, buffer.Length);
                        s += Encoding.Default.GetString(buffer);
                        //MessageBox.Show(s);
                        MashrutZapros(s, (int)num);
                    }

                    /*if (s != null)
                    {
                        // ������ �����, ���� � ���������� � ��������� ������ (�� � �������),
                        // �� ������� ������������ ����� � ��������� ������� ��������� � �������� ��������� 
                        // � ������� ������, ��������� �������� ��������� �����.
                        //Invoke(new UpdateReceiveDisplayDelegate(UpdateReceiveDisplay), new object[] { (int)num, s });

                        // �������� ��������� �� ������� �������������� ���� ��������
                        // ����� ��������.
                        s = "�" + ((int)num).ToString() + ": " + s;
                        SendToClients(s, (int)num);
                        s = String.Empty;
                    }*/

                    // ����������� ������� ��� �������� �������� ����������.
                    // ��������� ������.
                    Thread.Sleep(100);
                }
                catch
                {
                    // ���������� ��������� ����������
                    SaveError("ReceiveRun");
                    ErrorSound();
                }


                if (_stopNetwork == true) break;

            }
        }

        #endregion


        #region ������������ ������� ������



        // ������� ������� � �������� ����� labelCountClient �� ���������������� ������.
        protected delegate void UpdateClientsDisplayDelegate();

        // �������� ���������� � ������������� ������.


        void SaveError(string message)
        {
            string fileName = Environment.CurrentDirectory + "\\Recources\\error.xml";
            XDocument doc;
            if (File.Exists(fileName) == false)
            {
                doc = new XDocument(
                new XElement("base",
                    new XElement("track",
                        new XAttribute("Error-", message)
                       )));
                doc.Save(fileName);
            }
            else
            {
                doc = XDocument.Load(fileName);
                XElement track = new XElement("track");
                track.Add(new XAttribute("Error-", message));
                doc.Root.Add(track);
                doc.Save(fileName);
            }
        }
        #endregion

    }
    

}

