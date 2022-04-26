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

        SqlConnection conn = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString);
        DataTable dt = new DataTable("AddressLists");
        DataSet ds = new DataSet();
        
        private void FrmTreeview_Load(object sender, EventArgs e)
        {
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);
            treeView1.LabelEdit = true;
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Parent", typeof(string));

            using (conn) 
            {
                string str = "select distinct Country from Customers order by Country; select distinct City,Country from Customers;";
                SqlCommand command = new SqlCommand(str, conn);
                conn.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    dt.Rows.Add(new string[] { dr[0].ToString(), "0" });
                }

                dr.NextResult();

                while (dr.Read())
                {
                    dt.Rows.Add(new string[] { dr[0].ToString(), dr[1].ToString() });
                }

                ds.Tables.Add(dt);
                ds.Relations.Add("TreeParentChild", ds.Tables["AddressLists"].Columns["Name"], ds.Tables["AddressLists"].Columns["Parent"], false);
            }

            foreach (DataRow dr in ds.Tables["AddressLists"].Rows)
            {
                if (dr["Parent"].ToString() == "0")
                {
                    TreeNode root = new TreeNode(dr["Name"].ToString());
                    treeView1.Nodes.Add(root);     
                    PopulateTree(dr, root);
                }
            }
            treeView1.ExpandAll();

        }

        public void PopulateTree(DataRow dr, TreeNode pNode)
        {          
            foreach (DataRow row in dr.GetChildRows("TreeParentChild"))
            {
                TreeNode cChild = new TreeNode(row["Name"].ToString());
                pNode.Nodes.Add(cChild);
                PopulateTree(row, cChild);
            }
        }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.NorthwindConnectionString))
            {
                SqlCommand command = new SqlCommand("select * from Customers where city = @city", con);
                command.Parameters.AddWithValue("@city",e.Node.Text);
                con.Open();

                SqlDataReader dr = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                this.dataGridView2.DataSource = dt;

            }
        }
    }
}
