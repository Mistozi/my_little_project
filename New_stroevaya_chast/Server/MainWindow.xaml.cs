using New_Stroevaya_chast.ViewModal;
using System;
using System.Collections.Generic;
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

namespace New_Stroevaya_chast
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TCP T = new TCP();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new TCP();
        }

        private void Create_Server_Click(object sender, RoutedEventArgs e)
        {
            T.StartServer();
            T.Zagruzchik();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            T.StopServer();
        }

        private void Replace_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            T.SaveIzm();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            T.PrevLoad();
        }
    }
}
