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
    /// Логика взаимодействия для PredlagaemaProdukt.xaml
    /// </summary>
    public partial class PredlagaemaProdukt : Window
    {
        public static string parti;
        public static int idpart;

        /// <summary>
        /// SpisokProduk.ItemsSource  - вывод информации в DataGrid
        /// </summary>
        public PredlagaemaProdukt()
        {
            InitializeComponent();

            SpisokProduk.ItemsSource = MainWindow.entities.Products_.ToList();

            //parti = MainWindow.parts;
            //idpart = MainWindow.idparts;
        }

        private void raschet_Click(object sender, RoutedEventArgs e)
        {
            Rachet rachet = new Rachet();
            rachet.Show();
        }

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
