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
    public partial class information : Form
    {
        public Components comp;
        public information(Components components)
        {
            InitializeComponent();
            comp = components;
            textBox1.Text = components.СommentBegin;
            textBox2.Text = components.DatetimeBegin.ToString();
            textBox3.Text = components.WidhtBegin;
            textBox4.Text = components.LongitudetBegin;
            textBox5.Text = components.СommentEnd;
            textBox6.Text = components.DatetimeEnd.ToString();
            textBox7.Text = components.LongitudetEnd;
            textBox8.Text = components.WidhtEnd;

            if (components.Sec != 0d)
                textBox9.Text = (components.Sec*1000).ToString("F2") + " мc";

            
            textBox10.Text = (components.MedS*1000).ToString("F2") + " мм";
            textBox11.Text = components.MedV.ToString("F6") + @" м/c";
            textBox12.Text = components.SizeFile.ToString("F2");
            textBox13.Text = components.s.ToString("F2") + " м";
            textBox14.Text = components.t.ToString("F0") + " c";
            textBox15.Text = components.numError.ToString();
            textBox16.Text = components.num.ToString();
            textBox17.Text =(100* (double)components.numError/ (double)(components.num+ components.numError)).ToString("F2");
        }

        public void SetX(double EndX)
        {
            textBox13.Text = (EndX).ToString("F2") + " м";
            textBox11.Text = (EndX / comp.t).ToString("F2") + " м/c";
            textBox10.Text = (EndX * 1000 / comp.n).ToString("N2") + " мм";
        }

        private void information_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }
    }
}
