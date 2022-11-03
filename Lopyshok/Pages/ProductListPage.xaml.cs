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
using Lopyshok.DataBase;

namespace Lopyshok.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        public List<Product> products { get; set; }
        public List<ProductType> types { get; set; }
        public ProductListPage()
        {
            InitializeComponent();
            products = bd_connection.connection.Product.ToList();

            types = bd_connection.connection.ProductType.ToList();
            types.Insert(0, new ProductType { Id = 0, Name = "Все типы" });
            cbFiltr.ItemsSource = types;
            cbFiltr.DisplayMemberPath = "Name";

            this.DataContext = this;
        }

        public void Filter()
        {
            List<Product> filterProduct = products;

            if(tbSearch.Text.Trim().Length != 0)
            {
                filterProduct = filterProduct.Where(x => x.Name.Contains(tbSearch.Text.Trim())).ToList();
            }

            if(cbFiltr.SelectedItem != null)
            {
                var selectFiltr = cbFiltr.SelectedItem as ProductType;
                if (selectFiltr.Id != 0)
                {
                    filterProduct = filterProduct.Where(x => x.ProductType.Id == selectFiltr.Id).ToList();
                }
            }

            lvProduct.ItemsSource = filterProduct;
        }

        private void tbSearchSelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void cbFiltrSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
