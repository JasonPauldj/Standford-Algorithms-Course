using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Course3_Week1_Problem3
{
    class MinHeap
    {
        public int _capacity;
        public int heapsize;
        public int[] verts_ind = new int[501];
        public VertsScore[] keys;

        public MinHeap(int s)
        {
            _capacity = s;
            heapsize = s;
            keys = new VertsScore[s];
            for (int i = 0; i < _capacity; i++)
            {
                keys[i] = new VertsScore();
                keys[i].score = int.MaxValue;
                keys[i].vert = i + 2;
                verts_ind[i + 2] = i;
            }
        }


        public void DecreaseKey(int vert1, int val)
        {
            // _heapVertices[k] = val;
            // int vert1 = v;
            int vert1_index = verts_ind[vert1];
            int k = vert1_index;
            keys[k].score = val;
            while (k != 0 && (keys[GetParent(k)].score> keys[k].score))
            {
                int vert2 = keys[GetParent(k)].vert;
                int vert2_index = GetParent(k);

                verts_ind[vert1] = vert2_index;
                verts_ind[vert2] = k;

                int temp = keys[k].score;
                keys[k].score = keys[GetParent(k)].score;
                keys[GetParent(k)].score = temp;

                int temp_ver = keys[k].vert;
                keys[k].vert = vert2;
                keys[GetParent(k)].vert = vert1;

                k = GetParent(k);

            }

        }

        public int[] ExtractMin()
        {

            if (heapsize <= 0)
            {
                return new int[] { 0, 0 };
            }

            int[] ret_val = new int[] { keys[0].vert, keys[0].score };

            if (heapsize == 1)
            {
                heapsize--;
                return ret_val;
            }

            keys[0].score = keys[heapsize - 1].score;
            keys[0].vert = keys[heapsize - 1].vert;
            heapsize--;
            MinHeapify(0);
            return ret_val;

        }

        public void MinHeapify(int k)
        {
            int l_ind = GetLeftChild(k);
            int r_ind = GetRightChild(k);
            int smallest_ind = k;

            if (l_ind < heapsize && keys[l_ind].score < keys[k].score)
            {
                smallest_ind = l_ind;
            }
            if (r_ind < heapsize && keys[r_ind].score < keys[smallest_ind].score)
            {
                smallest_ind = r_ind;
            }

            if (smallest_ind != k)
            {
                int vert1 = keys[k].vert;
                int vert2 = keys[smallest_ind].vert;
                verts_ind[vert1] = smallest_ind;
                verts_ind[vert2] = k;

                int temp_score = keys[k].score;
                int temp_vert = keys[k].vert;

                keys[k].score = keys[smallest_ind].score;
                keys[smallest_ind].score = temp_score;


                keys[k].vert = keys[smallest_ind].vert;
                keys[smallest_ind].vert = temp_vert;
                MinHeapify(smallest_ind);
            }


        }

        public int GetParent(int k)
        {
            return (k - 1) / 2;
        }

        public int GetLeftChild(int k)
        {
            return 2 * k + 1;
        }

        public int GetRightChild(int k)
        {
            return 2 * k + 2;
        }


    }
}
