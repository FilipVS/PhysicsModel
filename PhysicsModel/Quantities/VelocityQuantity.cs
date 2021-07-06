using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsModel
{
    /// <summary>
    /// Velocity quantity of an object
    /// </summary>
    public class VelocityQuantity
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// New instance with 0 velocity and meters/second as default units
        /// </summary>
        public VelocityQuantity()
        {
            Unit = VelocityUnits.MetersPerSecond;
            Velocity = new Vector2D();
        }

        /// <summary>
        /// New instance with specified velocity using the specified units
        /// </summary>
        /// <param name="velocity">The new velocity</param>
        /// <param name="unit">Unit used for the velocity</param>
        public VelocityQuantity(Vector2D velocity, VelocityUnits unit = VelocityUnits.MetersPerSecond)
        {
            Unit = unit;
            Velocity = velocity;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// The velocity value in meters/second
        /// </summary>
        private Vector2D velocity;

        #endregion

        #region Public Properties

        /// <summary>
        /// The velocity value in specified units
        /// </summary>
        public Vector2D Velocity
        {
            get
            {
                // The mass is stored in meters/second --> convert back to the right unit
                switch (Unit)
                {
                    case VelocityUnits.MetersPerSecond:
                        return velocity;
                    case VelocityUnits.KilometrsPerHour:
                        return (velocity * (3.6));
                    default:
                        throw new ArgumentException("Unknown unit!");
                }
            }
            set
            {
                // The velocity needs to be stored in meters/second --> convert
                switch (Unit)
                {
                    case VelocityUnits.MetersPerSecond:
                        velocity = value;
                        break;
                    case VelocityUnits.KilometrsPerHour:
                        velocity = value / (3.6);
                        break;
                    default:
                        throw new ArgumentException("Unknown unit!");
                }
            }
        }

        /// <summary>
        /// The units used for the velocity
        /// </summary>
        public VelocityUnits Unit { get; set; } = VelocityUnits.MetersPerSecond;

        #endregion

    }

    /// <summary>
    /// Units of velocity
    /// </summary>
    public enum VelocityUnits
    {
        /// <summary>
        /// meters/second
        /// </summary>
        MetersPerSecond = 0,

        /// <summary>
        /// kilometers/hour
        /// </summary>
        KilometrsPerHour
    }
}
