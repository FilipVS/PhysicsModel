using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ModelDisplay
{
    /// <summary>
    /// This converter takes in a double value and returns it * (-1)
    /// </summary>
    public class MakeDoubleNegativeValueConverter : IValueConverter
    {
        /// <summary>
        /// This method takes in a double value and returns it * (-1)
        /// </summary>
        /// <param name="value">The value to make opposite</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>value * (-1)</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
