using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsModel
{
    /// <summary>
    /// Represents a Point in 2D space with mass and charge quantities
    /// </summary>
    public class PointMassCharge : PointMass
    {

        #region Constants

        private const string STATIONARY_POSITIVE_SYMBOL = "+Q";
        private const string STATIONARY_NEGATIVE_SYMBOL = "-Q";
        private const string MOVABLE_POSITIVE_SYMBOL = "+q";
        private const string MOVABLE_NEGATIVE_SYMBOL = "-q";

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// Returns an instance with Position set to [0,0], 0 kg mass and 0 C charge
        /// </summary>
        public PointMassCharge()
        {
            Charge = new ChargeQuantity();
        }

        /// <summary>
        /// Returns an instance with specified position, 0 kg mass and 0 C charge
        /// </summary>
        /// <param name="position">The position of the Point</param>
        public PointMassCharge(Vector2D position) : base(position)
        {
            Charge = new ChargeQuantity();
        }

        /// <summary>
        /// Returns an instance with specified position, specified mass and 0 C charge
        /// </summary>
        /// <param name="position">The position of the Point</param>
        /// <param name="mass">The mass in kilograms of the Point</param>
        public PointMassCharge(Vector2D position, double mass) : base(position, mass)
        {
            Charge = new ChargeQuantity();
        }

        /// <summary>
        /// Returns new instance with specified position, mass and charge
        /// </summary>
        /// <param name="position">The position of the Point</param>
        /// <param name="mass">The mass in kilograms of the Point</param>
        /// <param name="charge">The charge in Coulombs of the point</param>
        public PointMassCharge(Vector2D position, double mass, double charge) : base(position, mass)
        {
            Charge = new ChargeQuantity(charge, ChargeUnit.C);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The Charge quantity of the point
        /// </summary>
        public ChargeQuantity Charge { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Move the point through an electric force field
        /// </summary>
        /// <param name="field">The electric field to move the point through</param>
        /// <param name="moveFor">How long to move the point for</param>
        /// <param name="segments">Divide the movement time into this amount of segments with assumed constant acceleration in each of those</param>
        public async Task MoveInElectricFieldAsync(ElectricForceField field, double moveFor, double segments = 1)
        {
            // Make sure the number of segments is legal
            if (segments < 1)
                throw new ArgumentException("The movement cannot be divided into " + segments + " segments");

            // How long is each segment with constant acceleration assumed
            double segmentTime = moveFor / segments;

            // Execute the segments
            for(int i = 0; i < segments; i++)
            {
                // Get the acceleration
                await field.ActOnPointAsync(this);

                // Move the point with that acceleration
                MoveAccelerated(segmentTime);
            }
        }

        #endregion

        #region Overrides of object's methods

        /// <summary>
        /// Returns the string representation of the object (based on charge and movability)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Pattern match on the object
            string result = this switch
            {
                { Stationary: true } and { Charge: { Charge: >=0 } } => STATIONARY_POSITIVE_SYMBOL,
                { Stationary: true } and { Charge: { Charge: < 0} } => STATIONARY_NEGATIVE_SYMBOL,
                { Stationary: false } and { Charge: { Charge: >=0 } } => MOVABLE_POSITIVE_SYMBOL,
                { Stationary: false } and { Charge: { Charge: < 0} } => MOVABLE_NEGATIVE_SYMBOL,
                _ => "UnknownParticle"
            };

            return result;
        }

        #endregion

    }
}
