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

        public static double AngleBetweenTwoPoints(Vector2 point0, Vector2 point1)
        {
            Vector2 a = new Vector2(0, 0);
            Vector2 b = point1 - point0;

            double result = Math.Atan2(a.Y, a.X) - Math.Atan2(b.Y, b.X);
            return 2 * Math.PI - (result < 0 ? result + 2 * Math.PI : result);
        }

        public static bool CircleCircleCollision(Vector2 center0, double radius0, Vector2 center1, double radius1)
        {
            return DistanceBetweenTwoPoints(center0, center1) <= radius0 + radius1;
        }
    }
}
