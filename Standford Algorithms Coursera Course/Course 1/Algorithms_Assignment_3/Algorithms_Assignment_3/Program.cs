using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Algorithms_Assignment_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stp_watch = Stopwatch.StartNew();
            string path = @"C:\Users\joshu\Desktop\QuickSort";
            string[] str_nums = File.ReadAllLines(path);
            int[] nums = new int[str_nums.Length];
            int no_of_comparisons = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = Convert.ToInt16(str_nums[i]);
            }

            QuickSort(0, nums.Length - 1);

            Console.WriteLine("no of comparisons {0}", no_of_comparisons);

            void QuickSort(int start_pos, int last_pos)
            {

                int len = last_pos - start_pos + 1;
                if (len > 1)
                {
                    //Part 3
                    int middle;
                    // checking if the len is even or odd and setting the location of the median
                    if (len % 2 == 0)
                    {
                        middle = start_pos + len / 2 - 1;
                    }
                    else
                    {
                        middle = start_pos + (len / 2);
                    }


                    //checking the median between start_pos,middle and end_pos and swapping the median with start_pos and later on set the pivot to start_pos
                    if (nums[start_pos] > nums[middle])
                    {
                        if (nums[last_pos] > nums[start_pos])
                        {
                            //int pivot = nums[start_pos];
                            Swap(start_pos, start_pos);
                        }
                        else if (nums[last_pos] > nums[middle])
                        {
                            //int pivot = nums[last_pos];
                            Swap(start_pos, last_pos);
                        }
                        else
                        {
                            //int pivot = nums[len / 2];
                            Swap(start_pos, middle);
                        }
                    }
                    else
                    {
                        if (nums[last_pos] > nums[middle])
                        {
                            // int pivot = nums[len / 2];
                            Swap(start_pos, middle);
                        }
                        else if (nums[last_pos] > nums[start_pos])
                        {
                            //int pivot = nums[last_pos];
                            Swap(start_pos, last_pos);
                        }
                        else
                        {
                            // int pivot = nums[start_pos];
                            Swap(start_pos, start_pos);
                        }

                    }


                    //Part 2 -- swap last_pos with start_pos and later on set the pivot to start_pos
                    //Swap(last_pos, start_pos);

                    //Part1 -- no swap takes place and we set the pivot value to the start_pos
                    int pivot = nums[start_pos];

                    no_of_comparisons += (len - 1);
                    int i = start_pos;

                    // i - reprsents the index of the last element smaller than the pivot
                    // j - traverses the entire array and compares it with the pivot.
                    for (int j = start_pos + 1; j <= last_pos; j++)
                    {
                        if (nums[j] < pivot)
                        {
                            Swap(i + 1, j);
                            i++;
                        }
                    }

                    Swap(i, start_pos);
                    QuickSort(start_pos, i - 1);
                    QuickSort(i + 1, last_pos);
                }
            }

            void Swap(int i, int j)
            {
                int temp = nums[j];
                nums[j] = nums[i];
                nums[i] = temp;
            }
            Console.WriteLine("time taken is {0}", stp_watch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
