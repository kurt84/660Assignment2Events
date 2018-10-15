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

namespace EventPlanner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewButtonClick(object sender, RoutedEventArgs e)
        {
            var win = new CreateWindow();
            win.Owner = this;
            win.Show();
        }
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to delete My New Event?", "Delete Event", MessageBoxButton.OKCancel);
        }
    }
}
