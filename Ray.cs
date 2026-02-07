using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public class Ray
    {
        private Point3 origin;
        private Vec3 direction;


        public Point3 Origin => origin;

        public Vec3 Direction => direction;



        public Ray() 
        {
            origin = new Point3();
            direction = new Vec3();
        }

        public Ray(Point3 origin, Vec3 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        public Point3 at(double t)
        {
            return origin + t*direction;
        }
    }
}
