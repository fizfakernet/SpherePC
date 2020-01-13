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
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();
            //textBox1.SelectionStart = 0;
            textBox1.TabStop = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
