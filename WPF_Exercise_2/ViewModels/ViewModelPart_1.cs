using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WPF_Exercise_2
{
    public class ViewModelPart_1 : ViewModelBase
    {
       /* Seems like the implementation of INotifyPropertyChanged here is unnecessary,
        * but when I delete all the code if INotifyPropertyChanged, the event, the method,
        * and the interface inheritance, the application going mad,
        * and when just one slider passing 90% all the 3 user control are getting a red border.
        * When this allegedly unnecessary code is left in place, the app behaves properly,
        * each user control is responding only to its slider.
        * And this is true even when I don't write "UpdateSourceTrigger=PropertyChanged" in XAML.         
        */


        public double VmSliderMax { get; set; }
        public double VmSliderValue { get; set; }

        protected override string GetError()
        {
            double locVmSliderMax = VmSliderMax;
            VmSliderMax = double.MaxValue;

            if (VmSliderValue > locVmSliderMax * 0.9) return "fuckingError";

            return String.Empty;
        }
    }
}
