using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MyHW.Properties;

namespace MyHW
{
    public partial class FrmCustomers : Form
    {
        public FrmCustomers()
        {
            InitializeComponent();
            LoadColumns();
            LoadCombox();
        }

        private void FrmCustomers_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
            {
                SqlCommand command = new SqlCommand("select * from Customers where Country = @country", conn);
                command.Parameters.AddWithValue("@country", cbCountry.SelectedItem);
                conn.Open();
                SqlDataReader dr = command.ExecuteReader();

                this.listView1.Items.Clear();

                while (dr.Read())
                {
                    ListViewItem li = listView1.Items.Add(dr[0].ToString());
                    li.BackColor = (li.Index % 2 == 0) ? Color.Gold : Color.Orange;

                    for (int col = 1; col < dr.FieldCount; col++)
                    {
                        if (dr.IsDBNull(col))
                        {
                            li.SubItems.Add("Empty");
                        }
                        else
                        {
                            li.SubItems.Add(dr[col].ToString());
                        }
                    }

                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
        }
        private void LoadCombox()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString))
            {
                SqlCommand command = new SqlCommand("select distinct Country from Customers order by Country", conn);
                conn.Open();
                SqlDataReader dr = command.ExecuteReader();

                cbCountry.Items.Clear();

                while (dr.Read())
                {
                    this.cbCountry.Items.Add(dr[0]);
                }

                this.cbCountry.SelectedIndex = 0;
            }
        }
        private void LoadColumns()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select * from Customers", conn);
                SqlDataReader dataReader = command.ExecuteReader();

                DataTable dt = dataReader.GetSchemaTable();

                for (int col = 0; col < dt.Rows.Count; col++)
                {
                    listView1.Columns.Add(dt.Rows[col][0].ToString());
                }

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }
        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString))
            {
                SqlCommand command = new SqlCommand("select * from Customers where Country=@country", conn);
                command.Parameters.AddWithValue("@country", cbCountry.Text);
                conn.Open();

                SqlDataReader dr = command.ExecuteReader();

                this.listView1.Items.Clear();

                while (dr.Read())
                {
                    ListViewItem li = listView1.Items.Add(dr[0].ToString());
                    li.BackColor = (li.Index % 2 == 0) ? Color.Gold : Color.Orange;

                    for (int col = 1; col < dr.FieldCount; col++)
                    {
                        if (dr.IsDBNull(col))
                        {
                            li.SubItems.Add("Empty");
                        }
                        else
                        {
                            li.SubItems.Add(dr[col].ToString());
                        }
                    }
                }
            }
        }
    }
}
