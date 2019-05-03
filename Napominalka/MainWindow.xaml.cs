using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Whatch
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon m_notifyIcon;
        static ObservableCollection<People> list_Secret = new ObservableCollection<People>();
        People p = new People();

        public ObservableCollection<People> TopicList
        {
            get
            {
                if (list_Secret == null)
                {

                }
                return list_Secret;
            }
            set { list_Secret = value; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            m_notifyIcon = new System.Windows.Forms.NotifyIcon();
            m_notifyIcon.BalloonTipTitle = "Оповещашка";
            m_notifyIcon.BalloonTipText = "Тут трудится напоминашка";

            m_notifyIcon.Text = "Оно отслеживает даты до дня рождения";
            m_notifyIcon.Icon = new System.Drawing.Icon(Environment.CurrentDirectory + "\\hm.ico");
            m_notifyIcon.Click += new EventHandler(m_notifyIcon_Click);
            //this.Visibility = Visibility.Hidden;
            //this.ShowInTaskbar = false;
            this.WindowState = WindowState.Minimized;
            DateTime t = DateTime.Today;
            if (File.Exists(Environment.CurrentDirectory + "\\dannie.xml") == true)
            {
                string fileName = Environment.CurrentDirectory + "\\dannie.xml";
                XDocument doc = XDocument.Load(fileName);
                foreach (XElement el in doc.Root.Elements("track"))
                {
                    list_Secret.Add(new People(el));
                }
                int irs = 1;
                foreach (People i in list_Secret)
                {
                    i.IDD = irs++;
                }
                string pi = p.Date(t);
                string pi7 = p.Date(t.AddDays(7));
                string pi10 = p.Date(t.AddDays(10));
                foreach (People i in list_Secret)
                {
                    if (pi == i.Date_brd)
                    {
                        MessageBox.Show("Сегодня день рождения у " + i.Fio);
                    }
                    if (pi7 == i.Date_brd)
                    {
                        MessageBox.Show("Через неделю день рождения у " + i.Fio);
                    }
                    if (pi10 == i.Date_brd)
                    {
                        MessageBox.Show("Через 10 дней день рождения у " + i.Fio);
                    }
                }
            }
            Table.ItemsSource = TopicList;
        }


        private WindowState m_storedWindowState = WindowState.Normal;
        void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                //if (m_notifyIcon != null)
                    //m_notifyIcon.ShowBalloonTip(2000);
            }
            else
                m_storedWindowState = WindowState;
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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_notifyIcon.Dispose();
            m_notifyIcon = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool ir = true;
            if(zvanie_create.Text == "") { MessageBox.Show("Введите звание"); return; }
            if (Dolg_create.Text == "") { MessageBox.Show("Введите Должность"); return; }
            if (fio_create.Text == "") { MessageBox.Show("Введите ФИО"); return; }
            if (tel_create.Text == "") { MessageBox.Show("Введите телефон"); return; }
            if (Date_create.Text == "") { MessageBox.Show("Укажите дату рождения"); return; }
            DateTime dt = Date_create.SelectedDate.Value;
            foreach (People i in list_Secret)
            {
                if (fio_create.Text == i.Fio)
                {
                    i.Dolg = Dolg_create.Text;
                    i.Fio = fio_create.Text;
                    i.Nomber = tel_create.Text;
                    i.Zvanie = zvanie_create.Text;
                    i.Date_brd = i.Date(dt);
                    i.ToXml();
                    ir = false;
                    break;
                }
                else
                {
                    ir = true;
                }
            }
            if(ir == true)
            {
                People p = new People();
                p.Dolg = Dolg_create.Text;
                p.Fio = fio_create.Text;
                p.Nomber = tel_create.Text;
                p.Zvanie = zvanie_create.Text;
                p.Date_brd = p.Date(dt);
                p.ToXml();
                list_Secret.Add(p);
            }
            
            Titles.Visibility = Visibility.Visible;
            Creats.Visibility = Visibility.Hidden;
            zvanie_create.Text = "";
            Dolg_create.Text = "";
            fio_create.Text = "";
            tel_create.Text = "";
            Date_create.Text = "";
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Titles.Visibility = Visibility.Visible;
            Creats.Visibility = Visibility.Hidden;
            zvanie_create.Text = "";
            Dolg_create.Text = "";
            fio_create.Text = "";
            tel_create.Text = "";
            Date_create.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Titles.Visibility = Visibility.Hidden;
            Creats.Visibility = Visibility.Visible;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            People ap = Table.SelectedItem as People;
            if (File.Exists(Environment.CurrentDirectory + "\\dannie.xml") == true)
            {
                string fileName = Environment.CurrentDirectory + "\\dannie.xml";
                XDocument doc = XDocument.Load(fileName);
                doc.Descendants("track")
                    .Where(x => (string)x.Attribute("ln") == ap.LN)
                    .Remove();
                //var nodes = doc.Elements().Where(x => x.Element("track").Attribute("ln") != null).ToList();

                //foreach (var node in nodes)
                //    if(node.Value == ap.LN)
                //        node.Remove();
                doc.Save(fileName);
                list_Secret.Remove(ap);
            }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            People p = Table.SelectedItem as People;
            Titles.Visibility = Visibility.Hidden;
            Creats.Visibility = Visibility.Visible;
            zvanie_create.Text = p.Zvanie;
            Dolg_create.Text = p.Dolg;
            fio_create.Text = p.Fio;
            tel_create.Text = p.Nomber;
            Date_create.Text = p.Date_brd;
        }
    }
}
