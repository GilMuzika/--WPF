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
    /// Interaction logic for Part_3.xaml
    /// </summary>
    public partial class Part_3 : UserControl
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

        private System.Timers.Timer _timer;

        public string UserName
        {
            get => txtName.Text;
            set
            {                
                txtName.Text = value;
            }
        }
        public string Password
        {
            get => txtPassword.Text;
            set
            {                
                txtPassword.Text = value;
            }
        }
        public string UserNameDependency
        {
            get => (string)this.GetValue(UserNameDependencyProperty);
            set => this.SetValue(UserNameDependencyProperty, value);
        }
        public static readonly DependencyProperty UserNameDependencyProperty =
        DependencyProperty.Register("UserNameDependency", typeof(string), typeof(Part_3), new PropertyMetadata(String.Empty));

        public string PasswordDependency
        {
            get => (string)this.GetValue(PasswordDependencyProperty);
            set => this.SetValue(PasswordDependencyProperty, value);
        }
        public static readonly DependencyProperty PasswordDependencyProperty =
        DependencyProperty.Register("PasswordDependency", typeof(string), typeof(Part_3), new PropertyMetadata(String.Empty));

        public Brush BackgroundDependency
        {
            get => (Brush)this.GetValue(BackgroundDependencyProperty);
            set => this.SetValue(BackgroundDependencyProperty, value);
        }
        public static readonly DependencyProperty BackgroundDependencyProperty =
        DependencyProperty.Register("BackgroundDependency", typeof(Brush), typeof(Part_3), new PropertyMetadata(Brushes.LightGreen));

        /*public Brush IsUserDetailsRightDependency
        {
            get => (Brush)this.GetValue(IsUserDetailsRightDependencyProperty);
            set => this.SetValue(IsUserDetailsRightDependencyProperty, value);
        }
        public static readonly DependencyProperty IsUserDetailsRightDependencyProperty =
        DependencyProperty.Register("IsUserDetailsRightDependency", typeof(bool), typeof(Part_3), new PropertyMetadata(true));*/
        /*public UserDetails UserDetailsDependency
        {
            get => (UserDetails)this.GetValue(UserDetailsDependencyProperty);
            set => this.SetValue(UserDetailsDependencyProperty, value);
        }
        public static readonly DependencyProperty UserDetailsDependencyProperty =
        DependencyProperty.Register("UserDetailsDependency", typeof(UserDetails), typeof(Part_3));*/

        public Part_3()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = 10;
            _timer.Elapsed += (object sender, ElapsedEventArgs e) => 
            {
                SafeInvoke(() => 
                {
                    UserNameDependency = UserName;
                    PasswordDependency = Password;
                    Background = BackgroundDependency;
                });
            };
            

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
            Thread.Sleep(100);
            _timer.Stop();
        }
    }
}
