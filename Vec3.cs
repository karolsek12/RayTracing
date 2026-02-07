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
   
    public class Vec3
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
            return new Vec3(v1.y * v2.z - v2.y * v1.z, v1.z * v2.x - v1.x - v2.z, v1.x * v2.y - v1.y * v2.x);
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


        public double this[int i]
        {
            get => e[i];
            set => e[i] = value;
        }


        public override string ToString()
        {
            return x + " " + y + " " + z;
        }

        public static void WriteColor(StreamWriter s, Color3 color)
        {
            Interval intensity = new Interval(0.0, 1.0);
            int r = (int)(255 * intensity.clamp(color.x));
            int g = (int)(255 * intensity.clamp(color.y));
            int b = (int)(255 * intensity.clamp(color.z));
            

            if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255)
                throw new Exception("Wrong color");

            s.WriteLine(r + " " + g + " " + b);

        }

    }
}
