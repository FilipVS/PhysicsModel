using System;
using System.ComponentModel;

namespace PhysicsModel
{
    /// <summary>
    /// Acceleration quantity of an object
    /// </summary>
    public class AccelerationQuantity
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// New instance with 0 acceleration and meters/second/second as default units
        /// </summary>
        public AccelerationQuantity()
        {
            Unit = AccelerationUnits.MetersPerSecondPerSecond;
            Acceleration = new Vector2D();
        }

        /// <summary>
        /// New instance with specified acceleration using the specified units
        /// </summary>
        /// <param name="acceleration">The new acceleration</param>
        /// <param name="unit">Unit used for the acceleration</param>
        public AccelerationQuantity(Vector2D acceleration, AccelerationUnits unit = AccelerationUnits.MetersPerSecondPerSecond)
        {
            Unit = unit;
            Acceleration = acceleration;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// The acceleration value in meters/second/second
        /// </summary>
        private Vector2D acceleration;

        #endregion

        #region Public Properties

        /// <summary>
        /// The acceleration value in specified units
        /// </summary>
        public Vector2D Acceleration
        {
            get
            {
                // The mass is stored in meters/second/second --> convert back to the right unit
                switch (Unit)
                {
                    case AccelerationUnits.MetersPerSecondPerSecond:
                        return acceleration;
                    default:
                        throw new ArgumentException("Unknown unit!");
                }
            }
            set
            {
                // The velocity needs to be stored in meters/second/second --> convert
                switch (Unit)
                {
                    case AccelerationUnits.MetersPerSecondPerSecond:
                        acceleration = value;
                        break;
                    default:
                        throw new ArgumentException("Unknown unit!");
                }
            }
        }

        /// <summary>
        /// The units used for the velocity
        /// </summary>
        public AccelerationUnits Unit { get; set; } = AccelerationUnits.MetersPerSecondPerSecond;

        #endregion

    }

    /// <summary>
    /// Units of acceleration
    /// </summary>
    public enum AccelerationUnits
    {
        /// <summary>
        /// meters/second/second
        /// </summary>
        [Description ("m/ss")]
        MetersPerSecondPerSecond = 0,

    }
}
