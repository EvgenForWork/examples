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

namespace WPFparkingApp
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private ParkingManager parkingManager;
    
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void CancelRegButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow logWindow = new LoginWindow();
            logWindow.Show();
            Close();
        }

        private void GoRegButton_Click(object sender, RoutedEventArgs e)
        {
            parkingManager = new ParkingManager();

            string name = newUserNameTextBox.Text;
            string phone = newUserPhoneTextBox.Text;
            string carPlateNum = newCarPlateNumberTextBox.Text;
            string password = newUserPassPassBox.Password;
            string passwordConf = newUserConfPassPassBox.Password;

            User user = parkingManager.users.Find(u => u.Phone == phone);

            bool notNullFields = name == "" || phone == "" || carPlateNum == "" || password == "";

            var users = parkingManager.users;
            int ids = users.Count;
            bool flag = true;

            if (notNullFields)
            {
                newUserPhoneTextBox.Text = null;
                newUserPassPassBox.Password = null;
                newUserNameTextBox.Text = null;
                newUserConfPassPassBox.Password = null;
                newCarPlateNumberTextBox.Text = null;
                MessageBox.Show("Fill all fields!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                foreach (User item in users)
                {
                    if (item.Phone == phone)
                    {
                        MessageBox.Show("This phone number is already exists!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        flag = false;

                    }
                }

                if (flag)
                {
                    if (password == passwordConf)
                    {
                        User newuser = new User()
                        {
                            Id = ids + 1,
                            Phone = phone,
                            Name = name,
                            Password = password,
                            CarPlateNumber = carPlateNum,
                        };
                        users.Add(newuser);
                        parkingManager.SaveUser(users);
                        MessageBox.Show("You are succesfully sign up!", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.Show();
                        Close();

                    }
                    else
                    {
                        MessageBox.Show("Confirm your password!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        newUserPassPassBox.Password = null;
                        newUserConfPassPassBox.Password = null;
                    }
                }
                else
                {
                    newUserPhoneTextBox.Text = null;
                    newUserPassPassBox.Password = null;
                    newUserNameTextBox.Text = null;
                    newUserConfPassPassBox.Password = null;
                    newCarPlateNumberTextBox.Text = null;
                }
            }
        }
        
    }
}
