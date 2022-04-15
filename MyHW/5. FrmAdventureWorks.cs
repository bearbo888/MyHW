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

namespace MyHomeWork
{
    public partial class FrmAdventureWorks : Form
    {
        public FrmAdventureWorks()
        {
            InitializeComponent();
            bindingNavigator1.BindingSource = bindingSource1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            productPhotoTableAdapter1.FillByDate(this.dataSet1.ProductPhoto, dateTimePicker1.Value, dateTimePicker2.Value);
            bindingSource1.DataSource = this.dataSet1.ProductPhoto;
            this.dataGridView1.DataSource = bindingSource1;
        }

        private void button2_Click(object sender, EventArgs e) // sort ASC
        {
            if (comboBox1.SelectedItem == null)
            {
                productPhotoTableAdapter1.FillByOBDate(this.dataSet1.ProductPhoto,dateTimePicker1.Value,dateTimePicker2.Value);
            }
            else {
                productPhotoTableAdapter1.FillByOBYear(this.dataSet1.ProductPhoto, Decimal.Parse(comboBox1.SelectedItem.ToString()));
            }

            bindingSource1.DataSource = this.dataSet1.ProductPhoto;
            this.dataGridView1.DataSource = bindingSource1;
        }

        private void FrmAdventureWorks_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sql = new SqlConnection("Data Source=DESKTOP-EKRVM5K;Initial Catalog=AdventureWorks;Integrated Security=True;");
                sql.Open();
                SqlCommand command = new SqlCommand("select DISTINCT datepart(yyyy,[ModifiedDate]) from [Production].[ProductPhoto]", sql);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string str = $"{dataReader[0]}";
                    this.comboBox1.Items.Add(str);
                }
                this.comboBox1.Items.Add("");
                sql.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            productPhotoTableAdapter1.FillByYear(this.dataSet1.ProductPhoto, Decimal.Parse(comboBox1.SelectedItem.ToString()));
            bindingSource1.DataSource = this.dataSet1.ProductPhoto;
            this.dataGridView1.DataSource = bindingSource1;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveFirst();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveLast();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            this.label4.Text = $"{bindingSource1.Position + 1}/{bindingSource1.Count}";
        }
    }
}
