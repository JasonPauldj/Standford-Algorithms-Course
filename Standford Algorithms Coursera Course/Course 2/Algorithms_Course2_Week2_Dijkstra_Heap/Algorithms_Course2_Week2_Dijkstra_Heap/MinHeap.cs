using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Course2_Week2_Dijkstra_Heap
{
    class MinHeap
    {
        public int _capacity;
        public VertsDijkstraScore[] vertsScores;
        public int[] verts_ind = new int[201];
        public int heapsize;

        public MinHeap(int s)
        {
            _capacity = s;
            heapsize = s;
            vertsScores = new VertsDijkstraScore[_capacity];
            for(int i=0;i<_capacity;i++)
            {
                vertsScores[i] = new VertsDijkstraScore();
                vertsScores[i].vert = i + 2;
                vertsScores[i].score = int.MaxValue;

                verts_ind[i + 2] = i;
            }
        }

        public void DeleteKey(int v)
        {
            DecreaseKey(v, int.MinValue);
            ExtractMin();
        }

        public void DecreaseKey(int v, int val)
        {
            // _heapVertices[k] = val;
            int vert1 = v;
            int k = verts_ind[vert1];
            vertsScores[k].score = val;

            while (k != 0 && vertsScores[GetParent(k)].score > vertsScores[k].score)
            {
                int vert2 = vertsScores[GetParent(k)].vert;
                verts_ind[vert1] = GetParent(k);
                verts_ind[vert2] = k;

                int temp_score = vertsScores[k].score;
                int temp_vert = vertsScores[k].vert;

                vertsScores[k].score = vertsScores[GetParent(k)].score;
                vertsScores[GetParent(k)].score = temp_score;
               
                
                vertsScores[k].vert = vert2;
                vertsScores[GetParent(k)].vert = vert1;

                k = GetParent(k);

            }

        }
        public int[] ExtractMin()
        {

            if(heapsize <=0)
            {
                return new int[] { 0, 0 };
            }

            int [] ret_val = new int[] { vertsScores[0].vert, vertsScores[0].score };

            if(heapsize == 1)
            {
                heapsize--;
                return ret_val;
            }

            vertsScores[0].score = vertsScores[heapsize - 1].score;
            vertsScores[0].vert = vertsScores[heapsize - 1].vert;
            heapsize--;
            MinHeapify(0);
            return ret_val;

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

        public void MinHeapify(int k)
        {
            int l_ind = GetLeftChild(k);
            int r_ind = GetRightChild(k);
            int smallest_ind = k;

            if(l_ind < heapsize && vertsScores[l_ind].score < vertsScores[k].score)
            {
                smallest_ind = l_ind;
            }
            if(r_ind < heapsize && vertsScores[r_ind].score < vertsScores[smallest_ind].score)
            {
                smallest_ind = r_ind;
            }

            if(smallest_ind != k)
            {
                int vert1 = vertsScores[k].vert;
                int vert2 = vertsScores[smallest_ind].vert;
                verts_ind[vert1] = smallest_ind;
                verts_ind[vert2] = k;

                int temp_score = vertsScores[k].score;
                int temp_vert = vertsScores[k].vert;

                vertsScores[k].score = vertsScores[smallest_ind].score;
                vertsScores[smallest_ind].score = temp_score;


                vertsScores[k].vert = vertsScores[smallest_ind].vert;
                vertsScores[smallest_ind].vert = temp_vert;
                MinHeapify(smallest_ind);
            }


        }
    }
}
