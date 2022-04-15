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
    public partial class FrmDataSet_結構 : Form
    {
        public FrmDataSet_結構()
        {
            InitializeComponent();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            productsTableAdapter.Fill(nwDataSet1.Products);
            categoriesTableAdapter.Fill(nwDataSet1.Categories);
            customersTableAdapter.Fill(nwDataSet1.Customers);
            dataGridView4.DataSource = nwDataSet1.Products;
            dataGridView5.DataSource = nwDataSet1.Categories;
            dataGridView6.DataSource = nwDataSet1.Customers;



            for (int i = 0; i < nwDataSet1.Tables.Count; i++)
            {
                DataTable dt = this.nwDataSet1.Tables[i];

                listBox2.Items.Add(dt.TableName);

                string str = "";
                for (int col = 0; col < dt.Columns.Count - 1; col++)
                {
                    str += dt.Columns[col] + " ";

                }


                listBox2.Items.Add(str);

                for (int row = 0; row < dt.Rows.Count - 1; row++)
                {
                    string str2 = "";
                    for (int rowd = 0; rowd < dt.Columns.Count; rowd++)
                    {
                        str2 += dt.Rows[row][rowd] + "  ";
                    }
                    listBox2.Items.Add(str2);
                }

                listBox2.Items.Add("=================================");
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.splitContainer2.Panel1Collapsed = !this.splitContainer2.Panel1Collapsed;
        }
    }
}
