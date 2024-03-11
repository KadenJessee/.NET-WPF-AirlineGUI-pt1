using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Assignment6Flight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Class that holds window for adding the passenger
        /// </summary>
        wndAddPassenger wndAddPassenger;

        /// <summary>
        /// class for flightManager
        /// </summary>
        clsFlightManager flightManager;

        /// <summary>
        /// class for passengerManager
        /// </summary>
        clsPassengerManager fpassengerManager;


        /// <summary>
        /// initalizes the main window
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                //including this from the previous assignment
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                //set the object
                wndAddPassenger = new wndAddPassenger();
                //set managers
                flightManager = new clsFlightManager();
                fpassengerManager = new clsPassengerManager();
                cmbboxChooseFlight.ItemsSource = flightManager.GetFlights();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// opens the Add Passenger Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //hide this window
                this.Hide();
                //show the dialog
                wndAddPassenger.ShowDialog();
                //show this
                this.Show();
            }catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }








        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// enables properties and displays the chosen flight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbboxChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                clsFlight clsSelectedFlight = (clsFlight)cmbboxChooseFlight.SelectedItem;

                int id = clsSelectedFlight.sFlightID;

                //Flight ID = 1 display flight, else
                if(id == 1)
                {
                    canvasA380.Visibility = Visibility.Visible;
                    canvas767.Visibility = Visibility.Hidden;
                }
                else
                {
                    canvas767.Visibility = Visibility.Visible;
                    canvasA380.Visibility = Visibility.Hidden;
                }

                //enable the proper information
                cmbboxChoosePassenger.IsEnabled = true;
                btnAddPassenger.IsEnabled = true;
                cmbboxChoosePassenger.ItemsSource = fpassengerManager.GetPassengers(clsSelectedFlight.sFlightID.ToString());

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}
