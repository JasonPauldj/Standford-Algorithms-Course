using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Week2_Batch2_Problem_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { -90, -80, 2, 4, 5, 89 };
            int is_ans = 0;
            int val;
            string seq = "";
            is_ans = CheckArray(numbers, out val);

            int CheckArray(int[] nums,out int pos)
            {
                int len = nums.Length;
                if (len == 2)
                {
                    if (nums[0] == 0 || nums[1]==1)
                    {
                        pos = 0;
                        return 1;
                    }
                    else
                    {
                        pos = -1;
                        return 0;
                    }
                }
                else
                {
                    int mid = len / 2;
                    int l_len = len / 2;
                    int r_len = len - len / 2;
                    int[] r_arr = new int[r_len-1];
                    int[] l_arr = new int[l_len];
                    if (nums[mid] == mid)
                    {
                        pos = mid;
                        return 1;
                    }
                    else if (nums[mid] < 0 || (nums[mid] < mid))
                    {
                        for (int i = 0; i < r_len -1 ; i++)
                        {
                            r_arr[i] = nums[mid + 1 + i] - (mid + 1);
                        }
                        foreach(int i in r_arr)
                        {
                            Console.Write("{0}  ",i);
                        }
                        Console.WriteLine();
                        seq += "R ";
                        return CheckArray(r_arr,out pos);
                    }
                    else 
                    {
                       
                        {
                            for (int i = 0; i < l_len  ; i++)
                            {
                                l_arr[i] = nums[i];
                            }
                            seq += "L ";
                            foreach (int i in l_arr)
                            {
                                Console.Write("{0} ", i);
                            }
                            Console.WriteLine();
                            return CheckArray(l_arr, out pos);
                        }
                        
                    }
                }

            }
            Console.WriteLine(seq);
            Console.WriteLine("ans is {0} and value is {1}",is_ans,val);
            Console.ReadKey();

        }
    }
}
