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
    public partial class InfUnfold : Form
    {
        public DataTable table { get; private set; }
        public DataTable tableGrad { get; private set; }
        public InfUnfold(Components comp, int[][] numsPanels)
        {
            InitializeComponent();

            double[] arrMax = new double[comp.y.Count];
            double[] arrMin = new double[comp.y.Count];

            arrMax = comp.y.Select(d => d.Max()).ToArray();
            arrMin = comp.y.Select(d => d.Min()).ToArray();

            table = new DataTable();

            DataColumn idColumn = new DataColumn("Номер", Type.GetType("System.Int32"));
            DataColumn maxColumn = new DataColumn("Максимум", Type.GetType("System.Int32"));
            DataColumn minColumn = new DataColumn("Минимум", Type.GetType("System.Int32"));

            table.Columns.Add(idColumn);
            table.Columns.Add(maxColumn);
            table.Columns.Add(minColumn);

            tableGrad = table.Clone();
            //double[] MaxUnifold = new double[21];
            //double[] MinUnifold = new double[21];
            //int[] a = numsPanels[1];//new int[] { 1, 2, 3 };
            //double min = arrMin.Where((m, j) => numsPanels[i].Any(n => j == n)).Min();
            //bool b = numsPanels[1].Any(n => 4 == n);

            //string s = "";
            //for (int j = 0; j < a.Length; j++)
            //    s += a[j].ToString();

            //MessageBox.Show(s);
            double max;
            double min;

            for (int i = 0; i < 21; i++)
            {
                max = arrMax.Where((m, j) => numsPanels[i].Any(n => j == n * 2 - 1)).Max();
                min = arrMin.Where((m, j) => numsPanels[i].Any(n => j == n * 2 - 1)).Min();

                table.Rows.Add(new object[] { i+1, max, min });

                max = arrMax.Where((m, j) => numsPanels[i].Any(n => j == n * 2 - 2)).Max();
                min = arrMin.Where((m, j) => numsPanels[i].Any(n => j == n * 2 - 2)).Min();

                tableGrad.Rows.Add(new object[] { i+1, max, min });
            }

            dataGridView1.DataSource = table;
            dataGridView2.DataSource = tableGrad;

            //int sdf = dataGridView1.Columns[0].Width;
            //dataGridView2.Columns[0].Width = 30;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView1.Rows.Add()
        }
    }
}
