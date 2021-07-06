using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ModelDisplay
{
    /// <summary>
    /// This value converter accepts an enum value and returns its description instead of ToString()
    /// </summary>
    public class DescriptionOfEnumValueConverter : IValueConverter
    {
        /// <summary>
        /// This method accepts an enum value and returns its desctiption (if it has one) instead of the ToString()
        /// </summary>
        /// <param name="value">The enum value</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Description of the enum value if available, otherwise ToString()</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Got this method from: https://stackoverflow.com/questions/1415140/can-my-enums-have-friendly-names

            Enum enumValue = value as Enum;

            Type enumType = enumValue.GetType();

            string name = Enum.GetName(enumType, enumValue);

            if(name != null)
            {
                FieldInfo field = enumType.GetField(name);
                if(field != null)
                {
                    DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                    if(attribute != null)
                    {
                        return attribute.Description;
                    }
                }
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
