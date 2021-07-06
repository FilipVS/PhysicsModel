using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsModel
{
    /// <summary>
    /// Representation of two-dimensional vector
    /// </summary>
    public class Vector2D
    {

        #region Constructors

        /// <summary>
        /// Default constructor
        /// Initializes new instance with coordinates set to [0,0]
        /// </summary>
        public Vector2D()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Initializes new instance with coordinates set to [x,y]
        /// </summary>
        /// <param name="x">The X coordinate of the vector</param>
        /// <param name="y">The y coordinate of the vector</param>
        public Vector2D(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The X coordinate of the vector
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The Y coordinate of the vector
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// The magnitude of the vector
        /// </summary>
        public double Magnitude
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Retunrs the distance between the end points of this vector and some other
        /// </summary>
        /// <param name="distanceTo">The other vector</param>
        /// <returns></returns>
        public double DistanceTo(Vector2D distanceTo)
        {
            return Math.Sqrt((this.X - distanceTo.X) * (this.X - distanceTo.X) + (this.Y - distanceTo.Y) * (this.Y - distanceTo.Y));
        }

        /// <summary>
        /// Make the Vector's magnitude a unit vector
        /// </summary>
        public void MakeUnitVector()
        {
            double magnitude = Magnitude;

            X /= magnitude;

            Y /= magnitude;
        }

        #endregion

        #region Public static methods

        /// <summary>
        /// Add more vectors together
        /// </summary>
        /// <param name="vectors">The vectors to be combined</param>
        /// <returns>The combined vector of all the individual ones</returns>
        public static Vector2D CombineVectors(IEnumerable<Vector2D> vectors)
        {
            double combinedX = 0;
            double combinedY = 0;

            foreach(Vector2D vector in vectors)
            {
                combinedX += vector.X;
                combinedY += vector.Y;
            }

            return new Vector2D(combinedX, combinedY);
        }

        #endregion

        #region Operators override

        public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D((a.X + b.X), (a.Y + b.Y));

        public static Vector2D operator -(Vector2D a, Vector2D b) => new Vector2D((a.X - b.X), (a.Y - b.Y));

        public static Vector2D operator *(Vector2D vector, double num) => new Vector2D((vector.X * num), (vector.Y * num));

        public static Vector2D operator /(Vector2D vector, double num) => new Vector2D((vector.X / num), (vector.Y / num));

        #endregion

    }
}
