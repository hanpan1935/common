using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Lanpuda.Client.Theme.Converters
{
    /// <summary>
    /// 相同返回黑色，不相同返回红色
    /// </summary>
    public class StringIsEqualToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? str = value as string;

            string? para = parameter as string;

            if (str == null || para == null)
            {
                throw new NotImplementedException();
            }
            if (str == para)
            {
                Color colorSame = Color.FromRgb(0,0,0);
                Brush brushSame = new SolidColorBrush(colorSame);
                return brushSame;
            }

            Color color = Color.FromRgb(255, 0, 0);
            Brush brush = new SolidColorBrush(color);
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
