using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public static class Constants
    {
        public const double infinity = Double.PositiveInfinity;
        public const double pi = Math.PI;
        public const double tau = Math.Tau;
        private static Random random = new Random();

        public static double degToRad(double deg)
        {
            return deg * (pi / 180.0);
        }

        public static double randomDouble()
        {
            return random.NextDouble();
        }

        public static double randomDouble(double min, double max)
        {
            return min + (max - min) * randomDouble();
        }

    }
}
