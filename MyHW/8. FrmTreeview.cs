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
    public partial class FrmTreeview : Form
    {
        public FrmTreeview()
        {
            InitializeComponent();
        }

        private void FrmTreeview_Load(object sender, EventArgs e)
        {
            treeView1.LabelEdit = true;
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString);

            try 
            {
                SqlCommand command = new SqlCommand("select distinct Country from Customers order by Country", conn);
                conn.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    TreeNode node = new TreeNode();
                    node.Text = dr[0].ToString();
                    treeView1.Nodes.Add(node);

                    //SqlCommand comm = new SqlCommand("select City from Customers where Country = @Country", conn);
                    //comm.Parameters.AddWithValue("@Country", dr[0]);
                    //SqlDataReader drr = comm.ExecuteReader();

                    //while (drr.Read())
                    //{
                    //    TreeNode node1 = new TreeNode();
                    //    node1.Text = drr[0].ToString();
                    //    node.Nodes.Add(node1);
                    //}
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
