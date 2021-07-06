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
    /// Value converter that takes in a decimal value and rounds it to an integer
    /// </summary>
    public class DecimalRounderValueConverter : IValueConverter
    {
        /// <summary>
        /// Takes in a decimal value and returns it rounded to an integer
        /// </summary>
        /// <param name="value">The decimal value to round</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>A rounded value of the decimal</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value.ToString();

            double dc = Double.Parse(str);

            return Math.Round(dc);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
