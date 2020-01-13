using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph
{
    public partial class formSKO : Form
    {
        public formSKO(Form1 form)
        {
            InitializeComponent();
            form1 = form;

            textBox1.Text = Properties.Settings.Default.sko1;
            textBox2.Text = Properties.Settings.Default.sko2;
        }
        Form1 form1;
        bool ok = false;
        public double Coef { get; private set; } = 1d;

        
        double[] filter(double[] x, double[] coeff_b, double[] coeff_a)
        {
            int len_x = x.Length;
            int len_b = coeff_b.Length;
            int len_a = coeff_a.Length;

            double[] zi = new double[len_b];
            double[] filter_x = new double[len_x];


            if (len_a == 1)
            {
                for (int m = 0; m < len_x; m++)
                {
                    filter_x[m] = coeff_b[0] * x[m] + zi[0];
                    for (int i = 1; i < len_b; i++)
                    {
                        zi[i - 1] = coeff_b[i] * x[m] + zi[i];//-coeff_a[i]*filter_x[m];
                    }
                }
            }
            else
            {
                for (int m = 0; m < len_x; m++)
                {
                    filter_x[m] = coeff_b[0] * x[m] + zi[0];
                    for (int i = 1; i < len_b; i++)
                    {
                        zi[i - 1] = coeff_b[i] * x[m] + zi[i] - coeff_a[i] * filter_x[m];
                    }
                }
            }

            return filter_x;
        }




        public Components K(double k, Components comp)
        {
            if (this.radioButton1.Checked)
            {

                /* calculate the d coefficients */
                double[] dcof = Formulas.dcof_bwlp(4, comp.MedS * 1000 / k);

                double sf = Formulas.sf_bwlp(4, comp.MedS * 1000 / k); /* scaling factor for the c coefficients */


                /* calculate the c coefficients */
                int[] ccof = Formulas.ccof_bwlp(4);
                double[] ccof2 = new double[ccof.Length];
                for (int ys = 0; ys < ccof.Length; ys++)
                {
                    ccof2[ys] = sf * ccof[ys];
                }





                for (int i = 0; i < comp.y.Count; i++)
                {
                    comp.y[i] = filter(comp.y[i], ccof2, dcof);

                }
            }
            else if (this.radioButton2.Checked)
            {
                /* calculate the d coefficients */
                double[] dcof = Formulas.dcof_bwlp(4, comp.MedS * 1000 / k);

                double sf = Formulas.sf_bwhp(4, comp.MedS * 1000 / k); /* scaling factor for the c coefficients */




                /* calculate the c coefficients */
                int[] ccof = Formulas.ccof_bwhp(4);
                double[] ccof2 = new double[ccof.Length];
                for (int ys = 0; ys < ccof.Length; ys++)
                {
                    ccof2[ys] = sf * ccof[ys];
                }

                for (int i = 0; i < comp.y.Count; i++)
                {
                    comp.y[i] = filter(comp.y[i], ccof2, dcof);

                }
            }
            else if (this.radioButton3.Checked)
            {
                /* calculate the d coefficients */
                double[] dcof = Formulas.dcof_bwbp(4, (comp.MedS * 1000 / Convert.ToDouble(this.textBox2.Text)), (comp.MedS * 1000 / Convert.ToDouble(this.textBox1.Text)));

                double sf = Formulas.sf_bwbp(4, (comp.MedS * 1000 / Convert.ToDouble(this.textBox2.Text)), (comp.MedS * 1000 / Convert.ToDouble(this.textBox1.Text))); /* scaling factor for the c coefficients */




                /* calculate the c coefficients */
                int[] ccof = Formulas.ccof_bwbp(4);
                double[] ccof2 = new double[ccof.Length];
                for (int ys = 0; ys < ccof.Length; ys++)
                {
                    ccof2[ys] = sf * ccof[ys];
                }

                for (int i = 0; i < comp.y.Count; i++)
                {
                    comp.y[i] = filter(comp.y[i], ccof2, dcof);

                }
            }
            else if (this.radioButton4.Checked)
            {
                /* calculate the d coefficients */
                double[] dcof = Formulas.dcof_bwbs(4, (comp.MedS * 1000 / Convert.ToDouble(this.textBox2.Text)), (comp.MedS * 1000 / Convert.ToDouble(this.textBox1.Text)));

                double sf = Formulas.sf_bwbs(4, (comp.MedS * 1000 / Convert.ToDouble(this.textBox2.Text)), (comp.MedS * 1000 / Convert.ToDouble(this.textBox1.Text))); /* scaling factor for the c coefficients */




                /* calculate the c coefficients */
                double[] ccof = Formulas.ccof_bwbs(4, (comp.MedS * 1000 / Convert.ToDouble(this.textBox2.Text)), (comp.MedS * 1000 / Convert.ToDouble(this.textBox1.Text)));
                double[] ccof2 = new double[ccof.Length];
                for (int ys = 0; ys < ccof.Length; ys++)
                {
                    ccof2[ys] = sf * ccof[ys];
                }

                for (int i = 0; i < comp.y.Count; i++)
                {
                    comp.y[i] = filter(comp.y[i], ccof2, dcof);

                }
            }
            return comp;
        }





        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                form1.xToolStripMenuItem.Checked = true;
                Coef = double.Parse(textBox1.Text);
                Processing.AddFunc("b", comp => K(Coef, comp));
                //form1.button1_Click(sender, e);
                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при чтении данных B");
            }

        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void choice_second_group(object sender, EventArgs e)
        {
            textBox2.Visible = true;
            label1.Text = "Интервал длин волн:";
        }

        private void choice_first_group(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            label1.Text = "Длина волны среза в мм:";
        }



        private void formSKO_Click(object sender, EventArgs e)
        {

        }

        private void formSKO_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.sko1 = textBox1.Text;
            if(textBox2.Visible)
                Properties.Settings.Default.sko2 = textBox2.Text;

            Properties.Settings.Default.Save();
        }

        private void formSKO_Load(object sender, EventArgs e)
        {

        }
    }
}
