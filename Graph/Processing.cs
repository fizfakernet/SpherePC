using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph
{
    class Processing
    {
        private static Dictionary<string, Func<Components, Components>> ListFunc = new Dictionary<string, Func<Components, Components>>();
        public static Components Comp { get; set; }
        public static void AddFunc(string key, Func<Components, Components> func)
        {
            ListFunc.Add(key, func);
            Comp = func(Comp);

            //Comp.x = comp.x;
            //Comp.y = comp.y;
            //Update();
        }

        public static Components DelFunc(string key, Components comp)
        {
            ListFunc.Remove(key);
            Comp.x = comp.x.Select(t => t).ToArray();
            Comp.y = comp.y.Select(t => t.Select(u => u).ToArray()).ToList();

            Thread thread = new Thread(Update);
            thread.Start();
            //Update();

            return Comp;
        }

        public static void Update()
        {
            //
            if (ListFunc.Count == 0)
                return;
            foreach (KeyValuePair<string, Func<Components, Components>> dict in ListFunc)
            {
                //double x = Comp.y[0][0];
                Comp = dict.Value(Comp);
                //x = Comp.y[0][0];
            }
            //
            ;
            //return comp;
        }
    }

    //class K:Processing
    //{
    //    double k = 1d;
    //    K(double x)
    //    {
    //        k = x;
    //    }


    //}

}
