using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public static class Functions
    {
        public static double DistanceBetweenTwoPoints(Vector2 point0, Vector2 point1)
        {
            return Math.Sqrt(Math.Pow(point0.X - point1.X, 2) + Math.Pow(point0.Y - point1.Y, 2));
        }
    }
}
