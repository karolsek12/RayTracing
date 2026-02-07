using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public  class Camera
    {
        public int imgWidth;

        public double aspectRatio;

        public int samplesPerPixel;

        private int imgHeight;

        private Vec3 cameraCenter;

        private Vec3 pixel100Loc;

        private Vec3 pixelDeltau;

        private Vec3 pixelDeltav;

        private double pixelSamplesScale;



        private void Initialize()
        {
            imgHeight = (int)(imgWidth / aspectRatio);

            if (imgHeight < 1)
                imgHeight = 1;

            pixelSamplesScale = 1.0 / samplesPerPixel;

            cameraCenter = new Point3(0, 0, 0);

            double focalLength = 1.0;

            double viewportHeight = 2.0;

            double viewportWidth = viewportHeight * (((double)imgWidth) / imgHeight);

            Vec3 viewportu = new Vec3(viewportWidth, 0, 0);

            Vec3 viewportv = new Vec3(0, -viewportHeight, 0);

            pixelDeltau = viewportu / imgWidth;

            pixelDeltav = viewportv / imgHeight;

            Vec3 viewportUpperLeft = cameraCenter - new Vec3(0, 0, focalLength) - viewportu / 2 - viewportv / 2;

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
                        pixelColor += rayColor(r, world);
                    }
                    Color3.WriteColor(sw, pixelColor * pixelSamplesScale);

                }
                Console.WriteLine("Progress: " + (i + 1) * imgWidth + "/" + (imgWidth * imgHeight));
            }

            sw.Close();
        }

        private Color3 rayColor(Ray r,IHittable world)
        {
            HitRecord rec = new HitRecord();
            if (world.hit(r, new Interval(0, Constants.infinity), ref rec))
            {
                return 0.5 * (rec.normal + new Color3(1, 1, 1));
            }

            Vec3 unitDirection = Vec3.unitVector(r.Direction);
            double a = 0.5 * (unitDirection.y + 1.0);

            return (1.0 - a) * new Color3(1.0, 1.0, 1.0) + a * new Color3(0.5, 0.7, 1.0);
        }
    }
}
