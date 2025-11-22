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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для DodavProdukts.xaml
    /// </summary>
    public partial class DodavProdukts : Window
    {
        public static NewTechnologiEntities entities = new NewTechnologiEntities();
        public static int idpartners;
        public static int idapplication;
        public static int idproduct;
        public static int kolvo;
        public DodavProdukts()
        {
            InitializeComponent();

            var product = entities.Products_.ToList();
            ListProdukt.ItemsSource = entities.Products_.ToList();
            TypeProduct.ItemsSource = entities.Type_products_.ToList();


            idapplication =RedakZayvka.idzayvka;
            idpartners = RedakZayvka.idpart;
        }

        private void DobavitVZayvky_Click(object sender, RoutedEventArgs e)
        {
            if (ListProdukt.SelectedItem == null)
            {
                MessageBox.Show("Выберите продукцию из списка!");
                return;
            }

            var products_application = new Products_Application
            {
                ID_partner_request = idapplication,
                ID_products = idproduct,
                Number_products = int.Parse(CountProduct.Text),
            };

           MainWindow.entities.Products_Application.Add(products_application);
           MainWindow.entities.SaveChanges();

            // После добавления продукции:
            var product = MainWindow.entities.Products_.First(p => p.ID == idproduct);
            var itog = product.Minimum_cost_partner * int.Parse(CountProduct.Text);
            var zayvka = MainWindow.entities.Partner_products_request_.First(z => z.ID == idapplication);
            zayvka.Cost += itog;
            MainWindow.entities.SaveChanges();
            MessageBox.Show("Продукция добавлена в заявку!");
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            int CountB = Convert.ToInt32(CountProduct.Text);
            if (CountB > 0)
            {
                CountB--;
                CountProduct.Text = CountB.ToString();
            }
            else
            {
                CountProduct.Text = "0";
            }
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            int CountB = Convert.ToInt32(CountProduct.Text);
            if (CountB <= 99)
            {
                CountB++;
                CountProduct.Text = CountB.ToString();
            }
            else
            {
                CountProduct.Text = "99";
            }
        }

        private void ListProdukt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectProduct = ListProdukt.SelectedItem as Products_;
            CountProduct.Text = "0";
            kolvo = int.Parse(CountProduct.Text);
            Name.Text = SelectProduct.Name_products;
            idproduct = SelectProduct.ID;
            Articul.Content = SelectProduct.Article;
            Price.Content = SelectProduct.Minimum_cost_partner.ToString();
            TypeProduct.Text = SelectProduct.Type_products_.Type;
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
        }
    }
}
