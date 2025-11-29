using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Class1
    {
        public int ID { get; set; }
        public int ID_partner_request { get; set; }
        public int ID_products { get; set; }
        public int Number_products { get; set; }
        public bool qs { get; set; }

        public string Name_products { get; set; }
        public double Article { get; set; }



        public Class1(int ID, int ID_partner_request, int ID_products, int Number_products, bool qs, string Name_products, double Article)
        {
            this.ID = ID;
            this.ID_partner_request = ID_partner_request;
            this.ID_products = ID_products;
            this.Number_products = Number_products;
            this.qs = qs;
            this.Name_products = Name_products;
            this.Article = Article;
        }
    }
}
