using System.Drawing;
using System.Security.Cryptography;

namespace RayTracing
{

    internal class Program
    {
        static void Main(string[] args)
        {
            HittableList world= new HittableList();
            Lambertian groundMaterial = new Lambertian(new Color3(0.5,0.5, 0.5));
            world.Add(new Sphere(new Point3(0, -1000, 0), 1000, groundMaterial));

            for(int a = -11; a <= 11; a++)
            {
                for(int b = -11; b <= 11; b++)
                {
                    double chooseMat = Constants.randomDouble();
                    Point3 center = new Point3(a + 0.9 * Constants.randomDouble(),0.2, b + 0.9 * Constants.randomDouble());

                    if((center - new Point3(4,0.2,0)).length() > 0.9)
                    {
                        Material sphereMaterial;

                        if(chooseMat < 0.8)
                        {
                            Color3 albedo = Color3.random() * Color3.random();
                            sphereMaterial = new Lambertian(albedo);
                            world.Add(new Sphere(center, 0.2, sphereMaterial));
                        }else if(chooseMat < 0.95)
                        {
                            Color3 albedo = Color3.random(0.5, 1);
                            double fuzz = Constants.randomDouble(0, 0.5);
                            sphereMaterial = new Metal(albedo, fuzz);
                            world.Add(new Sphere(center,0.2,sphereMaterial));
                        }
                        else
                        {
                            sphereMaterial = new Dielectric(1.5);
                            world.Add(new Sphere(center,0.2,sphereMaterial));
                        }
                    }
                }
            }

            Material material1 = new Dielectric(1.5);
            world.Add(new Sphere(new Point3(0, 1, 0), 1.0, material1));

            Material material2 = new Lambertian(new Color3(0.4, 0.2, 0.1));
            world.Add(new Sphere(new Point3(-4,1,0),1.0, material2));

            Material material3 = new Metal(new Color3(0.7, 0.6, 0.5), 0.0);
            world.Add(new Sphere(new Point3(4,1,0),1.0, material3));

            Camera cam = new Camera();

            cam.aspectRatio = 16.0 / 9.0;
            cam.imgWidth= 1200;
            cam.samplesPerPixel = 10;
            cam.maxDepth = 50;

            cam.vfov = 20;
            cam.lookFrom= new Point3(13, 2, 3);
            cam.lookAt = new Point3(0, 0, 0);
            cam.vup = new Vec3(0, 1, 0);

            cam.defocusAngle = 0.6;
            cam.focusDist = 10.0;

            cam.Render(world);
        }
    }
}
