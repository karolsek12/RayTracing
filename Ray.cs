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

        /*
        public double hitSphere(Point3 center, double r)
        {
            Vec3 oc = center - origin;
            double a = direction.lengthSquared();
            double h = Vec3.dot(direction, oc);
            double c = oc.lengthSquared() - r*r;

            double delta = h*h - a*c;

            if (delta < 0)
                return -1;

            return (h - Math.Sqrt(delta)) / a;
        }
        */
        public Color3 rayColor(IHittable world)
        {
            HitRecord rec = new HitRecord();
            if (world.hit(this, new Interval(0,Constants.infinity), ref rec)){
                return 0.5 * (rec.normal + new Color3(1, 1, 1));
            }

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
