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
    /// Логика взаимодействия для AddApplication.xaml
    /// </summary>
    public partial class AddApplication : Window
    {
        /// <summary>
        /// partlist  - лист с данными о партнёрах
        /// </summary>
        public static NewTechnologiEntities entities = new NewTechnologiEntities();
        public static List<Partners_> partlist = MainWindow.entities.Partners_.ToList();
        public static int idproduct;
        public static int idzayvki;
        public static int kolvo;
        public static int idpartners;
        public AddApplication()
        {
            InitializeComponent();

            var product = entities.Products_.ToList();
            ListProdukt.ItemsSource = entities.Products_.ToList();
            TypeProduct.ItemsSource = entities.Type_products_.ToList();
            Partnersik.ItemsSource = entities.Partners_.ToList();
        }

        private void DobavitVZayvky_Click(object sender, RoutedEventArgs e)
        {
            if (idzayvki == 0)
            {
                MessageBox.Show("Сначала создайте заявку!");
                return;
            }

            if (ListProdukt.SelectedItem == null)
            {
                MessageBox.Show("Выберите продукцию из списка!");
                return;
            }
            if (Partnersik == null)
            {
                MessageBox.Show("Выберите партнёра!");
                return;
            }

            var products_application = new Products_Application
            {
                ID_partner_request = idzayvki,
                ID_products = idproduct,
                Number_products = int.Parse(CountProduct.Text),
            };

            entities.Products_Application.Add(products_application);
            entities.SaveChanges();

            // После добавления продукции:
            var product = entities.Products_.First(p => p.ID == idproduct);
            var itog = product.Minimum_cost_partner * int.Parse(CountProduct.Text);
            var zayvka = entities.Partner_products_request_.First(z => z.ID == idzayvki);
            zayvka.Cost += itog;
            entities.SaveChanges();
            MessageBox.Show("Продукция добавлена в заявку!");
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

        private void SozdatiZayvky_Click(object sender, RoutedEventArgs e)
        {
            if (Partnersik.SelectedItem == null)
            {
                MessageBox.Show("Выберите партнёра!");
            }
            else
            {
                idpartners = partlist.FirstOrDefault(a => a.Name_partners == Partnersik.Text).ID;
                Partner_products_request_ partner_Products_Request_ = new Partner_products_request_
                {
                    DateApplication = DateTime.Now,
                    ID_partners = idpartners,
                    Cost = 0,
                };

                entities.Partner_products_request_.Add(partner_Products_Request_);
                entities.SaveChanges();
                idzayvki = partner_Products_Request_.ID;
                MessageBox.Show("Заявка открыта!");
            }
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Хотите закрыть заявку?", "", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Yes:
                    MainWindow mainWindow = new MainWindow();
                    this.Hide();
                    break;
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.None:
                    break;
                default:
                    break;
            }
        }
    }
}
