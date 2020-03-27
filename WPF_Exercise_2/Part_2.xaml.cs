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
    /// Interaction logic for Part_2.xaml
    /// </summary>
    public partial class Part_2 : UserControl
    {
        private List<UserControl> _alluserControls = new List<UserControl>();
        public string Title
        {
            get => tblTitle.Text;
            set
            {
                tblTitle.Text = value;
            }
        }
        public override string ToString()
        {
            return this.Title;
        }
        public Part_2()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            btnReadOnly.Tag = grdPart_1.GetType().Name;
            btnEditable.Tag = grdPart_2.GetType().Name;
            btnCustomized.Tag = grdPart_3.GetType().Name;
            _alluserControls.Add(grdPart_1);
            _alluserControls.Add(grdPart_2);
            _alluserControls.Add(grdPart_3);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(var s in _alluserControls)
            {
                if (s.GetType().Name == (sender as Button).Tag)
                    s.Visibility = Visibility.Visible;
                else
                    s.Visibility = Visibility.Collapsed;

                
            }
        }
    }
}
