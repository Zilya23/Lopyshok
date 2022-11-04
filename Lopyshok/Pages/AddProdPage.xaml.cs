using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace Lopyshok.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProdPage.xaml
    /// </summary>
    public partial class AddProdPage : Page
    {
        public static Product selectProduct { get; set; }
        public List<Workshop> workshops { get; set; }
        public List<Material> materials { get; set; }
        public List<ProductType> types { get; set; }
        public AddProdPage(Product product)
        {
            InitializeComponent();
            selectProduct = product;

            workshops = bd_connection.connection.Workshop.ToList();
            cbWork.ItemsSource = workshops;
            cbWork.DisplayMemberPath = "Name";

            types = bd_connection.connection.ProductType.ToList();
            cbType.ItemsSource = types;
            cbType.DisplayMemberPath = "Name";

            if (product != null)
            {
                cbWork.SelectedItem = product.Workshop;
                cbType.SelectedItem = product.ProductType;
                btnDelete.Visibility = Visibility.Visible;
            }

            materials = bd_connection.connection.Material.ToList();
            cbMaterial.ItemsSource = materials;
            cbMaterial.DisplayMemberPath = "Name";

            DataContext = this;
        }

        private void btnBackClick(object sender, RoutedEventArgs e)
        {
            bd_connection.connection = new LopushokEntities();
            NavigationService.Navigate(new ProductListPage());
        }

        private void btnSaveClick(object sender, RoutedEventArgs e)
        {
            int newArticle = Convert.ToInt32(tbArticl.Text.Trim());
            var uniqAricle = bd_connection.connection.Product.FirstOrDefault(x => x.ID == newArticle);
            if(uniqAricle == selectProduct || uniqAricle == null)
            {
                try
                {
                    bd_connection.connection.Product.Add(selectProduct);
                }
                catch
                {
                    MessageBox.Show("Введены некорректные значения");
                    return;
                }
                bd_connection.connection.SaveChanges();
                NavigationService.Navigate(new ProductListPage());
            }
            else if(uniqAricle != selectProduct)
            {
                MessageBox.Show("Артикул не уникальный, придумайте другой!");
            }
        }

        private void btnDeleteClick(object sender, RoutedEventArgs e)
        {
            var messageDelete = MessageBox.Show("Вы хотите удалить продукт?", "Внимание", MessageBoxButton.YesNoCancel);
            if (messageDelete == MessageBoxResult.Yes)
            {
                bd_connection.connection.Product.Remove(selectProduct);
                NavigationService.Navigate(new ProductListPage());
            }
        }

        private void cbMaterialSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbMaterial.SelectedItem != null)
            {
                var selectMaterial = cbMaterial.SelectedItem as Material;
                var uniqMaterial = bd_connection.connection.ProductMaterial.FirstOrDefault(x => x.Material.ID == selectMaterial.ID && x.Product.ID == selectProduct.ID);
                if(uniqMaterial == null)
                {
                    ProductMaterial productMaterial = new ProductMaterial()
                    {
                        Product = selectProduct, 
                        Material = selectMaterial
                    };
                    selectProduct.ProductMaterial.Add(productMaterial);
                }

                lvListMaterial.ItemsSource = selectProduct.ProductMaterial;
                lvListMaterial.Items.Refresh();
            }
        }

        private void lvListMaterialSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvListMaterial.SelectedItem != null)
            {
                var selectMaterial = lvListMaterial.SelectedItem as ProductMaterial;
                var messageDelete = MessageBox.Show("Вы хотите удалить материал?", "Внимание", MessageBoxButton.YesNoCancel);
                if (messageDelete == MessageBoxResult.Yes)
                {
                    selectProduct.ProductMaterial.Remove(selectMaterial);
                }
            }

            lvListMaterial.ItemsSource = selectProduct.ProductMaterial;
            lvListMaterial.Items.Refresh();
        }

        private void btnAddPhotoClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };

            if (fileDialog.ShowDialog().Value)
            {
                var image = File.ReadAllBytes(fileDialog.FileName);
                selectProduct.Photo = image;

                imgPhoto.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void btnDelPhotoClick(object sender, RoutedEventArgs e)
        {
            var messageDelete = MessageBox.Show("Вы хотите удалить фото?", "Внимание", MessageBoxButton.YesNoCancel);
            if (messageDelete == MessageBoxResult.Yes)
            {
                selectProduct.Photo = null;
                imgPhoto.Source = null;
            }
        }
    }
}
