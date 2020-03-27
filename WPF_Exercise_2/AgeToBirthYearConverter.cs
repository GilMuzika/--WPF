using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_Exercise_2
{
    class AgeToBirthYearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int birthYear =  DateTime.Now.Year - System.Convert.ToInt32(value);
            //DateTime date = new DateTime(year, month, day, hour, minute, 0);
            if ((long)value == new Person().Age) return DateTime.MinValue;


            return new DateTime(birthYear, 1, 1, 1, 1, 1);
 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).Year;
        }
    }
}
