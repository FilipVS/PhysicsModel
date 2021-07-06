using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsModel
{
    /// <summary>
    /// Charge quantity of an object
    /// </summary>
    public class ChargeQuantity
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// Returns new instance with 0 Charge and default unit set to Coulomb
        /// </summary>
        public ChargeQuantity()
        {
            Unit = ChargeUnit.C;
            Charge = 0;
        }

        /// <summary>
        /// New instance with specified Charge using specified Units
        /// </summary>
        /// <param name="charge">The new Charge of the object</param>
        /// <param name="unit">The Unit used for the Charge</param>
        public ChargeQuantity(double charge, ChargeUnit unit = ChargeUnit.C)
        {
            Unit = unit;
            Charge = charge;
        }

        #endregion

        #region Private members

        /// <summary>
        /// The charge of the object in Coulombs
        /// </summary>
        private double charge;

        #endregion

        #region Public Properties

        /// <summary>
        /// The charge value in specified units
        /// </summary>
        public double Charge
        {
            get
            {
                // The charge is stored in Coulombs --> convert back to the right units
                switch (Unit)
                {
                    case ChargeUnit.C:
                        return charge;
                    case ChargeUnit.microC:
                        return charge * 1000;
                    case ChargeUnit.milliC:
                        return charge * 1000000;
                    case ChargeUnit.nanoC:
                        return charge * 1000000000;
                    default:
                        throw new ArgumentException("Unknown unit!");
                }
            }
            set
            {
                // The charge needs to be stored in Coulombs --> convert
                switch (Unit)
                {
                    case ChargeUnit.C:
                        charge = value;
                        break;
                    case ChargeUnit.milliC:
                        charge = value / 1000;
                        break;
                    case ChargeUnit.microC:
                        charge = value / 1000000;
                        break;
                    case ChargeUnit.nanoC:
                        charge = value / 1000000000;
                        break;
                }
            }
        }

        /// <summary>
        /// The unit used for the charge
        /// </summary>
        public ChargeUnit Unit { get; set; } = ChargeUnit.C;

        #endregion

    }

    /// <summary>
    /// The units of electric charge
    /// </summary>
    public enum ChargeUnit
    {
        /// <summary>
        /// Coulomb
        /// </summary>
        C = 0,

        /// <summary>
        /// Millicoulomb
        /// </summary>
        milliC,

        /// <summary>
        /// Microcoulomb
        /// </summary>
        microC,

        /// <summary>
        /// Nanocoulomb
        /// </summary>
        nanoC
    }
}
