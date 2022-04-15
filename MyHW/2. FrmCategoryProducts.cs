using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.FillBy(dataSet1.Products,comboBox1.SelectedItem.ToString());
            dataGridView1.DataSource = dataSet1.Products;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            categoriesTableAdapter1.Fill(dataSet1.Categories);
            foreach (var i in dataSet1.Categories) { comboBox1.Items.Add(i.CategoryName); }

            //try
            //{
            //    string a = "\"";
            //    SqlConnection sql = new SqlConnection("Data Source=DESKTOP-EKRVM5K;Initial Catalog=Northwind;Integrated Security=True;");
            //    sql.Open();
            //    SqlCommand command = new SqlCommand("select CategoryName from Categories", sql);
            //    SqlDataReader dataReader = command.ExecuteReader();

            //    while (dataReader.Read())
            //    {
            //        string str = $"{dataReader["CategoryName"]}";
            //        this.comboBox1.Items.Add(str);
            //    }
            //    sql.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
