using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public class HittableList : IHittable
    {
        public List<IHittable> objs;

        public HittableList() { 
            objs = new List<IHittable>();
        }

        public HittableList(IHittable obj) : this()
        {
            Add(obj);
        }

        public void Add(IHittable obj)
        {
            objs.Add(obj);
        }

        public void Clear()
        {
            objs.Clear();
        }

        public bool hit(Ray r, Interval rayt, ref HitRecord rec)
        {
            HitRecord tempRec = new HitRecord();
            bool hitAnything = false;
            double closestSoFar = rayt.max;

            foreach(IHittable obj in objs)
            {
                if (obj.hit(r, new Interval(rayt.min,closestSoFar), ref tempRec))
                {
                    hitAnything = true;
                    closestSoFar = tempRec.t;
                    rec = tempRec;
                }
            }

            return hitAnything;

        }
    }
}
