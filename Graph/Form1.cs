using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NCalc;
using System.IO;
using System.Text.RegularExpressions;
using System.Media;
using System.Threading;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Graph
{
    public partial class Form1 : Form
    {
        public Graph graph;
        public bool mouse { get; set; } = false;
        public bool initiolize { get; set; } = false;
        public bool openFile { get; set; } = false;

        public bool square { get; set; } = false;
        public string Path { get; set; } = "";

        public RawSphere rawSphere=new RawSphere();
        void menu_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            var menuText = menuItem.Text;
            textBox3.Text = menuItem.Text.ToString();
            button1_Click(sender, e);
        }
        ToolStripMenuItem Comp1 = new System.Windows.Forms.ToolStripMenuItem();
        ToolStripMenuItem Comp2 = new System.Windows.Forms.ToolStripMenuItem();
        ToolStripMenuItem Comp3 = new System.Windows.Forms.ToolStripMenuItem();
        ToolStripMenuItem Comp4 = new System.Windows.Forms.ToolStripMenuItem();
        ToolStripMenuItem Comp5 = new System.Windows.Forms.ToolStripMenuItem();

        public Form1()
        {
            InitializeComponent();
            graph = new Graph(openGLControl1.OpenGL, openGLControl1.Width, openGLControl1.Height);  //712  341

            this.MouseWheel += new MouseEventHandler(OpenGLControl1_MouseWheel);
            initiolize = true;
            //Width = 1450;
            //Height = 800;
            //openGLControl1.Width = 1300;
            //openGLControl1.Height = 650;

            textBox4.Text = "0";
            textBox5.Text = "0";
            Comp1.Click += new EventHandler(menu_Click);
            Comp2.Click += new EventHandler(menu_Click);
            Comp3.Click += new EventHandler(menu_Click);
            Comp4.Click += new EventHandler(menu_Click);
            Comp5.Click += new EventHandler(menu_Click);
            
            //double[] a = Unfolding.getArr(0, 10, 7);


            //string s = "";
            //for(int i = 0; i < a.Length; i++)
            //{
            //    s += a[i] + ";  ";
            //}

            //MessageBox.Show(s);
            //for (int i = 0; i < graph.Selected.n; i++)
            //{
            //    //textBox4.Text += list.str2[i][5] + "  ";
            //    textBox4.Text += graph.Selected.x[i].ToString() + "  " ;
            //}
            // MessageBox.Show((new Expression("2.5*2").Evaluate().ToString()));
        }

        //arr.x = list.x;
        //arr.y = new List<double[]>();
        //int count = list.y.Count;
        //List<double[]> y = new List<double[]>();
        //graph.NumD = new List<int>();
        //foreach (string s in str)
        //{
        //    if(int.Parse(s) > list.y.Count || int.Parse(s) < 0 )
        //    {
        //        MessageBox.Show("Указаны неккоректные номера датчиков, они должны находится от 1 до " + list.y.Count.ToString());
        //        return;
        //    }

        //    graph.NumD.Add(int.Parse(s) - 1);

        //    //y.Add(list.y[int.Parse(s) - 1]);
        //}

        //list.y = y;
        //string pattern = "d";
        //string replacement = "";
        //string input = "d32";
        //string result = Regex.Replace(input, pattern, replacement);
        //MessageBox.Show(result);
        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            //graph.Selected = new Component();
            //graph.Selected.n = 200;
            //graph.Selected.Create();
            //int x = 1;
            graph.Draw();


        }

        Components comp;
        public Components copyComp;

        List<double[]> d;
    

        public void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(UpdateGraph);
            thread.Start();

            //UpdateGraph();
        }


        Label[] labels1 = new Label[5];
        LinkedList<Label> labelsTire = new LinkedList<Label>();
        LinkedList<Label> labelsExp = new LinkedList<Label>();

        LinkedListNode<Label> nodeTire;
        LinkedListNode<Label> nodeExp;

        public void UpdateGraph()
        {
            try
            {
                //graph.Scale = float.Parse(textBox1.Text);
                //MessageBox.Show(graph.Scale.ToString());
            }
            catch
            {
                MessageBox.Show("Введите корректный масштаб (через запятую)");
                return;
            }


            // try
            {
                //string path = ;

                if (comp == null)
                    return;



                Action action = () =>
                {
                    copyComp = (Components)comp.Clone();
                    //MessageBox.Show(comp.MedS.ToString());
                    Processing.Comp = copyComp;
                    Processing.Update();

                    //MessageBox.Show(copyComp.MedS.ToString());

                    d = copyComp.y.Select(x => x).ToList();

                    copyComp.y.Clear();

                    label25.Text = "";
                    labelsTire.AddLast(label25);

                    label16.Text = "";
                    labelsExp.AddLast(label16);

                    label24.Text = "";
                    labelsTire.AddLast(label24);

                    label17.Text = "";
                    labelsExp.AddLast(label17);

                    label23.Text = "";
                    labelsTire.AddLast(label23);

                    label18.Text = "";
                    labelsExp.AddLast(label18);

                    label22.Text = "";
                    labelsTire.AddLast(label22);

                    label19.Text = "";
                    labelsExp.AddLast(label19);

                    label21.Text = "";
                    labelsTire.AddLast(label21);

                    label20.Text = "";
                    labelsExp.AddLast(label20);

                    nodeTire = labelsTire.First;
                    nodeExp = labelsExp.First;
                };



                Invoke(action);
                    //Path = @"D:\Загрузки\Realnye_fayly\Реальные файлы\120419213621.da";

               //Thread.Sleep(5000);



                //string[] str = comboBox1.Text.Split(',');
                //if (str.Length > 5)
                //{
                //    MessageBox.Show("Укажите не более 5 компонент");
                //    return;
                //}



                string[] str = textBox3.Text.Split(';');
                double[,] colors = new double[5, 3]
                {
                    {0d, 0d, 1d },
                    {0d, 1d, 0d },
                    {0.8d, 0.8d, 0d },
                    {0.8d, 0d, 0.8d },
                    {0d, 0.8d, 0.8d },
                };


                //arr.
                //Components arr = new Components(list.n);


                //arr.y = new List<double[]>();
                List<double[]> Y = new List<double[]>();
                string[] S = new String[str.Length];
                int j = 0;
                foreach (string s in str)
                {

                    string res = "";
                    Regex regex = new Regex(@"[a-z](\d*)");
                    MatchCollection matches = regex.Matches(s);
                    //list.y.Add(new double[list.n]);
                    double[] mas = new double[comp.n];

                    int num;

                    //arr.x = new double[list.n];
                    for (int i = 0; i < comp.n; i++)
                    {
                        res = s;
                        foreach (Match match in matches)
                        {
                            //if (j == 10)


                            if (match.Value.LastIndexOf("s") != -1)
                            {
                                res = res.Replace(match.Value, d[2 * int.Parse(match.Value.Replace("s", "")) - 1][i].ToString());
                            }
                            if (match.Value.LastIndexOf("g") != -1)
                            {
                                res = res.Replace(match.Value, d[2 * int.Parse(match.Value.Replace("g", "")) - 2][i].ToString());
                            }

                            if (match.Value.LastIndexOf("a") != -1)
                            {
                                num = int.Parse(match.Value.Replace("a", ""));
                                res = res.Replace(match.Value, (d[2 * num - 1][i]  + 2 * d[2 * num - 2][i]).ToString());
                            }
                            if (match.Value.LastIndexOf("b") != -1)
                            {
                                num = int.Parse(match.Value.Replace("b", ""));
                                res = res.Replace(match.Value, (d[2 * num - 1][i] - 2 * d[2 * num - 2][i]).ToString());
                            }


                            if (match.Value.LastIndexOf("e") != -1)
                            {
                                res = res.Replace(match.Value, d[int.Parse(match.Value.Replace("e", "")) + 84][i].ToString());
                            }
                        }

                        res = res.Replace(',', '.');

                        Expression expr = new Expression(res);

                        mas[i] = Double.Parse(expr.Evaluate().ToString());
                        //arr.x[i] = list.List_Components[0].x[i];
                        //arr.y[i] = list.List_Components[int.Parse(comboBox1.Text)].y[i];//Double.Parse(expr.Evaluate().ToString());
                    }

                    S[j] = s;
                    Y.Add(mas);

                    j++;
                }
                //if (checkBox2.Checked)
                //    arr.Derivative();


                //if (checkBox3.Checked)
                //{
                //    arr.Derivative();
                //    arr.Derivative();
                //}
                //try
                //{
                //    graph.SetAreaX(float.Parse(textBox4.Text) * 1000, float.Parse(textBox5.Text) * 1000);
                //    textBox6.Text = graph.ScaleX.ToString();
                //}
                //catch
                //{
                //    MessageBox.Show("Указаны неккоректные границы");
                //}
                //try
                //{                 //graph.Expr = ;                     //Selected = list.List_Components[int.Parse(comboBox1.Text)];
                action = () =>
                {

                    for(int i = 0; i < str.Length; i++)
                    {
                        nodeTire.Value.Text = "—";
                        nodeTire = nodeTire.Next;

                        nodeExp.Value.Text = S[i];
                        nodeExp = nodeExp.Next;
                    }

                    //MessageBox.Show(Y.Count.ToString());

                    copyComp.y = Y;

                    //d = new List<double[]>();
                    d = copyComp.y.Select(x => x.Select(t => t).ToArray()).ToList();
                    //for (int i = 0; i < copyComp.y.Count; i++)
                    //{
                    //    d.Add(copyComp.y[i]);
                    //}

                    if (copyComp != null && checkBox4.Checked)
                    {

                        float step = float.Parse(textBox7.Text) * 1000f / copyComp.n;
                        for (int i = 0; i < copyComp.n; i++)
                        {
                            copyComp.x[i] = i * step;
                        }
                        //graph.ScaleX = 25;

                    }
                    //} 
                    //else graph.ScaleX = 25000;



                    //copyComp = (Components) comp.Clone();



                    //copyComp.LongitudetBegin = "sjscoidjcwodj";
                    //copyComp.y[0] = copyComp.y[0].Select(x => x+100000d).ToArray();
                    //copyComp.DatetimeBegin = copyComp.DatetimeBegin.AddYears(10);

                    graph.Arr = copyComp;
                    Unfold = new Unfolding(comp, panel1, panel2, panel3);
                

                    //Unfold.readNumPlanes();
                    //for(int i = 0; i < Unfold.VertLabels.Count; i++)
                    //{
                    //    panel1.Controls.Add(Unfold.VertLabels[i]);
                    //}


                    if (openFile)
                    {
                        textBox4.Text = "0";
                        textBox5.Text = (graph.Arr.x.Max() / 1000).ToString("N4");

                        //int max2 = users.Max(n => n.Age);

                        //List<List<int>> a = new List<List<int>>
                        //{
                        //    new  List<int>{ 1, 6, 3 },
                        //    new List<int> { 3, 4},
                        //    new List<int> { 5, 3}
                        //}; 

                        //MessageBox.Show(max.ToString());

                        openFile = false;
                    }

                    textBox6.Text = graph.ScaleX.ToString("N3");

                    try
                    {

                        graph.SetAreaX(float.Parse(textBox4.Text) * 1000, float.Parse(textBox5.Text) * 1000);
                        textBox6.Text = graph.ScaleX.ToString();

                        float min = (float)graph.Arr.y.Select(p => p.Min()).Min();
                        float max = (float)graph.Arr.y.Select(p => p.Max()).Max();

                        //float min = (float)graph.Arr.y.Where((p, i) => graph.NumD.All((n => i == n))).Select(p => p.Min()).Min();
                        //float max = (float)graph.Arr.y.Where((p, i) => graph.NumD.All((n => i == n))).Select(p => p.Max()).Max();

                        graph.SetAreaY(min, max);



                    }
                    catch
                    {
                        MessageBox.Show("Указаны некоректные границы");
                    }

                    graph.kX = (double)1300 / openGLControl1.Width;
                    graph.kY = (double)650 / openGLControl1.Height;

                    graph.Draw();


                    //MessageBox.Show(d[0][10].ToString());
                    //copyComp.y[0][10] = 123.0;
                    //MessageBox.Show(d[0][10].ToString());

                    //MessageBox.Show(graph.Scale.ToString() + "  " + graph.ScaleX.ToString() + "  " + graph.ScaleY.ToString());
                };
                Invoke(action);
            }
            //catch
            //{
            //    MessageBox.Show("Ошибка при чтение файла или выражения");
            //}

            
        }


            private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse ^ square)
            {
                graph.Move(e.X, e.Y);
                textBox4.Text = ((-graph.moveX - graph.Wight * 0.5) / (graph.ScaleX * 1000)).ToString("N4");
                textBox5.Text = ((-graph.moveX + graph.Wight * 0.5) / (graph.ScaleX * 1000)).ToString("N4");

            }

            if(square)
            {
                graph.square.Point2X = (float) (e.X - graph.moveX - graph.Wight * 0.5);//e.X.ToString(); //((float)e.X / openGLControl1.Width).ToString();
                graph.square.Point2Y = (float) (-e.Y + graph.moveY + graph.Height * 0.5); //((float)e.Y / openGLControl1.Width).ToString(); 
                 
            }

            label1.Text = ((e.X - graph.moveX - graph.Wight*0.5) / ( graph.ScaleX * 1000)).ToString("N3");//e.X.ToString(); //((float)e.X / openGLControl1.Width).ToString();
            label2.Text = ((-e.Y + graph.moveY + graph.Height * 0.5) / ( graph.ScaleY * 1000)).ToString("N3"); //((float)e.Y / openGLControl1.Width).ToString(); 


        }

        public float Ex { get; set; } = 0f;
        public float Ey { get; set; } = 0f;

        private void openGLControl1_MouseUp(object sender, MouseEventArgs e)
        {
            mouse = false;

            if (square)
            {
                graph.square.Point1X = graph.square.Point1X / (float)graph.ScaleX;
                graph.square.Point1Y = graph.square.Point1Y / (float)graph.ScaleY;

                graph.square.Point2X = (float)((e.X - graph.moveX - graph.Wight * 0.5) / graph.ScaleX);
                graph.square.Point2Y = (float)((-e.Y + graph.moveY + graph.Height * 0.5) / graph.ScaleY);
                //MessageBox.Show(graph.square.Point2X.ToString() + "  " + graph.square.Point2Y.ToString());

                graph.Magnifier();

                square = false;
            }

            graph.square.Zeroize();
            

            //label2.Text = graph.moveY.ToString();


        }

        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse = true;

            if (checkBox5.Checked)
            {
                if (e.Button == MouseButtons.Left)
                {
                    square = true;
                    graph.square.Visible = true;

                    graph.square.Point1X = (float)((e.X - graph.Wight * 0.5 - graph.moveX));
                    graph.square.Point1Y = (float)((-e.Y + graph.moveY + graph.Height * 0.5)); //((float)e.Y / openGLControl1.Width).ToString(); 
                    graph.square.Point2X = (float)((e.X - graph.Wight * 0.5 - graph.moveX) );
                    graph.square.Point2Y = (float)((-e.Y + graph.moveY + graph.Height * 0.5)); //((float)e.Y / openGLControl1.Width).ToString(); 
                }
                else
                {
                    graph.square.Point1X = (float)((-graph.moveX - graph.Wight * 0.5) / graph.ScaleX);
                    graph.square.Point1Y = (float)((graph.moveY + graph.Height * 0.5) / graph.ScaleY); //((float)e.Y / openGLControl1.Width).ToString(); 
                    graph.square.Point2X = (float)((-graph.moveX + graph.Wight * 0.5 ) / graph.ScaleX);
                    graph.square.Point2Y = (float)((graph.moveY - graph.Height * 0.5) / graph.ScaleY); //((float)e.Y / openGLControl1.Width).ToString(); 

                    //MessageBox.Show((graph.square.Point1X / 1000).ToString() + " " + (graph.square.Point1Y / (graph.Scale * graph.ScaleY * 1000)).ToString() + " | " + (graph.square.Point2X / 1000).ToString() + "  " + (graph.square.Point2Y / (graph.Scale * graph.ScaleY * 1000)).ToString());
                    float l = graph.square.Point2X - graph.square.Point1X;
                    graph.square.Point1X -= l * 0.25f;
                    graph.square.Point2X += l * 0.25f;

                    float h = graph.square.Point1Y - graph.square.Point2Y;

                    graph.square.Point1Y += h * 0.25f;
                    graph.square.Point2Y -= h * 0.25f;

                    //MessageBox.Show((graph.square.Point1X/1000).ToString() + " " + (graph.square.Point1Y / (graph.Scale * graph.ScaleY * 1000)).ToString() + " | " + (graph.square.Point2X/1000).ToString() + "  " + (graph.square.Point2Y / (graph.Scale * graph.ScaleY * 1000)).ToString());

                    //graph.SetAreaX(graph.square.Point1X, graph.square.Point2X);

                    graph.square.Visible = false;
                    graph.Magnifier();
                }
                
            }

            graph.Move_save(e.X, e.Y);


            if(checkBox1.Checked && e.Button == MouseButtons.Left)
            {
                graph.markers.Add((e.X - graph.Wight * 0.5 - graph.moveX) / graph.ScaleX / 1000);
            }
            if (checkBox1.Checked && e.Button == MouseButtons.Right)
            {
                graph.markers.Del((e.X - graph.Wight * 0.5 - graph.moveX) / graph.ScaleX / 1000);
            }


            
            //textBox2.Text = graph.ShiftY.ToString();
            //textBox3.Text = Ey.ToString();

            //label1.Text = e.Y.ToString(); 
            //label2.Text = graph.moveY.ToString();
            //((e.X - graph.moveX - graph.Wight * 0.5) * graph.c / (graph.Scale * 45 * 1000)).ToString("N3");

            //graph.ShiftX -= 2*e.X;
            //graph.ShiftY += Ey*2;
        }

        private void OpenGLControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            textBox1.Text = graph.Wheel(e.Delta).ToString();
            textBox4.Text = ((-graph.moveX - graph.Wight * 0.5) / (graph.ScaleX * 1000)).ToString("N4");
            textBox5.Text = ((-graph.moveX + graph.Wight * 0.5) / (graph.ScaleX * 1000)).ToString("N4");

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openGLControl1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                textBox2.Text = openFileDialog1.FileName;
                sr.Close();
                button1_Click(sender, e);
            }

        }

        private void textBox3_MouseEnter(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(sender, e); 
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(sender, e);
            }
        }

        private void openGLControl1_SizeChanged(object sender, EventArgs e)
        {
            if (initiolize)
            {
                textBox4.Text = ((-graph.moveX - graph.Wight * 0.5) / (graph.ScaleX * 1000)).ToString("N4");
                textBox5.Text = ((-graph.moveX + graph.Wight * 0.5) / (graph.ScaleX * 1000)).ToString("N4");


                //graph.kX = (double)650 / openGLControl1.Height;
                //graph.kY = (double) 650 / openGLControl1.Height;
                graph.Wight = openGLControl1.Width;
                graph.Height = openGLControl1.Height;

                //double intervalX = graph.Wight / 7;
                //double intervalY = graph.Height / 6;
                //label9.Text = ((intervalX * 1 - graph.moveX - graph.Wight * 0.5) * graph.c * graph.kX).ToString() ;
                //label10.Text = ((-intervalY * 1 + graph.moveY + graph.Height * 0.5) * graph.c * graph.kY).ToString();
            //(-intervalY * j + moveY + Height * 0.5) * c * kY
            }
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                openFile = true;
                button1_Click(sender, e);
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(sender, e);

                //graph.moveX = Double.Parse(textBox4.Text) * graph.Scale * 45 / (graph.c * graph.kX) - graph.moveX;
                //- moveX - Wight * 0.5) * c * kX / (Scale * 45)).ToString("N2"))
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(graph.moveX.ToString());

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label11_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.Clear(this.BackColor);
            //e.Graphics.RotateTransform(-90);
            //SizeF textSize = e.Graphics.MeasureString(label1.Text, label1.Font);
            //label1.Width = (int)textSize.Height + 2;
            //label1.Height = (int)textSize.Width + 2;
            //e.Graphics.TranslateTransform(-label1.Height / 2, label1.Width / 2);
            //e.Graphics.DrawString(label1.Text, label1.Font, Brushes.Black, -(textSize.Width / 2), -(textSize.Height / 2));
            
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(sender, e);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            openFile = true;

            button1_Click(sender, e);

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(sender, e);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(graph.moveY.ToString() + "  " + graph.ScaleY.ToString());
            graph.SetAreaY(float.Parse(textBox4.Text) * 1000, float.Parse(textBox5.Text) * 1000);
            MessageBox.Show(graph.moveY.ToString() + "  " + graph.ScaleY.ToString());


            //Components list = new Components(1);
            //list.readBinFile(textBox2.Text);
            //MessageBox.Show(list.DatetimeBegin.ToString());
            //MessageBox.Show(list.DatetimeEnd.ToString());
            //MessageBox.Show(list.Sec.ToString());
            //MessageBox.Show(list.y[0][0].ToString() + " " + list.y[0][1].ToString() + " " + list.y[0][2].ToString());
            //for (int i = 0; i < 10; i++)
            //    MessageBox.Show(list.x[i].ToString() + "  " + list.y[0][i].ToString() + "  " + list.y[1][i].ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        public void open_File(string path)
        {
            this.Path = path;
            openFile = true;

            if (Path[Path.Length - 3] == '.' && Path[Path.Length - 2] == 'd' && Path[Path.Length - 1] == 'a')
            {
                comp = new Components(1);
                comp.readBinFile(Path);

                //if(!checkBox4.Checked)
                //    graph.ScaleX = 25000;
            }
            else
            {
                comp = new Components(path);
                //               comp = new Components(textBox2.Text); //D:\Загрузки\Prokhod_2_15_datchikov.txt
                //graph.ScaleX = 25;
            }
        }

        [STAThread]
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Все файлы (*.*)|*.*|Двоичный файл (*.da)|*.da|Текстовый файл (*.txt)|*.txt";
            openFileDialog1.FileName = Path;
            //checkBox1.Text = Path;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.open_File(openFileDialog1.FileName);
                /*
                Path = openFileDialog1.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                textBox2.Text = openFileDialog1.FileName;
                sr.Close();

                openFile = true;

                if (Path[Path.Length - 3] == '.' && Path[Path.Length - 2] == 'd' && Path[Path.Length - 1] == 'a')
                {
                    comp = new Components(1);
                    comp.readBinFile(Path);

                    //if(!checkBox4.Checked)
                    //    graph.ScaleX = 25000;
                }
                else
                {
                    comp = new Components(textBox2.Text); //D:\Загрузки\Prokhod_2_15_datchikov.txt
                    //graph.ScaleX = 25;
                }
                //copyComp = (Components) comp.Clone();
                //Processing.Comp = copyComp;*/
                button1_Click(sender, e);
            }
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void открытьПапкуСФайомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Path != "")
                {
                    System.Diagnostics.Process.Start("explorer.exe", @"/select, " + Path);
                }
                else MessageBox.Show("Файл не был выбран");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при открытии папки");
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сохранитьМаркерыToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (graph.markers.LMarkers.Count == 0)
            {
                MessageBox.Show("Нет созданных маркеров");
                return;
            }

            saveFileDialog1.FileName = "markers.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            

            try
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.Default))
                {
                    int n = graph.markers.LMarkers.Count;
                    for (int i = 0; i < n; i++)
                    {
                        writer.WriteLine(graph.markers.LMarkers[i].ToString("N3"));
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка записи в файл");
            }
        }

        private void заграузитьМаркерыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Все файлы (*.*)|*.*|Текстовый файл (*.txt)|*.txt";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            try
            {
                using (StreamReader reader = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default))
                {
                    string line;

                    int i = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        graph.markers.LMarkers.Add(double.Parse(line));
                        i++;
                    }
                    if (i == 0)
                    {
                        MessageBox.Show("В файле нет записей");
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка чтения файла");
            }
        }



        private void сведеияОФайлеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(graph.Arr == null)
            {
                MessageBox.Show("Не открыт файл");
                return;
            }
            information form = new information(graph.Arr);
            if(checkBox4.Checked)
            {
                form.SetX(double.Parse(textBox7.Text));
            }
            form.ShowDialog();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            openFile = true;
            button1_Click(sender, e);


        }

        private void конвертироватьВtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = System.IO.Path.ChangeExtension(Path.Substring(Path.LastIndexOf("\\")+1), ".txt");

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            List<double[]> tmpY = graph.Arr.y;

            comp.ConvertTo_txt(saveFileDialog1.FileName);

            graph.Arr.y = tmpY;
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    graph.ScaleX = double.Parse(textBox6.Text) * 1000;
                }
            }
            catch
            {
                MessageBox.Show("Некорректные данные");
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(sender, e);
            }
        }

        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFile = true;
            button1_Click(sender, e);
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                openFile = true;
                button1_Click(sender, e);
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }


        //private void addFunc(object sender, Form form)
        //{
        //    if (((ToolStripMenuItem)sender).Checked)
        //    {
        //        Processing.DelFunc("k", comp);
        //        button1_Click(sender, new EventArgs());
        //        ((ToolStripMenuItem)sender).Checked = false;
        //    }
        //    else
        //    {
        //        ((ToolStripMenuItem)sender).Checked = true;
        //        formSKO sko = new formSKO(this);
        //        sko.Show();
        //    }
        //}
        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                copyComp.y = d.Select(x => x.Select(t => t).ToArray()).ToList();
                Processing.DelFunc("b", copyComp);
                //button1_Click(sender, e);
                xToolStripMenuItem.Checked = false;
            }
            else
            { 
                formSKO sko = new formSKO(this);
                sko.Show();
            }
            //+= button1_Click(sender, e); 

        }
        
        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                copyComp.y = d.Select(x => x.Select(t => t).ToArray()).ToList(); ;
                Processing.DelFunc("e", copyComp);
                //button1_Click(sender, e);
                cToolStripMenuItem.Checked = false;
            }
            else
            {

                //cToolStripMenuItem.Checked = true;
                LPF sko = new LPF(this);
                sko.Show();
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        public Unfolding Unfold { get; set; }

          private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            e.SuppressKeyPress = true;
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Bitmap colr = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            try
            {
                int num = int.Parse(textBox8.Text);

                Unfold.inc(num, checkBox6.Checked, bitmap);
                Unfold.draw(num, checkBox6.Checked, bitmap);
                Unfold.drawColr(colr);
                Unfold.CreateVertLabels(num, checkBox6.Checked);
                Unfold.CreateHorizLabels();
                Unfold.CreateColorLabels();
                this.разностиКомпонентToolStripMenuItem.DropDownItems.Clear();








                if (num == 1)
                {
                    bool grad = checkBox6.Checked;
                    if (!grad) { Comp1.Text = "d8-d17"; } else { Comp1.Text = "g8-g17"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp1});
                    if (!grad) { Comp2.Text = "d5-d21"; } else { Comp2.Text = "g5-g21"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp2});
                    if (!grad) { Comp3.Text = "d1-d41"; } else { Comp3.Text = "g1-g41"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp3});
                    if (!grad) { Comp4.Text = "d7-d19"; } else { Comp4.Text = "g7-g19"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp4});
                }

                if (num == 2)
                {
                    bool grad = checkBox6.Checked;
                    if (!grad) { Comp1.Text = "d5-d21"; } else { Comp1.Text = "g5-g21"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp1});
                    if (!grad) { Comp2.Text = "d6-d27"; } else { Comp2.Text = "g6-g27"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp2});
                    if (!grad) { Comp3.Text = "d14-d26"; } else { Comp3.Text = "g14-g26"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp3});
                    if (!grad) { Comp4.Text = "d37-d22"; } else { Comp4.Text = "g37-g22"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp4});
                }

                if (num == 3)
                {
                    bool grad = checkBox6.Checked;
                    if (!grad) { Comp1.Text = "d40-d18"; } else { Comp1.Text = "g40-g18"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp1});
                    if (!grad) { Comp2.Text = "d4-d29"; } else { Comp2.Text = "g4-g29"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp2});
                    if (!grad) { Comp3.Text = "d2-d30"; } else { Comp3.Text = "g2-g30"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp3});
                    if (!grad) { Comp4.Text = "d5-d21"; } else { Comp4.Text = "g5-g21"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp4});
                }

                if (num == 4)
                {
                    bool grad = checkBox6.Checked;
                    if (!grad) { Comp1.Text = "d5-d21"; } else { Comp1.Text = "g5-g21"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp1});
                    if (!grad) { Comp2.Text = "d24-d35"; } else { Comp2.Text = "g24-g35"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp2});
                    if (!grad) { Comp3.Text = "d25-d16"; } else { Comp3.Text = "g25-g16"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp3});
                    if (!grad) { Comp4.Text = "d3-d23"; } else { Comp4.Text = "g3-g23"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp4});
                }

                if (num == 5)
                {
                    bool grad = checkBox6.Checked;
                    if (!grad) { Comp1.Text = "d5-d21"; } else { Comp1.Text = "g5-g21"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp1});
                    if (!grad) { Comp2.Text = "d11-d38"; } else { Comp2.Text = "g11-g38"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp2});
                    if (!grad) { Comp3.Text = "d20-d39"; } else { Comp3.Text = "g20-g39"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp3});
                    if (!grad) { Comp4.Text = "d12-d28"; } else { Comp4.Text = "g12-g28"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp4});
                }

                if (num == 6)
                {
                    bool grad = checkBox6.Checked;
                    if (!grad) { Comp1.Text = "d8-d17"; } else { Comp1.Text = "g8-g17"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp1});
                    if (!grad) { Comp2.Text = "d16-d25"; } else { Comp2.Text = "g16-g25"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp2});
                    if (!grad) { Comp3.Text = "d14-d26"; } else { Comp3.Text = "g14-g26"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp3});
                    if (!grad) { Comp4.Text = "d10-d31"; } else { Comp4.Text = "g10-g31"; }
                    this.разностиКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Comp4});
                }

                pictureBox1.Image = bitmap;
                pictureBox2.Image = colr;
            } catch {  }
       
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Bitmap colr = new Bitmap(pictureBox2.Width, pictureBox2.Height);

           // try
           // {
                int num = int.Parse(textBox8.Text);


                Unfold.inc(num, checkBox6.Checked, bitmap);
                Unfold.draw(num, checkBox6.Checked, bitmap);
                Unfold.drawColr(colr);
                Unfold.CreateVertLabels(num, checkBox6.Checked);
                Unfold.CreateHorizLabels();
                Unfold.CreateColorLabels();
           // }
            /*catch
            {
                MessageBox.Show("Ошибка при построении развёртки");
            }*/
            //Unfold.CreateHorizLabels();
            //for(int i = 0; i < 5; i++) bitmap.SetPixel(700 + i, bitmap.Height - 269 - 1, Color.FromArgb(0, 1, 1));

            pictureBox1.Image = bitmap;
            pictureBox2.Image = colr;
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            if (graph.Arr == null)
                return;

            if (Unfold.matrix == null)
                return;

            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            int num = int.Parse(textBox8.Text);

            Unfold.draw(num, checkBox6.Checked, bitmap);

            //for(int i = 0; i < 5; i++) bitmap.SetPixel(700 + i, bitmap.Height - 269 - 1, Color.FromArgb(0, 1, 1));

            pictureBox1.Image = bitmap;

        }

        private void tabPage2_SizeChanged(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            if (graph.Arr == null)
                return;

            Unfold.UpdateVertLabels();
        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            if (graph.Arr == null)
                return;

            Unfold.UpdateHorizLabels();
        }

        private void panel3_SizeChanged(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            if (graph.Arr == null)
                return;

            Unfold.UpdateColorLabels();
        }

        private void pictureBox2_SizeChanged(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            if (graph.Arr == null)
                return;

            Bitmap colr = new Bitmap(pictureBox2.Width, pictureBox2.Height);

            Unfold.drawColr(colr);

        }

        private void pictureBox2_SizeChanged_1(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            if (graph.Arr == null)
                return;

            Bitmap colr = new Bitmap(pictureBox2.Width, pictureBox2.Height);

            Unfold.drawColr(colr);
            pictureBox2.Image = colr;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Unfold.readNumPlanes();
            InfUnfold infUnfold = new InfUnfold(comp, Unfold.numsPlanes);
            infUnfold.Show();
        }

        private void параметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_ImeModeChanged(object sender, EventArgs e)
        {

        }

        private void фильтрЧебышеваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                copyComp.y = d.Select(x => x.Select(t => t).ToArray()).ToList();
                Processing.DelFunc("c", copyComp);
                //button1_Click(sender, e);
                фильтрЧебышеваToolStripMenuItem.Checked = false;
            }
            else
            {
                FormChebyshev Cheb = new FormChebyshev(this);
                Cheb.Show();
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHelp help = new FormHelp();
            help.Show();
        }

        private void КоннектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPropertesServer propServ = new FormPropertesServer();
            propServ.Show();
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ОтправитьИсходныйФайлНаСерверToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tempFileName = "tmp.f";
            File.Copy(this.Path, Properties.Settings.Default.shareForTempFiles+"tmp.f", true);


            String serv = Properties.Settings.Default.server + "/upload.php";
            rawSphere.convert(this.comp);


            var request = (HttpWebRequest)WebRequest.Create(serv);

            var postData = "submit=True&file_is_raw=True";
            postData += "&login=" + Properties.Settings.Default.login;
            postData += "&password=" + Properties.Settings.Default.password;
            //           postData += "&userfile=" + this.Path;
            //            postData += "&userfile=" + File.ReadAllBytes(this.Path);
            postData += "&tempfilename=" + tempFileName;
            postData += "&jsonstring=" + JsonConvert.SerializeObject(rawSphere);
            Console.WriteLine(JsonConvert.SerializeObject(rawSphere));
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
 //           request.ContentType = "multipart/form-data";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            MessageBox.Show(responseString+"  "+ JsonConvert.SerializeObject(rawSphere));
            
        }

        private void СкачатьФайлССервераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGetFile formGetFile = new FormGetFile(this);
            formGetFile.Show();
        }
    }

}
