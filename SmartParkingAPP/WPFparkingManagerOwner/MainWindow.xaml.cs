using SmartParkingApp;
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

namespace WPFparkingManagerOwner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ParkingManager parkingManager = new ParkingManager();

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public MainWindow()
        {
            InitializeComponent();

            decimal percentage = (Convert.ToDecimal(parkingManager.activeSessions.Count) / Convert.ToDecimal(parkingManager.parkingCapacity)) * 100;
            PercentageLabel.Content = percentage;
            CapacityLabel.Content = parkingManager.parkingCapacity;

            foreach (var s in parkingManager.activeSessions)
            {
                s.TotalPayment = parkingManager.GetRemainingCost(s.TicketNumber);
            }

            AsessionsDataGrid.ItemsSource =
            (from actS in parkingManager.activeSessions
             select new
             {
                 actS.EntryDt,
                 actS.CarPlateNumber,
                 actS.TicketNumber,
                 actS.TotalPayment,

             });

            PsessionsDataGrid.ItemsSource =
            (from pS in parkingManager.pastSessions
             select new
             {
                 pS.EntryDt,
                 pS.PaymentDt,
                 pS.ExitDt,
                 pS.TotalPayment,
                 pS.TicketNumber,
                 pS.CarPlateNumber
             });
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = StartDate.SelectedDate;
            DateTime? finishDate = FinishDate.SelectedDate;

            if (finishDate == null)
            {
                CalculateLabel.Content = 0;
            }
            else
            {
                finishDate = finishDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                var step1 = parkingManager.pastSessions.FindAll(s => startDate <= s.EntryDt && finishDate >= s.ExitDt);
                var step2 = step1.Select(i => i.TotalPayment);
                var step3 = step2.Sum();
                CalculateLabel.Content = step3;
            }
           
        }

    }
}

