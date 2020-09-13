using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Algorithms_Course3_Week3_Problem1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + @"\huffman.txt"; ;
            string[] lines = File.ReadAllLines(path);
            int noOfSymbols = lines.Length;
            MinHeap heapWeights = new MinHeap(noOfSymbols);
            int[] noOfMergers = new int[noOfSymbols];
            int index = 0;
            foreach (var line in lines)
            {
                Symbols sym = new Symbols(index, Convert.ToInt32(line), null, null);
                heapWeights.Insert(sym);
                index++;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int[] arr = new int[10500];
            //for (int i = 0; i < noOfSymbols - 1; i++)
            //{

            //    Symbols minSymbol1 = heapWeights.ExtractMin();

            //    Symbols minSymbol2 = heapWeights.ExtractMin();


            //    if (metaSymbols[minSymbol1.symbol] != null)
            //    {
            //        foreach (var s in metaSymbols[minSymbol1.symbol].subsymbols)
            //        {
            //            noOfMergers[s]++;
            //        }
            //    }
            //    if (metaSymbols[minSymbol2.symbol] != null)
            //    {
            //        foreach (var s in metaSymbols[minSymbol2.symbol].subsymbols)
            //        {
            //            noOfMergers[s]++;
            //        }
            //    }

            //    noOfMergers[minSymbol1.symbol]++;
            //    noOfMergers[minSymbol2.symbol]++;


            //    Symbols minMergeSymbol = new Symbols(minSymbol2.symbol, minSymbol1.weight + minSymbol2.weight);

            //    minMergeSymbol.weight = minSymbol1.weight + minSymbol2.weight;
            //    minMergeSymbol.symbol = minSymbol2.symbol;

            //    if (metaSymbols[minSymbol2.symbol] == null)
            //    {
            //        metaSymbols[minSymbol2.symbol] = new MetaSymbols();
            //    }
            //    metaSymbols[minSymbol2.symbol].subsymbols.Add(minSymbol1.symbol);
            //    if (metaSymbols[minSymbol1.symbol] != null)
            //    {
            //        metaSymbols[minSymbol2.symbol].subsymbols.AddRange(metaSymbols[minSymbol1.symbol].subsymbols);
            //        metaSymbols[minSymbol1.symbol].subsymbols = null;
            //    }

            //    heapWeights.Insert(minMergeSymbol.symbol, minMergeSymbol.weight);
            //}

            int noOfMerges = 0;
            int ind = 0;
            Merge();
         
            void Merge()
            {
                if (heapWeights.heapSize > 1)
                {
                    Symbols minSymbol1 = heapWeights.ExtractMin();
                    Symbols minSymbol2 = heapWeights.ExtractMin();
             
                    Symbols minMergeSymbol = new Symbols(-1, minSymbol1.weight + minSymbol2.weight,minSymbol1,minSymbol2);
                    heapWeights.Insert(minMergeSymbol);
                  
                    noOfMerges++;
                    Merge();
               
                }
                else
                {
                    PrintCodes(heapWeights.ExtractMin());
                }
            }

          
            void PrintCodes(Symbols root)
            {
              
                if (root.left != null)
                {
                    arr[ind] = 0;
                    ind++;
                    PrintCodes(root.left);
                }

                if (root.right != null)
                {
                    arr[ind] = 1;
                    ind++;
                    PrintCodes(root.right);
                }

                if (root.left == null && root.right == null)
                {
                    Console.WriteLine("the symbol is  {0}",root.symbol);
                    for(int i=0; i<ind;i++)
                    {
                        Console.Write("{0}",arr[i]);
                    }
                    Console.WriteLine("code length is {0}",ind);

                    Console.WriteLine();
                }
                ind = ind - 1;


            }

            QuickSort(0, noOfSymbols-1);

            void QuickSort(int start_pos, int last_pos)
            {
                int len = last_pos - start_pos + 1;
                if (len > 1)
                {
                    int i = start_pos;

                    int pivot = noOfMergers[start_pos];

                    for (int j = start_pos + 1; j <= last_pos; j++)
                    {
                        if (noOfMergers[j] < pivot)
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
                int temp = noOfMergers[i];
                noOfMergers[i] = noOfMergers[j];
                noOfMergers[j] = temp;
            }
            Console.WriteLine("maximum length of encoding is {0}", noOfMergers[noOfSymbols-1]);
            Console.WriteLine("minimum length of encoding is {0}", noOfMergers[0]);
            Console.WriteLine("time taken is {0}", stopwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
