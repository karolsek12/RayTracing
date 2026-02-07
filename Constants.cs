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


        public static double degToRad(double deg)
        {
            return deg * (pi / 180.0);
        }

    }
}
