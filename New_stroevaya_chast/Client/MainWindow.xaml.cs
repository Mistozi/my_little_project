using Client.View;
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

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TCP p = new TCP();
        public MainWindow()
        {
            InitializeComponent();
            Osn.Visibility = Visibility.Visible;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {

            Client_osn P = new Client_osn(Login.Text, Pass.Text);
            P.Show();


        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "Create")
            {
                if (Pass_one.Text != Pass_two.Text)
                {
                    MessageBox.Show("Пароли не совпадают");
                    return;
                }
                if (Pass_one.Text == "")
                {
                    MessageBox.Show("Пароля нет");
                    return;
                }
                if (Pass_two.Text == "")
                {
                    MessageBox.Show("Пароля нет");
                    return;
                }
                if (LNumber.Text == "")
                {
                    MessageBox.Show("Нет личного номера");
                    return;
                }
                p.SendMessage("Reg|" + LNumber.Text + "|" + Pass_one.Text + "|" + Pass_two.Text);
                MessageBox.Show("Данные направлены на сервер");
                Osn.Visibility = Visibility.Visible;
                Reg_grid.Visibility = Visibility.Hidden;
            }
            if (btn.Name == "Otmena")
            {
                Osn.Visibility = Visibility.Visible;
                Reg_grid.Visibility = Visibility.Hidden;
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Reg_grid.Visibility = Visibility.Visible;
            Osn.Visibility = Visibility.Hidden;
        }
    }
}
