using MyHW.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHW
{
    class Class1
    {
        public bool check(int[] array, int number)
        {
            bool val = false;
            int point = 0;
            for (int i = 0; i < 6; i++)
            {
                if (array[i] != number)
                {
                    point++;
                }
            }

            return (point == 6) ? !val : val;
        }

        public static int[] sort(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 0; j < nums.Length - 1; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        int temp = nums[j];
                        nums[j] = nums[j + 1];
                        nums[j + 1] = temp;
                    }
                }
            }

            return nums;
        }

        public static int getCityNo(string city)
        {
            int code = 4;

            using (SqlConnection conn = new SqlConnection(Settings.Default.DatabaseConnectionString))
            {
                SqlCommand command = new SqlCommand("select * from cityNo where name = @name", conn);
                conn.Open();
                command.Parameters.AddWithValue("@name",city);
                SqlDataReader dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        code = int.Parse(dr[0].ToString());
                    }
                }
            }
            return code;
        }




        enum cityCode
        {
            Taipei = 1,
            London = 2,
            Paris = 3
        }
    }
}
