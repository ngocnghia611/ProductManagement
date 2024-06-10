using Microsoft.Identity.Client.NativeInterop;
using ProductManagementDemo;
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
using WPFApp.Models;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly MyStoreContext myStore;
        public LoginWindow()
        {
            InitializeComponent();
            myStore = new MyStoreContext();
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string memberID = txtUser.Text;
            string password = txtPass.Password;
            var accountMember = myStore.AccountMembers.
                SingleOrDefault(am => am.MemberId == memberID && am.MemberPassword == password && am.MemberRole == 1);
            if (accountMember != null)
            {
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("You are not permision !");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
