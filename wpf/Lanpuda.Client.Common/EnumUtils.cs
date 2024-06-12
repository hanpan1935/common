using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.Client.Common
{
    public static class EnumUtils
    {
        public static string GetEnumDisplayName(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString()); if (memInfo != null && memInfo.Length > 0)
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
            return en.ToString();

        }

        public static string GetEnumDisplayDescription(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString()); 
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false); 
                if (attrs != null && attrs.Length > 0)
                {
                    string? res = ((DisplayAttribute)attrs[0]).Description;
                    if (res != null)
                    {
                        return res;
                    }
                }
            }
            return en.ToString();

        }


        public static Dictionary<string, int> EnumToDictionaryInt<T>(string keyDefault = "", int valueDefault = 0)
        {
            Dictionary<string, int> dicEnum = new Dictionary<string, int>();
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                return dicEnum;
            }
            if (!string.IsNullOrEmpty(keyDefault)) //判断是否添加默认选项
            {
                dicEnum.Add(keyDefault, valueDefault);
            }

            string[] fieldstrs = Enum.GetNames(enumType); //获取枚举字段数组
            foreach (var item in fieldstrs)
            {
                string? description = string.Empty;
                FieldInfo? field = enumType.GetField(item);
                if (field != null)
                {
                    DisplayAttribute? descAttr = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;
                    if (descAttr != null)
                    {
                        description = descAttr.GetName();
                    }
                }
                if (description != null)
                {
                    dicEnum.Add(description, (int)Enum.Parse(enumType, item));
                }

                // dicEnum.Add(description, (int)Enum.Parse(enumType, item));  //不用枚举的value值作为字典key值的原因从枚举例子能看出来，其实这边应该判断他的值不存在，默认取字段名称
            }
            return dicEnum;
        }

        public static Dictionary<string, T> EnumToDictionary<T>() where T :System.Enum
        {
            Dictionary<string, T> dicEnum = new Dictionary<string, T>();
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                return dicEnum;
            }

            string[] fieldstrs = Enum.GetNames(enumType); //获取枚举字段数组

            foreach (var item in fieldstrs)
            {
                string? description = string.Empty;
                FieldInfo? field = enumType.GetField(item);
                if (field != null)
                {
                    DisplayAttribute? displayAttr = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;
                    if (displayAttr != null)
                    {
                        description = displayAttr.GetName();
                        if (description != null)
                        {
                            dicEnum.Add(description, (T)Enum.Parse(enumType, item));
                        }
                    }
                }
            }
            return dicEnum;
        }
    }
}
