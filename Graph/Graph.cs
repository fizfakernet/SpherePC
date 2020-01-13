using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NCalc;

namespace Graph
{
    public class Graph
    {
        public OpenGL Gl { get; set; }
        //public Component Selected { get; set; }
        public Components Arr { get; set; }
        public List<int> NumD { get; set; } = new List<int> { 87 };
        public string Expr { get; set; }
        public Markers markers { get; set; }
        public Square square { get; set; }

        //private double scale;
        public double Scale //{ get; set; } = 0.03;
        {
            get
            {
                return Math.Sqrt(ScaleX * ScaleY);
            }
            set
            {
                if (value < 0)

                    value = 0.0001;


                double k = value / Scale;

                ScaleX *= k;
                ScaleY *= k;

                //scale = value;
            }
        }
        public double ScaleX { get; set; } = 45d;
        public double ScaleY { get; set; } = 0.03d;
        public double moveX { get; set; } = 0f;
        public double moveY { get; set; } = 0f;
        public double ShiftX { get; set; } = 0f;
        public double ShiftY { get; set; } = 0f;
        public double Height { get; set; } = 0d;
        public double Wight { get; set; } = 0d;



        public double c = 0.006365;   // 0.006365
        public double kX = 1;
        public double kY = 1;
        public Graph(OpenGL Gl, float x, float y)
        {
            this.Gl = Gl;
            
            markers = new Markers();
            Wight = x;
            Height = y;

            square = new Square(Gl);


        }

        public void Move(float x, float y)
        {
            moveX = x + ShiftX;
            moveY = y - ShiftY;
        }

        public void Move_save(double x, double y)
        {
            ShiftX = -x + moveX;
            ShiftY = y - moveY;
        }

        public double Wheel(double delta)
        {
            double tmpS = Scale;
            if (delta < 0)
            {
                Scale /= 1.1;
            }
            else Scale *= 1.1;

            //Scale *= //delta * 0.004;

            tmpS = Scale / tmpS;
            moveX *= tmpS;//(moveX - lenX )*0.01;
            moveY *= tmpS;
            //MessageBox.Show(Scale.ToString() + " " + ScaleX.ToString() + " " + ScaleY.ToString());
            return Scale;
            
        }
        //public static Graph operator ^( Graph c1, Graph c2)
        //{
        //    return new Graph(new OpenGL(), 1, 2);
        //}

        public void SetAreaX(float x1, float x2)
        {
            if(x1 >= x2)
            {
                MessageBox.Show("Начальная граница должна быть меньше конечной");
                return;
            }

            float l = x2 - x1;
            ScaleX = Wight / l ;

            moveX = -(x1 + x2) * ScaleX / 2;
        }

        public void SetAreaY(float y1, float y2)
        {
            if (y1 >= y2)
            {
                MessageBox.Show("Нижняя граница должна быть меньше верхней");
                return;
            }

            float l = y2 - y1;
            ScaleY = Height/ l;

            moveY = (y1 + y2) * ScaleY / 2;
        }

        public void Magnifier()
        {
            if(square.GetArea() < 0.000001f)
            {
                return;
            }
            square.Normalize();

            SetAreaX(square.Point1X , square.Point2X);
            SetAreaY(square.Point2Y, square.Point1Y);
        }

        public void Test_Draw()
        {

            //moveX = 20000;
            //moveY = -20000;
            //Gl.Translate(moveX * c * kX, -moveY * c * kY, -1f);
            Gl.Translate(moveX, -moveY, -1f);
            double _scale = Scale;
            Gl.Scale(_scale, _scale, 1.0);


            Gl.Color(0f, 1f, 0f);
            Gl.Begin(OpenGL.GL_LINE_STRIP);

            Gl.Vertex(2d, -1000000);
            Gl.Vertex(2d, 1000000);

            Gl.End();

            Gl.Color(0f, 0f, 1f);
            Gl.Begin(OpenGL.GL_LINE_STRIP);

            Gl.Vertex(500d, 0);
            Gl.Vertex(15000d, 1000000);

            Gl.End();

            Gl.Color(1f, 0f, 0f);
            Gl.Begin(OpenGL.GL_LINE_STRIP);

            Gl.Vertex(-1000000, 2); //659
            Gl.Vertex(1000000, 2);

            Gl.End();

            

        }

