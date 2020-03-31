using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SmartParkingApp;
using SmartParkingApp.Models;
using WPFparkingApp;

namespace WPFparkingAppClient
{
    /// <summary>
    /// Логика взаимодействия для ViewWindow.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
        private User user;
        private ParkingManager parkingManager;

    
        public ViewWindow(User user, ParkingManager parkingManager) 
        {
            this.parkingManager = parkingManager;
            this.user = user;
            InitializeComponent();
            PhoneNumbLabel.Content = user.Phone;
            
            var pastSessions = 
            (from ps in parkingManager.pastSessions
             where ps.UserId == user.Id
             select new
             {
                 EntryDate = ps.EntryDt,
                 ps.ExitDt,
                 ps.TicketNumber,
                 ps.TotalPayment

             });

            if (pastSessions.Count() == 0)
            {
                NotFoundSesLabel.Content = "You do not have past parking sessions";
                NotFoundSesLabel.Background = new SolidColorBrush(Colors.Red);                
            }

            else
            {
                PSgrid.ItemsSource = pastSessions;
            }


            var activeSession = parkingManager.activeSessions.Find(s => s.UserId == user.Id);

            if (activeSession != null)
            {
                EntryDTLabel.Content = activeSession.EntryDt;

                TicketNumLabel.Content = activeSession.TicketNumber;

                CurrentPayLabel.Content = parkingManager.GetRemainingCost(activeSession.TicketNumber);

                CarPlateNumLabel.Content = activeSession.CarPlateNumber;
            }
            else
            {
                EntryDTLabel.Content = null;

                TicketNumLabel.Content = null;

                CurrentPayLabel.Content = 0;

                CarPlateNumLabel.Content = user.CarPlateNumber;

                NotFoundLabel.Content = "Not Found";

                NotFoundLabel.Background = new SolidColorBrush(Colors.Red);

            }
           

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();

        }
    }
}
