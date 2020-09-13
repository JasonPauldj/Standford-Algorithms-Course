using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Course3_Week3_Problem1
{
    class Symbols
    {
        public int symbol;
        public int weight;
        //public List<int> subsymbols
        public Symbols left;
        public Symbols right;

        public Symbols (int sy,int wt,Symbols left, Symbols right)
        {
            this.symbol = sy;
            this.weight = wt;
            this.right = left;
            this.left = right;
        }
    }
}
