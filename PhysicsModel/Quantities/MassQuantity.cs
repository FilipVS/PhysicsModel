using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsModel
{
    /// <summary>
    /// Mass quantity of an object
    /// </summary>
    public class MassQuantity
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// New instance with 0 Mass and kilograms as default units
        /// </summary>
        public MassQuantity()
        {
            Unit = MassUnit.kg;
            Mass = 0;
        }

        /// <summary>
        /// New instance with specified mass using the specified units
        /// </summary>
        /// <param name="mass">The new mass</param>
        /// <param name="unit">Unit used for the mass</param>
        public MassQuantity(double mass, MassUnit unit = MassUnit.kg)
        {
            Unit = unit;
            Mass = mass;
        }

        #endregion

        #region Private Members

        /// <summary>
        /// The mass value in kilograms
        /// </summary>
        private double mass;

        #endregion

        #region Public Properties

        /// <summary>
        /// The mass value in specified units
        /// </summary>
        public double Mass
        {
            get
            {
                // The mass is stored in kilograms --> convert back to the right unit
                switch (Unit)
                {
                    case MassUnit.kg:
                        return mass;
                    case MassUnit.g:
                        return (mass * 1000);
                    default:
                        throw new ArgumentException("Unknown unit!");
                }
            }
            set
            {
                // The mass needs to be stored in kilograms --> convert
                switch (Unit)
                {
                    case MassUnit.kg:
                        mass = value;
                        break;
                    case MassUnit.g:
                        mass = value / 1000;
                        break;
                    default:
                        throw new ArgumentException("Unknown unit!");
                }
            }
        }

        /// <summary>
        /// The units used for the mass
        /// </summary>
        public MassUnit Unit { get; set; } = MassUnit.kg;

        #endregion

    }

    /// <summary>
    /// Units of mass
    /// </summary>
    public enum MassUnit
    {
        /// <summary>
        /// kilogram
        /// </summary>
        kg = 0,

        /// <summary>
        /// grams
        /// </summary>
        g
    }
}
