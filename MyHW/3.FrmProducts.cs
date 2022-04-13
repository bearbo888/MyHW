using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHW
{
    public partial class FrmProducts : Form
    {
        public FrmProducts()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.FillByUnitPrice(dataSet1.Products,int.Parse(textBox1.Text),int.Parse(textBox2.Text));
            bindingSource1.DataSource = dataSet1.Products;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
            lblResult.Text = "結果" + bindingSource1.Count + "幾筆";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.FillByName(dataSet1.Products,textBox3.Text);
            bindingSource1.DataSource = dataSet1.Products;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
            lblResult.Text = "結果" + bindingSource1.Count + "幾筆";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveFirst();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveLast();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            lblResult.Text = "結果" + bindingSource1.Count + "幾筆";
            this.label2.Text = $"{bindingSource1.Position + 1}/{bindingSource1.Count}";
        }
    }
}
