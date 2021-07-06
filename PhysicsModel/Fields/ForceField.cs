using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsModel
{
    /// <summary>
    /// Represents a force field created by one or more points
    /// </summary>
    /// <typeparam name="ActingPoint">The type of Points that the ForceField will be comprised of</typeparam>
    /// <typeparam name="TargetPoint">The type of Points that the ForceField will be able to act upon</typeparam>
    public abstract class ForceField<ActingPoint, TargetPoint>
        where ActingPoint : PhysicsModel.Point
        where TargetPoint : PhysicsModel.Point
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ForceField()
        {
            PointsActing = new List<ActingPoint>();
        }

        #endregion

        #region Private/Protected members

        /// <summary>
        /// The Points whose force field overlap to create the final force field
        /// </summary>
        protected List<ActingPoint> PointsActing { get; set; }

        #endregion

        #region Public Methods

        #region Methods for adding/removing items of the force field

        /// <summary>
        /// Add a point whose force field will be part of the overall force field
        /// </summary>
        /// <param name="point">The point to be added</param>
        public void AddPointActing(ActingPoint point)
        {
            PointsActing.Add(point);
        }

        /// <summary>
        /// Add a range of points whose force fields will be part of the overall force field
        /// </summary>
        /// <param name="points">The points to be added</param>
        public void AddPointActingRange(IEnumerable<ActingPoint> points)
        {
            PointsActing.AddRange(points);
        }

        /// <summary>
        /// Removes the first instance of the point from the force field
        /// </summary>
        /// <param name="point">The point to be removed</param>
        /// <returns>True if item removed succesfully, otherwise false</returns>
        public bool RemovePointActing(ActingPoint point)
        {
            return PointsActing.Remove(point);
        }

        #endregion

        /// <summary>
        /// Acts on a specified point for a specified amount of time
        /// </summary>
        /// <param name="actOn">The Point to be acted on</param>
        public abstract Task ActOnPointAsync(TargetPoint actOn);

        #endregion

    }
}
