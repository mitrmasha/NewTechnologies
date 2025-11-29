using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public static NewTechnologiEntities entities = new NewTechnologiEntities();
        private Dictionary<WrapPanel, int> panelToPartnerIdMap = new Dictionary<WrapPanel, int>();
        public static List<Products_Application> listprod = MainWindow.entities.Products_Application.ToList();
        public static int idi;
        public static DateTime dat;
        public static string parts;
        public static double su;
        public static int idparts;

        public MainWindow()
        {
            InitializeComponent();
            TakePartners();
        }

        /// <summary>
        /// TakePartners - метод для вывода информации на форме
        /// </summary>
        private void TakePartners() //метод для вывода партнеров в листвью
        {
            try
            {
                listP.Items.Clear();
                panelToPartnerIdMap.Clear();

                var InfoParts = from p in entities.Partners_
                                join t in entities.Type_partners_ on p.ID_Type_partners equals t.ID
                                select new
                                {
                                    id = p.ID,
                                    name = p.Name_partners,
                                    type = t.Type,
                                    FIO = p.Partner_egal_address,
                                    phone = p.Phone,
                                    rate = p.Rating
                                };

                foreach (var part in InfoParts)
                {
                    WrapPanel wp = new WrapPanel();
                    wp.Width = 500;

                    TextBlock type = new TextBlock
                    {
                        Text = part.type + " | " + part.name,
                        FontSize = 16,
                        Margin = new Thickness(10, 0, 0, 0),
                        Width = wp.Width - 250,
                    };

                    List<Partner_products_request_> orders = entities.Partner_products_request_.Where(x => x.ID_partners == part.id).ToList();
                    double sum = 0;
                    for (int i = 0; i < orders.Count; i++)
                    {
                        sum += Convert.ToDouble(orders[i].Cost);
                    }

                    TextBlock discountTxt = new TextBlock
                    {
                        Text = sum + "руб",
                        FontSize = 16,
                        Margin = new Thickness(0, 0, 0, 0),
                        TextAlignment = TextAlignment.Right,
                        Width = wp.Width - 50,
                        HorizontalAlignment = HorizontalAlignment.Right
                    };

                    TextBlock role = new TextBlock
                    {
                        Text = part.FIO,
                        FontSize = 12,
                        Margin = new Thickness(10, 0, 0, 0),
                        Width = wp.Width
                    };

                    TextBlock phone = new TextBlock
                    {
                        Text = part.phone,
                        FontSize = 12,
                        Margin = new Thickness(10, 0, 0, 0),
                    };

                    TextBlock rating = new TextBlock
                    {
                        Text = "Рейтинг: " + part.rate,
                        FontSize = 12,
                        Margin = new Thickness(10, 0, 0, 0),
                        Width = wp.Width
                    };

                    wp.Children.Add(type);
                    wp.Children.Add(discountTxt);
                    wp.Children.Add(role);
                    wp.Children.Add(phone);
                    wp.Children.Add(rating);

                    listP.Items.Add(wp);

                    panelToPartnerIdMap[wp] = part.id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// redactPartnerBtn_Click - метод для передачи данных на другую форму
        /// </summary>
        private void redactPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listP.SelectedItem != null)
            {
                WrapPanel selectedPanel = listP.SelectedItem as WrapPanel;

                if (selectedPanel != null && panelToPartnerIdMap.ContainsKey(selectedPanel))
                {
                    int selectedPartnerId = panelToPartnerIdMap[selectedPanel];

                    var selectedOrder = entities.Partner_products_request_.FirstOrDefault(x => x.ID_partners == selectedPartnerId);

                    if (selectedOrder != null)
                    {
                        listprod = entities.Products_Application.Where(a => a.ID_partner_request == selectedOrder.ID).ToList();
                        idi = selectedOrder.ID;
                        dat = selectedOrder.DateApplication;
                        parts = selectedOrder.Partners_.Name_partners;
                        su = selectedOrder.Cost;
                        idparts = selectedOrder.ID_partners;
                        RedakZayvka redakZayvka = new RedakZayvka();
                        redakZayvka.Show();
                    }
                    else
                    {
                        MessageBox.Show("У выбранного партнера нет заявок!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите партнера для редактирования заявки!");
            }
        }

        private void addPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            AddApplication addApplication = new AddApplication();
            addApplication.Show();
        }

        private void predlag_Click(object sender, RoutedEventArgs e)
        {
                        PredlagaemaProdukt predlagaemaProdukt = new PredlagaemaProdukt();
                        predlagaemaProdukt.Show();
        }
    }
}