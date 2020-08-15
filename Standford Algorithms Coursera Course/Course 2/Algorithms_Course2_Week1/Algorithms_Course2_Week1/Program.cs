using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Algorithms_Course2_Week1
{
    class Program
    {
        //1 to 875714 - vertices
        //no of edges - 5105043
        static void Main(string[] args)
        {
            string path = @"C:\Users\joshu\Desktop\TextFiles\Graph_Test";
            string[] lines = File.ReadAllLines(path);
            int n = 10;
            
           List<AdjacencyList> vertices = new List<AdjacencyList>();
            for (int i = 0; i < n; i++)
            {
               vertices.Add(new AdjacencyList(i));
            }

            List<AdjacencyList> rev_vertices = new List<AdjacencyList>();
            for (int i = 0; i < n; i++)
            {
                rev_vertices.Add(new AdjacencyList(i));
            }
            foreach (string line in lines)
            {
                string[] nodes = line.Split(' ');
                int src_node = Convert.ToInt32(nodes[0]);
                int des_node = Convert.ToInt32(nodes[1]);


                vertices[src_node].des_nodes.Add(des_node);
                rev_vertices[des_node].des_nodes.Add(src_node);
            }

            //foreach (var v in rev_vertices)
            //{
            //    Console.WriteLine("src vertex is {0}", v.src_node);
            //    foreach (var num in v.des_nodes)
            //    {
            //        Console.WriteLine("adj vertex is {0}", num);
            //    }
            //    Console.WriteLine();
            //}
            string fullpath = @"C:\Users\joshu\Desktop\TextFiles\running_times";
            bool[] is_explored = new bool[n];
            int[] running_times = new int[n];
            int t = 0;
            for (int i = 1; i < n; i++)
            {
                if (!is_explored[i])
                {
                    //Console.WriteLine("starting from {0}",i);
                    is_explored[i] = true;
                    DFS_rev(rev_vertices, i);
                }
            }


            is_explored = new bool[n];
            int leader;
            int cnt = 0;
            for (int i = n - 1; i > 0; i--)
            {
                leader = running_times[i];
                if (!is_explored[leader])
                {
                    cnt = 1;
                    is_explored[leader] = true;
                    DFS(vertices, leader);
                    Console.WriteLine("the leader is {0} and the no of nodes in SCC are {1}", leader, cnt);
                }
            }



            void DFS_rev(List<AdjacencyList> nodes, int src)
            {

                int cnt_adj_nodes = nodes[src].des_nodes.Count;
                for (int i = 0; i < cnt_adj_nodes; i++)
                {
                    int des_node = nodes[src].des_nodes[i];
                    if (!is_explored[des_node])
                    {
                     
                        //Console.WriteLine("going to node {0}",des_node);
                        is_explored[des_node] = true;
                        DFS_rev(nodes, des_node);
                    }
                }

                t++;
               
                Console.WriteLine("The node exhausted is {0} and running time is {1}", src, t);
                running_times[t] = src;
            }

            void DFS(List<AdjacencyList> nodes, int src)
            {

                int cnt_adj_nodes = nodes[src].des_nodes.Count;
                for (int i = 0; i < cnt_adj_nodes; i++)
                {
                    int des_node = nodes[src].des_nodes[i];
                    if (!is_explored[des_node])
                    {
                       cnt++;
                        //Console.WriteLine("going to node {0}",des_node);
                        is_explored[des_node] = true;
                        DFS(nodes, des_node);
                    }
                }
            }
            
            //for (int i = 1; i < 10; i++)
            //{
            //    Console.WriteLine("the vertex with the running time {0} is {1}", i, running_times[i]);
            //}

            //Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
