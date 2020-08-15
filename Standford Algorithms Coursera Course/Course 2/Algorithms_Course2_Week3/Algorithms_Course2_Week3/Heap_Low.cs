using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_Course2_Week3
{
    //This heap supports extract_max
    class Heap_Low
    {
        public int capacity;
        public int heapSize;
        public int[] lowerHalf;

        public Heap_Low(int cap)
        {
            this.capacity = cap;
            this.lowerHalf = new int[cap];
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
                lowerHalf[heapSize] = n;
                int childIndex = heapSize;
                int parentIndex = GetParent(childIndex);
                while (childIndex > 0 && lowerHalf[childIndex] > lowerHalf[parentIndex])
                {
                    Swap(childIndex, parentIndex);
                    childIndex = parentIndex;
                    parentIndex = GetParent(childIndex);
                }
                heapSize++;
            }
        }

        public int ExtractMax()
        {
            int rootValue = lowerHalf[0];
            if (heapSize == 1)
            {
                heapSize--;
                return rootValue;
            }
            lowerHalf[0] = lowerHalf[heapSize - 1];
            heapSize--;
            MaxHeapify(0);
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
            int temp = lowerHalf[index1];
            lowerHalf[index1] = lowerHalf[index2];
            lowerHalf[index2] = temp;
        }

        public void MaxHeapify(int index)
        {
            int indexOfMaxValue = index;
            int leftChildIndex = GetLeftChild(index);
            int rightChildIndex = GetRightChild(index);

            if (leftChildIndex <= (heapSize - 1) && lowerHalf[leftChildIndex] > lowerHalf[indexOfMaxValue])
            {
                indexOfMaxValue = leftChildIndex;
            }
            if (rightChildIndex <= (heapSize - 1) && lowerHalf[rightChildIndex] > lowerHalf[indexOfMaxValue])
            {
                indexOfMaxValue = rightChildIndex;
            }

            if (indexOfMaxValue != index)
            {

                Swap(index, indexOfMaxValue);
                MaxHeapify(indexOfMaxValue);

            }
        }

        public int GetMax()
        {
            return this.lowerHalf[0];
        }
    }
}
