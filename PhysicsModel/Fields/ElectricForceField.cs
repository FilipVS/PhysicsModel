using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsModel
{
    public class ElectricForceField : ForceField<PointMassCharge, PointMassCharge>
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ElectricForceField()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Acts on a specified point for a specified amount of time
        /// </summary>
        /// <param name="actOn">The Point to be acted on</param>
        /// <param name="actFor">The time for the field to exert force on the point</param>
        public async override Task ActOnPointAsync(PointMassCharge actOn)
        {
            // Check if the point we are acting on has any mass
            if (actOn.Mass.Mass == 0)
                throw new ArgumentException("The point acted on cannot have zero mass!");

            // The tasks that will calculate the intensities for all the points acting
            List<Task<ElectricFieldIntenstiyQuantity>> tasks = new List<Task<ElectricFieldIntenstiyQuantity>>();

            // Calculate the electric field intensities for all points acting
            foreach(PointMassCharge pointActing in PointsActing)
            {
                tasks.Add(Task.Run(() => CalculateElectricFieldIntensityForPoint(pointActing, actOn.Position)));
            }

            // Wait for all tasks to complete
            ElectricFieldIntenstiyQuantity[] individualVectors = await Task.WhenAll(tasks);

            // Combine the intensity vectors to get the net intensity
            Vector2D fieldIntensityVector = Vector2D.CombineVectors(individualVectors.Select(x=>x.Intensity));

            // Get the force by multiplying the intensity by the point's charge
            Vector2D forceApplied = fieldIntensityVector * actOn.Charge.Charge;

            // Get the acceleration by dividing the force by the mass
            Vector2D accelerationApplied = (forceApplied) / actOn.Mass.Mass;

            // Set the new acceleration
            actOn.Acceleration.Acceleration = accelerationApplied;
        }

        #region Helper methods

        /// <summary>
        /// Calculates intenstiy of electic field of some PointMassCharge at a specified position
        /// </summary>
        /// <param name="fieldSource">The source of the electric field</param>
        /// <param name="intensityAt">The position where the field's intensity is meassured</param>
        /// <returns></returns>
        private ElectricFieldIntenstiyQuantity CalculateElectricFieldIntensityForPoint(PointMassCharge fieldSource, Vector2D intensityAt)
        {
            // Get the unit vector with the direction of the intensity
            Vector2D intensityDirection = intensityAt - fieldSource.Position;

            // Make it a unit vector
            intensityDirection.MakeUnitVector();

            // If the fieldSource's charge is negative, the intensity vector is opposite
            if (fieldSource.Charge.Charge < 0)
                intensityDirection *= -1;


            // Use coulomb's law
            // Distance
            double d = fieldSource.Position.DistanceTo(intensityAt);

            // Charge
            double charge = Math.Abs(fieldSource.Charge.Charge);

            // Coulom's constant
            double constant = 8.9875517923 * 1000000000;

            // Get the intensity magnitude
            double magnitude = constant * charge / (d * d);

            // Combine the direction with the magnitude
            Vector2D intensityVector = intensityDirection * magnitude;

            return new ElectricFieldIntenstiyQuantity(intensityVector, ElectricFieldIntenstiyUnit.NewtonsPerCoulomb);
        }

        #endregion

        #endregion

    }
}
