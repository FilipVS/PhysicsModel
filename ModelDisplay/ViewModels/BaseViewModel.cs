using System.ComponentModel;

namespace ModelDisplay
{
    /// <summary>
    /// The base view model class
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that notifies about a change of property
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The method that fired the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">The name of the property changed</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
