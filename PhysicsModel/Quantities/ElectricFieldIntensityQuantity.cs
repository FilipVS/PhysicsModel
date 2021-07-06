using System;

namespace PhysicsModel
{
    /// <summary>
    /// Electric field intensity at a point
    /// </summary>
    public class ElectricFieldIntenstiyQuantity
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// New instance with 0 intensity and newtons/coulomb as default units
        /// </summary>
        public ElectricFieldIntenstiyQuantity()
        {
            Unit = ElectricFieldIntenstiyUnit.NewtonsPerCoulomb;
            Intensity = new Vector2D();
        }

        /// <summary>
        /// New instance with specified intensity using the specified units
        /// </summary>
        /// <param name="intensity">The new intensity</param>
        /// <param name="unit">Unit used for the intensity</param>
        public ElectricFieldIntenstiyQuantity(Vector2D intensity, ElectricFieldIntenstiyUnit unit = ElectricFieldIntenstiyUnit.NewtonsPerCoulomb)
        {
            Unit = unit;
            Intensity = intensity;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// The intensity value in newtons/coulomb
        /// </summary>
        private Vector2D intensity;

        #endregion

        #region Public Properties

        /// <summary>
        /// The intensity value in specified units
        /// </summary>
        public Vector2D Intensity
        {
            get
            {
                // The mass is stored in newtons/coulomb --> convert back to the right unit
                switch (Unit)
                {
                    case ElectricFieldIntenstiyUnit.NewtonsPerCoulomb:
                        return intensity;
                    default:
                        throw new ArgumentException("Unknown unit!");
                }
            }
            set
            {
                // The velocity needs to be stored in newtons/coulomb --> convert
                switch (Unit)
                {
                    case ElectricFieldIntenstiyUnit.NewtonsPerCoulomb:
                        intensity = value;
                        break;
                    default:
                        throw new ArgumentException("Unknown unit!");
                }
            }
        }

        /// <summary>
        /// The units used for the velocity
        /// </summary>
        public ElectricFieldIntenstiyUnit Unit { get; set; } = ElectricFieldIntenstiyUnit.NewtonsPerCoulomb;

        #endregion

    }

    /// <summary>
    /// Units of electric field intensity
    /// </summary>
    public enum ElectricFieldIntenstiyUnit
    {
        /// <summary>
        /// newtons/coulomb
        /// </summary>
        NewtonsPerCoulomb

    }
}
