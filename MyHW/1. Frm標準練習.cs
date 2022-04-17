
using MyHW;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmHomeWork : Form
    {
        public FrmHomeWork()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = 100;
            int b = 66;
            int c = 77;

            int max = (a > b) ? a : b;
            max = (max > c) ? max : c;

            lblResult.Text = max.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int[] nums = { 33, 4, 5, 11, 222, 34 };
            int odd = 0, even = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] % 2 == 0) { even++; }
                else { odd++; }
            }

            lblResult.Text += "nums = { 33, 4, 5, 11, 222, 34 }\nOdd Number: " + odd + "\nEven Number: " + even;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string[] names = { "aaa", "ksdkfjsdk"};
            string name = "";
            
            for (int i = 0; i < names.Length - 1; i++)
            {
                if (names[i].Length < names[i + 1].Length)
                {
                    name = names[i + 1];
                }
                else
                {
                    name = names[i];
                }
            }
            lblResult.Text = "names = { aaa, ksdkfjsdk}\nLong Name: " + name;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int input = Convert.ToInt32(textBox4.Text);
                if (input % 2 == 0)
                {
                    MessageBox.Show(input + " is even number.");
                }
                else
                {
                    MessageBox.Show(input + " is odd number.");
                }
            }
            catch
            {
                MessageBox.Show("Input intger number");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] scores = { 2, 3, 46, 33, 22, 100,150, 33,55};
            lblResult.Text = "scores = { 2, 3, 46, 33, 22, 100, 150, 33, 55}\n";
            lblResult.Text += "最大值為: " + scores.Max().ToString() + " 最小值為: " + scores.Min().ToString();
        }

        int MyMinScore(int[] nums)
        {
            return 10;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            lblResult.Text = "九九乘法表\n";
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    lblResult.Text += (i * (j + 1) < 10) ? (j + 1) + "X" + i + "= " + i * (j + 1) + " | " : (j + 1) + "X" + i + "=" + i * (j + 1) + " | ";
                }
                lblResult.Text += "\n";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            lblResult.Text = Convert.ToString(100, 2);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int a = 0; string[] names = { "aaa", "ksdkfjsdk" };

            lblResult.Text = "names = { aaa, ksdkfjsdk }\n包含 C or c :";

            for (int i = 0; i < names.Length - 1; i++)
            {
                if (names[i].Contains('c') || names[i].Contains('C'))
                {
                    a++;
                    lblResult.Text += names[i] + " ";
                }
            }
            lblResult.Text += ", 總共 " + a + " 個";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int[] array = new int[6];
            lblResult.Text = "樂透號碼:\n";
            int i = 0;
            Class1 cs = new Class1();

            while (true)
            {
                Random random = new Random();
                int z = random.Next(1, 49);

                if (cs.check(array, z))
                {
                    array[i] = z;
                    i++;
                }
                if (array[5] > 0) break;
            };

            for (int j = 0; j < 6; j++)
            {
                lblResult.Text += (array[j] < 10) ? "0" + array[j] + " " : array[j] + " ";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 33, 4, 5, 11, 222, 34 };
            nums = Class1.sort(nums);
            lblResult.Text = "nums = { 33, 4, 5, 11, 222, 34 }\n";
            lblResult.Text += "Max val : " + nums[nums.Length - 1] + "\n";
            lblResult.Text += "Min val : " + nums[0];
        }

        private void button16_Click(object sender, EventArgs e)
        {
            lblResult.Text = "結果";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int total = 0;
            if (!String.IsNullOrWhiteSpace(textBox1.Text) && !String.IsNullOrWhiteSpace(textBox2.Text) && !String.IsNullOrWhiteSpace(textBox3.Text))
            {
                int f = int.Parse(textBox1.Text); int t = int.Parse(textBox2.Text); int s = int.Parse(textBox3.Text);
                for (int i = f; i <= t; i = i + s)
                {
                    total += i;
                }

                lblResult.Text = f + " 到 " + t + " 間隔：" + s + "\n加總為 " + total;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int total = 0;
            if (!String.IsNullOrWhiteSpace(textBox1.Text) && !String.IsNullOrWhiteSpace(textBox2.Text) && !String.IsNullOrWhiteSpace(textBox3.Text))
            {
                int f = int.Parse(textBox1.Text); int t = int.Parse(textBox2.Text); int s = int.Parse(textBox3.Text);

                while (f <= t)
                {
                    total += f;
                    f += s;
                }

                lblResult.Text = int.Parse(textBox1.Text) + " 到 " + t + " 間隔：" + s + "\n加總為 " + total;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int total = 0;
            if (!String.IsNullOrWhiteSpace(textBox1.Text) && !String.IsNullOrWhiteSpace(textBox2.Text) && !String.IsNullOrWhiteSpace(textBox3.Text))
            {
                int f = int.Parse(textBox1.Text); int t = int.Parse(textBox2.Text); int s = int.Parse(textBox3.Text);

                do
                {
                    total += f;
                    f += s;
                } while (f <= t);

                lblResult.Text = int.Parse(textBox1.Text) + " 到 " + t + " 間隔：" + s + "\n加總為 " + total;
            }
        }
    }
}
