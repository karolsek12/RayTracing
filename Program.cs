using System.Drawing;
using System.Security.Cryptography;

namespace RayTracing
{

    internal class Program
    {
        static void Main(string[] args)
        {

            HittableList world = new HittableList();

            Lambertian materialGround = new Lambertian(new Color3(0.8, 0.8, 0));
            Lambertian materialCenter = new Lambertian(new Color3(0.1, 0.2, 0.5));
            Dielectric materialLeft = new Dielectric(1.50);
            Dielectric materialBubble =  new Dielectric(1.00 / 1.50);
            Metal materialRight = new Metal(new Color3(0.8, 0.6, 0.2),1.0);

            

            world.Add(new Sphere(new Point3(0, -100.5, -1), 100,materialGround));

            world.Add(new Sphere(new Point3(0, 0, -1.2), 0.5,materialCenter));

            world.Add(new Sphere(new Point3(-1.0, 0, -1.0), 0.5, materialLeft));

            world.Add(new Sphere(new Point3(-1.0,0.0,-1.0),0.4,materialBubble));

            world.Add(new Sphere(new Point3(1.0, 0, -1.0), 0.5, materialRight));

            Camera cam = new Camera();

            cam.aspectRatio = 16.0 / 9.0;
            cam.imgWidth = 400;
            cam.samplesPerPixel = 100;
            cam.maxDepth = 50; ;

            cam.Render(world);

        }
    }
}
