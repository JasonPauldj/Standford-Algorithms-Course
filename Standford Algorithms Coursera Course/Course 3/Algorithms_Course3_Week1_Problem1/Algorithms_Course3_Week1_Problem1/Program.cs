using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Numerics;

namespace Algorithms_Course3_Week1_Problem1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\joshu\Desktop\TextFiles\jobs.txt";
            string[] lines = File.ReadAllLines(path);

            Jobs[] jobs = new Jobs[lines.Length];
            int index = 0;
            foreach (var line in lines)
            {
                string[] wtlt = line.Split(' ');

                jobs[index] = new Jobs();
                jobs[index].weight = Convert.ToInt16(wtlt[0]);
                jobs[index].length = Convert.ToInt16(wtlt[1]);
                jobs[index].diff = jobs[index].weight - jobs[index].length;
                jobs[index].ratio =(float) jobs[index].weight / jobs[index].length;
                //Console.WriteLine(jobs[index].diff);
                index++;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            QuickSort(0,lines.Length -1);

            //foreach (var v in jobs)
            //{
            //    Console.WriteLine(v.diff);
            //}

            long completiontime = 0;
            long weightedcompletion = 0;

            for(int i=lines.Length-1;i >=0;i--)
            {
                //Console.WriteLine("the weight of the job is {0} and the length of the job is {1} and the diff is {2}",jobs[i].weight,jobs[i].length,jobs[i].diff);
               // Console.WriteLine("the weight of the job is {0} and the length of the job is {1} and the ratio is {2}", jobs[i].weight, jobs[i].length, jobs[i].ratio);
                completiontime += jobs[i].length;
               // Console.WriteLine("completion time is {0}",completiontime);
               // Console.WriteLine("value being added to {0} is {1}", weightedcompletion, (completiontime * jobs[i].weight));
                weightedcompletion += (completiontime * jobs[i].weight);
            }

            Console.WriteLine(weightedcompletion);

            void QuickSort(int start_pos, int last_pos)
            {
                int len = last_pos - start_pos + 1;
                if (len > 1)
                {
                    int i = start_pos;

                    Jobs pivot = jobs[start_pos];

                    for (int j = start_pos + 1; j <= last_pos; j++)
                    {
                        if (jobs[j].ratio < pivot.ratio)
                        {
                            Swap(i + 1, j);
                            i++;
                        }
                        //else if (jobs[j].diff == pivot.diff)
                        //{

                        //    if (jobs[j].weight > pivot.weight)
                        //    {
                        //        Swap(start_pos, j);
                        //    }
                        //    Swap(i + 1, j);
                        //    i++;
                        //}

                    }
                    Swap(i, start_pos);
                    QuickSort(start_pos, i - 1);
                    QuickSort(i + 1, last_pos);
                }
            }
            void Swap(int i, int j)
            {
                int temp_weight = jobs[j].weight;
                int temp_length = jobs[j].length;
                int temp_diff = jobs[j].diff;
                float temp_ratio = jobs[j].ratio;

                jobs[j].weight = jobs[i].weight;
                jobs[j].length = jobs[i].length;
                jobs[j].diff = jobs[i].diff;
                jobs[j].ratio = jobs[i].ratio;

                jobs[i].weight = temp_weight;
                jobs[i].length = temp_length;
                jobs[i].diff = temp_diff;
                jobs[i].ratio = temp_ratio;
            }

            Console.ReadKey();
        }
    }
}
