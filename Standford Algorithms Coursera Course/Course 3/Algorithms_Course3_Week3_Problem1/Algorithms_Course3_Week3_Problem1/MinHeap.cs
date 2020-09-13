using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Course3_Week3_Problem1
{
    class MinHeap
    {
        public int capacity;
        public int heapSize;
       // public int[] weights;
        public Symbols[] symbols;

        public MinHeap(int cap)
        {
            this.capacity = cap;
           // this.weights = new int[cap];
            this.symbols = new Symbols[cap];
            this.heapSize = 0;
        }

        public void Insert(Symbols sym)
        {
            if (heapSize - 1 >= capacity)
            {
                Console.WriteLine("exceeded the capcaity of the heap");
            }
            else
            {
                //weights[heapSize] = n;
                symbols[heapSize] = sym;
              //  symbols[heapSize].symbol = sy ;
              //  symbols[heapSize].weight = wt; 
                int childIndex = heapSize;
                int parentIndex = GetParent(childIndex);
                while (childIndex > 0 && symbols[childIndex].weight < symbols[parentIndex].weight)
                {
                    Swap(childIndex, parentIndex);
                    childIndex = parentIndex;
                    parentIndex = GetParent(childIndex);
                }
                heapSize++;
            }
        }

        public Symbols getMin()
        {
            return symbols[0];
        }
        public Symbols ExtractMin()
        {
            Symbols rootValue = symbols[0];
            if (heapSize == 1)
            {
                heapSize--;
                return rootValue;
            }
            symbols[0] = symbols[heapSize - 1];
            heapSize--;
            MinHeapify(0);
            return rootValue;
        }

        public void MinHeapify(int index)
        {
            int indexOfMinValue = index;
            int leftChildIndex = GetLeftChild(index);
            int rightChildIndex = GetRightChild(index);

            if (leftChildIndex <= (heapSize - 1) && symbols[leftChildIndex].weight < symbols[indexOfMinValue].weight)
            {
                indexOfMinValue = leftChildIndex;
            }
            if (rightChildIndex <= (heapSize - 1) && symbols[rightChildIndex].weight < symbols[indexOfMinValue].weight)
            {
                indexOfMinValue = rightChildIndex;
            }

            if (indexOfMinValue != index)
            {

                Swap(index, indexOfMinValue);
                MinHeapify(indexOfMinValue);

            }
        }

        public void Swap(int index1, int index2)
        {
            Symbols temp = symbols[index1];
            symbols[index1] = symbols[index2];
            symbols[index2] = temp;
        }

        public int GetLeftChild(int parentIndex)
        {
            return (2 * parentIndex + 1);
        }

        public int GetRightChild(int parentIndex)
        {
            return (2 * parentIndex + 2);
        }

        public int GetParent(int childIndex)
        {
            return (childIndex - 1) / 2;
        }


    }
}
