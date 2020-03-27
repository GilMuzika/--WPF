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

namespace WPF_Exercise_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<UserControl> _alluserControls = new List<UserControl>();

        public MainWindow()
        {
            InitializeComponent();
            Initialize();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Statics.FindVisualChildren<UserControl>(this).ToList().ForEach(c => { c.Visibility = Visibility.Collapsed; _alluserControls.Add(c);  });
            this.cmbCombo.ItemsSource = _alluserControls.Select(x => x.ToString()).ToList();
        }

        private void cmbCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var s in _alluserControls)
            {
                if (String.Equals(s.ToString(), (string)(sender as ComboBox).SelectedItem)) { s.Visibility = Visibility.Visible;  }
                else s.Visibility = Visibility.Collapsed;                
            }
        }
    }
}
