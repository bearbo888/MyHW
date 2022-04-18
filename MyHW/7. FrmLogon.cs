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
    public partial class FrmLogon : Form
    {
        public FrmLogon()
        {
            InitializeComponent();
            tabControl1.Region = new Region(tabControl1.DisplayRectangle);
            LoadColumns();
            LoadCombox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool chk = true;
            using (SqlConnection conn = new SqlConnection(Settings.Default.DatabaseConnectionString))
            {
                SqlCommand command = new SqlCommand("select username From Member where username=@username", conn);
                command.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                conn.Open();

                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    chk = false;
                    MessageBox.Show("使用者名稱已經註冊");
                }
            }

            if (chk == true)
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.DatabaseConnectionString))
                {
                    SqlCommand command = new SqlCommand("Insert into Member(username,password) values (@username,@password)", conn);
                    command.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = UsernameTextBox.Text;
                    command.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = PasswordTextBox.Text;
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void OK_Click(object sender, EventArgs e)
        { 
            using (SqlConnection conn = new SqlConnection(Settings.Default.DatabaseConnectionString))
            {
                SqlCommand command = new SqlCommand("select * From Member where username=@username AND password=@password", conn);
                command.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                command.Parameters.AddWithValue("@password", PasswordTextBox.Text);
                conn.Open();

                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("登入成功");
                    tabControl1.SelectedTab = tabControl1.TabPages[1];
                    //FrmCustomers frmCustomers = new FrmCustomers();
                    //frmCustomers.TopLevel = false;
                    //frmCustomers.FormBorderStyle = FormBorderStyle.None;
                    //frmCustomers.Dock = DockStyle.Fill;
                    //panel1.Controls.Add(frmCustomers);
                    //frmCustomers.Show();
                }
                else
                {
                    MessageBox.Show("登入失敗");
                }

                conn.Dispose();
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

        private void FrmLogon_Load(object sender, EventArgs e)
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
    }
}
