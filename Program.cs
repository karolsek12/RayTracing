using System.Drawing;
using System.Security.Cryptography;

namespace RayTracing
{

    internal class Program
    {
        static void Main(string[] args)
        {
            int imgWidth = 400;
            double aspectRatio = 16.0 / 9.0;

            int imgHeight = (int)(imgWidth/ aspectRatio);

            if (imgHeight < 1)
            {
                imgHeight = 1;
            }

            double viewportHeight = 2.0;
            double viewportWidth = viewportHeight* (((double)imgWidth)/ imgHeight);

            double focalLength = 1.0;
            Vec3 cameraCenter = new Point3(0, 0, 0);
            Vec3 viewportu = new Vec3(viewportWidth,0,0);
            Vec3 viewportv = new Vec3(0, -viewportHeight, 0);

            Vec3 pixelDeltau = viewportu / imgWidth;
            Vec3 pixelDeltav = viewportv / imgHeight;

            Vec3 viewportUpperLeft = cameraCenter - new Vec3(0, 0, focalLength) - viewportu / 2 - viewportv / 2;

            Vec3 pixel100Loc = viewportUpperLeft + 0.5 * (pixelDeltau + pixelDeltav);



            if (File.Exists("image.ppm"))
            {
                File.Delete("image.ppm");
            }

            StreamWriter sw = new StreamWriter("image.ppm");

            sw.WriteLine("P3\n");
            sw.WriteLine(imgWidth + " " + imgHeight);
            sw.WriteLine(255);

            for(int i = 0;i< imgHeight; i++)
            {
                for(int j = 0;j< imgWidth; j++)
                {
                    Vec3 pixelCenter = pixel100Loc + (j*pixelDeltau) + (i*pixelDeltav);
                    Vec3 rayDirection = pixelCenter - cameraCenter;

                    Ray r = new Ray(cameraCenter, rayDirection);

                    Color3 pixelColor = r.rayColor();

                    Color3.WriteColor(sw, pixelColor);
                    
                }
                Console.WriteLine("Progress: " + (i + 1) * imgWidth + "/" + (imgWidth * imgHeight));
            }

            sw.Close();
        }
    }
}
