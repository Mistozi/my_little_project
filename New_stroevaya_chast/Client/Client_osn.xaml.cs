using New_Stroevaya_chast.Modal;
using New_Stroevaya_chast.ViewModal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;
using System.Xml.Linq;
using static New_Stroevaya_chast.Modal.Shtat;

namespace Client.View
{
    /// <summary>
    /// Логика взаимодействия для Client_osn.xaml
    /// </summary>
    public partial class Client_osn : Window
    {
        #region переменные
        List<string> Name_header;
        ObservableCollection<CheckedListItem<string>> TopicList;
        FlowDocument document = new FlowDocument();
        static int number = 0;
        string filter;
        TCP T = new TCP();
        private string text1;
        private string text2;
        bool MonoCreateShtatPeople = false;
        bool LD_BOOL = false;
        TreeViewItem item = new TreeViewItem();
        string Type_People_LD = ".";
        #endregion
        public Client_osn()
        {
            InitializeComponent();
        }
        public Client_osn(string text1, string text2)
        {
            this.text1 = text1;
            this.text2 = text2;
            T.Connect();
            T.SendMessage("log|" + text1 + "|" + text2);
            InitializeComponent();
            this.DataContext = new TCP();
            _fontFamily.ItemsSource = Fonts.SystemFontFamilies;
            _fontSize.ItemsSource = FontSizes;
            Oblus.Items.Remove(Redactor);

            BitmapImage bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\Ikon_setting.jpg", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Image Icon_Regim = new Image();
            Icon_Regim.Width = 30;
            Icon_Regim.Height = 30;
            Icon_Regim.Source = bm_1;
            Regim.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\charactergrowfont.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            Grow_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\editcut.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            Cut_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\editcopy.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            Copy_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\editpaste.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            Paste_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\editundo.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            Undo_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\editredo.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            Redo_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\charactershrinkfont.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            Shrink_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\listbullets.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            listbullers_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\listnumbering.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            listnumbering_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\paragraphleftjustify.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            paragraphleftjustify_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\paragraphcenterjustify.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            paragraphcenterjustify_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\paragraphrightjustify.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            paragraphrightjustify_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\paragraphfulljustify.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            paragraphfulljustify_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\paragraphincreaseindentation.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            paragraphincreaseindentation_btn.Content = Icon_Regim;

            bm_1 = new BitmapImage();
            bm_1.BeginInit();
            bm_1.UriSource = new Uri(Environment.CurrentDirectory + "\\Recources\\Image\\paragraphdecreaseindentation.png", UriKind.Relative);
            bm_1.CacheOption = BitmapCacheOption.OnLoad;
            bm_1.EndInit();
            Icon_Regim = new Image();
            Icon_Regim.Source = bm_1;
            paragraphdecreaseindentation_btn.Content = Icon_Regim;

        }
        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            T.CloseClient();
            Environment.Exit(0);
        }
       
        #region работа с приказом
        public double[] FontSizes
        {
            get
            {
                return new double[] {
                    3.0, 4.0, 5.0, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5,
                    10.0, 10.5, 11.0, 11.5, 12.0, 12.5, 13.0, 13.5, 14.0, 15.0,
                    16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0,
                    32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0, 76.0,
                    80.0, 88.0, 96.0, 104.0, 112.0, 120.0, 128.0, 136.0, 144.0
                    };
            }
        }

