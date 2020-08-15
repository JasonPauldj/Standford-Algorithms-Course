using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Algorithms_Course2_Week2_Dijkstra_Heap
{
    class Program
    {
        static void Main(string[] args)
        {
            // 7,37,59,82,99,115,133,165,188,197
            int[] ans_verts = new int[] { 7, 37, 59, 82, 99, 115, 133, 165, 188, 197 };
            string ans = "";
            string path = @"C:\Users\joshu\Desktop\TextFiles\dijkstraData_Algorithm.txt";
            string[] lines = File.ReadAllLines(path);
            int noOfVertices = lines.Length;
            AdjacencyList[] vertices = new AdjacencyList[lines.Length + 1];
            foreach (string line in lines)
            {

                string[] nodes = line.Split('\t');
                int src_ver = Convert.ToInt16(nodes[0]);
                vertices[src_ver] = new AdjacencyList(nodes.Length - 2);
                vertices[src_ver].src_node = src_ver;
                for (int i = 1; i < nodes.Length - 1; i++)
                {
                    string[] ver_len = (nodes[i]).Split(',');
                    //Console.WriteLine(ver_len[0]);
                    vertices[src_ver].edges[i - 1] = new Edges();
                    vertices[src_ver].edges[i - 1].des_node = Convert.ToInt16(ver_len[0]);
                    vertices[src_ver].edges[i - 1].edge_len = Convert.ToInt16(ver_len[1]);
                }

            }

            Stopwatch stpwatch = new Stopwatch();
            stpwatch.Start();
            bool[] isShortPathCalculated = new bool[noOfVertices + 1];
            int[] shortestPathValues = new int[noOfVertices + 1];
            shortestPathValues[1] = 0;
            isShortPathCalculated[1] = true;
            //int[] verts_ind = new int[noOfVertices + 1];

            //for (int i = 1; i < shortestPathValues.Length; i++)
            //{
            //    shortestPathValues[i] = int.MaxValue;
            //}

            MinHeap Nodes =new MinHeap(noOfVertices - 1);

            DijkstrasAlgorithm();

            foreach (var v in ans_verts)
            {
                ans += (shortestPathValues[v].ToString() + ",");
            }
            Console.WriteLine(ans);
            Console.WriteLine(stpwatch.ElapsedMilliseconds);
            Console.ReadKey();
            void DijkstrasAlgorithm()
            {
                int cnt = noOfVertices - 1;
                int nextvert = 1;
                while( cnt > 0)
                {
                    int shortestPathVal = shortestPathValues[nextvert];
                    int noOfEdges = vertices[nextvert].edges.Length;
                    for(int i=0;i<noOfEdges;i++)
                    {
                        int des_node = vertices[nextvert].edges[i].des_node;
                        if (!isShortPathCalculated[des_node])
                        {
                            int index = Nodes.verts_ind[des_node];
                            if(Nodes.vertsScores[index].score > shortestPathVal + vertices[nextvert].edges[i].edge_len)
                            {
                                Nodes.DecreaseKey(des_node, shortestPathVal + vertices[nextvert].edges[i].edge_len);
                            }
                        }
                    }

                    int[] min_vert_score = Nodes.ExtractMin();

                    nextvert = min_vert_score[0];
                    shortestPathValues[nextvert] = min_vert_score[1];

                    //Console.WriteLine(nextvert);
                    cnt--;
                    isShortPathCalculated[nextvert] = true;
                }
            }


        }
    }
}
