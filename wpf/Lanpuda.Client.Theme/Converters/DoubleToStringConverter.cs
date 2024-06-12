using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lanpuda.Client.Theme.Converters
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string res = (string)value;
            if (string.IsNullOrEmpty(res))
            {
                return null;
            }
            else
            {
                bool canParse =  double.TryParse(value.ToString(), out double result);

                if (canParse)
                {
                    return result;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
