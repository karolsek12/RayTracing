using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public  class Camera
    {
        public int imgWidth = 100;

        public double aspectRatio = 1.0;

        public int samplesPerPixel = 10;

        public int maxDepth = 10;

        public double vfov = 90.0;

        public Point3 lookFrom = new Point3(0.0, 0.0, 0.0);

        public Point3 lookAt = new Point3(0.0, 0.0, -1.0);

        public Vec3 vup = new Vec3(0.0, 1.0, 0.0);

        private int imgHeight;

        private Vec3 cameraCenter;

        private Vec3 pixel100Loc;

        private Vec3 pixelDeltau;

        private Vec3 pixelDeltav;

        private double pixelSamplesScale;

        private Vec3 u;

        private Vec3 v;

        private Vec3 w;


        private void Initialize()
        {
            imgHeight = (int)(imgWidth / aspectRatio);

            if (imgHeight < 1)
                imgHeight = 1;

            pixelSamplesScale = 1.0 / samplesPerPixel;

            cameraCenter = lookFrom;

            double focalLength = (lookFrom-lookAt).length();

            double theta = Constants.degToRad(vfov);

            double h = Math.Tan(theta / 2);

            double viewportHeight = 2 * h * focalLength;

            double viewportWidth = viewportHeight * (((double)imgWidth) / imgHeight);

            w = Vec3.unitVector(lookFrom - lookAt);

            u = Vec3.unitVector(Vec3.cross(vup,w));

            v = Vec3.cross(w, u);

            Vec3 viewportu = viewportWidth * u;

            Vec3 viewportv = viewportHeight * -v;

            pixelDeltau = viewportu / imgWidth;

            pixelDeltav = viewportv / imgHeight;

            Vec3 viewportUpperLeft = cameraCenter - (focalLength*w) - viewportu / 2 - viewportv / 2;

            pixel100Loc = viewportUpperLeft + 0.5 * (pixelDeltau + pixelDeltav);
            
        }

        private Vec3 sampleSquare()
        {
            return new Vec3(Constants.randomDouble() - 0.5, Constants.randomDouble() - 0.5, 0);
        }

        private Ray GetRay(int i, int j)
        {
            Vec3 offset = sampleSquare();

            Vec3 pixelSample = pixel100Loc + ((i + offset.x) * pixelDeltau) + ((j + offset.y) * pixelDeltav);

            Point3 rayOrigin = cameraCenter;

            Vec3 rayDirection = pixelSample - rayOrigin;

            return new Ray(rayOrigin, rayDirection);
        }

        public void Render(IHittable world)
        {
            Initialize();

            if (File.Exists("image.ppm"))
            {
                File.Delete("image.ppm");
            }

            StreamWriter sw = new StreamWriter("image.ppm");


            sw.WriteLine("P3\n");
            sw.WriteLine(imgWidth + " " + imgHeight);
            sw.WriteLine(255);

            for (int i = 0; i < imgHeight; i++)
            {
                for (int j = 0; j < imgWidth; j++)
                {
                    Color3 pixelColor = new Color3(0, 0, 0);
                    for(int p = 0;p< samplesPerPixel;p++)
                    {
                        Ray r = GetRay(j, i);
                        pixelColor += rayColor(r,maxDepth, world);
                    }
                    Color3.WriteColor(sw, pixelColor * pixelSamplesScale);

                }
                Console.WriteLine("Progress: " + (i + 1) * imgWidth + "/" + (imgWidth * imgHeight));
            }

            sw.Close();
        }

        private Color3 rayColor(Ray r,int depth,IHittable world)
        {
            if (depth <= 0)
                return new Color3(0, 0, 0);

            HitRecord rec = new HitRecord();
            if (world.hit(r, new Interval(0.001, Constants.infinity), ref rec))
            {
                Ray scattered = new Ray();
                Color3 attentuation = new Color3();

                if(rec.mat.scatter(r,rec,ref attentuation, ref scattered))
                    return attentuation * rayColor(scattered,depth-1,world);
                return new Color3(0, 0, 0);
            }

            Vec3 unitDirection = Vec3.unitVector(r.Direction);
            double a = 0.5 * (unitDirection.y + 1.0);

            return (1.0 - a) * new Color3(1.0, 1.0, 1.0) + a * new Color3(0.5, 0.7, 1.0);
        }
    }
}
