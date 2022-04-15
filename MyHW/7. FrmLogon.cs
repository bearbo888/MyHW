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
    public partial class FrmLogon : Form
    {
        public FrmLogon()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.DatabaseConnectionString);
                SqlCommand command = new SqlCommand("select username From Member where user=@username", conn);
                SqlDataReader dr = command.ExecuteReader();
                command.Parameters.Add("@username", SqlDbType.NVarChar, 16).Value = UsernameTextBox.Text;

                //if (dr.HasRows)
                //{
                //    MessageBox.Show("帳號重覆");
                //}
                //else
                //{
                //    SqlCommand command2 = new SqlCommand("insert into Member(username,password) values(@username,@password)",conn);

                //    command2.Parameters.Add("@username", SqlDbType.NVarChar, 16).Value = UsernameTextBox.Text;
                //    command2.Parameters.Add("@password", SqlDbType.NVarChar, 40).Value = PasswordTextBox.Text;
                //    command2.ExecuteNonQuery();
                //}
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    using (SqlConnection conn = new SqlConnection())
        //    {
        //        SqlCommand command = new SqlCommand();
        //        command.Connection = conn;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = "Signup";

        //        command.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = UsernameTextBox.Text;
        //        command.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = PasswordTextBox.Text;

        //        //SqlParameter p1 = new SqlParameter();
        //        //p1.ParameterName = "@return_values";
        //        //p1.Direction = ParameterDirection.ReturnValue;
        //        //command.Parameters.Add(p1);
        //        conn.Open();
        //        command.ExecuteNonQuery();

        //        //MessageBox.Show($"第 {0} 位" +p1.Value.ToString()+);
        //    }
        //}
    }
}
