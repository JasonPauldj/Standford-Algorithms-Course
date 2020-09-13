using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace Algorithms_Course3_Week2_Problem1
{
    //no of nodes =500 and no of clusters=4;
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\joshu\Desktop\TextFiles\clustering1.txt";
            string[] lines = File.ReadAllLines(path);
            Edges[] edges = new Edges[lines.Length + 1];
            int _noOfEdges = 0;
            int _noOfNodes = 500;
            int _maxSpacing = 0;
            foreach (var s in lines)
            {
                _noOfEdges++;
                edges[_noOfEdges] = new Edges();
                string[] _nodesAndDistances = s.Split(' ');

                edges[_noOfEdges].node1 = Convert.ToInt16(_nodesAndDistances[0]);
                edges[_noOfEdges].node2 = Convert.ToInt16(_nodesAndDistances[1]);
                edges[_noOfEdges].dist = Convert.ToInt16(_nodesAndDistances[2]);
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            QuickSort(1, edges.Length - 1);

            int[] _parentArray = new int[_noOfNodes + 1];
            Hashtable _groupSizes = new Hashtable();


            for(int i=1; i< edges.Length;i++)
            {
                int _leader1 = FindLeader(edges[i].node1);
                int _leader2 = FindLeader(edges[i].node2);
                if (_noOfNodes > 4)
                {
                    if (_leader1 != _leader2)
                    {
                       // Console.WriteLine("fusing  {0} and  {1} ",edges[i].node1,edges[i].node2);
                        Union(_leader1, _leader2);
                        _noOfNodes--;
                    }
                }
                else
                {
                    if (_leader1 != _leader2)
                    {
                        _maxSpacing = edges[i].dist;
                        break;
                    }
                }
            }

            for(int i=1;i<_parentArray.Length;i++)
            {
                if (_parentArray[i] == 0)
                {
                   // Console.WriteLine(i);
                    //Console.WriteLine(_groupSizes[i]);
                 //   Console.WriteLine();
                }
              // Console.WriteLine(_parentArray[i]);
            }

            Console.WriteLine("the max spacing is {0}",_maxSpacing);
            Console.WriteLine("the elapsed milliseconds is {0}",stopwatch.ElapsedMilliseconds);

            int FindLeader(int node)
            {
                while(_parentArray[node]!=0)
                {
                    node = _parentArray[node];
                }
                return node;
            }

            void Union(int _lead1, int _lead2)
            {
                int _grpSize1 = _groupSizes.ContainsKey(_lead1) ? (int)_groupSizes[_lead1] : 1;
                int _grpSize2 = _groupSizes.ContainsKey(_lead2) ? (int)_groupSizes[_lead2] : 1;



                if(_grpSize1 > _grpSize2)
                {
                    _parentArray[_lead2] = _lead1;
                    _groupSizes[_lead1] = _grpSize1 + _grpSize2;
                }
                else
                {
                    if (_grpSize1 == 1 && _grpSize2 == 1)
                    {
                        _groupSizes.Add(_lead2, 2);
                        _parentArray[_lead1] = _lead2;
                    }
                    else
                    {
                        _parentArray[_lead1] = _lead2;
                        _groupSizes[_lead2] = _grpSize2 + _grpSize1;
                    }
                }
            }
           /* foreach(var e in edges)
            {
                if (e != null)
                {
                    Console.WriteLine(e.dist);
                    Console.WriteLine(e.node1);
                }
            }*/




            void QuickSort(int start_pos, int last_pos)
            {
                int len = last_pos - start_pos + 1;
                if (len > 1)
                {
                    int i = start_pos;

                    Edges pivot = edges[start_pos];

                    for (int j = start_pos + 1; j <= last_pos; j++)
                    {
                        if (edges[j].dist < pivot.dist)
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
                 Edges temp = edges[i];
                 edges[i] = edges[j];
                 edges[j] = temp;

              /*  int _temp_node1 = edges[i].node1;
                int _temp_node2 = edges[i].node2;
                int _temp_dist = edges[i].dist;

                edges[i].node1 = edges[j].node1;
                edges[i].node2 = edges[j].node2;
                edges[i].dist = edges[j].dist;

                edges[j].node1 = _temp_node1;
                edges[j].node2 = _temp_node2;
                edges[j].dist = _temp_dist*/
            }
            

            Console.ReadKey();
        }
    }
}
