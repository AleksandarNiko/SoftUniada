using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08
{
    public class PriorityQueue<T>
    {
        private List<T> elements=new List<T>();
        private readonly IComparer<T> comparer;
        public PriorityQueue()
        {
        }
        public int Count => elements.Count;
        public void Add(T item)
        {
            elements.Add(item);
            int index = Count - 1;
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (comparer.Compare(elements[parentIndex], elements[index]) <= 0)
                    break;
                Swap(index, parentIndex);
                index = parentIndex;
            }
        }
        public T Poll()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty.");
            T front = elements[0];
            elements[0] = elements[Count - 1];
            elements.RemoveAt(Count - 1);
            int index = 0;
            while (true)
            {
                int leftChild = 2 * index + 1;
                if (leftChild >= Count)
                    break;
                int rightChild = leftChild + 1;
                int minChild = (rightChild < Count && comparer.Compare(elements[rightChild], elements[leftChild]) < 0)
                    ? rightChild
                    : leftChild;
                if (comparer.Compare(elements[index], elements[minChild]) <= 0)
                    break;
                Swap(index, minChild);
                index = minChild;
            }
            return front;
        }
        private void Swap(int i, int j)
        {
            T temp = elements[i];
            elements[i] = elements[j];
            elements[j] = temp;
        }
    }
}
