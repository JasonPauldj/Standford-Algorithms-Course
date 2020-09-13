using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Course3_Week1_Problem3
{
    class Edges
    {
        public int des_node;
        public int edge_cost;

        public Edges(int des, int cost)
        {
            des_node = des;
            edge_cost = cost;
        }
    }
}
