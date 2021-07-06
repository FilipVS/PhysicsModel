using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ModelDisplay.Controls;

namespace ModelDisplay
{
    /// <summary>
    /// This converter takes bool setting and a ChargeDisplayer and returns Visibility setting based on them
    /// </summary>
    public class ChargeSelectionEnablerMultiValueConverter : IMultiValueConverter
    {
        /// <summary>
        /// Takes bool setting and a ChargeDisplayer and returns Visibility setting based on them
        /// </summary>
        /// <param name="values">The bool and ChargeDisplayer values</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Get the AllowChargeSelection setting
            bool allowChargeSelection = (bool)values[0];
            // Get the SelectedElement setting
            ChargeDisplayerRadioButton selectedElement = values[1] as ChargeDisplayerRadioButton;

            // If selection is not allowed, return hidden
            if (!allowChargeSelection)
                return Visibility.Hidden;

            // If no element selected, return hidden
            if (selectedElement == null)
                return Visibility.Hidden;

            // Else return visible
            return Visibility.Visible;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
