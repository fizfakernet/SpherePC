using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Markers
    {
        public List<double> LMarkers { get; set; }

        public Markers()
        {
            LMarkers = new List<double>();
        }
        public void Add(double x)
        {
            LMarkers.Add(x);
        }

        public void Del(double x)
        {
            double delta = Double.MaxValue;
            double tmp = 0d;
            int n = -1;

            for (int i = 0; i < LMarkers.Count; i++)
            {
                tmp = Math.Abs(LMarkers[i] - x);
                if (delta > tmp)
                {
                    delta = tmp;
                    n = i;
                }
            }
            if (n == -1) return;
            LMarkers.RemoveAt(n);
        }

        public void Сlear()
        {
            LMarkers.Clear();
        }

    }
}
