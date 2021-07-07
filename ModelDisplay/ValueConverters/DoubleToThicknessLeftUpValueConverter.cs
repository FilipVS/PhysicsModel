using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ModelDisplay
{
    /// <summary>
    /// This class takes in a double value and returns a thickness of (-value, -value, 0, 0)
    /// </summary>
    public class DoubleToThicknessLeftUpValueConverter : IValueConverter
    {
        /// <summary>
        /// This method takes in a double value and returns a thickness of (-value, -value, 0, 0)
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>The thickness of (-value, -value, 0, 0)</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double dValue = (double)value;

            return new Thickness(-dValue, -dValue, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
