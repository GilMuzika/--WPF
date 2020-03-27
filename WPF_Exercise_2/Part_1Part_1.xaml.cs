using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Part_1Part_1.xaml
    /// </summary>
    public partial class Part_1Part_1 : UserControl
    {
        public string LeftTag
        {
            get => (string)lblLeftTag.Content;
            set => lblLeftTag.Content = value;
        }
        public string RightTagSliderValue
        {
            get => (string)lblRightTagSliderValue.Content;
            set
            {
                lblRightTagSliderValue.Content = value;
            }
        }
        public Brush RightTagSliderValueColor
        {
            get => lblRightTagSliderValue.Foreground;
            set => lblRightTagSliderValue.Foreground = value;
        }


        public Brush RightTagSliderValueColorDependency
        {
            get => (Brush)this.GetValue(RightTagSliderValueColorDependencyProperty);
            set => this.SetValue(RightTagSliderValueColorDependencyProperty, value);
        }
        public static readonly DependencyProperty RightTagSliderValueColorDependencyProperty = 
        DependencyProperty.Register("RightTagSliderValueColorDependency", typeof(Brush), typeof(Part_1Part_1), new PropertyMetadata(Brushes.DarkBlue));

        public string RightTagSliderMax
        {
            get => (string)lblRightTagSliderMax.Content;
            set
            {
                lblRightTagSliderMax.Content = value;
            }
        }
        public double SliderMax
        {
            get => sldSlider.Maximum;
            set => sldSlider.Maximum = value;
        }

        public double SliderMaxDependency
        {
            get => (double)this.GetValue(SliderMaxDependencyProperty);
            set => this.SetValue(SliderMaxDependencyProperty, value);
        }
        public static readonly DependencyProperty SliderMaxDependencyProperty = DependencyProperty.Register("SliderMaxDependency", typeof(double), typeof(Part_1Part_1), new PropertyMetadata(0d));

        public double SliderValue
        {
            get => SafeInvoke(() => { return sldSlider.Value; });
            set => sldSlider.Value = value;                        
        }

        public double SliderValueDependency
        {
            get => (double)this.GetValue(SliderValueDependencyProperty);
            set => this.SetValue(SliderValueDependencyProperty, value);
        }
        public static readonly DependencyProperty SliderValueDependencyProperty = DependencyProperty.Register("SliderValueDependency", typeof(double), typeof(Part_1Part_1), new PropertyMetadata(0d));


        public Part_1Part_1()
        {
            InitializeComponent();
            Initialize();
            
        }

            

        
        private void Initialize()
        {           
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 10;
            timer.Elapsed += (object sender, ElapsedEventArgs e) => 
            {
                SafeInvoke(() => 
                {
                    RightTagSliderValue = Convert.ToInt32(SliderValue).ToString();
                    RightTagSliderMax = Convert.ToInt32(SliderMax).ToString();
                    SliderValueDependency = SliderValue;
                    RightTagSliderValueColor = RightTagSliderValueColorDependency;
                    SliderMaxDependency = SliderMax;
                });

            };
            timer.Start();
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
        private T SafeInvoke<T>(Func<T> work)
        {
            if (Dispatcher.CheckAccess())
            {
                return work.Invoke();                
            }
            return this.Dispatcher.Invoke(work);
        }


    }
}
