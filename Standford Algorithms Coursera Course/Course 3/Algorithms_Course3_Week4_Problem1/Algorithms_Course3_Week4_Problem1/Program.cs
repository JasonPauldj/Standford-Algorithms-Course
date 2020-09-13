using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Algorithms_Course3_Week4_Problem1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\joshu\Desktop\TextFiles\knapsack1.txt";
            string[] lines = File.ReadAllLines(path);
            int capacity = Convert.ToInt32(lines[0].Split(' ')[0]);
            int noOfItems = Convert.ToInt32(lines[0].Split(' ')[1]);
            // adding 1 to start indexing from 1
            Items[] items = new Items[noOfItems + 1];
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 1; i <= noOfItems; i++)
            {
                string[] valAndWeg = lines[i].Split(' ');
                int val = Convert.ToInt32(valAndWeg[0]);
                int weg = Convert.ToInt32(valAndWeg[1]);

                items[i] = new Items(val, weg);
            }
            int[,] optimalSolutions = new int[noOfItems + 1, capacity + 1];

            for (int i = 1; i <= noOfItems; i++)
            {
                for (int j = 0; j <= capacity; j++)
                {
                    int weight = items[i].weight;
                    int value = items[i].value;
                    if (j >= weight)
                    {
                        optimalSolutions[i, j] = Math.Max(optimalSolutions[i-1,j],optimalSolutions[i-1,j-weight]+value);
                    }
                    else
                    {
                        optimalSolutions[i, j] = optimalSolutions[i - 1, j];
                    }
                }

            }

            Console.WriteLine("the value of the optimal solution is {0}",optimalSolutions[noOfItems,capacity]);
            Console.WriteLine("time taken is {0}",stopwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
