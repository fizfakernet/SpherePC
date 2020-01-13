using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Component
    {
        public double[] x;
        public double[] y;

        public double step = 10; 

        public int n = 1000;

        public Component(int n)
        {
            this.n = n;
            x = new double[n];
            y = new double[n];

        }
        public void Create ()
        {
            x = new double[n];
            y = new double[n];
            
            //for (int i = -n / 2; i < n; i++)
            //{
            //    x[i+100] = i * 0.25;
            //    y[i+100] = Math.Sin(i * 0.25);

            //    //x[i] = step * i - 2500f;
            //    //x[i] = (x[i] - 2500) / 130;
            //    //y[i] = 2 * (x[i] / (x[i] * x[i] + 2d * 2d) - x[i] / (x[i] * x[i] + (2d + 3d) * (2d + 3d)));
            //}
        }

        public void Derivative()
        {
            for (int i = 0; i < n - 1; i++)
                y[i] = (y[i + 1] - y[i])/ Math.Abs(x[i + 1] - x[i]);
            
            n -= 1;
        }


    }
}
