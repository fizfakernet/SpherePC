using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph
{
    public class Unfolding
    {

        public Components components { get; private set; }

        public int[][] numsPlanes { get; private set; } = new int[21][];
        public List<double[][]> Matrixes { get; private set; }

        //public List<Label> VertLabels { get; private set; }
        public Panel VertLabels { get; private set; }
        public Panel HorizLabels { get; private set; }
        public Panel ColorLabels { get; private set; }
        private double[] levels { get; set; }
        public Unfolding(Components comp, Panel vert, Panel Horiz, Panel Colr)
        {
            components = comp;
            VertLabels = vert;
            HorizLabels = Horiz;
            ColorLabels = Colr;

            //CreateVertLabels(Lockation.Y, Lockation.Y + h, Lockation.X - 25, 8);
        }

        public void CreateVertLabels(int num, bool grad)
        {
            int n = numsPlanes[num - 1].Length;


            double[] arr = getArr(0, VertLabels.Height - 12, n);
            VertLabels.Controls.Clear();
            //Label lab = new Label();
            //lab.Text = "qwer";
            //VertLabels.Controls.Add(lab);
            for (int i = 0; i < n; i++)
            {
                Label lb1 = new Label();
                lb1.Name = "LabelV" + i.ToString();
                lb1.Text = (numsPlanes[num - 1][i]).ToString();
                lb1.Location = new Point(2, (int)arr[n - i - 1]);
                //lb1.Anchor = AnchorStyles.Left;
              //  lb1.ForeColor = System.Drawing.Color.Red;
                VertLabels.Controls.Add(lb1);

            }
        }
        public void CreateHorizLabels()
        {
            int n = 7;

            double[] arr = getArr(0, HorizLabels.Width - 60, n);
            double[] x = getArr(0, components.x.Max() / 1000, n);

            HorizLabels.Controls.Clear();
            for (int i = 0; i < n; i++)
            {
                Label lb1 = new Label();
                lb1.Name = "LabelH" + i.ToString();
                lb1.Text = x[i].ToString("F3");
                lb1.Location = new Point((int)arr[i], 2);
                //lb1.Anchor = AnchorStyles.Left;
                HorizLabels.Controls.Add(lb1);
            }
        }
        public void CreateColorLabels()
        {
            int n = levels.Length / 2;

            double[] arr = getArr(0, ColorLabels.Height - 12, n);
            double[] t = getArr(levels.Min(), levels.Max(), n);
            ColorLabels.Controls.Clear();

            for (int i = 0; i < n; i++)
            {
                Label lb1 = new Label();
                lb1.Name = "LabelC" + i.ToString();
                lb1.Text = t[n - i - 1].ToString("F0");
                lb1.Location = new Point(2, (int)arr[i]);
                //lb1.Anchor = AnchorStyles.Left;
                ColorLabels.Controls.Add(lb1);

            }
        }

        public void UpdateVertLabels()
        {
            int n = VertLabels.Controls.Count;
            double[] arr = getArr(0, VertLabels.Height - 13, n);

            for (int i = 0; i < n; i++)
            {
                Label lb1 = (Label)VertLabels.Controls[i];
                lb1.Location = new Point(2, (int)arr[n - i - 1]);
            }
        }
        public void UpdateHorizLabels()
        {
            int n = HorizLabels.Controls.Count;
            double[] arr = getArr(0, HorizLabels.Width - 60, n);

            for (int i = 0; i < n; i++)
            {
                Label lb1 = (Label)HorizLabels.Controls[i];
                lb1.Location = new Point((int)arr[i], 2);
            }
        }
        public void UpdateColorLabels()
        {
            int n = ColorLabels.Controls.Count;
            double[] arr = getArr(0, ColorLabels.Height - 12, n);

            for (int i = 0; i < n; i++)
            {
                Label lb1 = (Label)ColorLabels.Controls[i];
                lb1.Location = new Point(2, (int)arr[i]);
            }
        }

        public static double[] LineLagrArr(double[] x, double[] y, double[] newX)
        {
            double[] result = new double[newX.Length];
            int k = 0;
            for (int i = 0; i < newX.Length; i++)
            {
                double newX0 = newX[i];
                double xK = x[k + 1];
                while (newX[i] > x[k + 1] && k + 2 < x.Length)
                    k++;

                result[i] = Line(x[k], x[k + 1], newX[i], y[k], y[k + 1]);
                double res = result[i];

            }

            return result;
        }
        public static double Line(double x1, double x2, double x, double y1, double y2)
        {
            return ((y2 - y1) / (x2 - x1)) * (x - x1) + y1;
        }

        public double Line1(double x1, double x2, double x, double y1, double y2)
        {
            return ((y2 - y1) / (x2 - x1)) * (x - x1) + y1;
        }

        public double[][] EditLen(double[] x, double[] y, double[] newX, double[] newY, double[][] z)
        {
            //double[] newX = getArr(0, 2 * Math.PI, w);
            //double[] newY = getArr(0, numsPlanes[num].Length, h);
            int w = newX.Length;
            int h = newY.Length;

            double[,] arr1 = new double[w, y.Length];

            double[] a = new double[x.Length];
            double[] b = new double[x.Length];

            double[] tmp;
            for (int i = 0; i < y.Length; i++)
            {

                for (int j = 0; j < x.Length; j++)
                {


                    a[j] = x[j];
                    b[j] = z[j][i];
                }
                double value = z[0][i];
                tmp = LineLagrArr(a, b, newX);
                for (int k = 0; k < w; k++)
                {
                    arr1[k, i] = tmp[k];
                }
                value = tmp[0];
            }

            double[][] arr2 = new double[w][];
            for (int i = 0; i < w; i++)
                arr2[i] = new double[h];

            a = new double[y.Length];
            b = new double[y.Length];

            for (int j = 0; j < w; j++)
            {
                for (int i = 0; i < y.Length; i++)
                {
                    double value = arr1[j, i];
                    a[i] = y[i];
                    b[i] = arr1[j, i];

                }
                tmp = LineLagrArr(a, b, newY);

                for (int k = 0; k < h; k++)
                {
                    //arr2[j][k] = lagrange(a, b, (short) (y.Length - 1), newY[k]);
                    arr2[j][k] = tmp[k];
                    //double value = arr2[j][k];
                }
            }

            return arr2;
        }

        static double lagrange(double[] x, double[] y, short n, double _x)
        {
            double result = 0.0;

            for (short i = 0; i < n; i++)
            {
                double P = 1.0;

                for (short j = 0; j < n; j++)
                    if (j != i)
                        P *= (_x - x[j]) / (x[i] - x[j]);

                result += P * y[i];
            }

            return result;
        }

        //static double lineLagr()double[,] arr, double[] levels, Bitmap bitmap

        public double[][] GetMatrix(int num, bool grad)
        {
            double[][] matrix = new double[components.n][];

            int s = 0;
            if (grad)
                s = -1;

            string index = "";
            for (int i = 0; i < components.n; i++)
            {
                matrix[i] = new double[numsPlanes[num].Length];
                //MessageBox.Show((numsPlanes[num][0] * 2 - 1 + s).ToString());

                for (int j = 0; j < numsPlanes[num].Length; j++)
                {
                    if (i == 0)
                    {
                        index += (numsPlanes[num][j] * 2 - 1 + s).ToString() + "; ";
                    }
                    double value = matrix[i][j];
                    matrix[i][j] = components.y[numsPlanes[num][j] * 2 - 1 + s][i];  // j = 0 >> d3 ;  j =1 >> d7
                }


            }
            //MessageBox.Show(index);
            return matrix;
        }

        public int getLevel(double value)
        {
            int i, k = 0;
            //MessageBox.Show(levels.Max().ToString() + "  " + matrix.Select(p => p.Max()).Max().ToString());

            for (i = 0; i < levels.Length ; i++)
            {
                //if (value >= 170000)
                //    MessageBox.Show("i = " + i.ToString() + " lev = " + levels[i].ToString() + " k = " + k.ToString());
                if (value < levels[i])
                {
                    k = i;
                    break;
                }
            }

            //double interval = max - min;

            return k;/*(int) ((value-min) * (len - 1) / interval);*/
        }
        public static double[] getArr(double min, double max, int len)
        {
            double[] x = new double[len];

            double interval = max - min;
            for (int i = 0; i < len; i++)
                x[i] = min + interval * i / (len - 1);

            return x;
        }
        public void drawColr(Bitmap colr)
        {
            Graphics Grap = Graphics.FromImage(colr);
            LinearGradientBrush gradBrush = new LinearGradientBrush(new Rectangle(0, 0, colr.Width, colr.Height), Color.Red, Color.Blue, LinearGradientMode.Vertical);
            Grap.FillRectangle(gradBrush, 0, 0, colr.Width, colr.Height); //с градиентной заливкой 
            Grap.Dispose();
        }
        public double[][] matrix { get; private set; }
        double[][] copyMatrix;

        double[] y;
        public void inc(int num, bool grad, Bitmap bitmap)
        {
            num--;

            readNumPlanes();

            matrix = GetMatrix(num, grad);
            levels = getArr(matrix.Select(p => p.Min()).Min(), matrix.Select(p => p.Max()).Max(), 17);

            y = getArr(0, numsPlanes[num].Length, numsPlanes[num].Length);
        }

        public void readNumPlanes()
        {
            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\matrix.txt"))
            {
                string line;
                //MessageBox.Show(Directory.GetCurrentDirectory() + @"\matrix.txt");
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    if(i == 20)
                    {
                        ;
                    }
                    //MessageBox.Show(i.ToString() + ":  " + line);
                    string[] arr = line.Split(' ');
                    numsPlanes[i] = new int[arr.Length];
                    for (int j = 0; j < arr.Length; j++)
                    {
                        //MessageBox.Show(i.ToString() + " " + j.ToString() + ":  " + arr[j]);
                        int t = int.Parse(arr[j]);
                        numsPlanes[i][j] = t;/*line.Split(' ').Select(s => int.Parse(s)).ToArray();*/
                    }
                    i++;

                }

                reader.Close();
            }
        }
        public void draw(int num, bool grad, Bitmap bitmap)
        {

                num--;

                //matrix = GetMatrix(num, grad);

                //MessageBox.Show(getLevel(50000).ToString());
                //MessageBox.Show(getLevel(110000).ToString());
                //MessageBox.Show(getLevel(180000).ToString());

                int r = 0;
                int b = 0;
                int g = 0;

                //MessageBox.Show(levels[0].ToString() + "; " + levels[1] + "; " + levels[2].ToString());
                // MessageBox.Show(" min = " + matrix.Select(p => p.Min()).Min().ToString() + ";  " + matrix.Select(p => p.Max()).Max().ToString());
                //double[,] arr = creatArr(Z, 3000, 8, 0, 2 * Math.PI); //new double[8, 700];



                double[] newX = getArr(0, components.x.Max(), bitmap.Width);
                double[] newY = getArr(0, numsPlanes[num].Length, bitmap.Height);



                copyMatrix = EditLen(components.x, y, newX, newY, matrix);
                //MessageBox.Show(" min = " + matrix.Select(p => p.Min()).Min().ToString() + ";  " + matrix.Select(p => p.Max()).Max().ToString());

                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {

                        if (i == 40 && j == 10)
                        {
                            ;
                        }
                        //for (int k = 0; k < levels.Length - 1; k++)
                        {
                            //double zn = Z(x, y);
                            //double l = levels[k];



                            //if (matrix[i][j] >= 999000)
                            //{
                            //    g = 255;
                            //    r = 0;
                            //    b = 0;
                            //}
                            //else
                            //{
                            int k = getLevel(copyMatrix[i][j]);//getLevel(matrix.Select(p => p.Min()).Min(), matrix.Select(p => p.Max()).Max(), matrix[i][j], 15);

                            //if(k >= 16 || k < 0)
                            // MessageBox.Show(k.ToString());
                            //if (matrix[i][j] >= levels[k])
                            //{

                            //var c = (int)(255 * Math.Abs(zn - levels[k]) / e); //378  269
                            b = 255 - k * 14;
                            r = k * 14;
                            g = 0;
                            //}
                            //}

                        }
                        bitmap.SetPixel(i, bitmap.Height - j - 1, Color.FromArgb(r, g, b));
                    }
                }
                //for (int i = 0; i < 10; i++) bitmap.SetPixel(i + 10, 20, Color.FromArgb(0, 1, 1));
            
        }


    }
}
