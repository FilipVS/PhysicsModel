using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ModelDisplay.Controls;

namespace ModelDisplay
{
    /// <summary>
    /// This converter can take a particle displayer IEnumerable and a particle displayer and return them as a single collection
    /// </summary>
    public class UniteParticleEnumerableAndParticleMultiValueConverter : IMultiValueConverter
    {

        /// <summary>
        /// This method takes a particle displayer IEnumerable and a particle displayer and returns them as a single collection
        /// </summary>
        /// <param name="values">The particle displayer IEnumerable and a particle displayer</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Single collection that contains all the particle displayers</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<ChargeDisplayerRadioButton> displayers = values[0] as IEnumerable<ChargeDisplayerRadioButton>;
            ChargeDisplayerRadioButton displayer = values[1] as ChargeDisplayerRadioButton;

            ObservableCollection<ChargeDisplayerRadioButton> result = new ObservableCollection<ChargeDisplayerRadioButton>();

            if (displayer != null)
                result.Add(displayer);

            foreach (ChargeDisplayerRadioButton item in displayers)
            {
                if (item != null)
                    result.Add(item);
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
