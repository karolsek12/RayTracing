using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public class HitRecord
    {
        public Point3 p;
        public Vec3 normal;
        public double t;
        public bool frontFace;

        public void setFaceNormal(Ray r, Vec3 outwardNormal)
        {
            frontFace = Vec3.dot(r.Direction, outwardNormal) < 0;
            normal = frontFace ? outwardNormal : -outwardNormal;
        }
    }

    public interface IHittable
    {
        public bool hit(Ray r, Interval rayt, ref HitRecord rec);
    }
}
