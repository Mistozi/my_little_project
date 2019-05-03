using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using МиниЧат.Modal;

namespace МиниЧат
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon m_notifyIcon;
        string ip;
        string NameS;
        string types;
        TCP_Client T1 = new TCP_Client();
        //TCP_Server T2 = new TCP_Server();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new TCP_Client();
            string fileName = Environment.CurrentDirectory + "\\Recources\\config.xml";
            XDocument doc = XDocument.Load(fileName);
            foreach (XElement el in doc.Root.Elements("Name"))
            {
                NameS = el.Attribute("value").Value;
            }
            foreach (XElement el in doc.Root.Elements("Ip"))
            {
                ip = el.Attribute("value").Value;
            }
            m_notifyIcon = new System.Windows.Forms.NotifyIcon();
            m_notifyIcon.BalloonTipTitle = "Миничат";
            m_notifyIcon.BalloonTipText = "Тут трудится МИНИЧАТ";

            m_notifyIcon.Text = "Простенький чат";
            m_notifyIcon.Icon = new System.Drawing.Icon(Environment.CurrentDirectory + "\\Recources\\16.ico");
            m_notifyIcon.Click += new EventHandler(m_notifyIcon_Click);


        }

        private void MinimizedWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            this.WindowState = WindowState.Minimized; //свернуть
        }

        private WindowState m_storedWindowState = WindowState.Normal;
        void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();

                T1.Minimazed(false);//if (m_notifyIcon != null)
                //m_notifyIcon.ShowBalloonTip(2000);
            }
            else
            {
                m_storedWindowState = WindowState;
                T1.Minimazed(true);
            }
        }
        void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            CheckTrayIcon();
        }
        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = m_storedWindowState;
            this.Visibility = Visibility.Visible;
            this.ShowInTaskbar = true;
        }
        void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }
        void ShowTrayIcon(bool show)
        {
            if (m_notifyIcon != null)
                m_notifyIcon.Visible = show;
        }

        private void Create_message_Click(object sender, RoutedEventArgs e)
        {
            if (Message_create_box.Text != "") {
                if (types == "ii")
                    T1.SendMessage(ip + ">" + Message_create_box.Text + ">" + NameS);
                
                if (types == "jj")
                    T1.SendToClients2(ip + ">" + Message_create_box.Text + ">" + NameS,-1);

                T1.ADD(ip, Message_create_box.Text, NameS);
            }
        }

        private void Menu_header_1_Click(object sender, RoutedEventArgs e)
        {
            if(types == "ii") { return; }
            MenuItem t = (MenuItem)sender; 
            if(t.Header.ToString() == "Включить сервер")
            {
                t.Header = "Выключить сервер";	
                T1.StartServer();
                types = "jj";
            }
            else
            {
                t.Header = "Включить сервер";
                T1.Stop();
                types = "";
            }
        }

        private void Menu_header_2_Click(object sender, RoutedEventArgs e)
        {
            if (types == "jj") { return; }
            MenuItem t = (MenuItem)sender;
            if (t.Header.ToString() == "Подключиться")
            {
                t.Header = "Отключиться";
                T1.Connect();
                T1.SendMessage("Подключение>"+ ip +  ">" + NameS);
                types = "ii";
            }
            else
            {
                T1.SendMessage("Отключение_клиент>"+NameS+">"+ip);
                t.Header = "Подключиться";
                T1.CloseClient();
                types = "";
            }
        }
        
        private void Window_Closed(object sender, EventArgs e)
        {
            if(types == "ii")
                T1.CloseClient();
            if (types == "jj")
                T1.Stop();
            Environment.Exit(0);
        }
    }
}
