using System;
using System.Collections.Generic;
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
    }
}
