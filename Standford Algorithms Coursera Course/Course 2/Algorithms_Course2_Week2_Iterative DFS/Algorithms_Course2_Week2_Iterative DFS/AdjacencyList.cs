using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Course2_Week2_Iterative_DFS
{
    class AdjacencyList
    {
        public int src_node;
        public List<int> des_nodes;

        public AdjacencyList(int n)
        {
            src_node = n;
            des_nodes = new List<int>();
        }
    }
}
