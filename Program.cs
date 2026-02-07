using System.Drawing;
using System.Security.Cryptography;

namespace RayTracing
{

    internal class Program
    {
        static void Main(string[] args)
        {
            int imgWidth = 256;
            int imgHeight = 256;
            Random random = new Random();
            if (File.Exists("image.ppm"))
            {
                File.Delete("image.ppm");
            }

            StreamWriter sw = new StreamWriter("image.ppm");

            sw.WriteLine("P3\n");
            sw.WriteLine(imgWidth + " " + imgHeight);
            sw.WriteLine(255);

            for(int i = 0;i< imgWidth; i++)
            {
                for(int j = 0;j< imgHeight; j++)
                {
                    double r =  (double)j / (imgWidth-1);
                    double g = (double)i / (imgHeight-1);
                    double b = 0;
                    Color3.WriteColor(sw, new Color3(r,g,b));
                    
                }
                Console.WriteLine("Progress: " + (i + 1) * imgHeight + "/" + (imgWidth * imgHeight));
            }

            sw.Close();
        }
    }
}
