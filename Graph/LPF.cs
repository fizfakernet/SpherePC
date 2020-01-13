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
    public partial class LPF : Form
    {
        public LPF(Form1 form)
        {
            InitializeComponent();
            form1 = form;

            textBox1.Text = Properties.Settings.Default.elip1;
            textBox2.Text = Properties.Settings.Default.elip2;
        }

        private void LPF_Load(object sender, EventArgs e)
        {

        }

        Form1 form1;
        public double shift { get; private set; } = 0d;
        public Components Shift(double shift, Components comp)
        {
            //Components newComp = (Components) comp.Clone();
            //MessageBox.Show(comp.MedS.ToString());


            comp.y = comp.y.Select(arr => arr.Select(d => d + shift).ToArray()).ToList();

            return comp;
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                shift = double.Parse(textBox1.Text);
                Processing.AddFunc("e", comp => Shift(shift, comp));
                //form1.button1_Click(sender, e);
                Close();
                form1.cToolStripMenuItem.Checked = true;
            }
            catch
            {
                MessageBox.Show("Ошибка при чтение данных");
            }

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

        private void LPF_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.elip1 = textBox1.Text;
            if (textBox2.Visible)
                Properties.Settings.Default.elip2 = textBox2.Text;

            Properties.Settings.Default.Save();
        }
    }
}
