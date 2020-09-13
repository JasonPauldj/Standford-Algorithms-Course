using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
namespace Algorithms_Course3_Week3_Problem3
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + @"\mwis.txt";
            string[] lines = File.ReadAllLines(path);
            int noOfVertices = Convert.ToInt32(lines[0]);
            int[] maxWt = new int[noOfVertices + 1];
            int[] vertInSol = new int[noOfVertices + 1];
            int[] vertPos = new int[] { 1, 2, 3, 4, 17, 117, 517, 997 };
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            maxWt[0] = 0;
            maxWt[1] = Convert.ToInt32(lines[1]);
            for (int i = 2; i < maxWt.Length; i++)
            {
                maxWt[i] = Math.Max(maxWt[i - 1], maxWt[i - 2] + Convert.ToInt32(lines[i]));
            }

            int index = maxWt.Length - 1;
            while (index >= 2)
            {
                int vertexWeight = 0;
                if (index > 0)
                {
                   vertexWeight = Convert.ToInt32(lines[index]);
                }
                if (maxWt[index - 1] >= maxWt[index - 2] + vertexWeight )
                {
                    index--;
                }
                else
                {
                    vertInSol[index] = 1;
                    index -= 2;
                }
            }

            //if vertex 2 is 0 then vertex 1 is 1 and vice versa
              foreach(var p in vertPos)
              {
                  Console.WriteLine(vertInSol[p]);
              }
              

            Console.WriteLine("the time taken for execution is {0}", stopwatch.ElapsedMilliseconds);

            Console.ReadKey();
        }
    }
}
