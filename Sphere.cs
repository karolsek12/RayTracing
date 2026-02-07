using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public class Sphere : IHittable
    {
        private Point3 center;
        private double radius;

        public Sphere(Point3 center, double radius)
        {
            this.center = center;
            this.radius = Math.Max(0,radius);
        }

        public bool hit(Ray r, Interval rayt,ref HitRecord rec)
        {
            Vec3 oc = center - r.Origin;
            double a = r.Direction.lengthSquared();
            double h = Vec3.dot(r.Direction, oc);
            double c = oc.lengthSquared() - radius * radius;

            double delta = h * h - a * c;

            if (delta < 0)
                return false;

            double root = (h - Math.Sqrt(delta)) / a;

            if(!rayt.surrounds(root))
            {
                root = (h + Math.Sqrt(delta)) / a;

                if (!rayt.surrounds(root))
                    return false;
            }

            rec.t = root;
            rec.p = r.at(rec.t);
            Vec3 outwardNormal = (rec.p - center) / radius;
            rec.setFaceNormal(r, outwardNormal);

            return true;
        }
    }
}
