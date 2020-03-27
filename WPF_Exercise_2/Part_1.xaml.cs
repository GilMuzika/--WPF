using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for Part_1.xaml
    /// </summary>
    public partial class Part_1 : UserControl
    {
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
        public Part_1()
        {            
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            System.Timers.Timer locTimer = new System.Timers.Timer();
            locTimer.Interval = 10;
            locTimer.Elapsed += (object sender, ElapsedEventArgs e) => 
            {
                SafeInvoke(() =>
                {                    
                    tblAverageValueSliders.Text = ((ukPart_1Part_1_1.SliderValue + ukPart_1Part_1_2.SliderValue + ukPart_1Part_1_3.SliderValue) / 3).ToString();                    
                });
            };
            locTimer.Start();
        }


        private void SafeInvoke(Action work)
        {
            if (Dispatcher.CheckAccess())
            {
                work.Invoke();
                return;
            }
            this.Dispatcher.BeginInvoke(work);
        }


    }
}
