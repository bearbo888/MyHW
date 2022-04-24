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

            this.flowLayoutPanel1.AllowDrop = true;
            this.flowLayoutPanel1.DragEnter += flowLayoutPanel1_DragEnter; ;
            this.flowLayoutPanel1.DragDrop += flowLayoutPanel1_DragDrop; ;
        }

        private void FrmMyAlbum_V1_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Default.DatabaseConnectionString))
            {
                SqlCommand command = new SqlCommand("select Name from cityNo", conn);
                conn.Open();
                SqlDataReader dr = command.ExecuteReader();

                this.comboBox1.Items.Clear();

                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++) { this.comboBox1.Items.Add(dr[i].ToString()); }
                }
            }

            this.comboBox1.SelectedIndex = 0;

            ShowImage();
            bindingCityNo();

            if(!string.IsNullOrEmpty(idTextBox.Text))
                bindingCity(int.Parse(idTextBox.Text));
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
                    command.CommandText = "select * From city as c join cityNo as n on c.name = n.id where n.name = @name";
                    command.Parameters.AddWithValue("@name", comboBox1.SelectedItem.ToString());
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
                MessageBox.Show(ex.Message + " ShowImage()");
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
                    command.CommandText = "select * From city as c join cityNo as n on c.name = n.id where n.name = @name";
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
                MessageBox.Show(ex.Message + " ShowImage2()");
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

                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = Class1.getCityNo(comboBox1.SelectedItem.ToString());
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


        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                picPictureBox.Image = Image.FromFile(openFileDialog.FileName);
            }
            else
            {
                MessageBox.Show("Cancel");
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream stream = new FileStream(openFileDialog.FileName, FileMode.Open);
                Image image = Image.FromStream(stream);

                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);
                byte[] bufferPhoto = ms.GetBuffer();

                try
                {
                    SqlConnection conn = new SqlConnection(Settings.Default.DatabaseConnectionString);
                    SqlCommand command = new SqlCommand("Insert into city(name,pic) values (@name,@pic)", conn);
                    command.Parameters.AddWithValue("@name", Class1.getCityNo(comboBox1.SelectedItem.ToString()));
                    command.Parameters.AddWithValue("@pic", bufferPhoto);
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            flowLayoutPanel1.Controls.Clear();
            ShowImage();
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

        void bindingCityNo()
        {
            cityNoDataGridView.DataBindings.Clear();
            idTextBox.DataBindings.Clear();
            nameTextBox.DataBindings.Clear();

            cityNoTableAdapter.Fill(this.dataSet1.cityNo);
            bindingSource.DataSource = this.dataSet1.cityNo;
            bindingNavigator.BindingSource = bindingSource;
            cityNoDataGridView.DataSource = bindingSource;

            this.idTextBox.DataBindings.Add("text", this.bindingSource, "id");
            this.nameTextBox.DataBindings.Add("text", this.bindingSource, "Name");


        }

        void bindingCity(int no)
        {
            cityDataGridView.DataBindings.Clear();
            idTextBox1.DataBindings.Clear();
            nameTextBox1.DataBindings.Clear();
            picPictureBox.DataBindings.Clear();

            cityTableAdapter1.FillBy(this.dataSet1.city,no);
            bindingSource1.DataSource = this.dataSet1.city;
            bindingNavigator1.BindingSource = bindingSource1;
            cityDataGridView.DataSource = bindingSource1;

            this.idTextBox1.DataBindings.Add("text", this.bindingSource, "id");
            this.nameTextBox1.DataBindings.Add("text", this.bindingSource, "Name");
            this.picPictureBox.DataBindings.Add("Image", this.bindingSource1, "pic", true);
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            bindingCity(int.Parse(idTextBox.Text));
        }
    }
}
