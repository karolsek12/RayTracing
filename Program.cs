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
                    int r =  (int)(255*(double)(i)/(imgWidth-1));
                    int g = (int)(255 * (double)(j) / (imgHeight - 1));
                    int b = 0;
                    sw.Write(r + " " + g + " " + b + " ");
                    
                }
                sw.WriteLine();
                Console.WriteLine("Progress: " + (i + 1) * imgHeight + "/" + (imgWidth * imgHeight));
            }

            sw.Close();
        }
    }
}
