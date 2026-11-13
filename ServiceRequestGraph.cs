using System;
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    public class ServiceRequestGraph
    {
        private readonly Dictionary<int, List<int>> adjacencyList =
            new Dictionary<int, List<int>>();

        public void AddRequest(int requestId)
        {
            if (!adjacencyList.ContainsKey(requestId))
            {
                adjacencyList[requestId] = new List<int>();
            }
        }

        public void AddConnection(int requestId1, int requestId2)
        {
            AddRequest(requestId1);
            AddRequest(requestId2);

            if (!adjacencyList[requestId1].Contains(requestId2))
            {
                adjacencyList[requestId1].Add(requestId2);
            }

            if (!adjacencyList[requestId2].Contains(requestId1))
            {
                adjacencyList[requestId2].Add(requestId1);
            }
        }

        public List<int> BreadthFirstTraversal(int startRequestId)
        {
            List<int> traversal = new List<int>();

            if (!adjacencyList.ContainsKey(startRequestId))
            {
                return traversal;
            }

            HashSet<int> visited = new HashSet<int>();
            Queue<int> queue = new Queue<int>();

            visited.Add(startRequestId);
            queue.Enqueue(startRequestId);

            while (queue.Count > 0)
            {
                int currentRequestId = queue.Dequeue();
                traversal.Add(currentRequestId);

                foreach (int neighbour in adjacencyList[currentRequestId])
                {
                    if (!visited.Contains(neighbour))
                    {
                        visited.Add(neighbour);
                        queue.Enqueue(neighbour);
                    }
                }
            }

            return traversal;
        }

        public List<int> DepthFirstTraversal(int startRequestId)
        {
            List<int> traversal = new List<int>();
            HashSet<int> visited = new HashSet<int>();

            DepthFirstRecursive(
                startRequestId,
                visited,
                traversal);

            return traversal;
        }

        private void DepthFirstRecursive(
            int requestId,
            HashSet<int> visited,
            List<int> traversal)
        {
            if (!adjacencyList.ContainsKey(requestId) ||
                visited.Contains(requestId))
            {
                return;
            }

            visited.Add(requestId);
            traversal.Add(requestId);

            foreach (int neighbour in adjacencyList[requestId])
            {
                DepthFirstRecursive(
                    neighbour,
                    visited,
                    traversal);
            }
        }

        // Validate graph vertices and bidirectional connections.
        public bool ValidateGraph()
        {
            foreach (KeyValuePair<int, List<int>> entry in adjacencyList)
            {
                foreach (int neighbour in entry.Value)
                {
                    // Every connected request must exist.
                    if (!adjacencyList.ContainsKey(neighbour))
                        return false;

                    // Every connection must have a reverse connection.
                    if (!adjacencyList[neighbour].Contains(entry.Key))
                        return false;
                }
            }

            return true;
        }


    }
}