using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Algorithms_Course2_Week3
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\joshu\Desktop\TextFiles\Median_Maintenance.txt";
            string[] lines = File.ReadAllLines(path);
            int lineNo = 0;
            double ans = 0;
            int maxOfLowerHalf = int.MaxValue;
            int minOfUpperHalf = int.MinValue;
            Heap_High secondHalf = new Heap_High(10000);
            Heap_Low firstHalf = new Heap_Low(10000);
            foreach (string line in lines)
            {
                lineNo++;

                int n = Convert.ToInt16(line);

                if(n < maxOfLowerHalf)
                {
                    //insert in HLOW
                   
                    firstHalf.Insert(n);
                   
                }
                else
                {
                    //insert in HHIGH
                 
                    secondHalf.Insert(n);
                   
                }
                int firstHalfHeapSize = firstHalf.heapSize;
                int secondHalfHeapSize = secondHalf.heapSize;
                if (lineNo % 2 == 0)
                {
                    //chk if the split is 50-50
                    //median is the maxOfLowerHalf
                    if (firstHalfHeapSize==secondHalfHeapSize)
                    {
                     
                        ans += firstHalf.GetMax();
                    }
                    else if(firstHalfHeapSize > secondHalfHeapSize)
                    {
                        int num = firstHalf.ExtractMax();

              
                        secondHalf.Insert(num);

                        ans += firstHalf.GetMax();
                    }
                    else
                    {
                        int num = secondHalf.ExtractMin();

                        firstHalf.Insert(num);

                        ans += firstHalf.GetMax();
                    }

                }
                else
                {
                    if(secondHalfHeapSize > firstHalfHeapSize)
                    {
                        ans += secondHalf.GetMin();
                      
                    }
                    else
                    {
                        ans += firstHalf.GetMax();
                        
                    }
                }

                minOfUpperHalf = secondHalf.GetMin();
                maxOfLowerHalf = firstHalf.GetMax();

            }

            Console.WriteLine(ans);
            Console.WriteLine(ans%10000);

           // Console.WriteLine(firstHalf.heapSize);
           // Console.WriteLine(secondHalf.heapSize);

            //for (int i = 0; i < firstHalf.heapSize; i++)
            //{
            //    Console.WriteLine(firstHalf.ExtractMax());
            //}
            //Console.WriteLine("*************");
            //for (int i = 0; i < secondHalf.heapSize; i++)
            //{
            //    Console.WriteLine(secondHalf.ExtractMin());
            //}


            Console.ReadKey();
        }
    }
}
