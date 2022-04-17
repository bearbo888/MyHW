using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHW
{
    public partial class FrmMyAlbum_V1 : Form
    {
        public FrmMyAlbum_V1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmToolCRUD cRUD = new frmToolCRUD();
            cRUD.Show();
        }

        private void lkTaipei_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cityDataGridView.DataBindings.Clear();
            cityTableAdapter.FillBy(this.dataSet1.city,"台北");
            bindingSource1.DataSource = dataSet1.city;
            cityDataGridView.DataSource = bindingSource1;

            this.cbCountry.DataBindings.Add("text", this.bindingSource1, "name");
            this.pictureBox1.DataBindings.Add("Image", this.bindingSource1, "pic",true);

        }

        private void lkParis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmToolCRUD cRUD = new frmToolCRUD();
            cRUD.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                //MessageBox.Show("OK" + openFileDialog.FileName);
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
            cityTableAdapter.Insert(cbCountry.Text, (byte[])converter.ConvertTo(pictureBox1.Image, typeof(byte[])));
            dataSet1.Tables["city"].AcceptChanges();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cityTableAdapter.Update(dataSet1.city);
            dataSet1.Tables["city"].AcceptChanges();
        }

        private void FrmMyAlbum_V1_Load(object sender, EventArgs e)
        {
            cityTableAdapter.Fill(this.dataSet1.city);
            bindingSource1.DataSource = dataSet1.city;
            cityDataGridView.DataSource = bindingSource1;
        }
    }
}
