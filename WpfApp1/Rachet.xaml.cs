using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для Rachet.xaml
    /// </summary>
    public partial class Rachet : Window
    {
        /// <summary>
        /// typeprodid - переменная для кода типа продукции
        /// typematerialid - переменная для кода типа материала
        /// </summary>
        public static int typeprodid;
        public static int typematerialid;
        public static int numberprodukt;
        public static int kolvonasklad;
        public static double par1;
        public static double par2;

        /// <summary>
        /// productlist - лист для типов продукции
        /// materiallist - лист для типов материала
        /// </summary>

        public static List<Type_products_> productlist = MainWindow.entities.Type_products_.ToList();

        public static List<Material_type_> materiallist = MainWindow.entities.Material_type_.ToList();
        public Rachet()
        {
            InitializeComponent();

            /// <summary>
            ///  TypeProduct.ItemsSource - заполенение данными из бд в ComboBox
            ///  TypeMaterial.ItemsSource - заполенение данными из бд в ComboBox
            /// </summary>

            TypeProduct.ItemsSource = MainWindow.entities.Type_products_.ToList();
            TypeMaterial.ItemsSource = MainWindow.entities.Material_type_.ToList();
        }

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void ras_Click(object sender, RoutedEventArgs e)
        {
            /// <summary>
            ///   Itog.Text - получение результата
            /// </summary>
            typeprodid = productlist.FirstOrDefault(a => a.Type == TypeProduct.Text).ID;
            typematerialid = materiallist.FirstOrDefault(a => a.Type == TypeMaterial.Text).ID;

            numberprodukt = int.Parse(kolvoprodukt.Text);
            kolvonasklad = int.Parse(kolvonasklade.Text);
            par1 = Convert.ToDouble(prametr1.Text, CultureInfo.InvariantCulture);
            par2 = Convert.ToDouble(prametr2.Text, CultureInfo.InvariantCulture);

            Itog.Text = raschetmetod(typeprodid, typematerialid, numberprodukt, kolvonasklad, par1, par2).ToString();        
        }

        /// <summary>
        ///  raschetmetod - метод для рассчёта одинакого количества материала
        /// </summary>
        public static int raschetmetod(int type1id, int type2id,int kolvoprodukt,int kolvonaskl,double param1, double param2)
        {
            try
            {
                // Проверка входных параметров
                if (type1id <= 0 || type2id <= 0 ||
                    kolvoprodukt < 0 || kolvonaskl < 0 ||
                    param1 <= 0 || param2 <= 0)
                {
                    return -1;
                }

                int productionNeeded = kolvoprodukt - kolvonaskl;
                if (productionNeeded <= 0)
                {
                    return 0; 
                }

                if (type1id <= 0 || type2id < 0 || type2id >= 100)
                {
                    return -1;
                }

                double materialPerUnit = param1 * param2 * type1id;

                double adjustedMaterialPerUnit = materialPerUnit / (1 - type2id / 100.0);

                double totalMaterial = productionNeeded * adjustedMaterialPerUnit;

                int itogi = (int)Math.Ceiling(totalMaterial);

                return itogi;
            }
            catch
            {
                return -1;
            }
        }   
    }
}

