using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Course2_Week2_Dijkstra_Heap
{
    class AdjacencyList
    {
        public int src_node;
        public Edges[] edges;
        public AdjacencyList(int size)
        {
            edges = new Edges[size];
        }
    }
}
