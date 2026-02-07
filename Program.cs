using System.Drawing;
using System.Security.Cryptography;

namespace RayTracing
{

    internal class Program
    {
        static void Main(string[] args)
        {

            HittableList world = new HittableList();

            world.Add(new Sphere(new Point3(0, 0, -1), 0.5));

            world.Add(new Sphere(new Point3(0, -100.5, -1), 100));

            Camera cam = new Camera();

            cam.aspectRatio = 16.0 / 9.0;
            cam.imgWidth = 400;

            cam.Render(world);

        }
    }
}
