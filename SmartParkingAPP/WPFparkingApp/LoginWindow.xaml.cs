using SmartParkingApp;
using SmartParkingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFparkingAppClient;

namespace WPFparkingApp
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private ParkingManager parkingManager = new ParkingManager();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow regWindow = new RegistrationWindow();
            regWindow.Show();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            string phone = phoneUserTextBox.Text;
            string password = passUserTextBox.Password;
            User user = parkingManager.users.Find(u => u.Phone == phone);
            if (user != null && user.Password == password)
            {
                ViewWindow viewWindow = new ViewWindow(user, parkingManager);
                viewWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("WRONG LOGIN OR PASSWORD", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