        public void Draw()
        {
            //if (Selected == null)
            //    return;
            Initialize();

            if (Arr == null)
                return;

            //Gl.Color(1.0f, 0, 0);
            // Двигаем перо вглубь экрана
            //moveX = 100;
            //moveY = 100;
            //Gl.Translate(moveX * c * kX, -moveY * c * kY, -1f);


            //Gl.Color(0f, 1f, 0f);
            //Gl.Begin(OpenGL.GL_LINE_STRIP);

            //Gl.Vertex(1, -1000000);
            //Gl.Vertex(1, 1000000);

            //Gl.End();

            //Gl.Color(1f, 0f, 0f);
            //Gl.Begin(OpenGL.GL_LINE_STRIP);

            //Gl.Vertex(-1000000, 0); //659
            //Gl.Vertex(1000000, 0);

            //Gl.End();


            Draw_grid();

            //Gl.Color(0.7f, 0f, 0.7f);
            //Gl.Begin(OpenGL.GL_LINE_STRIP);
            //Gl.Vertex(10, -1000000d);
            //Gl.Vertex(10, 1000000d);
            //Gl.End();

            Draw_Markers();

            // Указываем цвет вершин
            Gl.Color(0f, 0f, 1f);

            int i = 0;

            double[,] colors = new double[5, 3]
            {
                {0d, 0d, 1d },
                {0d, 1d, 0d },
                {0.8d, 0.8d, 0d },
                {0.8d, 0d, 0.8d },
                {0d, 0.8d, 0.8d },
            };
            



            //for(int j = 0; j < 6; j++)
            //{
            //    colors[j, 0] = 1d;

            //}
            //for (int j = 0; j < Arr.y.Count; j++) //Arr.y.Count
            int t = 0;
            foreach(double[] d in Arr.y)
            {
                Gl.Begin(OpenGL.GL_LINE_STRIP);
                Gl.Color((float)colors[t, 0], (float)colors[t, 1], (float)colors[t, 2], 0.9f);

                for (i = 0; i < Arr.n; i++)
                {
                    //if (i == h) { h = 9999; }
                    //double y = 
                    //Gl.Vertex(i * 0.25 * Scale, Math.Sin(i * 0.25) * Scale);

                    Gl.Vertex(Arr.x[i] , d[i] );

                }
                Gl.End();
                t++;
            }

            if (square.Visible)
                DrawSquare();
            //for (i = 0; i < 100; i++)
            //{
            //    Gl.Vertex((Selected.x[999] + i * 1000 + 1000)* Scale * 45, Selected.y[999] * Scale);
            //}



            Gl.PushMatrix();
            Gl.LoadIdentity();

            Gl.Translate(Wight / 2 + moveX, -Height / 2 - moveY, -1f);

            Gl.Color(0f, 0f, 0f);
            Gl.Begin(OpenGL.GL_LINE_STRIP);

            Gl.Vertex(-Wight / 2 - moveX, 0d); //1276
            Gl.Vertex(Wight / 2 - moveX, 0d);

            Gl.End();

            Gl.Begin(OpenGL.GL_LINE_STRIP);

            Gl.Vertex(0d, -Height / 2 + moveY); //659
            Gl.Vertex(0d, Height / 2 + moveY);

            Gl.End();

            Gl.PopMatrix();


            Gl.Flush();
        }

        private void Initialize()
        {
            Gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            Gl.Enable(OpenGL.GL_DEPTH_TEST);
            //Gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);

            Gl.ClearColor(1, 1, 1, 0);

            Gl.Viewport(0, 0, (int)Wight, (int)Height);

            // Сбрасываем модельно-видовую матрицу
            Gl.MatrixMode(OpenGL.GL_PROJECTION);
            Gl.LoadIdentity();

            Gl.Ortho(0, Wight, -Height, 0, -2, 2);
            Gl.MatrixMode(OpenGL.GL_MODELVIEW);
            Gl.LoadIdentity();

            Gl.Translate(Wight / 2 + moveX, -Height / 2 - moveY, -1f);
            Gl.Scale(ScaleX, ScaleY, 1.0);
        }
        private void Draw_Markers()
        {
            int s = 0;
            Gl.Color(1f, 0f, 0f);

            foreach (double mark in markers.LMarkers)
            {
                s++;
                Gl.Begin(OpenGL.GL_LINE_STRIP);
                //Gl.Color(0f, 0f, 0f);

                Gl.Vertex(mark * 1000 , (-Height / 2 + moveY) / ScaleY);
                Gl.Vertex(mark * 1000 , ( Height / 2 + moveY) / ScaleY);

                //Gl.Vertex(0.001, -Height / 2 + moveY);
                //Gl.Vertex(0.001, Height / 2 + moveY);

                Gl.End();
            }
        }

