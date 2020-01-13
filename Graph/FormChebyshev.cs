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
    public partial class FormChebyshev : Form
    {
        public FormChebyshev(Form1 form)
        {
            InitializeComponent();
            form1 = form;

            textBox1.Text = Properties.Settings.Default.cheb1;
            textBox2.Text = Properties.Settings.Default.cheb2;
        }
        Form1 form1;

        public Components n_plus(Components comp)
        {
            //Components newComp = (Components) comp.Clone();
            double[] arr = new double[comp.n * 3];
            for (int i = 0; i < comp.n * 3; i++)
                arr[i] = i * 0.5d;

            comp.n = comp.n * 3;
            comp.y = comp.y.Select(x => arr.Select(u => u).ToArray()).ToList();

            return comp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                form1.фильтрЧебышеваToolStripMenuItem.Checked = true;
                //Coef = double.Parse(textBox1.Text);
                Processing.AddFunc("c", comp => n_plus(comp));
                //form1.button1_Click(sender, e);
                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при чтении данных");
            }
        }

        private void choice_second_group(object sender, EventArgs e)
        {
            textBox2.Visible = true;
            label1.Text = "Интервал длин волн, мм:";
        }

        private void choice_first_group(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            label1.Text = "Длина волны среза в мм:";
        }

        private void FormChebyshev_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.cheb1 = textBox1.Text;
            if (textBox2.Visible)
                Properties.Settings.Default.cheb2 = textBox2.Text;

            Properties.Settings.Default.Save();
        }
    }
}
