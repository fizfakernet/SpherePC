using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace Graph
{
    public class Square
    {
        public float Point1X { get; set; } = 0f;
        public float Point1Y { get; set; } = 0f;

        public float Point2X { get; set; } = 0f;
        public float Point2Y { get; set; } = 0f;
        public bool Visible { get; set; } = true;
        public OpenGL Gl { get; set; }

        public Square(OpenGL Gl)
        {
            this.Gl = Gl;
        }



        public void Zeroize()
        {
            Point1X = 0f;
            Point1Y = 0f;
            Point2X = 0f;
            Point2Y = 0f;
        }

        public void Normalize()
        {
            float tmp;

            if(Point1X > Point2X)
            {
                tmp = Point1X;
                Point1X = Point2X;
                Point2X = tmp;
            }

            if (Point1Y < Point2Y)
            {
                tmp = Point1Y;
                Point1Y = Point2Y;
                Point2Y = tmp;
            }
        }

        public float GetArea()
        {
            return Math.Abs(Point2X - Point1X) * Math.Abs(Point1Y - Point2Y);
        }
    }
}