        private void _fontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FontFamily editValue = (FontFamily)e.AddedItems[0];
            ApplyPropertyValueToSelectedText(TextElement.FontFamilyProperty, editValue);
        }
        private void _fontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyPropertyValueToSelectedText(TextElement.FontSizeProperty, e.AddedItems[0]);
        }
        void ApplyPropertyValueToSelectedText(DependencyProperty formattingProperty, object value)
        {
            if (value == null)
                return;

            mainRTB.Selection.ApplyPropertyValue(formattingProperty, value);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainRTB.Document.Blocks.Clear();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("П Р И К А З\n КОМАНДИРА ВОЙСКОВОЙ ЧАСТИ 51460\n")));
            paragraph.Inlines.Add(new Run("(по строевой части)"));
            paragraph.TextAlignment = TextAlignment.Center;
            paragraph.FontSize = 16;
            paragraph.Name = "Head";
            paragraph.FontFamily = new FontFamily("Times New Roman");
            document.Blocks.Add(paragraph);

            DateTime dt = DateTime.Today;
            string day = "";
            string mounth = "";
            string filename = Environment.CurrentDirectory + "\\Recources\\pz\\Numbers.xml";
            XDocument doc = XDocument.Load(filename);
            XElement El = doc.Root;
            XElement Ele = El.Elements("track").Last();
            number = int.Parse(Ele.Attribute("id").Value) + 1;
            if (dt.Day < 10)
            {
                day = "0" + dt.Day;
            }
            else day = dt.Day.ToString();
            if (dt.Month == 1) mounth = "января";
            else
                if (dt.Month == 2) mounth = "февраля";
            else
                if (dt.Month == 3) mounth = "марта";
            else
                if (dt.Month == 4) mounth = "апреля";
            else
                if (dt.Month == 5) mounth = "мая";
            else
                if (dt.Month == 6) mounth = "июня";
            else
                if (dt.Month == 7) mounth = "июля";
            else
                if (dt.Month == 8) mounth = "августа";
            else
                if (dt.Month == 9) mounth = "сентября";
            else
                if (dt.Month == 10) mounth = "октября";
            else
                if (dt.Month == 11) mounth = "ноября";
            else
                if (dt.Month == 12) mounth = "декабря";
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run("«" + day + "» " + mounth + " " + dt.Year.ToString() + " г. № " + number.ToString()));
            paragraph.TextAlignment = TextAlignment.Center;
            paragraph.FontSize = 16;
            paragraph.Name = "Head";
            paragraph.FontFamily = new FontFamily("Times New Roman");
            document.Blocks.Add(paragraph);
            mainRTB.Document = document;


        }//новый приказ

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("КОМАНДИР ВОЙСКОВОЙ ЧАСТИ 51460\n полковник")));
            paragraph.TextAlignment = TextAlignment.Center;
            paragraph.FontSize = 16;
            paragraph.Name = "Foot1";
            paragraph.FontFamily = new FontFamily("Times New Roman");
            document.Blocks.Add(paragraph);
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("А. Шершнёв")));
            paragraph.TextAlignment = TextAlignment.Right;
            paragraph.FontSize = 16;
            paragraph.Name = "Foot2";
            paragraph.FontFamily = new FontFamily("Times New Roman");
            document.Blocks.Add(paragraph);
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("НАЧАЛЬНИК ШТАБА ВОЙСКОВОЙ ЧАСТИ 51460\n полковник")));
            paragraph.TextAlignment = TextAlignment.Center;
            paragraph.FontSize = 16;
            paragraph.Name = "Foot3";
            paragraph.FontFamily = new FontFamily("Times New Roman");
            document.Blocks.Add(paragraph);
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("А.Винс")));
            paragraph.TextAlignment = TextAlignment.Right;
            paragraph.FontSize = 16;
            paragraph.Name = "Foot4";
            paragraph.FontFamily = new FontFamily("Times New Roman");
            document.Blocks.Add(paragraph);
            mainRTB.Document = document;
            string filename = Environment.CurrentDirectory + "\\Recources\\pz\\Numbers.xml";
            XDocument doc = XDocument.Load(filename);
            XElement Ele = new XElement("track", new XAttribute("id", number));
            doc.Root.LastNode.AddAfterSelf(Ele);
            doc.Save(filename);
        }//Закрыть приказ
        #endregion

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) // ОТкрытие Выборки для постановки в штат (контектное меню Добавить)
        {
            //MessageBox.Show(N.Podr1);
            Viborka.Visibility = Visibility.Visible;
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)//РСЗ
        {
            T.SendMessage("RSZ-бригадная");
        }

        private void Close_vibor_Click(object sender, RoutedEventArgs e)
        {
            Viborka.Visibility = Visibility.Hidden;
            Viborka2.Visibility = Visibility.Hidden;
        }

        private void Create_people_Click(object sender, RoutedEventArgs e)
        {
            OpenLoad N = Shtat.SelectedValue as OpenLoad;
            if (N.Lnumber != ".") return;
            OpenLoadZaShtat NN = List_vibor.SelectedValue as OpenLoadZaShtat;
            // MessageBox.Show(N.Shtat_type + " ??? " + NN.Lnumber);
            T.Create_Shtat_People(N.Id, NN.Lnumber);
            if (MonoCreate.IsChecked == false)
                Viborka.Visibility = Visibility.Hidden;
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e) //Уборка из штата
        {
            OpenLoad N = Shtat.SelectedValue as OpenLoad;
            if (N.Lnumber == ".") return;
            // MessageBox.Show(N.Shtat_type + " ??? " + NN.Lnumber);
            T.Del_Shtat_People(N.Id);
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)//Временное убытие
        {
            if (Viborka.Visibility == Visibility.Visible || Viborka2.Visibility == Visibility.Visible) return;
            Otr_grid.Visibility = Visibility.Visible;
            Osn_otr_grid.Visibility = Visibility.Visible;
            Otp_grid.Visibility = Visibility.Hidden;
            Kom_grid.Visibility = Visibility.Hidden;
            Med_grid.Visibility = Visibility.Hidden;
            Gos_grid.Visibility = Visibility.Hidden;
            Are_grid.Visibility = Visibility.Hidden;
            Soc_grid.Visibility = Visibility.Hidden;
            dr_pr_grid.Visibility = Visibility.Hidden;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox Ch = (CheckBox)sender;
            if (Ch.Name == "CBub_otp")
            {
                //if(Ch.IsChecked == false)
                VPD_otp_grid.Visibility = Visibility.Visible;
                //if (Ch.IsChecked == true)
                //    VPD_otp_grid.Visibility = Visibility.Hidden;
            }
        }

        private void Close_otp_ub_Click(object sender, RoutedEventArgs e) //Закрытие убытия
        {
            if (Otr_grid.Visibility == Visibility.Visible)
                Otr_grid.Visibility = Visibility.Hidden;

        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            ComboBoxItem CB = (ComboBoxItem)sender;
            if (CB.Content.ToString() == "Отпуск")
            {
                Osn_otr_grid.Visibility = Visibility.Hidden;
                Otp_grid.Visibility = Visibility.Visible;
            }
            if (CB.Content.ToString() == "Командировка")
            {
                Osn_otr_grid.Visibility = Visibility.Hidden;
                Kom_grid.Visibility = Visibility.Visible;
            }
            if (CB.Content.ToString() == "Мед.рота")
            {
                Osn_otr_grid.Visibility = Visibility.Hidden;
                Med_grid.Visibility = Visibility.Visible;
            }
            if (CB.Content.ToString() == "Госпиталь")
            {
                Osn_otr_grid.Visibility = Visibility.Hidden;
                Gos_grid.Visibility = Visibility.Visible;
            }
            if (CB.Content.ToString() == "Арест")
            {
                Osn_otr_grid.Visibility = Visibility.Hidden;
                Are_grid.Visibility = Visibility.Visible;
            }
            if (CB.Content.ToString() == "СОЧ")
            {
                Osn_otr_grid.Visibility = Visibility.Hidden;
                Soc_grid.Visibility = Visibility.Visible;
            }
            if (CB.Content.ToString() == "Другие причины")
            {
                Osn_otr_grid.Visibility = Visibility.Hidden;
                dr_pr_grid.Visibility = Visibility.Visible;
            }
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            ComboBoxItem CB = (ComboBoxItem)sender;
            if (CB.Content.ToString() == "Срочной службы")
            {
                Type_People_LD = "срок";
                Redactor_grid.Height = 3450;
                Kontr.Visibility = Visibility.Visible;
                Dop_LD_kontr.Visibility = Visibility.Hidden;
                Dop_LD_Zvanie.Margin = new Thickness(0,1180,0,0);
                Dop_LD_Posl.Visibility = Visibility.Hidden;
                Dop_LD_Family.Margin = new Thickness(0, 1810, 0, 0);
                Dop_LD_Docum.Margin = new Thickness(0, 2970, 0, 0);
                Dop_nagrad.Visibility = Visibility.Hidden;
                Dop.Visibility = Visibility.Hidden;
                Save_Btn.Margin = new Thickness(0, 3415, 0, 0);
            }
            if (CB.Content.ToString() == "Контрактной службы")
            {
                Type_People_LD = "контр";
                Redactor_grid.Height = 5000;
                Kontr.Visibility = Visibility.Visible;
                Dop_LD_kontr.Visibility = Visibility.Visible;
                Dop_LD_Zvanie.Margin = new Thickness(0, 1810, 0, 0);
                Dop_LD_Posl.Visibility = Visibility.Visible;
                Dop_LD_Family.Margin = new Thickness(0, 2970, 0, 0);
                Dop_LD_Docum.Margin = new Thickness(0, 3415, 0, 0);
                Dop_nagrad.Visibility = Visibility.Visible;
                Dop.Visibility = Visibility.Visible;
                Save_Btn.Margin = new Thickness(0, 4900, 0, 0);
            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            Viborka2.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenLoad NN = Shtat.SelectedValue as OpenLoad;
                OpenLoad NNN = List_vibor2.SelectedValue as OpenLoad;
                OpenLoad tmp = new OpenLoad();
                Button N = (Button)sender;
                if (NN.Lnumber == ".") return;
                if (N.Content.ToString() == "+")
                {
                    tmp = NN;
                    T.Create_viborka(tmp, "+");
                }
                else if (N.Content.ToString() == "-")
                {
                    tmp.Lnumber = NNN.Lnumber;
                    T.Create_viborka(tmp, "-");
                }
            }
            catch
            {

            }
        }



        private void MenuItem_Click_8(object sender, RoutedEventArgs e) //Добавление наряда
        {
            try
            {
                OpenLoad NN = Shtat.SelectedValue as OpenLoad;
                T._Delete_people(NN);
                //tmp.Date = ЗАВТРА
            }
            catch
            {

            }
        }
        private void MenuItem_Click_7(object sender, RoutedEventArgs e)//Отбор на контракт
        {
            try
            {
                OpenLoad NN = Shtat.SelectedValue as OpenLoad;
                if (NN.Lnumber == ".") return;
                if (NN.Types != "О" && NN.Types != "контр" && NN.Types != "жен")
                {
                    Otbor_na_kontr N = new Otbor_na_kontr();
                    N.Lnumber = NN.Lnumber;
                    N.FIO = NN.Fio;
                    DateTime DT = DateTime.Today;
                    N.Date1 = DT.Day + "." + DT.Month + "." + DT.Year;
                    T.New_otbor(N);
                }
            }
            catch { }
        }
        private void MenuItem_Click_9(object sender, RoutedEventArgs e) //передача в кадры
        {
            try
            {
                Otbor_na_kontr NN = OK_Meropr.SelectedValue as Otbor_na_kontr;
                T.Pered_vkadry(NN);
            }
            catch { }
        }

        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            try
            {
                Otbor_na_kontr NN = OK_VPricaz.SelectedValue as Otbor_na_kontr;
                T.OK_sost(NN);
            }
            catch { }
        }
        private void Otkaz_OK1(object sender, RoutedEventArgs e)
        {
            try
            {
                Otbor_na_kontr NN = OK_Meropr.SelectedValue as Otbor_na_kontr;
                T.OK_otkaz(NN, 2);
            }
            catch { }
        }
        private void Otkaz_OK2(object sender, RoutedEventArgs e)
        {
            try
            {
                Otbor_na_kontr NN = OK_VPricaz.SelectedValue as Otbor_na_kontr;
                T.OK_otkaz(NN, 2);
            }
            catch { }
        }

        private void MenuItem_Click_11(object sender, RoutedEventArgs e)
        {
            try
            {
                Otbor_na_kontr NN = OK_Otkaz.SelectedValue as Otbor_na_kontr;
                T.OK_Snowa_otbor(NN);
            }
            catch { }
        }

        private void Create_otp_ub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenLoad N = Shtat.SelectedValue as OpenLoad;
                Button btn = (Button)sender;
                if (btn.Name == "Create_otp_ub")
                {
                    ComboBoxItem i = (ComboBoxItem)TB_vrio_otp.SelectedItem;
                    ComboBoxItem CB = Types_otp.SelectedValue as ComboBoxItem;
                    Ub tmp = new Ub();
                    tmp.Dolg = N.Dolgnost;
                    tmp.FIO = N.Fio;
                    tmp.Lnumber = N.Lnumber;
                    tmp.Zvanie = N.Zvanie;
                    tmp.Types = N.Types;
                    tmp.Podr = N.Podr;
                    tmp.Types_ub = "отпуск";
                    tmp.Cel = CB.Content.ToString() + "_за_"+TB_year_otp.Text+"_на_"+ TB_srok_otp.Text;
                    tmp.Mesto = TB_mesto_otp.Text;
                    tmp.Date_ub = TB_date1_otp.SelectedDate.Value.ToString();
                    tmp.Date_preb = TB_date2_otp.SelectedDate.Value.ToString();
                    tmp.Osn = TB_osn_otp.Text;
                    //tmp.Vrio = i.Content.ToString();
                    if(CBub_otp.IsChecked == true)
                    {
                        tmp.VPD_ST1 = TB_st1_otp.Text;
                        tmp.VPD_ST2 = TB_st2_otp.Text;
                        tmp.VPD_ST3 = TB_st3_otp.Text;
                        tmp.VPD_ST4 = TB_st4_otp.Text;
                        tmp.VPD_ST5 = TB_st5_otp.Text;
                        tmp.VPD_ST6 = TB_st6_otp.Text;
                        tmp.VPD_ST7 = TB_st7_otp.Text;
                    }
                    T.new_ub(tmp);
                } //отпуск
                if (btn.Name == "Create_otp_ub2")
                {
                    ComboBoxItem i = (ComboBoxItem)VRIO_ub_kom.SelectedItem;
                    ComboBoxItem CB = Type_ub_kom.SelectedValue as ComboBoxItem;
                    Ub tmp = new Ub();
                    tmp.Dolg = N.Dolgnost;
                    tmp.FIO = N.Fio;
                    tmp.Lnumber = N.Lnumber;
                    tmp.Zvanie = N.Zvanie;
                    tmp.Types_ub = "командировка";
                    tmp.Podr = N.Podr;
                    tmp.Types = N.Types;
                    tmp.Date_ub = Date_ub_kom.SelectedDate.Value.ToString();
                    tmp.Mesto = Mes_ub_kom.Text;
                    tmp.Cel = Cel_ub_kom.Text;
                    tmp.Osn = Osn_ub_kom.Text;
                    //tmp.Vrio = i.Content.ToString();
                    if (VPD_ub_kom.IsChecked == true)
                    {
                        tmp.VPD_ST1 = ST1_ub_kom.Text;
                        tmp.VPD_ST2 = ST2_ub_kom.Text;
                        tmp.VPD_ST3 = ST3_ub_kom.Text;
                        tmp.VPD_ST4 = ST4_ub_kom.Text;
                        tmp.VPD_ST5 = ST5_ub_kom.Text;
                        tmp.VPD_ST6 = ST6_ub_kom.Text;
                        tmp.VPD_ST7 = ST7_ub_kom.Text;
                    }
                    T.new_ub(tmp);

                } //командировка
                if (btn.Name == "Create_otp_ub3")
                {
                    ComboBoxItem i = (ComboBoxItem)CB_ub_Gos.SelectedItem;
                    Ub tmp = new Ub();
                    tmp.Dolg = N.Dolgnost;
                    tmp.FIO = N.Fio;
                    tmp.Lnumber = N.Lnumber;
                    tmp.Zvanie = N.Zvanie;
                    tmp.Types_ub = "госпиталь";
                    tmp.Podr = N.Podr;
                    tmp.Types = N.Types;
                    tmp.Mesto = i.Content.ToString();
                    tmp.Date_ub = Date_gos_ub.SelectedDate.Value.ToString();
                    tmp.Osn = Osn_gos_ub.Text;
                    T.new_ub(tmp);
                }// Госпиталь
                if (btn.Name == "Create_otp_ub4")
                {
                    Ub tmp = new Ub();
                    tmp.Dolg = N.Dolgnost;
                    tmp.FIO = N.Fio;
                    tmp.Lnumber = N.Lnumber;
                    tmp.Zvanie = N.Zvanie;
                    tmp.Types_ub = "мед.рота";
                    tmp.Podr = N.Podr;
                    tmp.Types = N.Types;
                    tmp.Date_ub = Date_med_ub.SelectedDate.Value.ToString();
                    tmp.Osn = Osn_med_ub.Text;
                    tmp.Cel = "лечение";
                    tmp.Mesto = "Медицинская рота";
                    T.new_ub(tmp);
                }// мед рота
                if (btn.Name == "Create_otp_ub5")
                {
                    Ub tmp = new Ub();
                    tmp.Dolg = N.Dolgnost;
                    tmp.FIO = N.Fio;
                    tmp.Lnumber = N.Lnumber;
                    tmp.Zvanie = N.Zvanie;
                    tmp.Types_ub = "арест";
                    tmp.Podr = N.Podr;
                    tmp.Types = N.Types;
                    tmp.Date_ub = Date_are_ub.SelectedDate.Value.ToString();
                    tmp.Osn = Osn_are_ub.Text;
                    T.new_ub(tmp);

                }// арест
                if (btn.Name == "Create_otp_ub6")
                {
                    Ub tmp = new Ub();
                    tmp.Dolg = N.Dolgnost;
                    tmp.FIO = N.Fio;
                    tmp.Lnumber = N.Lnumber;
                    tmp.Zvanie = N.Zvanie;
                    tmp.Types_ub = "СОЧ";
                    tmp.Podr = N.Podr;
                    tmp.Types = N.Types;
                    tmp.Date_ub = Soch_ub_date.SelectedDate.Value.ToString();
                    T.new_ub(tmp);
                }// соч
                if (btn.Name == "Create_otp_ub7")
                {
                    Ub tmp = new Ub();
                    tmp.Dolg = N.Dolgnost;
                    tmp.FIO = N.Fio;
                    tmp.Lnumber = N.Lnumber;
                    tmp.Zvanie = N.Zvanie;
                    tmp.Types_ub = "другие причины";
                    tmp.Podr = N.Podr;
                    tmp.Types = N.Types;
                    tmp.Cel = Prich_ub_dr.Text;
                    tmp.Date_ub = Date_ub_dr.SelectedDate.Value.ToString();
                    tmp.Osn = Osn_ub_dr.Text;
                    T.new_ub(tmp);
                }//Другие причины
                Otr_grid.Visibility = Visibility.Hidden;
            }
            catch { }
        }
        ObservableCollection<Kontrakt> K1 = new ObservableCollection<Kontrakt>();
        ObservableCollection<Nagrad> N2 = new ObservableCollection<Nagrad>();
        ObservableCollection<family> F1 = new ObservableCollection<family>();
        ObservableCollection<Poslug> P2 = new ObservableCollection<Poslug>();
        ObservableCollection<zvanie> Z1 = new ObservableCollection<zvanie>();
        ObservableCollection<Docum> D1 = new ObservableCollection<Docum>();
        Photo_ ph = new Photo_();
        private void kontr_Btn_Create_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if(btn.Name == "kontr_Btn_Create")
            {
                ComboBoxItem i = (ComboBoxItem)kontr_Sl_CB.SelectedItem;
                Kontrakt K = new Kontrakt();
                if (kontr_Date_Zakl_Box.SelectedDate.Value.ToString() != "") K.data_zakl = date(kontr_Date_Zakl_Box.SelectedDate.Value);
                if (kontr_Date_okon_Box.SelectedDate.Value.ToString() != "") K.data_okon = date(kontr_Date_okon_Box.SelectedDate.Value);
                if (kontr_Date_pricaz_Box.SelectedDate.Value.ToString() != "") K.Date_pricaz = date(kontr_Date_pricaz_Box.SelectedDate.Value);
                if (kontr_Nomber_pricaz_Box.Text != "") K.Nomber_pricaz = kontr_Nomber_pricaz_Box.Text;
                K.types = i.Content.ToString();
                if (kontr_Who_pricaz_Box.Text != "") K.Who_pricaz = kontr_Who_pricaz_Box.Text;
                K1.Add(K);
                kontr_Date_Zakl_Box.Text = "";
                kontr_Date_okon_Box.Text = "";
                kontr_Date_pricaz_Box.Text = "";
                kontr_Nomber_pricaz_Box.Text = "";
                kontr_Who_pricaz_Box.Text = "";
            }
            if (btn.Name == "Zvan_Btn_Create")
            {
                ComboBoxItem i = (ComboBoxItem)Zvan_Sl_CB.SelectedItem;
                zvanie z = new zvanie();
                z.Name = i.Content.ToString();
                if (Zvan_Date_pricaz_Box.SelectedDate.Value.ToString() != "") z.Date = date(Zvan_Date_pricaz_Box.SelectedDate.Value);
                if (Zvan_Nomber_pricaz_Box.Text != "") z.Pr = int.Parse(Zvan_Nomber_pricaz_Box.Text);
                if (Zvan_Who_pricaz_Box.Text != "") z.What = Zvan_Who_pricaz_Box.Text;
                Z1.Add(z);
                Zvan_Date_pricaz_Box.Text = "";
                Zvan_Nomber_pricaz_Box.Text = "";
                Zvan_Who_pricaz_Box.Text = "";
            }
            if (btn.Name == "Posl_Btn_Create")
            {
                Poslug p = new Poslug();
                if (Posl_chast_Box.Text != "") p.Chast_ = Posl_chast_Box.Text;
                if (Posl_dolg_Box.Text != "") p.Dolgnost_ = Posl_dolg_Box.Text;
                if (Posl_podr_Box.Text != "") p.Podr_ = Posl_podr_Box.Text;
                if (Posl_vus_Box.Text != "") p.VUS_ = Posl_vus_Box.Text;
                if (Posl_who_pricaz_Box.Text != "") p.Name_pricaz = Posl_who_pricaz_Box.Text;
                if (Posl_nomber_pricaz_Box.Text != "") p.Nomber_pricaz = Posl_nomber_pricaz_Box.Text;
                if (Posl_date_pricaz_Box.SelectedDate.Value.ToString() != "") p.Date_pricaz = date(Posl_date_pricaz_Box.SelectedDate.Value);
                P2.Add(p);
                Posl_chast_Box.Text = "";
                Posl_dolg_Box.Text = "";
                Posl_podr_Box.Text = "";
                Posl_vus_Box.Text = "";
                Posl_who_pricaz_Box.Text = "";
                Posl_nomber_pricaz_Box.Text = "";
                Posl_date_pricaz_Box.Text = "";
            }
            if (btn.Name == "Family_Btn_Create")
            {
                ComboBoxItem i = (ComboBoxItem)Family_Sl_CB.SelectedItem;
                family f = new family();
                if (Family_FIO_Box.Text != "") f.Name = Family_FIO_Box.Text;
                f.types = i.Content.ToString();
                if (Family_Brd_Box.SelectedDate.Value.ToString() != "") f.Brd = date(Family_Brd_Box.SelectedDate.Value);
                F1.Add(f);
                Family_FIO_Box.Text = "";
                Family_Brd_Box.Text = "";
            }
            if (btn.Name == "Nagrad_Btn_Create")
            {
                Nagrad n = new Nagrad();
                if (Nagrad_type_Box.Text != "") n.Type = Nagrad_type_Box.Text;
                if (Nagrad_Who_pricaz_Box.Text != "") n.Who_pricaz = Nagrad_Who_pricaz_Box.Text;
                if (Nagrad_pricaz_Box.Text != "") n.Pricaz = Nagrad_pricaz_Box.Text;
                if (Nagrad_Date_Box.SelectedDate.Value.ToString() != "") n.Date_ = date(Nagrad_Date_Box.SelectedDate.Value);
                if (Nagrad_date_pricaz_Box.SelectedDate.Value.ToString() != "") n.Date_pricaz = date(Nagrad_date_pricaz_Box.SelectedDate.Value);
                Nagrad_type_Box.Text = "";
                Nagrad_Who_pricaz_Box.Text = "";
                Nagrad_pricaz_Box.Text = "";
                Nagrad_Date_Box.Text = "";
                Nagrad_date_pricaz_Box.Text = "";
            }
        }
        private void Regim_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LD_BOOL == false)
                {
                    Oblus.Items.Remove(Otryv_LD);
                    Oblus.Items.Remove(Documentation);
                    Oblus.Items.Remove(Info);
                    Oblus.Items.Remove(Perem);
                    Oblus.Items.Remove(Information);
                    Oblus.Items.Add(Redactor);
                    LD_BOOL = true;
                    return;
                }
                else
                if (LD_BOOL == true)
                {
                    Oblus.Items.Add(Otryv_LD);
                    Oblus.Items.Add(Documentation);
                    Oblus.Items.Add(Info);
                    Oblus.Items.Add(Perem);
                    Oblus.Items.Add(Information);
                    Oblus.Items.Remove(Redactor);
                    LD_BOOL = false;
                    return;
                }
            }
            catch { }
        }

        bool b1 = false;
        bool b2 = false;
        bool b3 = false;
        bool b4 = false;
        bool b5 = false;
        private void Open_table_LD_modal1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if(btn.Name == "Open_table_LD_modal1")
                {
                    if(b1 == false)
                    {
                        LD_modal_table1.Visibility = Visibility.Visible;
                        b1 = true;
                    }
                    else
                    {
                        LD_modal_table1.Visibility = Visibility.Hidden;
                        b1 = false;
                    }
                    
                }
                if (btn.Name == "Open_table_LD_modal2")
                {
                    if (b2 == false)
                    {
                        LD_modal_table2.Visibility = Visibility.Visible;
                        b2 = true;
                    }
                    else
                    {
                        LD_modal_table2.Visibility = Visibility.Hidden;
                        b2 = false;
                    }
                    
                }
                if (btn.Name == "Open_table_LD_modal3")
                {
                    if (b3 == false)
                    {
                        LD_modal_table3.Visibility = Visibility.Visible;
                        b3 = true;
                    }
                    else
                    {
                        LD_modal_table3.Visibility = Visibility.Hidden;
                        b3 = false;
                    }
                    
                }
                if (btn.Name == "Open_table_LD_modal4")
                {
                    if (b4 == false)
                    {
                        LD_modal_table4.Visibility = Visibility.Visible;
                        b4 = true;
                    }
                    else
                    {
                        LD_modal_table4.Visibility = Visibility.Hidden;
                        b4 = false;
                    }
                    
                }
                if (btn.Name == "Open_table_LD_modal5")
                {
                    if (b5 == false)
                    {
                        LD_modal_table5.Visibility = Visibility.Visible;
                        b5 = true;
                    }
                    else
                    {
                        LD_modal_table5.Visibility = Visibility.Hidden;
                        b5 = false;
                    }
                }
            }
            catch { }
        }

        private void Search_LN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string message = LN_TBox.Text;
                T.SendMessage("Search|" + message);
            }
            catch { }
        }
        private void MenuItem_Click_12(object sender, RoutedEventArgs e)//увольнение, перевод
        {
            try
            {
                OpenLoadZaShtat N = AnShtat.SelectedValue as OpenLoadZaShtat;
                MenuItem t = (MenuItem)sender;
                if (t.Header.ToString() == "Уволить")
                {
                    T.Delete_people(N);
                }
                else
                    if (t.Header.ToString() == "Перевести")
                {
                    T.Perevod_people(N);
                }
            }
            catch { }
        }
        string date(DateTime d)
        {
            string messege = "";
            if (d.Day == 1 || d.Day == 2 || d.Day == 3 || d.Day == 4 || d.Day == 5 || d.Day == 6 || d.Day == 7 || d.Day == 8 || d.Day == 9)
                messege = "0" + d.Day.ToString();
            else
                messege = d.Day.ToString();
            if (d.Month == 1 || d.Month == 2 || d.Month == 3 || d.Month == 4 || d.Month == 5 || d.Month == 6 || d.Month == 7 || d.Month == 8 || d.Month == 9)
                messege += ".0" + d.Month.ToString();
            else
                messege += "." + d.Month.ToString();
            messege += "." + d.Year.ToString();
            return messege;
        }
        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            #region Первый блок ЛИЧНЫЕ ДАННЫЕ
            ComboBoxItem t = (ComboBoxItem)pol_CB.SelectedItem;
            Dannie t1 = new Dannie();
            if(LD_LN_Box.Text != "") t1.Lnumber = LD_LN_Box.Text;
            if (LD_FIO_Box.Text != "") t1.FIO = LD_FIO_Box.Text;
            if (t.Content.ToString() != "") t1.POL = t.Content.ToString();
            if (t1.POL == "жен" && Type_People_LD == "контр") t1.Types = "жен";
            if (t1.POL == "муж" && Type_People_LD == "контр") t1.Types = "контр";
            if (t1.POL == "муж" && Type_People_LD == "срок")
            {
                foreach(zvanie i in Z1)
                {
                    if(i.Name == "рядовой")
                    {
                        string it = "";
                        string[] word = i.Date.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        if (word[1] != "08" && word[1] != "09" && word[1] != "10" && word[1] != "11" && word[1] != "12")
                        {
                            it = "2-";
                        }
                        else it = "1-";
                        it += word[2];
                        t1.Types = it;
                    }
                }
            }
            if (LD_Date_Brd_Box.SelectedDate.Value.ToString() != "")
            {
                t1.Date_brd = date(LD_Date_Brd_Box.SelectedDate.Value);
            }
            if (LD_Mest_Brd_Box.Text != "") t1.Mesto_brd = LD_Mest_Brd_Box.Text;
            if (LD_Nac_Box.Text != "") t1.Nac = LD_Nac_Box.Text;
            if (LD_Gr_Obr_Box.Text != "") t1.Gr_obr = LD_Gr_Obr_Box.Text;
            if (LD_Voen_Obr_Box.Text != "") t1.Voen_obr = LD_Voen_Obr_Box.Text;
            if (LD_Bank_Rec_Box.Text != "") t1.Bank_card = LD_Bank_Rec_Box.Text;
            if (LD_Vod_Udost_Box.Text != "") t1.Vod_prava = LD_Vod_Udost_Box.Text;
            if (LD_Voenkom_Box.Text != "") t1.Voenkomat = LD_Voenkom_Box.Text;
            if (LD_Home_adress_Box.Text != "") t1.Home_adres = LD_Home_adress_Box.Text;
            if (Docum_PAC_Box.Text != "") t1.PAC = Docum_PAC_Box.Text;
            LD_LN_Box.Text = "";
            LD_FIO_Box.Text = "";
            LD_Date_Brd_Box.Text = "";
            LD_Mest_Brd_Box.Text = "";
            LD_Nac_Box.Text = "";
            LD_Gr_Obr_Box.Text = "";
            LD_Voen_Obr_Box.Text = "";
            LD_Bank_Rec_Box.Text = "";
            LD_Vod_Udost_Box.Text = "";
            LD_Voenkom_Box.Text = "";
            LD_Home_adress_Box.Text = "";
            Docum_PAC_Box.Text = "";
            #endregion
            #region Второй блок ДОКУМЕНТЫ
            Docum d1 = new Docum(); //Паспорт
            if (Pass_ser_Box.Text != "") d1.Seriya = Pass_ser_Box.Text;
            d1.Kod = "01";
            if (Pass_nomber_Box.Text != "") d1.Nomber = Pass_nomber_Box.Text;
            if (Pass_who_Box.Text != "") d1.Who_vidal = Pass_who_Box.Text;
            if (Pass_date_Box.Text != "") d1.Date_vid = date(Pass_date_Box.SelectedDate.Value);
            Pass_ser_Box.Text = "";
            Pass_nomber_Box.Text = "";
            Pass_who_Box.Text = "";
            Pass_date_Box.Text = "";
            Docum d2 = new Docum();//Удостоверение
            d2.Kod = "04";
            if (Udost_ser_Box.Text != "") d2.Seriya = Udost_ser_Box.Text;
            if (Udost_nomber_Box.Text != "") d2.Nomber = Udost_nomber_Box.Text;
            if (Udost_who_Box.Text != "") d2.Who_vidal = Udost_who_Box.Text;
            if (Udost_date_Box.Text != "") d2.Date_vid = date(Udost_date_Box.SelectedDate.Value);
            Udost_ser_Box.Text = "";
            Udost_nomber_Box.Text = "";
            Udost_who_Box.Text = "";
            Udost_date_Box.Text = "";
            D1.Add(d1);
            D1.Add(d2);
            #endregion
            #region Третий блок НАЗНАЧЕНИЕ/ПРИЕМ ДЕЛ/ВЫЖИВАЙКА
            Naznach N1 = new Naznach();
            if (Naznach_Nomber_Box.Text != "") N1.pricaz = Naznach_Nomber_Box.Text;
            if (Naznach_who_pricaz_Box.Text != "") N1.who_pricaz = Naznach_who_pricaz_Box.Text;
            if (Naznach_date_Box.Text != "") N1.date_pricaz = date(Naznach_date_Box.SelectedDate.Value);
            Naznach_Nomber_Box.Text = "";
            Naznach_who_pricaz_Box.Text = "";
            Naznach_date_Box.Text = "";
            PriemDel P1 = new PriemDel();
            if (priemDel_Nomber_pricaz_Box.Text != "") P1.pricaz = priemDel_Nomber_pricaz_Box.Text;
            if (PriemDel_who_pricaz_Box.Text != "") P1.who_pricaz = PriemDel_who_pricaz_Box.Text;
            if (priemDel_Date_pricaz_Box.Text != "") P1.date_pricaz = date(priemDel_Date_pricaz_Box.SelectedDate.Value);
            priemDel_Nomber_pricaz_Box.Text = "";
            PriemDel_who_pricaz_Box.Text = "";
            priemDel_Date_pricaz_Box.Text = "";
            Vizhivanie V1 = new Vizhivanie();
            if (Vigivanie_Mesto_Box.Text != "") V1.Mesto = Vigivanie_Mesto_Box.Text;
            if (Vigivanie_period_Box.Text != "") V1.Period = Vigivanie_period_Box.Text;
            if (Vigivanie_osnovanie_Box.Text != "") V1.Osnovanie = Vigivanie_osnovanie_Box.Text;
            Vigivanie_Mesto_Box.Text = "";
            Vigivanie_period_Box.Text = "";
            Vigivanie_osnovanie_Box.Text = "";
            #endregion
            T.Create_LD(Z1, P2, F1, N2, ph, D1, t1, K1, N1, P1, V1);
            OpenLoadZaShtat tt = new OpenLoadZaShtat();
            tt.FIO = t1.FIO;
            tt.Lnumber = t1.Lnumber;
            
            if (Type_People_LD == "срок")
            {
                foreach(zvanie i in Z1)
                {
                    if(i.Name == "рядовой")
                    {
                        tt.Types = RD(i.Date);
                        tt.Zvanie = i.Name;
                    }
                    if(i.Name == "ефрейтор")
                        tt.Zvanie = i.Name;
                    if (i.Name == "младший сержант")
                        tt.Zvanie = i.Name;
                }
            }
            else
            {
                if (t1.POL == "жен")
                    tt.Types = "жен";
                else
                    tt.Types = "контр";
                foreach (zvanie i in Z1)
                {
                    if (i.Name == "рядовой")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "ефрейтор")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "младший сержант")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "сержант")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "старший сержант")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "старшина")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "прапорщик")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "старший прапорщик")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "лейтенант")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "старший лейтенант")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "капитан")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "майор")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "подполковник")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "полковник")
                    {
                        tt.Zvanie = i.Name;
                    }
                    if (i.Name == "генерал-майор")
                    {
                        tt.Zvanie = i.Name;
                    }
                }
            }
            T.zashtatLD(tt);
            Izmen Td = new Izmen();
            Td.Types = "Удалить";
            Td.Name_table = "Новое личное дело";
            Td.Old_value = tt.Lnumber;
            Td.New_value = ".";
            Td.Col = tt.Last_dolg + ">" + tt.Last_podr + ">" + tt.FIO + ">" + tt.Zvanie + ">" + tt.Types;
            T.SendMessage("izm|" + Td.Types + "|" + Td.Row + "|" + Td.Name_table + "|" + Td.Old_value + "|" + Td.New_value + "|" + Td.Col);
        }
        private string RD(string w)
        {
            string d = ".";
            string[] word = w.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (word[1] == "08" || word[1] == "09" || word[1] == "10" || word[1] == "11" || word[1] == "12")
            {
                d = "2-";
            }
            else
                d = "1-";
            d += word[2];
            return d;
        }
        private void Otm_preb_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = (Button)sender;
            if (Btn.Name == "Otm_preb")
            {
                Preb_grid.Visibility = Visibility.Hidden;
                Date_preb.Text = "";
                Osn_preb.Text = "";
            }
            if (Btn.Name == "Create_preb")
            {
                Ub N = Ub_Table.SelectedValue as Ub;
                N.Date3 = Date_preb.SelectedDate.Value.ToString();
                N.Osn = Osn_preb.Text;
                T.new_ub(N);
                Date_preb.Text = "";
                Osn_preb.Text = "";
                Preb_grid.Visibility = Visibility.Hidden;
            }
        }

        private void MenuItem_Click_13(object sender, RoutedEventArgs e)
        {
            MenuItem I = (MenuItem)sender;
            if (I.Header.ToString() == "Пребытие")
            {
                Preb_grid.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//Сохранение наряда
        {

        }

        private void Create_people2_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem t = (ComboBoxItem)pol_CB.SelectedItem;
            if (t.Content.ToString() == "Наряд")
            {

            }
            if (t.Content.ToString() == "Пребытие")
            {

            }
            if (t.Content.ToString() == "Убытие")
            {

            }
            if (t.Content.ToString() == "Отбор на контракт")
            {
                
            }
        }

        private void kotel_Click(object sender, RoutedEventArgs e)
        {
            MenuItem i = (MenuItem)sender;
            if(i.Name == "kotel")
            {
                OpenLoad N = Shtat.SelectedValue as OpenLoad;
                T.Creat_kotel(N, "Добавить");
            }
            if(i.Name == "kotel1")
            {
                OpenLoad N = Shtat.SelectedValue as OpenLoad;
                T.Creat_kotel(N, "Убрать");
            }
        }

        private void iskl_perev_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if(b.Name == "iskl_perev")
            {

            }
            if (b.Name == "iskl_del")
            {

            }
        }
    }
}
