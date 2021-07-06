using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PhysicsModel;

namespace ModelDisplay.Controls
{
    /// <summary>
    /// A radio button subclass that allows for displaying a charged particle
    /// </summary>
    public class ChargeDisplayerRadioButton : RadioButton
    {

        #region Constants

        // Default brushes to be used for positive/negative charge's fills (and to outline the opposite charges)
        private static readonly Brush POSITIVE_CHARGE_BRUSH = Brushes.Orange;
        private static readonly Brush NEGATIVE_CHARGE_BRSUH = Brushes.Blue;

        // Default sizes to be used for stationary/movable particles
        private const double STATIONARY_CHARGE_SIZE = 30;
        private const double MOVABLE_CHARGE_SIZE = 15;

        // The thickness of the outer border (for outlining...)
        private const int OUTER_BORDER_THICKNESS = 4;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChargeDisplayerRadioButton()
        {

            // Set the DataContext to the particle
            this.DataContext = this;

        }

        #endregion

        #region Dependency propeties

        /// <summary>
        /// Dependency property that represents the charged particle to be displayed
        /// </summary>
        public static readonly DependencyProperty ChargeToDisplayProperty = DependencyProperty.Register(nameof(ChargeToDisplay), typeof(PointMassCharge),
            typeof(ChargeDisplayerRadioButton), new PropertyMetadata(null, OnChargeToDisplayChanged));

        #endregion

        #region Public properties

        /// <summary>
        /// The charged particle to be displayed
        /// </summary>
        public PointMassCharge ChargeToDisplay
        {
            get { return (PointMassCharge)GetValue(ChargeToDisplayProperty); }
            set { SetValue(ChargeToDisplayProperty, value); }
        }

        /// <summary>
        /// The brush for the background of the main circle
        /// </summary>
        public Brush BackgroundColor { get; private set; } = Brushes.Transparent;

        /// <summary>
        /// The brush for the text inside the circle and outlining
        /// </summary>
        public Brush ForegroundColor { get; private set; } = Brushes.Transparent;

        /// <summary>
        /// The size of the main circle
        /// </summary>
        public double MainCircleRadius
        {
            get { return this.Width - OUTER_BORDER_THICKNESS; }
        }

        /// <summary>
        /// The size of the larger outer circle that is used for outlining
        /// </summary>
        public double OutlineCircleRadius
        {
            get { return this.Width; }
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Reworks the colors and sizes every time the associated particle changes
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnChargeToDisplayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // If the property is set on different object, do not do anything
            if (d is not ChargeDisplayerRadioButton displayer)
                return;

            // If the particle is reset, shrink the control and return
            if (e.NewValue == null)
            {
                displayer.Height = displayer.Width = 0;
                return;
            }

            // Set the background color depending on the new particle
            displayer.BackgroundColor = (displayer.ChargeToDisplay).Charge.Charge >= 0 ? POSITIVE_CHARGE_BRUSH : NEGATIVE_CHARGE_BRSUH;
            // Set the foreground color depending on the new particle
            displayer.ForegroundColor = (displayer.ChargeToDisplay).Charge.Charge >= 0 ? NEGATIVE_CHARGE_BRSUH : POSITIVE_CHARGE_BRUSH;

            // Set the proper width depending on the new particle
            displayer.Width = (displayer.ChargeToDisplay).Stationary ? STATIONARY_CHARGE_SIZE : MOVABLE_CHARGE_SIZE;
            // Set the proper height depending on the new particle
            displayer.Height = (displayer.ChargeToDisplay).Stationary ? STATIONARY_CHARGE_SIZE : MOVABLE_CHARGE_SIZE;
        }

        /// <summary>
        /// Gets fired when the radio button gets clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            // Raise the click event on the control
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent);
            RaiseEvent(args);
        }

        #endregion

    }
}
