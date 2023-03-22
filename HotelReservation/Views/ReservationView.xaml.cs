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

namespace HotelReservation.Views
{
    /// <summary>
    /// Lógica interna para ReservationView.xaml
    /// </summary>
    public partial class ReservationView : Window
    {
        public ReservationView()
        {
            InitializeComponent();
        }

        public void BtnSubmit(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
