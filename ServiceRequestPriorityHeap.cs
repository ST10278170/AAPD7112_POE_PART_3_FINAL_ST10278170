
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    public class ServiceRequestPriorityHeap
    {
        private readonly List<ServiceRequest> heap =
            new List<ServiceRequest>();

        public int Count
        {
            get { return heap.Count; }
        }

        // Insert a request into the Max-Heap.
        public void Insert(ServiceRequest request)
        {
            // Prevent null requests from entering the heap.
            if (request == null)
                return;

            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

        // Return the highest-priority request without removing it.
        public ServiceRequest Peek()
        {
            if (heap.Count == 0)
                return null;

            return heap[0];
        }

        // Remove and return the highest-priority request.
        public ServiceRequest RemoveHighestPriority()
        {
            if (heap.Count == 0)
                return null;

            ServiceRequest highestPriority = heap[0];

            int lastIndex = heap.Count - 1;

            heap[0] = heap[lastIndex];
            heap.RemoveAt(lastIndex);

            if (heap.Count > 0)
                HeapifyDown(0);

            return highestPriority;
        }

        // Move a newly inserted request upwards.
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;

                if (GetPriority(heap[index]) <=
                    GetPriority(heap[parentIndex]))
                    break;

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        // Move a request downwards to restore heap order.
        private void HeapifyDown(int index)
        {
            while (true)
            {
                int leftChild = (index * 2) + 1;
                int rightChild = (index * 2) + 2;
                int highest = index;

                if (leftChild < heap.Count &&
                    GetPriority(heap[leftChild]) >
                    GetPriority(heap[highest]))
                {
                    highest = leftChild;
                }

                if (rightChild < heap.Count &&
                    GetPriority(heap[rightChild]) >
                    GetPriority(heap[highest]))
                {
                    highest = rightChild;
                }

                if (highest == index)
                    break;

                Swap(index, highest);
                index = highest;
            }
        }

        // Determine priority according to request category.
        private int GetPriority(ServiceRequest request)
        {
            switch (request.Category)
            {
                case "Water":
                case "Electricity":
                    return 3;

                case "Sanitation":
                case "Roads":
                    return 2;

                default:
                    return 1;
            }
        }

        // Swap two heap elements.
        private void Swap(int firstIndex, int secondIndex)
        {
            ServiceRequest temp = heap[firstIndex];
            heap[firstIndex] = heap[secondIndex];
            heap[secondIndex] = temp;
        }

        // Validate the Max-Heap ordering property.
        public bool ValidateHeap()
        {
            for (int i = 0; i < heap.Count; i++)
            {
                if (heap[i] == null)
                    return false;

                int leftChild = (i * 2) + 1;
                int rightChild = (i * 2) + 2;

                if (leftChild < heap.Count &&
                    GetPriority(heap[i]) <
                    GetPriority(heap[leftChild]))
                {
                    return false;
                }

                if (rightChild < heap.Count &&
                    GetPriority(heap[i]) <
                    GetPriority(heap[rightChild]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
