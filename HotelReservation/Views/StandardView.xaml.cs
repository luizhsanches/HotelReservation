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
    /// Lógica interna para StandardView.xaml
    /// </summary>
    public partial class StandardView : Window
    {
        public StandardView()
        {
            InitializeComponent();
        }

        public void btnSubmitStd(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
