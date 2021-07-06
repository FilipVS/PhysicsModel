using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModelDisplay.Controls;

namespace ModelDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        #region Event handlers

        /// <summary>
        /// This event is fired when a button in the panel with charges gets clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelWithCharges_Click(object sender, RoutedEventArgs e)
        {
            // Check that the original source was really a radio button with particle type
            if (!(e.OriginalSource is RadioButtonWithParticleType button))
                return;

            // Get the proper type of the window's view model
            WindowViewModel viewModel = this.DataContext as WindowViewModel;

            // Set the ParticleTypeChosen property on the view model
            viewModel.ParticleTypeChosen = button.ParticleType;
        }

        /// <summary>
        /// This event is fired when the user clicks on the canvas (to place a particle)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvasSpace_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Get the mouse position relative to the canvas
            Point mousePosition = e.GetPosition(canvasSpace);

            // Place the new particle
            PlaceNewParticle(mousePosition);
        }

        /// <summary>
        /// Fires when a button inside the canvas gets clicked, changes the selected displayer property on the view model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvasSpace_Click(object sender, RoutedEventArgs e)
        {
            // Check that the original source was a charge displayer
            if (e.OriginalSource is not ChargeDisplayerRadioButton chargeDisplayer)
                return;

            // Get the correct type view model
            WindowViewModel viewModel = this.DataContext as WindowViewModel;

            // If the button is chekced, set it as the new selected element
            if (chargeDisplayer.IsChecked == true)
                viewModel.SelectedParticleElement = chargeDisplayer;
        }

        /// <summary>
        /// Moves all the children of the canvas, so they stay at the same relative position to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvasSpace_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Get the correct type view model
            WindowViewModel viewModel = this.DataContext as WindowViewModel;

            // Calculate the resize ratio
            double xRatio = e.NewSize.Width / e.PreviousSize.Width;
            double yRatio = e.NewSize.Height / e.PreviousSize.Height;

            // Move all the elements and their associated charges accordingly
            // Move the elements of the force field
            foreach (ChargeDisplayerRadioButton displayer in viewModel.ForceFieldElements)
                MoveDisplayer(displayer);

            // Move the movable particle
            MoveDisplayer(viewModel.ChargedParticleElement);



            // Helper method that moves an individual displayer element
            void MoveDisplayer(ChargeDisplayerRadioButton displayer)
            {
                Point newPosition;

                // Check for null
                if (displayer == null)
                    return;

                // Calculate the position
                newPosition = new Point(Canvas.GetLeft(displayer) * xRatio, Canvas.GetTop(displayer) * yRatio);

                // Set it on the element and associated charge
                Canvas.SetLeft(displayer, newPosition.X);
                Canvas.SetTop(displayer, newPosition.Y);
                displayer.ChargeToDisplay.Position = new PhysicsModel.Vector2D(newPosition.X, newPosition.Y);
            }
        }

        #endregion

        #region Commands

        #region Delete Command

        /// <summary>
        /// Cam the delete command be executed (only when an element is selected)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Get the correct type view model
            WindowViewModel viewModel = this.DataContext as WindowViewModel;

            // Delete command can be invoked when an element is selected
            e.CanExecute = viewModel.SelectedParticleElement != null;
        }

        /// <summary>
        /// Deletes the currently selected element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Get the correct type view model
            WindowViewModel viewModel = this.DataContext as WindowViewModel;

            // Delete the currently selected element
            viewModel.DeleteSelectedElement(canvasSpace);
        }

        #endregion

        #region Play Command

        /// <summary>
        /// Can the play command be executed (only when movable charge is present)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Get the correct type view model
            WindowViewModel viewModel = this.DataContext as WindowViewModel;

            // Play command can be executed only when a movable particle is present
            e.CanExecute = viewModel.ChargedParticleElement != null;
        }

        /// <summary>
        /// Starts the simulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Get the correct type view model
            WindowViewModel viewModel = this.DataContext as WindowViewModel;

            viewModel.Move();
        }

        #endregion

        #endregion

        #region Helper methods

        /// <summary>
        /// Places a new particle onto the canvas and registers it with the view model
        /// </summary>
        /// <param name="position">The position on Canvas that the charge is supposed to be placed</param>
        private void PlaceNewParticle(Point position)
        {
            // Get the proper type of the window's view model
            WindowViewModel viewModel = this.DataContext as WindowViewModel;

            // If movable particle is being placed, remove the current one
            if(viewModel.ParticleTypeChosen is ParticleTypes.MovablePositive or ParticleTypes.MovableNegative)
                canvasSpace.Children.Remove(viewModel.ChargedParticleElement);

            // Registrer a new particle displayer
            viewModel.RegisterNewParticle(new PhysicsModel.Vector2D(position.X, position.Y), canvasSpace);

        }




        #endregion

        
    }
}
