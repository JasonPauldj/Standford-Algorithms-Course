using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This file contains all of the 100,000 integers between 1 and 100,000 (inclusive) in some order, with no integer repeated.
//Your task is to compute the number of inversions in the file given, where the ith row of the file indicates the
//ith the entry of an array.
//Because of the large size of this array, you should implement the fast divide-and-conquer 
//algorithm covered in the video lectures.
//The numeric answer for the given input file should be typed in the space below.
namespace Algorithms_Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\Users\joshu\Desktop\IntegerArray";
            string[] str_nums = File.ReadAllLines(path);
            int[] nums = new int[str_nums.Length];
            int len = nums.Length;
            for (int i=0;i<len;i++)
            {
                nums[i] = Convert.ToInt32(str_nums[i]);
            }
            long ans = 0;
            MergeSort(len, 0, len - 1);
            void MergeSort(int arr_len, int arr_start, int arr_end)
            {
                // if even number of elements then split in half or if odd number eg 5 - split 2 and 3
                if (arr_len > 1)
                {
                    int half = (arr_len / 2);
                    int l_end = arr_start + half - 1;
                    int r_start = l_end + 1;
                    int l_len = l_end - arr_start + 1;
                    int r_len = arr_end - r_start + 1;
                    MergeSort(l_len, arr_start, l_end);
                    MergeSort(r_len, r_start, arr_end);
                    int[] arr_left = new int[l_len];
                    int[] arr_right = new int[r_len];
                    for(int i=0;i<l_len;i++)
                    {
                        arr_left[i] = nums[arr_start + i];
                    }
                    for(int j=0;j<r_len;j++)
                    {
                        arr_right[j] = nums[r_start + j];
                    }
                    int l = 0;
                    int r = 0;
                    for (int i = 0; i < arr_len; i++)
                    {
                        if (l <= l_len - 1 && r <= r_len - 1)
                        {
                            if (arr_right[r] >= arr_left[l])
                            {
                                nums[arr_start + i] = arr_left[l];
                                l++;
                            }
                            else if(arr_left[l]>arr_right[r])
                            {
                                nums[arr_start + i] = arr_right[r];
                                r++;
                                ans += (arr_left.Length - 1)-l + 1;
                            }
                        }
                        else
                        {
                            if(l<=l_len-1)
                            {
                                nums[arr_start + i] = arr_left[l];
                                l++;
                            }
                            else if(r <=r_len -1)
                            {
                                nums[arr_start + i] = arr_right[r];
                                r++;
                            }
                        }

                    }
                 }
                else
                {
                    return;
                }


            }

            //foreach (int n in nums)
            //    Console.WriteLine(n);

            Console.WriteLine("ans is {0}",ans);
            Console.ReadKey();
        }
    }
}
