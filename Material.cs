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

    public class Dielectric: Material
    {
        private double refractionIndex;

        public Dielectric(double refractionIndex)
        {
            this.refractionIndex = refractionIndex;
        }

        public override bool scatter(Ray rIn, HitRecord rec, ref Point3 attentuation, ref Ray scattered)
        {
            attentuation = new Color3(1.0, 1.0, 1.0);
            double ri = rec.frontFace ? (1.0/refractionIndex) : refractionIndex;

            Vec3 unitDir = Vec3.unitVector(rIn.Direction);
            double cosTheta  = Math.Min(Vec3.dot(-unitDir ,rec.normal), 1.0);
            double sinTheta = Math.Sqrt(1.0 - cosTheta * cosTheta);

            bool cannotRefract = ri * sinTheta > 1.0;
            Vec3 dir;

            if (cannotRefract)
            {
                dir = Vec3.Reflect(unitDir,rec.normal);
            }
            else
            {
                dir = Vec3.Refract(unitDir, rec.normal, ri);
            }

            scattered = new Ray(rec.p, dir);

            return true;
        }

        private static double reflectance(double cos,double refractionIndex)
        {
            double r0 = (1 - refractionIndex) / (1 + refractionIndex);
            r0 = r0 * r0;

            return r0 + (1.0 - r0) * Math.Pow(1.0 - cos, 5);
        }
    }
}
