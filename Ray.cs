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


        public Point3 Origin { get; set; }

        public Vec3 Direction { get; set; }


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

        public Color3 rayColor()
        {
            Vec3 unitDirection = Vec3.unitVector(direction);
            double a = 0.5 * (unitDirection.y + 1.0);

            return (1.0 - a) * new Color3(1.0, 1.0, 1.0) + a * new Color3(0.5, 0.7, 1.0);
        }

        public Point3 at(double t)
        {
            return origin + t*direction;
        }
    }
}
