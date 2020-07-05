using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Week2_Batch2_Problem_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] unimodal_arr = { 1, 2, 3, 5, 7, 10, 9,8 };
            int max=MaxValue(unimodal_arr);
            
            int MaxValue(int[] nums)
            {

                int len = nums.Length;
                if (len > 2)
                {
                    int mid = nums.Length / 2;
                    int l_len = len / 2;
                    int r_len = len - len / 2;

                    if (len % 2 == 0)
                    {
                        l_len = (len / 2);
                        r_len = (len / 2);
                    }
                    else
                    {
                        l_len = len / 2;
                        r_len = (len / 2) + 1;
                    }

                    if ((nums[mid - 1] < nums[mid]) && (nums[mid] < nums[mid + 1]))
                    {
                        Console.WriteLine("checking value {0} ", nums[mid]);
                        //call maxvalue with focus on only right half.
                        int[] r_arr = new int[r_len];
                        for (int i = 0; i < r_len; i++)
                        {
                            r_arr[i] = nums[mid + i];
                        }
                        return MaxValue(r_arr);

                    }
                    else if ((nums[mid - 1] > nums[mid]) && (nums[mid] > nums[mid + 1]))
                    {
                        Console.WriteLine("checking value {0}", nums[mid]);
                        //call maxvalue with focus on only left half
                        int[] l_arr = new int[l_len];
                        for (int i = 0; i < l_len; i++)
                        {
                            l_arr[i] = nums[i];
                        }
                        return MaxValue(l_arr);
                    }
                    else
                    {
                        return nums[mid];
                    }
                }
                else
                {
                    return nums.Max();
                }
            }

            Console.WriteLine("max value is {0}", max);
            Console.ReadKey();
        }
    }
}