        private void Draw_grid()
        {
            double intervalY = Height / 6;
            double intervalX = Wight / 7;

            Gl.PushMatrix();
            Gl.LoadIdentity();

            Gl.Translate(Wight / 2, -Height / 2 , -1f);
            // Указываем цвет вершин
            Gl.Color(0.8f, 0.8f, 0.8f);
            for (int j = 1; j <= 7; j++)
            {
                Gl.Begin(OpenGL.GL_LINE_STRIP);
                Gl.Vertex((intervalX * j - Wight * 0.5), -Height / 2);
                Gl.Vertex( (intervalX * j - Wight * 0.5), Height / 2);
                Gl.End();
            }

            for (int j = 1; j <= 6; j++)
            {
                Gl.Begin(OpenGL.GL_LINE_STRIP);
                Gl.Vertex(-Wight / 2,  (-intervalY * j + Height * 0.5) );
                Gl.Vertex(Wight / 2, (-intervalY * j + Height * 0.5) );
                Gl.End();
            }


            Gl.Color(0f, 0f, 0f);
            Gl.DrawText(0, (int)intervalY * 5, 0, 0, 0, "", 11, ((-intervalY * 1 + moveY + Height * 0.5) / ScaleY).ToString("N3"));
            Gl.DrawText(0, (int)intervalY * 4, 0, 0, 0, "", 11, ((-intervalY * 2 + moveY + Height * 0.5) / ScaleY).ToString("N3"));
            Gl.DrawText(0, (int)intervalY * 3, 0, 0, 0, "", 11, ((-intervalY * 3 + moveY + Height * 0.5) / ScaleY).ToString("N3"));
            Gl.DrawText(0, (int)intervalY * 2, 0, 0, 0, "", 11, ((-intervalY * 4 + moveY + Height * 0.5) / ScaleY).ToString("N3"));
            Gl.DrawText(0, (int)intervalY * 1, 0, 0, 0, "", 11, ((-intervalY * 5 + moveY + Height * 0.5) / ScaleY).ToString("N3"));


            Gl.DrawText((int)intervalX * 1, 0, 0, 0, 0, "", 11, ((intervalX * 1 - moveX - Wight * 0.5) / (ScaleX * 1000)).ToString("N3"));
            Gl.DrawText((int)intervalX * 2, 0, 0, 0, 0, "", 11, ((intervalX * 2 - moveX - Wight * 0.5) / (ScaleX * 1000)).ToString("N3"));
            Gl.DrawText((int)intervalX * 3, 0, 0, 0, 0, "", 11, ((intervalX * 3 - moveX - Wight * 0.5) / (ScaleX * 1000)).ToString("N3"));
            Gl.DrawText((int)intervalX * 4, 0, 0, 0, 0, "", 11, ((intervalX * 4 - moveX - Wight * 0.5) / (ScaleX * 1000)).ToString("N3"));
            Gl.DrawText((int)intervalX * 5, 0, 0, 0, 0, "", 11, ((intervalX * 5 - moveX - Wight * 0.5) / (ScaleX * 1000)).ToString("N3"));
            Gl.DrawText((int)intervalX * 6, 0, 0, 0, 0, "", 11, ((intervalX * 6 - moveX - Wight * 0.5) / (ScaleX * 1000)).ToString("N3"));

            Gl.PopMatrix();
        }

        private void DrawSquare()
        {
            if (square.Point1X == 0f && square.Point1Y == 0f && square.Point2X == 0f && square.Point2Y == 0f)
            {
                return;
            }

            Gl.PushMatrix();
            Gl.LoadIdentity();

            Gl.Translate(Wight / 2 + moveX, -Height / 2 - moveY, -1f);

            Gl.Begin(OpenGL.GL_LINE_STRIP);

            Gl.Color(0f, 0f, 0f);

            Gl.Vertex(square.Point1X, square.Point1Y);
            Gl.Vertex(square.Point2X, square.Point1Y);
            Gl.Vertex(square.Point2X, square.Point2Y);
            Gl.Vertex(square.Point1X, square.Point2Y);
            Gl.Vertex(square.Point1X, square.Point1Y);

            Gl.End();

            Gl.PopMatrix();
        }
    }
}
