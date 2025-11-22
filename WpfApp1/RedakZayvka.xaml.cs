using System;
using System.Collections;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для RedakZayvka.xaml
    /// </summary>
    public partial class RedakZayvka : Window
    {
        public static int idzayvka;
        public static DateTime datee;
        public static string parti;
        public static int idpart;
        public static double sumik;
        public static List<Partners_> partnerslist = MainWindow.entities.Partners_.ToList();

        List<Products_Application> prapp = MainWindow.entities.Products_Application.ToList();
        public static List<Class1> produksappli = new List<Class1>();
        public List<Class1> list = new List<Class1>();

        public RedakZayvka()
        {
            InitializeComponent();
            list.Clear();

            var productsForThisOrder = from pa in MainWindow.entities.Products_Application
                                       join p in MainWindow.entities.Products_ on pa.ID_products equals p.ID
                                       where pa.ID_partner_request == idzayvka
                                       select new
                                       {
                                           pa.ID,
                                           pa.ID_partner_request,
                                           pa.ID_products,
                                           pa.Number_products,
                                           p.Name_products,
                                           p.Article
                                       };

            foreach (var product in productsForThisOrder)
            {
                list.Add(new Class1(
                    product.ID,
                    product.ID_partner_request,
                    product.ID_products,
                    product.Number_products,
                    true,
                    product.Name_products,
                    product.Article
                ));
            }

            SpisokProduk.ItemsSource = list;
            SetOrderData();
        }

        private void izmen_Click(object sender, RoutedEventArgs e)
        {
            var qwer = MainWindow.entities.Partner_products_request_.FirstOrDefault(a => a.ID == idzayvka);
            qwer.DateApplication = DateTime.Parse(datePick.Text);
            qwer.ID_partners= partnerslist.FirstOrDefault(a => a.Name_partners == parther.Text).ID;
            qwer.Cost = double.Parse(summaik.Text);
            MainWindow.entities.SaveChanges();
            MessageBox.Show("Изменения успешно сохранены!");
            MainWindow mainWindow = new MainWindow();
            this.Hide();
        }
        public void SetOrderData()
        {
            idzayvka = MainWindow.idi;
            kodzayvki.Text = idzayvka.ToString();
            datee = MainWindow.dat;
            datePick.SelectedDate = datee;
            parti = MainWindow.parts;
            parther.Text = parti;
            idpart = MainWindow.idparts;
            sumik = MainWindow.su;
            summaik.Text = sumik.ToString();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            DodavProdukts dodavProdukts = new DodavProdukts();
            dodavProdukts.Show();           
            var select = MainWindow.entities.Partner_products_request_.FirstOrDefault(a => a.ID_partners == idpart);
            if (select != null)
            {
                idzayvka = select.ID;
                idpart = select.ID_partners;
            }
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            var hyperlink = sender as Hyperlink;
            if (hyperlink != null)
            {
                Class1 Products_Application = hyperlink.DataContext as Class1;

                if (Products_Application != null)
                {
                    var qw = MainWindow.entities.Products_Application.FirstOrDefault(a =>
                        a.ID_partner_request == idzayvka && a.ID_products == Products_Application.ID_products);

                    if (qw != null)
                    {
                        MainWindow.entities.Products_Application.Remove(qw);
                        MainWindow.entities.SaveChanges();
                        MessageBox.Show("Продукция удалена!");
                    }
                }
            }
        }

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
        }
    }
}
