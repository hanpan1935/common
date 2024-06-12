using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lanpuda.Client.Theme.Converters
{
    public class DisplayAttributeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value.GetType();

            //if (type.IsEnum == false)
            //{
            //    return "绑定错误,非Enum类型";
            //}

            if (value == null) 
            { 
                return ""; 
            }
            string? memberName = value.ToString();
            if (memberName == null)
            {
                memberName = "";
            }

            MemberInfo[] memInfo = type.GetMember(memberName); if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    if (attrs != null && attrs[0] != null)
                    {
                        string? name = ((DisplayAttribute)attrs[0]).GetName();
                        if (name != null) return name;
                    }
                }
            }

            return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
