using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public class Interval
    {
        public double min;
        public double max;

        public Interval()
        {
            min = -Constants.infinity;
            max = Constants.infinity;
        }

        public Interval(double min, double max)
        {
            this.min = min;
            this.max = max;
        }

        public double size()
        {
            return max- min;
        }

        public bool contains(double x)
        {
            return x >= min && x <= max;
        }

        public bool surrounds(double x)
        {
            return min<x && x<max;
        }

        public double clamp(double x)
        {
            if (x < min) return min;
            if (x > max) return max;
            return x;
        }

        public static Interval empty = new Interval(+Constants.infinity,-Constants.infinity);

        public static Interval universe = new Interval(-Constants.infinity, +Constants.infinity);
    }
}
