using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    abstract public class Material
    {
        public virtual bool scatter(Ray rIn, HitRecord rec, ref Color3 attentuation, ref Ray scattered)
        {
            return false;
        }
    }

    public class Lambertian : Material
    {
        private Color3 albedo;

        public Lambertian(Color3 albedo) 
        {
            this.albedo = albedo;
        }

        public override bool scatter(Ray rIn, HitRecord rec, ref Point3 attentuation, ref Ray scattered)
        {
            Vec3 scatterDirection = rec.normal + Vec3.randomUnitVector();

            if (scatterDirection.NearZero())
                scatterDirection = rec.normal;

            scattered = new Ray(rec.p, scatterDirection);
            attentuation = albedo;

            return true;
        }
    }

    public class Metal : Material
    {
        private Color3 albedo;
        private double fuzz;

        public Metal(Color3 albedo, double fuzz)
        {
            this.albedo = albedo;
            this.fuzz = fuzz;
        }

        public override bool scatter(Ray rIn, HitRecord rec, ref Point3 attentuation, ref Ray scattered)
        {
            Vec3 reflected = Vec3.Reflect(rIn.Direction, rec.normal);
            reflected = Vec3.unitVector(reflected) + (fuzz * Vec3.randomUnitVector());
            scattered = new Ray(rec.p, reflected);
            attentuation = albedo;

            return Vec3.dot(scattered.Direction,rec.normal) > 0;
        }
    }
}
