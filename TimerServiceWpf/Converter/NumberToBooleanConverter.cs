using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerServiceWpf
{
    using System.Globalization;
    using System.Windows.Data;

    public class NumberToBooleanConverter : IValueConverter
    {
        private double cachedvalue=60;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double tempvalue=System.Convert.ToDouble(value);
            if (tempvalue > 0)
                this.cachedvalue = tempvalue;
            return (tempvalue > 0);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return this.cachedvalue;
            else
            {
                return 0;
            }
        }
    }
}
