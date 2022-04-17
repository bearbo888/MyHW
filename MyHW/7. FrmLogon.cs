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
        }

        SqlConnection conn = new SqlConnection(Settings.Default.DatabaseConnectionString);

        private void button1_Click(object sender, EventArgs e)
        {
            bool chk = true;        
            using (conn)
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
                using (conn)
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
            using (conn)
            {
                SqlCommand command = new SqlCommand("select * From Member where username=@username AND password=@password", conn);
                command.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                command.Parameters.AddWithValue("@password", PasswordTextBox.Text);
                conn.Open();

                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("登入成功");
                    FrmCustomers frmCustomers = new FrmCustomers();    
                    frmCustomers.TopLevel = false;
                    frmCustomers.FormBorderStyle = FormBorderStyle.None;
                    frmCustomers.Dock = DockStyle.Fill;
                    panel1.Controls.Add(frmCustomers);
                    frmCustomers.Show();
                }
                else
                {
                    MessageBox.Show("登入失敗");
                }
            }

            
        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //using (SqlConnection conn = new SqlConnection())
        //    //{
        //    //    SqlCommand command = new SqlCommand();
        //    //    command.Connection = conn;
        //    //    command.CommandType = CommandType.StoredProcedure;
        //    //    command.CommandText = "Signup";

        //    //    command.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = UsernameTextBox.Text;
        //    //    command.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = PasswordTextBox.Text;

        //    //    SqlParameter p1 = new SqlParameter();
        //    //    p1.ParameterName = "@return_values";
        //    //    p1.Direction = ParameterDirection.ReturnValue;
        //    //    command.Parameters.Add(p1);
        //    //    conn.Open();
        //    //    command.ExecuteNonQuery();

        //    //    //MessageBox.Show($"第 {0} 位" +p1.Value.ToString());
        //    //}
        //}
    }
}
