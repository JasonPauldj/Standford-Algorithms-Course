using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Course2_Week3
{
    //This Heap supports extract min
    class Heap_High
    {
        public int capacity;
        public int heapSize;
        public int[] upperHalf;

        public Heap_High(int cap)
        {
            this.capacity = cap;
            this.upperHalf = new int[cap];
            this.heapSize = 0;
        }

        public void Insert(int n)
        {
            if (heapSize - 1 >= capacity)
            {
                Console.WriteLine("exceeded the capcaity of the heap");
            }
            else
            {
                upperHalf[heapSize] = n;
                int childIndex = heapSize;
                int parentIndex = GetParent(childIndex);
                while (childIndex > 0 && upperHalf[childIndex] < upperHalf[parentIndex])
                {
                    Swap(childIndex, parentIndex);
                    childIndex = parentIndex;
                    parentIndex = GetParent(childIndex);
                }
                heapSize++;
            }
        }

        public int ExtractMin()
        {
            int rootValue = upperHalf[0];
            if (heapSize == 1)
            {
                heapSize--;
                return rootValue;
            }
            upperHalf[0] = upperHalf[heapSize - 1];
            heapSize--;
            MinHeapify(0);
            return rootValue;
        }

        public int GetParent(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        public int GetLeftChild(int parentIndex)
        {
            return (2 * parentIndex + 1);
        }

        public int GetRightChild(int parentIndex)
        {
            return (2 * parentIndex + 2);
        }
        public void Swap(int index1, int index2)
        {
            int temp = upperHalf[index1];
            upperHalf[index1] = upperHalf[index2];
            upperHalf[index2] = temp;
        }

        public void MinHeapify(int index)
        {
            int indexOfMinValue = index;
            int leftChildIndex = GetLeftChild(index);
            int rightChildIndex = GetRightChild(index);

            if (leftChildIndex <= (heapSize - 1) && upperHalf[leftChildIndex] < upperHalf[indexOfMinValue])
            {
                indexOfMinValue = leftChildIndex;
            }
            if (rightChildIndex <= (heapSize - 1) && upperHalf[rightChildIndex] < upperHalf[indexOfMinValue])
            {
                indexOfMinValue = rightChildIndex;
            }

            if (indexOfMinValue != index)
            {

                Swap(index, indexOfMinValue);
                MinHeapify(indexOfMinValue);

            }
        }

        public int GetMin()
        {
            return this.upperHalf[0];
        }

    }
}
