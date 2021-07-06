using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsModel
{
    /// <summary>
    /// Represents a simple point in 2D space
    /// </summary>
    public class Point
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// Returns an instance with Position set to [0,0]
        /// </summary>
        public Point()
        {
            Position = new Vector2D();
        }

        /// <summary>
        /// Returns an instance with specified position
        /// </summary>
        /// <param name="position">The position of the Point</param>
        public Point(Vector2D position)
        {
            Position = position;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The position of the Point in a 2D space
        /// </summary>
        public Vector2D Position { get; set; }

        /// <summary>
        /// The velocity of the Point
        /// </summary>
        public VelocityQuantity Velocity { get; set; } = new VelocityQuantity();

        /// <summary>
        /// Can the point move?
        /// </summary>
        public bool Stationary { get; set; } = true;

        #endregion

        #region Public methods

        /// <summary>
        /// Make the point move forward with its velocity
        /// </summary>
        /// <param name="moveFor">How long to move for</param>
        public void Move(double moveFor)
        {
            // Check if the point can move
            if (Stationary)
                throw new ArgumentException("Stationary points can't move!");

            // Make sure the units are right
            Velocity.Unit = VelocityUnits.MetersPerSecond;

            // Calculate, how far wil we move
            Vector2D changeInPosition = Velocity.Velocity * moveFor;

            // Add the distance travelled to out starting position
            Position += changeInPosition;
        }

        #endregion

    }
}
