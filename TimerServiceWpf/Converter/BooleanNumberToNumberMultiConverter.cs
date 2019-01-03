using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerServiceWpf
{
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Navigation;

    class BooleanNumberToNumberMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2)
            {
                if (!(bool)values[0])
                    return 0;
                else
                {
                    return values[1];
                }
            }
            else
                return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {


            if (System.Convert.ToDouble(value) == 0)
                return new object[] { false, value };
            else
            {
                return new object[] { true, value };
            }
        }
    }
}
