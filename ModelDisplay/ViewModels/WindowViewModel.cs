using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using PhysicsModel;
using ModelDisplay.Controls;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace ModelDisplay
{
    public class WindowViewModel : BaseViewModel
    {

        #region Constants

        // Default masses of new particles
        private const double DEFAULT_MASS_STATIONARY = 0;
        private const double DEFAULT_MASS_MOVABLE = 0.00001;

        // Default charges of new particles
        private const double DEFAULT_CHARGE_STATIONARY_ABSOLUTE_VALUE = 0.01;
        private const double DEFAULT_CHARGE_MOVABLE_ABSOLUTE_VALUE = 0.00001;

        // How many seconds long is each single move (from one frame to another)
        private const double MOVE_LENGTH = 1d/60d;

        // How long does the thread wait between updating frames
        private const int MILLISECONDS_BETWEEN_FRAMES = 15;

        // Movement precision (1-infinity), how precisely calculated is the movement
        private const int MOVEMENT_PRECISION = 10;

        //

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public WindowViewModel()
        {
            ForceFieldElements.CollectionChanged += ForceFieldElements_CollectionChanged;
        }

        #endregion

        #region Private members

        /// <summary>
        /// The particle type chosen to be placed
        /// </summary>
        private ParticleTypes particleTypeChosen = ParticleTypes.StationaryPositive;

        /// <summary>
        /// Allow selecting and editing of placed charges
        /// </summary>
        private bool allowChargeSelection = true;

        /// <summary>
        /// The display element for the charged particle
        /// </summary>
        private ChargeDisplayerRadioButton chargedParticleElement = null;

        /// <summary>
        /// The particle that is currently selected
        /// </summary>
        private ChargeDisplayerRadioButton selectedParticleElement = null;

        #endregion

        #region Public properties

        /// <summary>
        /// The particle type chosen to be placed
        /// </summary>
        public ParticleTypes ParticleTypeChosen
        {
            get { return particleTypeChosen; }
            set
            {
                particleTypeChosen = value;
                OnPropertyChanged(nameof(ParticleTypeChosen));
            }
        }

        /// <summary>
        /// Allow selecting and editing of placed charges
        /// </summary>
        public bool AllowChargeSelection
        {
            get { return allowChargeSelection; }
            set
            {
                allowChargeSelection = value;

                // Allow or disallow hit testing based on the new setting
                ChangeHitTestingOfDisplayer(value);

                OnPropertyChanged(nameof(AllowChargeSelection));
            }
        }

        /// <summary>
        /// The display elements for the individual force field elements
        /// </summary>
        public ObservableCollection<ChargeDisplayerRadioButton> ForceFieldElements { get; set; } = new ObservableCollection<ChargeDisplayerRadioButton>();

        /// <summary>
        /// Returns an electric force field based on the placed stationary charge displayers
        /// </summary>
        public ElectricForceField ForceField
        {
            get
            {
                ElectricForceField forceField = new ElectricForceField();
                foreach (ChargeDisplayerRadioButton displayer in ForceFieldElements)
                    forceField.AddPointActing(displayer.ChargeToDisplay);
                return forceField;
            }
        }

        /// <summary>
        /// The display element for the charged particle
        /// </summary>
        public ChargeDisplayerRadioButton ChargedParticleElement
        {
            get { return chargedParticleElement; }
            set
            {
                chargedParticleElement = value;
                OnPropertyChanged(nameof(ChargedParticleElement));
            }
        }

        /// <summary>
        /// The particle that is currently selected
        /// </summary>
        public ChargeDisplayerRadioButton SelectedParticleElement
        {
            get { return selectedParticleElement; }
            set
            {
                if(SelectedParticleElement != null)
                    SelectedParticleElement.IsChecked = false;

                selectedParticleElement = value;

                if(SelectedParticleElement != null)
                    SelectedParticleElement.IsChecked = true;

                OnPropertyChanged(nameof(SelectedParticleElement));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Used for registering displayer for a new particle (which is created based on the settings of the view model) with the view model
        /// </summary>
        /// <param name="position">Where is the new particle located</param>
        /// <param name="displayCanvas">The canvas that will display the charge</param>
        /// <returns>Returns the new displayer for the particle</returns>
        public void RegisterNewParticle(Vector2D position, Canvas displayCanvas)
        {
            // Create the display element for the new particle
            ChargeDisplayerRadioButton newParticleDisplayer = new ChargeDisplayerRadioButton();

            // Based on the type chosen, set the DependencyProperty on the new particle displayer
            switch (ParticleTypeChosen)
            {
                case ParticleTypes.StationaryPositive:
                    newParticleDisplayer.ChargeToDisplay = new PointMassCharge(position, DEFAULT_MASS_STATIONARY, DEFAULT_CHARGE_STATIONARY_ABSOLUTE_VALUE) { Stationary = true };
                    break;
                case ParticleTypes.StationaryNegative:
                    newParticleDisplayer.ChargeToDisplay = new PointMassCharge(position, DEFAULT_MASS_STATIONARY, -DEFAULT_CHARGE_STATIONARY_ABSOLUTE_VALUE) { Stationary = true };
                    break;
                case ParticleTypes.MovablePositive:
                    newParticleDisplayer.ChargeToDisplay = new PointMassCharge(position, DEFAULT_MASS_MOVABLE, DEFAULT_CHARGE_MOVABLE_ABSOLUTE_VALUE) { Stationary = false };
                    break;
                case ParticleTypes.MovableNegative:
                    newParticleDisplayer.ChargeToDisplay = new PointMassCharge(position, DEFAULT_MASS_MOVABLE, -DEFAULT_CHARGE_MOVABLE_ABSOLUTE_VALUE) { Stationary = false };
                    break;
                default:
                    throw new ArgumentException("Unknown particle type!");
            }

            // Set the starting position of the displaye based on the position of the particle
            Canvas.SetLeft(newParticleDisplayer, newParticleDisplayer.ChargeToDisplay.Position.X);
            Canvas.SetTop(newParticleDisplayer, newParticleDisplayer.ChargeToDisplay.Position.Y);

            // If the new particle is movable, remove the previous movable particle from the canvas
            if (!newParticleDisplayer.ChargeToDisplay.Stationary)
                displayCanvas.Children.Remove(ChargedParticleElement);

            // Register the new movable particle displayer
            if (!newParticleDisplayer.ChargeToDisplay.Stationary)
                ChargedParticleElement = newParticleDisplayer;
            // Or register the new stationary particle
            else
                ForceFieldElements.Add(newParticleDisplayer);

            // If selecting is allowed, select the newely placed particle
            if (AllowChargeSelection)
            {
                // Select this one
                SelectedParticleElement = newParticleDisplayer;
            }
            // Otherwise disallow hit testing on it
            else
            {
                newParticleDisplayer.IsHitTestVisible = false;
            }

            // Add the enw particle to the canvas
            displayCanvas.Children.Add(newParticleDisplayer);
            
        }

        /// <summary>
        /// Start the movement of the movable charge
        /// </summary>
        public async void Move()
        {
            // Stopwatch to meassure time between frames
            Stopwatch watch = new Stopwatch();
            // List to store five last ellapsed times between frames
            List<int> ellapsedMillisecondsBetweenFrames = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            // If no movable charge specified, return
            if (ChargedParticleElement == null)
                return;

            // The movement itself
            while (true)
            {
                // If the particle doesn't exist anymore, return
                if (ChargedParticleElement == null)
                    return;

                // Move the particle
                await ChargedParticleElement.ChargeToDisplay.MoveInElectricFieldAsync(ForceField, MOVE_LENGTH, MOVEMENT_PRECISION);

                // TODO: Improve
                // Force the property to call OnPropertyChanged
                ChargeDisplayerRadioButton displayer = SelectedParticleElement;
                SelectedParticleElement = null;
                SelectedParticleElement = displayer;

                // Get the averadge ellapsed time between last five frames
                int averadgeEllapsed = (int)Math.Round(Enumerable.Average(ellapsedMillisecondsBetweenFrames));

                // Stop meassuring ellapsed time
                watch.Stop();
                // Save the ellapsed time to the list and remove the first one in the order (to maintain five last ellapsed times)
                ellapsedMillisecondsBetweenFrames.Add((int)watch.ElapsedMilliseconds);
                ellapsedMillisecondsBetweenFrames.RemoveAt(0);

                // Wait for the actual move length in milliseconds (minus the averadge time between frames)
                await Task.Delay((MILLISECONDS_BETWEEN_FRAMES - averadgeEllapsed) > 0 ? (MILLISECONDS_BETWEEN_FRAMES - averadgeEllapsed) : 0);

                // Reset the stopwatch
                watch = new Stopwatch();
                watch.Start();

                // If the particle doesn't exist anymore, return
                if (ChargedParticleElement == null)
                    return;

                // Move the display object that represents the particle
                Canvas.SetLeft(ChargedParticleElement, ChargedParticleElement.ChargeToDisplay.Position.X);
                Canvas.SetTop(ChargedParticleElement, ChargedParticleElement.ChargeToDisplay.Position.Y);
            }
        }

        /// <summary>
        /// Deletes the currently selected displayer element and removes it from the argument canvas
        /// </summary>
        /// <param name="deleteFrom">The canvas to remove the element from</param>
        public void DeleteSelectedElement(Canvas deleteFrom)
        {
            // If no element is selected, return
            if (SelectedParticleElement == null)
                return;

            // Remove the element from the canvas
            deleteFrom.Children.Remove(SelectedParticleElement);

            // Delete the element from the ChargedParticle setting or from the ForceField
            if (ChargedParticleElement == SelectedParticleElement)
                ChargedParticleElement = null;
            else if (ForceFieldElements.Contains(SelectedParticleElement))
                ForceFieldElements.Remove(SelectedParticleElement);

            // Make the selected element null
            SelectedParticleElement = null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Allows/Disallows hit testing on the displayer elements
        /// </summary>
        /// <param name="allow">Allow or disallow hit testing?</param>
        private void ChangeHitTestingOfDisplayer(bool allow)
        {
            // Change hit testing setting on the force field displayer elements
            foreach (ChargeDisplayerRadioButton displayer in ForceFieldElements)
            {
                if(displayer != null)
                {
                    displayer.IsHitTestVisible = allow;
                }
            }

            // Change hit testing setting on the movable charge element
            if(ChargedParticleElement != null)
            {
                ChargedParticleElement.IsHitTestVisible = allow;
            }

            // Unselect the current element
            SelectedParticleElement = null;
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Gets called when the ForceFieldElements collection is changed and in turns calls the OnPropertyChanged(ForceFieldElements)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForceFieldElements_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(ForceFieldElements));
        }

        #endregion

    }
}
