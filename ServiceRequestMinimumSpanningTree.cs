using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServicesApp
{
    public class ServiceRequestMinimumSpanningTree
    {
        public class Edge
        {
            public int FromRequestId { get; set; }
            public int ToRequestId { get; set; }
            public int Weight { get; set; }

            public Edge(
                int fromRequestId,
                int toRequestId,
                int weight)
            {
                FromRequestId = fromRequestId;
                ToRequestId = toRequestId;
                Weight = weight;
            }
        }

        private readonly List<Edge> edges =
            new List<Edge>();

        public void AddEdge(
            int fromRequestId,
            int toRequestId,
            int weight)
        {
            if (fromRequestId == toRequestId)
            {
                return;
            }

            edges.Add(
                new Edge(
                    fromRequestId,
                    toRequestId,
                    weight));
        }

        public List<Edge> GenerateMST()
        {
            List<Edge> minimumSpanningTree =
                new List<Edge>();

            if (edges.Count == 0)
            {
                return minimumSpanningTree;
            }

            HashSet<int> vertices =
                new HashSet<int>();

            foreach (Edge edge in edges)
            {
                vertices.Add(edge.FromRequestId);
                vertices.Add(edge.ToRequestId);
            }

            Dictionary<int, int> parent =
                new Dictionary<int, int>();

            foreach (int vertex in vertices)
            {
                parent[vertex] = vertex;
            }

            List<Edge> sortedEdges =
                edges
                .OrderBy(edge => edge.Weight)
                .ToList();

            foreach (Edge edge in sortedEdges)
            {
                int fromParent =
                    FindParent(parent, edge.FromRequestId);

                int toParent =
                    FindParent(parent, edge.ToRequestId);

                if (fromParent != toParent)
                {
                    minimumSpanningTree.Add(edge);

                    parent[fromParent] = toParent;
                }

                if (minimumSpanningTree.Count ==
                    vertices.Count - 1)
                {
                    break;
                }
            }

            return minimumSpanningTree;
        }

        private int FindParent(
            Dictionary<int, int> parent,
            int requestId)
        {
            if (parent[requestId] != requestId)
            {
                parent[requestId] =
                    FindParent(
                        parent,
                        parent[requestId]);
            }

            return parent[requestId];
        }

        // Validate the generated Minimum Spanning Tree.
        public bool ValidateMST()
        {
            HashSet<int> vertices = new HashSet<int>();

            foreach (Edge edge in edges)
            {
                vertices.Add(edge.FromRequestId);
                vertices.Add(edge.ToRequestId);
            }

            // An empty graph or a single isolated vertex is valid.
            if (vertices.Count <= 1)
                return true;

            List<Edge> mst = GenerateMST();

            // A connected MST must contain V - 1 edges.
            if (mst.Count != vertices.Count - 1)
                return false;

            Dictionary<int, int> parent =
                new Dictionary<int, int>();

            foreach (int vertex in vertices)
                parent[vertex] = vertex;

            // Check that the selected edges contain no cycles.
            foreach (Edge edge in mst)
            {
                int fromParent = FindParent(
                    parent, edge.FromRequestId);

                int toParent = FindParent(
                    parent, edge.ToRequestId);

                if (fromParent == toParent)
                    return false;

                parent[fromParent] = toParent;
            }

            return true;
        }

        // Calculate the total weight of the generated MST.
        public int GetMSTTotalWeight()
        {
            return GenerateMST().Sum(edge => edge.Weight);
        }


    }
}