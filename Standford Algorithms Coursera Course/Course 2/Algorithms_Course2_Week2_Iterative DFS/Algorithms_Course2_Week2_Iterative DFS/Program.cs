using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Algorithms_Course2_Week2_Iterative_DFS
{
    class Program
    {
        //1 to 875714 - vertices
        //no of edges - 5105043
        static void Main(string[] args)
        {
            string path = @"C:\Users\joshu\Desktop\TextFiles\Kosaraju_Algorithm.txt";
            string[] lines = File.ReadAllLines(path);
            int n = 875715;

            //making a copy of the graph
            List<AdjacencyList> vertices = new List<AdjacencyList>();
            for (int i = 0; i < n; i++)
            {
                vertices.Add(new AdjacencyList(i));
            }

            //making a copy of the reverse of the graph
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

                //populating the adajcency list representation of the original graph
                vertices[src_node].des_nodes.Add(des_node);

                //populating the adajcency list representation of the transpose of the original graph
                rev_vertices[des_node].des_nodes.Add(src_node);
            }

            //initiating the stack which traces the path. Everytime we do a peek we are going to the node returned.
            Stack<int> vs = new Stack<int>();

            bool[] is_explored = new bool[n];
            bool[] is_labeled = new bool[n];

            int[] running_times = new int[n];
            int t = 0;

            for (int i = 1; i < n; i++)
            {
                if (!is_explored[i])
                {
                    vs.Push(i);
                    Depth_First_Search();
                }
            }



            is_explored = new bool[n];
            List<int> size_scc = new List<int>();
            int leader;
            int cnt = 0;
            for (int i = n - 1; i > 0; i--)
            {
                leader = running_times[i];
                if (!is_explored[leader])
                {
                    cnt = 0;
                    vs.Push(leader);
                    Depth_First_Search_for();
                    size_scc.Add(cnt);

                }
            }

            size_scc.Sort();

            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine(size_scc[size_scc.Count - i]);
            }


            void Depth_First_Search()
            {

                while (vs.Count > 0)
                {
                    //here we are obtaining the top node of the stack. 
                    //Basically we checking if we can proceed with the path going to the node "v".
                    //we can go to "v" only if it is not yet explored (i.e the if statement)
                    int v = vs.Peek();

                    //checking if top node has already been covered.
                    if (!is_explored[v])
                    {

                        is_explored[v] = true;

                        int cnt_adj_nodes = rev_vertices[v].des_nodes.Count;
                        for (int i = 0; i < cnt_adj_nodes; i++)
                        {

                            int des_node = rev_vertices[v].des_nodes[i];
                            if (!is_explored[des_node])
                            {
                                vs.Push(des_node);
                            }

                        }
                    }
                    //if "v" has been explored it means all of its adjacent nodes have been added to the stack.
                    //Therefore it is exhausted for all its adjacent nodes would have been explored(because in stack it is LIFO).
                    // A snap of somewhere in the middle of the stack  - |v||v1||v2||v3|. Here assume v1,v2,v3 are adj nodes of v.
                    //If we are reaching v it means we popped out v1,v2,v3. Hence the adjnodes of |v| are exhausted and we must assign the running time.
                    else if (!is_labeled[v])
                    {
                        running_times[++t] = v;
                        is_labeled[v] = true;
                        vs.Pop();

                    }

                    //if the node is explored and exhausted.
                    else
                        vs.Pop();
                }
            }

            void Depth_First_Search_for()
            {

                while (vs.Count > 0)
                {
                    int v = vs.Peek();

                    if (!is_explored[v])
                    {
                        cnt++;
                        is_explored[v] = true;

                        int cnt_adj_nodes = vertices[v].des_nodes.Count;
                        for (int i = 0; i < cnt_adj_nodes; i++)
                        {

                            int des_node = vertices[v].des_nodes[i];
                            if (!is_explored[des_node])
                            {
                                vs.Push(des_node);
                            }

                        }
                    }
                    else
                        vs.Pop();
                }
            }

            Console.ReadKey();
        }
    }
}
