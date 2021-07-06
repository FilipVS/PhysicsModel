using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsModel
{
    /// <summary>
    /// Represents a point in 2D space with some mass
    /// </summary>
    public class PointMass : Point
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// Returns an instance with Position set to [0,0] and 0 kg mass
        /// </summary>
        public PointMass()
        {
            Mass = new MassQuantity();
        }

        /// <summary>
        /// Returns an instance with specified position and 0 kg mass
        /// </summary>
        /// <param name="position">The position of the Point</param>
        public PointMass(Vector2D position) : base(position)
        {
            Mass = new MassQuantity();
        }

        /// <summary>
        /// Returns an instance with specified position and specified mass
        /// </summary>
        /// <param name="position">The position of the Point</param>
        /// <param name="mass">The mass in kilograms of the Point</param>
        public PointMass(Vector2D position, double mass) : base(position)
        {
            Mass = new MassQuantity(mass, MassUnit.kg);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The Mass quantity of the Point
        /// </summary>
        public MassQuantity Mass { get; set; }

        /// <summary>
        /// The acceleration of a point
        /// </summary>
        public AccelerationQuantity Acceleration { get; set; } = new AccelerationQuantity();

        #endregion

        #region Public methods

        /// <summary>
        /// Move the point forward using its acceleration
        /// </summary>
        /// <param name="moveFor">How long to move the point for</param>
        public void MoveAccelerated(double moveFor)
        {
            // Make sure the units are right
            Acceleration.Unit = AccelerationUnits.MetersPerSecondPerSecond;
            Velocity.Unit = VelocityUnits.MetersPerSecond;

            // Make the point displace due to its original velocity
            Move(moveFor);

            // Add to that the displacement due to the acceleration  (s=1/2 * a * t^2)
            Position += (Acceleration.Acceleration) * (1d / 2d) * (moveFor * moveFor);

            // Update the velocity due to our acceleration  (change in v = a * t)
            Velocity.Velocity += (Acceleration.Acceleration) * moveFor;
        }

        #endregion
    }
}
