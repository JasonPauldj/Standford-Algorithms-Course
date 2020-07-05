using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Karger_Algorithm
{
    class Program
    {
        public class edge
        {
            public int src;
            public int des;
        }

        public class VerticeRepresent
        {

            public int ver; //the vertice itself

        }
        static void addEdge(LinkedList<int>[] adj, int v, int u)
        {
            //adj[u-1].AddLast(v);
            adj[v].AddLast(u - 1);
        }

        static void Main(string[] args)
        {
            //initializing the path to get the lines from the text file
            
            
            //creating an array of linkedlist - each item(i) in this array is a represention of the vertex i and the linkedlist nodes is what it is connected to
            // LinkedList<int>[] vertices = new LinkedList<int>[v];

            //initializing the vertices linkedlist array to have linkedlist as the items i.e v[i] is a linked list.
            

            int trials = 1;
            int min = 2518;
            while (trials <= 1000444)
            {
                string path = @"C:\Users\joshu\Desktop\KragerAlgorithm";
                string[] lines = File.ReadAllLines(path);
    
                int no_of_edges = 0;
                int v = lines.Length;

                int ans = 0;
                List<edge> edges = new List<edge>();
                //finding the edges and populating the edges of a vertice i.e vertices[i] (which is a node of linkedlist vertices)
                for (int i = 0; i < lines.Length; i++)
                {
                    FindVerticesinLine(lines[i], i);
                }

                //function to find the edges of a vertice(Vertices[i]) and adding it as a LinkedListNode
                void FindVerticesinLine(string s, int i)
                {
                    string num = "";
                    foreach (char c in s)
                    {
                        if (c != '\t')
                        {
                            num += c;
                            //Console.WriteLine("num is {0}",num);
                        }
                        else
                        {
                            if ((i + 1).ToString() != num)
                            {
                                int adj_ver = Convert.ToInt16(num);
                                //addEdge(vertices, i, adj_ver);
                                if (i < adj_ver)
                                {
                                    no_of_edges++;
                                    edge e = new edge();
                                    e.src = i;
                                    e.des = adj_ver - 1;
                                    edges.Add(e);
                                }
                                num = "";
                            }
                            else
                            {
                                num = "";
                            }
                        }
                    }

                }

               // no_of_edges = no_of_edges;
                //Console.WriteLine("no of edges is {0}", no_of_edges);



                VerticeRepresent[] verts = new VerticeRepresent[v];
                for (int i = 0; i < v; i++)
                {
                    verts[i] = new VerticeRepresent();
                    verts[i].ver = i;
                }
                Random random = new Random();

                int no_of_vertices = v;

                while (no_of_vertices > 2)
                {
                    KragerAlgorithm();
                    //Console.WriteLine("no of vertices is {0}",no_of_vertices);
                    //Console.WriteLine("no of edges is {0}",no_of_edges);
                }

                void KragerAlgorithm()
                {
                    edge random_Edge = edges[random.Next() % edges.Count];
                    int src_random_Edge = GetVertice(random_Edge.src);
                    int des_random_Edge = GetVertice(random_Edge.des);

                    //checking that i is not contracted
                    if (src_random_Edge != des_random_Edge)
                    {
                        //getting a random edge

                        verts[des_random_Edge].ver = src_random_Edge;
                        no_of_vertices--;

                    }


                }

                for (int i = 0; i < edges.Count; i++)
                {
                    if (GetVertice(edges[i].src) != GetVertice(edges[i].des))
                    {
                        ans++;
                    }
                }

                if (ans < min)
                {
                    min = ans;
                    using (StreamWriter sw = File.AppendText(@"C:\Users\joshu\Desktop\ans"))
                    {
                        sw.WriteLine(min);

                    }
                    Console.WriteLine(min);
                }

                int GetVertice(int edge_ver)
                {
                    while (edge_ver != verts[edge_ver].ver)
                    {
                        edge_ver = verts[edge_ver].ver;
                    }
                    return edge_ver;
                }

                trials++;
            }


            Console.ReadKey();
        }
    }
}

