using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Algorithms_Course3_Week1_Problem3
{
    class Program
    {
        //500 2184
        static void Main(string[] args)
        {

            string path = @"C:\Users\joshu\Desktop\TextFiles\edges.txt";
            string[] lines = File.ReadAllLines(path);
            int noOfVertices = 500;
            AdjacencyList[] vertices = new AdjacencyList[501];

            for (int i = 1; i < vertices.Length; i++)
            {
                vertices[i] = new AdjacencyList();
            }

            foreach (string line in lines)
            {
                string[] vert_cost = line.Split(' ');
                int vert1 = Convert.ToInt16(vert_cost[0]);
                int vert2 = Convert.ToInt16(vert_cost[1]);
                int cost = Convert.ToInt16(vert_cost[2]);
                Edges edge1 = new Edges(vert2, cost);
                Edges edge2 = new Edges(vert1, cost);

                vertices[vert1].edges.Add(edge1);
                vertices[vert2].edges.Add(edge2);
            }

            Stopwatch stpwatch = new Stopwatch();
            stpwatch.Start();

            bool[] isVertExplored = new bool[501];
            isVertExplored[1] = true;
            MinHeap nodes = new MinHeap(499);

            int overallcost = 0;

            Prim_Algorithm();

            void Prim_Algorithm()
            {
                int cnt = 499;
                int vert1 = 1;
                while(cnt>0)
                {
                    int _noOfAdjacentVerts = vertices[vert1].edges.Count;
                    for(int i=0;i<_noOfAdjacentVerts;i++)
                    {
                        int vert2 = vertices[vert1].edges[i].des_node;
                        if(!isVertExplored[vert2])
                        {
                            int _costOfEdge = vertices[vert1].edges[i].edge_cost;
                            int vert2_index = nodes.verts_ind[vert2];
                            if (_costOfEdge < nodes.keys[vert2_index].score)
                            {
                                //Console.WriteLine("node being passed is {0} and val is {1}",vert2,_costOfEdge);
                                nodes.DecreaseKey(vert2, _costOfEdge);
                            }
                        }
                    }
                    int[] _nextVert_minScore = nodes.ExtractMin();
                    vert1 = _nextVert_minScore[0];
                    Console.WriteLine("vertice being absorbed is {0}",vert1);
                   // Console.WriteLine("score being added is {0}", _nextVert_minScore[1]);
                    overallcost += _nextVert_minScore[1];
                    isVertExplored[vert1] = true;
                    cnt--;
                }
            }

            Console.WriteLine("overall cost is {0}",overallcost );
            Console.WriteLine("time elapsed is {0}",stpwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
