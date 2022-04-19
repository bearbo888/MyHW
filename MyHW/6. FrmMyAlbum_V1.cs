using MyHW.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace MyHW
{
    public partial class FrmMyAlbum_V1 : Form
    {
        public FrmMyAlbum_V1()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            this.flowLayoutPanel1.AllowDrop = true;
            this.flowLayoutPanel1.DragEnter += flowLayoutPanel1_DragEnter; ;
            this.flowLayoutPanel1.DragDrop += flowLayoutPanel1_DragDrop; ;
            ShowImage();
        }

        private void ShowImage()
        {
            flowLayoutPanel1.Controls.Clear();
            try
            {
                using (SqlConnection sql = new SqlConnection(Settings.Default.DatabaseConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = sql;
                    sql.Open();
                    command.CommandText = "select * From city where name=@name";
                    command.Parameters.AddWithValue("@name", comboBox1.SelectedItem);
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        MemoryStream ms = new MemoryStream((byte[])dr["pic"]);
                        PictureBox pictureBox = new PictureBox();

                        pictureBox.Image = Image.FromStream(ms);
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox.Width = 200;
                        pictureBox.Height = 120;
                        pictureBox.Click += PictureBox_Click; ;

                        flowLayoutPanel1.Controls.Add(pictureBox);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowImage2(string city)
        {
            flowLayoutPanel2.Controls.Clear();
            try
            {
                using (SqlConnection sql = new SqlConnection(Settings.Default.DatabaseConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = sql;
                    sql.Open();
                    command.CommandText = "select * From city where name=@name";
                    command.Parameters.AddWithValue("@name", city);
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        MemoryStream ms = new MemoryStream((byte[])dr["pic"]);
                        PictureBox pictureBox = new PictureBox();

                        pictureBox.Image = Image.FromStream(ms);
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox.Padding = new Padding(5,5,5,5);
                        pictureBox.Width = 200;
                        pictureBox.Height = 120;
                        pictureBox.Click += PictureBox_Click; ;

                        flowLayoutPanel2.Controls.Add(pictureBox);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);

            for (int i = 0; i < file.Length; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(file[i]);

                try
                {
                    SqlConnection conn = new SqlConnection(Settings.Default.DatabaseConnectionString);
                    SqlCommand command = new SqlCommand("Insert into city(name,pic) values (@name,@pic)", conn);

                    MemoryStream stream = new MemoryStream();
                    pictureBox.Image.Save(stream, ImageFormat.Jpeg);
                    byte[] b = stream.GetBuffer();

                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = comboBox1.SelectedItem;
                    command.Parameters.Add("@pic", SqlDbType.Image).Value = b;
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Width = 120;
                pictureBox.Height = 80;
                pictureBox.Click += PictureBox_Click; ;
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
           
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.BackgroundImage = ((PictureBox)sender).Image;
            f.BackgroundImageLayout = ImageLayout.Stretch;
            f.Show();
        }

        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmToolCRUD cRUD = new frmToolCRUD();
            cRUD.Show();
        }

        private void lkTaipei_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cityDataGridView.DataBindings.Clear();
            cityTableAdapter1.FillBy(this.dataSet1.city,"台北");
            bindingSource1.DataSource = dataSet1.city;
            cityDataGridView.DataSource = bindingSource1;

            this.cbCountry.DataBindings.Add("text", this.bindingSource1, "name");
            this.pictureBox1.DataBindings.Add("Image", this.bindingSource1, "pic",true);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }
            else
            {
                MessageBox.Show("Cancel");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ImageConverter converter = new ImageConverter();
            cityTableAdapter1.Insert(cbCountry.Text, (byte[])converter.ConvertTo(pictureBox1.Image, typeof(byte[])));
            dataSet1.Tables["city"].AcceptChanges();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cityTableAdapter1.Update(dataSet1.city);
            dataSet1.Tables["city"].AcceptChanges();
        }

        private void FrmMyAlbum_V1_Load(object sender, EventArgs e)
        {
            //cityTableAdapter1.Fill(this.dataSet1.city);
            //bindingSource1.DataSource = dataSet1.city;
            //cityDataGridView.DataSource = bindingSource1;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);

                try
                {
                    SqlConnection conn = new SqlConnection(Settings.Default.DatabaseConnectionString);
                    SqlCommand command = new SqlCommand("Insert into city(name,pic) values (@name,@pic)", conn);

                    MemoryStream stream = new MemoryStream();
                    pictureBox1.Image.Save(stream, ImageFormat.Jpeg);
                    byte[] b = stream.GetBuffer();

                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = comboBox1.SelectedItem;
                    command.Parameters.Add("@pic", SqlDbType.Image).Value = b;
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            ShowImage();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowImage2("台北");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowImage2("倫敦");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
