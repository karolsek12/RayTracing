global using Point3 = RayTracing.Vec3;
global using Color3 = RayTracing.Vec3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;




namespace RayTracing
{
   
    public struct Vec3
    {
        public double[] e;

        public Vec3()
        {
            e = [0, 0, 0];
        }

        public Vec3(double e1, double e2, double e3)
        {
            e = [e1, e2, e3];
        }

        public double x => e[0];
        public double y => e[1];
        public double z => e[2];

        public static Vec3 operator -(Vec3 v)
        {
            return new Vec3(-v.x, -v.y, -v.z);
        }

        public static Vec3 operator +(Vec3 v1, Vec3 v2)
        {
            return new Vec3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vec3 operator -(Vec3 v1, Vec3 v2)
        {
            return new Vec3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vec3 operator *(Vec3 v, double a)
        {
            return new Vec3(v.x * a, v.y * a, v.z * a);
        }

        public static Vec3 operator *(double a, Vec3 v)
        {
            return new Vec3(v.x * a, v.y * a, v.z * a);
        }

        public static Vec3 operator *(Vec3 v1, Vec3 v2){

            return new Vec3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        public static Vec3 operator /(Vec3 v,double a)
        {
            return new Vec3(v.x / a, v.y / a, v.z / a);
        }

        public static double dot(Vec3 v1, Vec3 v2)
        {
            return v1.x*v2.x + v1.y*v2.y + v1.z*v2.z;
        }

        public static Vec3 cross(Vec3 v1,Vec3 v2)
        {
            return new Vec3(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
        }

        public static Vec3 unitVector(Vec3 v)
        {
            return v / v.length();
        }

        public double lengthSquared()
        {
            return x * x + y * y + z * z;
        }

        public double length()
        {
            return Math.Sqrt(lengthSquared());
        }

        public static Vec3 random()
        {
            return new Vec3(Constants.randomDouble(), Constants.randomDouble(), Constants.randomDouble());
        }
        
        public static Vec3 random(double min, double max)
        {
            return new Vec3(Constants.randomDouble(min,max), Constants.randomDouble(min,max), Constants.randomDouble(min,max));
        }

        public static Vec3 randomUnitVector()
        {
            while (true)
            {
                Vec3 p = random(-1, 1);
                double lensq = p.lengthSquared();
                if (1e-160 < lensq && lensq <= 1)
                    return p / Math.Sqrt(lensq);
            }

        }

        public static Vec3 randomOnHemisphere(Vec3 normal)
        {
            Vec3 onUnitSphere = randomUnitVector();
            if (dot(onUnitSphere, normal) > 0.0)
            {
                return onUnitSphere;
            }
            else
            {
                return -onUnitSphere;
            }
        }

        public double this[int i]
        {
            get => e[i];
            set => e[i] = value;
        }


        public override string ToString()
        {
            return x + " " + y + " " + z;
        }

        public static double LinearToGamma(double linear)
        {
            if(linear > 0)
            {
                return Math.Sqrt(linear);
            }

            return 0;
        }

        public bool NearZero()
        {
            double s = 1e-8;
            return Math.Abs(e[0]) < s && Math.Abs(e[1]) < s && Math.Abs(e[2]) < s;
        }

        public static Vec3 Reflect(Vec3 v, Vec3 n)
        {
            return v - n*(2*dot(v, n));
        }
        
        public static Vec3 Refract(Vec3 uv, Vec3 n, double ratio)
        {
            double cosTheta = Math.Min(dot(-uv, n), 1.0);
            Vec3 rOutPerp = ratio * (uv + cosTheta * n);
            Vec3 rOutParr = -Math.Sqrt(Math.Abs(1.0 - rOutPerp.lengthSquared())) * n;
            return rOutPerp + rOutParr;
        }

        public static Vec3 RandomInUnitDisk()
        {

            while (true)
            {
                Vec3 p = new Vec3(Constants.randomDouble(-1,1),Constants.randomDouble(-1,1), 0);
                if (p.lengthSquared() < 1)
                    return p;
            }
        }
        public static void WriteColor(StreamWriter s, Color3 color)
        {
            Interval intensity = new Interval(0.0, 1.0);
            int r = (int)(255 * intensity.clamp(LinearToGamma(color.x)));
            int g = (int)(255 * intensity.clamp(LinearToGamma(color.y)));
            int b = (int)(255 * intensity.clamp(LinearToGamma(color.z)));
            

            if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255)
                throw new Exception("Wrong color");

            s.WriteLine(r + " " + g + " " + b);

        }

    }
}
