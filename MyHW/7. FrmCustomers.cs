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

namespace MyHW
{
    public partial class FrmCustomers : Form
    {
        public FrmCustomers()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString);

        private void FrmCustomers_Load(object sender, EventArgs e)
        {
            //Load combox
            using (conn)
            {
                SqlCommand command = new SqlCommand("select distinct Country from Customers order by Country");
                conn.Open();
                SqlDataReader dr = command.ExecuteReader();

                cbCountry.Items.Clear();

                while (dr.Read())
                {
                    this.cbCountry.Items.Add(dr[0]);
                }

                this.cbCountry.SelectedIndex = 0;
            }

            using (conn)
            {
                SqlCommand command = new SqlCommand("select * from Customers");
                conn.Open();
                SqlDataReader dr = command.ExecuteReader();

                this.listView1.Items.Clear();

                while (dr.Read())
                {
                    for (int col = 0; col <11 ; col++) { this.listView1.Items.Add(dr[col].ToString()); }
                }
            }
        }
    }
}
