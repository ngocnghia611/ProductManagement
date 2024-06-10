using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFApp.Models;

namespace ProductManagementDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MyStoreContext myStore;
        public MainWindow()
        {
            InitializeComponent();
            myStore = new MyStoreContext();
        }
        public void LoadCategoryList()
        {
            try
            {
                var carList = myStore.GetCategories();
                cboCategory.ItemsSource = carList;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on Load list of categories");
            }
        }

        public void LoadProductList()
        {
            try
            {
                var productList = myStore.GetProducts();
                dgData.ItemsSource = productList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of product");
            }
            finally
            {
                resetInput();
            }
        }

        public void resetInput()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtUnitsInStock.Text = "";
            cboCategory.SelectedValue = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
            LoadProductList();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dgData.SelectedItem is Product product)
            {
                txtProductID.Text = product.ProductId.ToString();
                txtProductName.Text = product.ProductName;
                txtPrice.Text = product.UnitPrice?.ToString();
                txtUnitsInStock.Text = product.UnitsInStock?.ToString();
                cboCategory.SelectedValue = product.CategoryId;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product();
                product.ProductName = txtProductName.Text;
                product.UnitPrice = Decimal.Parse(txtPrice.Text);
                product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                product.CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString());
                myStore.SaveProduct(product);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                LoadProductList();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(txtProductID.Text, out int productId))
                {
                    var existingProduct = myStore.Products.Find(productId);
                    if (existingProduct != null)
                    {
                        myStore.Entry(existingProduct).State = EntityState.Detached;
                    }

                    var product = new Product
                    {
                        ProductId = productId,
                        ProductName = txtProductName.Text,
                        UnitPrice = Decimal.Parse(txtPrice.Text),
                        UnitsInStock = short.Parse(txtUnitsInStock.Text),
                        CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString())
                    };
                    myStore.UpdateProduct(product);
                }
                else
                {
                    MessageBox.Show("You must select a Product!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductList();
            }
            //try
            //{
            //    if (txtProductID.Text.Length > 0)
            //    {
            //        Product product = new Product();
            //        product.ProductId = Int32.Parse(txtProductID.Text);
            //        product.ProductName = txtProductName.Text;
            //        product.UnitPrice = Decimal.Parse(txtPrice.Text);
            //        product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
            //        product.CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString());
            //        myStore.UpdateProduct(product);
            //    }
            //    else
            //    {
            //        MessageBox.Show("You must selected a Product !");

            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    LoadProductList();
            //}

        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
                //try
                //{
                //    if (int.TryParse(txtProductID.Text, out int productId))
                //    {
                //        var existingProduct = myStore.Products.Find(productId);
                //        if (existingProduct != null)
                //        {
                //            myStore.Entry(existingProduct).State = EntityState.Detached;
                //        }

                //        var product = myStore.GetProductById(productId);
                //        if (product != null)
                //        {
                //            myStore.DeleteProduct(product);
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("You must select a Product");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                //finally
                //{
                //    LoadProductList();
                //}

            try
            {
                if (txtProductID.Text.Length > 0)
                {
                    Product product = new Product();
                    product.ProductId = Int32.Parse(txtProductID.Text);
                    product.ProductName = txtProductName.Text;
                    product.UnitPrice = Decimal.Parse(txtPrice.Text);
                    product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                    product.CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString());
                    myStore.DeleteProduct(product);
                }
                else
                {
                    MessageBox.Show("You must select a Product");
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

                LoadProductList();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}