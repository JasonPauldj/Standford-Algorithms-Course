using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//You are given as input an unsorted array of n distinct numbers, where n is a power of 2. Give an algorithm that 
//identifies the second-largest number in the array, and that uses at most n + log (n) -2 comparisons

//Logic is as follows
/*In the above algorithm we compute the maximum element by divide and conquer: we divide the list into two, 
compute the maxima of both lists and then compare them.We keep the larger of the two and put the 
smaller of the two in a list.So the output of the algorithm is the largest value, together with a 
list of all the elements that it was compared to in the merge steps.So the list is of length log(n)-1.


The second largest value can only be discarded in a merge step if it is compared to the largest value in the list, 
so it has to be in the list that the algorithm spits out. Finding the maximum element in a list of length n 
can be done with n-1 comparisons, so we need log(n)-1 comparisons to find the largest element in the list 
that the algorithm gives us.
There are in total n-1 merge steps and hence n-1 comparisons in the algorithm*/

namespace Algorithms_Week2_Batch1_Problem_1
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] numbers = { 24, 64, 45, 37, 67, 31, 87, 88 ,72, 47, 16, 90, 89, 10, 70,56 };
            int cnt_comp = 0;
            List<int> num_comp = new List<int>();
            int max = Split(numbers, out num_comp);

/* When an array calls for split function.
 * The split function splits the passed Array into 2 and returns the maximum of the passed array.
 * So, in the end the function splits out the maximum value(Max) of the Parent Array.
 * 
 * Also, the we are maintaining a list of the values that the maximum value(Max) is being compared to.
 * We can be sure that in the list we are maintaining the Second maximum value in the parent array will be present
 * because at some point in the levels we will make this comparison.*/
            int Split(int[] nums, out List<int> max_num_comp)
            {

                int len = nums.Length;
                if (len == 1)
                {
                    List<int> empty = new List<int> { 0 };
                    max_num_comp = empty;
                    return nums[0];
                }
                else
                {
                    int mid = len / 2;
                    int l_len;
                    int r_len;

                    if (len % 2 != 0)
                    {
                        l_len = len / 2;
                        r_len = (len / 2) + 1;
                    }
                    else
                    {
                        l_len = len / 2;
                        r_len = len / 2;
                    }

                    int[] l_nums = new int[l_len];
                    int[] r_nums = new int[r_len];

                    for (int i = 0; i < l_len; i++)
                    {
                        l_nums[i] = nums[i];
                    }
                    for (int i = 0; i < r_len; i++)
                    {
                        r_nums[i] = nums[l_len + i];
                    }
                    int l_max = Split(l_nums, out max_num_comp);
                    List<int> l_num_comp = max_num_comp;
                    int r_max = Split(r_nums, out max_num_comp);
                    List<int> r_num_comp = max_num_comp;

                    //if (l_max == 89 || r_max == 89)
                    //{
                    //    cnt_comp++;
                    //    if (l_max == 89)
                    //        num_comp.Add(r_max);
                    //    else
                    //        num_comp.Add(l_max);
                    //}

                    if (l_max >= r_max)
                    {
                        cnt_comp++;
                        l_num_comp.Add(r_max);
                        max_num_comp = l_num_comp;
                        return l_max;
                    }
                    else
                    {
                        cnt_comp++;
                        r_num_comp.Add(l_max);
                        max_num_comp = r_num_comp;
                        return r_max;
                    }

                }
            }

            num_comp.RemoveAt(0);
            foreach (int i in num_comp)
            {
                Console.WriteLine(i);
            }

            int second_max = 0;
            //foreach(int i in num_comp)
            //{
            //    cnt_comp++;
            //    if(i>second_max)
            //    {
            //        second_max = i;
            //    }
            //}
            for(int i=0;i<num_comp.Count -1 ;i++)
            {
                cnt_comp++;
                if (num_comp[i] > num_comp[i + 1])
                    second_max = num_comp[i];
            }

            Console.WriteLine();

            Console.WriteLine("second max is {0} and no of comparisons is {1}", second_max, cnt_comp);
            Console.ReadKey();

        }
    }
}
