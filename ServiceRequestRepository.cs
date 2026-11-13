
using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServicesApp
{
    public static class ServiceRequestRepository
    {
        // Dictionary provides fast lookup using unique Request IDs.
        private static readonly Dictionary<int, ServiceRequest> serviceRequests =
            new Dictionary<int, ServiceRequest>();

        // Queue maintains the order in which requests are submitted.
        private static readonly Queue<ServiceRequest> requestQueue =
            new Queue<ServiceRequest>();

        // SortedDictionary groups requests according to status.
        private static readonly SortedDictionary<string, List<ServiceRequest>>
            requestsByStatus =
                new SortedDictionary<string, List<ServiceRequest>>();

        // Binary Search Tree organises requests by Request ID.
        private static ServiceRequestBinarySearchTree requestTree =
            new ServiceRequestBinarySearchTree();

        // AVL Tree maintains balanced request storage.
        private static ServiceRequestAVLTree requestAVLTree =
            new ServiceRequestAVLTree();

        // Red-Black Tree maintains balanced request storage.
        private static ServiceRequestRedBlackTree requestRedBlackTree =
            new ServiceRequestRedBlackTree();

        // Max-Heap prioritises important service requests.
        private static ServiceRequestPriorityHeap priorityHeap =
            new ServiceRequestPriorityHeap();

        // Graph represents relationships between service requests.
        private static ServiceRequestGraph requestGraph =
            new ServiceRequestGraph();

        // Minimum Spanning Tree handles weighted request relationships.
        private static ServiceRequestMinimumSpanningTree requestMST =
            new ServiceRequestMinimumSpanningTree();


        // Initialise the repository with sample service requests.
        static ServiceRequestRepository()
        {
            AddRequest(new ServiceRequest(
                1001,
                "Water leak reported near residential property.",
                "Water",
                "Cape Town",
                "Pending",
                DateTime.Now));

            AddRequest(new ServiceRequest(
                1002,
                "Streetlight is not working.",
                "Electricity",
                "Cape Town",
                "In Progress",
                DateTime.Now));

            AddRequest(new ServiceRequest(
                1003,
                "Waste collection was missed.",
                "Waste Management",
                "Cape Town",
                "Completed",
                DateTime.Now));
        }


        // Generate the next available Request ID.
        public static int GetNextRequestId()
        {
            if (serviceRequests.Count == 0)
                return 1001;

            return serviceRequests.Keys.Max() + 1;
        }


        // Add a new service request to all relevant data structures.
        // Returns false if the request is invalid or its ID already exists.
        public static bool AddRequest(ServiceRequest request)
        {
            // Reject null requests.
            if (request == null)
                return false;

            // Reject invalid Request IDs.
            if (request.RequestId <= 0)
                return false;

            // Reject requests without a valid status.
            if (string.IsNullOrWhiteSpace(request.Status))
                return false;

            // Prevent duplicate Request IDs.
            if (serviceRequests.ContainsKey(request.RequestId))
                return false;

            // Connect the new request to existing requests.
            foreach (ServiceRequest existingRequest in serviceRequests.Values)
            {
                // Connect requests that share the same category.
                if (existingRequest.Category == request.Category)
                {
                    requestGraph.AddConnection(
                        existingRequest.RequestId,
                        request.RequestId);
                }

                // Calculate the relationship weight for the MST.
                int weight = CalculateRelationshipWeight(
                    existingRequest,
                    request);

                requestMST.AddEdge(
                    existingRequest.RequestId,
                    request.RequestId,
                    weight);
            }

            // Store the request in the Dictionary.
            serviceRequests.Add(request.RequestId, request);

            // Store the request in the submission Queue.
            requestQueue.Enqueue(request);

            // Insert the request into the Binary Search Tree.
            requestTree.Insert(request);

            // Insert the request into the AVL Tree.
            requestAVLTree.Insert(request);

            // Insert the request into the Red-Black Tree.
            requestRedBlackTree.Insert(request);

            // Insert the request into the Priority Max-Heap.
            priorityHeap.Insert(request);

            // Add the request as a graph vertex.
            requestGraph.AddRequest(request.RequestId);

            // Group the request according to its status.
            if (!requestsByStatus.ContainsKey(request.Status))
            {
                requestsByStatus[request.Status] =
                    new List<ServiceRequest>();
            }

            requestsByStatus[request.Status].Add(request);

            return true;
        }


        // Update the details of an existing service request.
        // The Request ID, status and submission date remain unchanged.
        public static bool UpdateRequestDetails(
            int requestId,
            string description,
            string category,
            string location)
        {
            // Find the existing request.
            if (!serviceRequests.TryGetValue(
                requestId,
                out ServiceRequest request))
            {
                return false;
            }

            // Validate the updated information.
            if (string.IsNullOrWhiteSpace(description) ||
                string.IsNullOrWhiteSpace(category) ||
                string.IsNullOrWhiteSpace(location))
            {
                return false;
            }

            // Update the existing request.
            request.Description = description.Trim();
            request.Category = category.Trim();
            request.Location = location.Trim();

            // Rebuild the affected data structures.
            RebuildRequestDataStructures();

            return true;
        }


        // Rebuild the data structures after request details change.
        private static void RebuildRequestDataStructures()
        {
            // Create fresh data structures.
            requestTree = new ServiceRequestBinarySearchTree();
            requestAVLTree = new ServiceRequestAVLTree();
            requestRedBlackTree = new ServiceRequestRedBlackTree();
            priorityHeap = new ServiceRequestPriorityHeap();
            requestGraph = new ServiceRequestGraph();
            requestMST = new ServiceRequestMinimumSpanningTree();

            // Retrieve all requests from the Dictionary.
            List<ServiceRequest> allRequests =
                serviceRequests.Values.ToList();

            // Reinsert requests into the trees, heap and graph.
            foreach (ServiceRequest request in allRequests)
            {
                requestTree.Insert(request);
                requestAVLTree.Insert(request);
                requestRedBlackTree.Insert(request);
                priorityHeap.Insert(request);
                requestGraph.AddRequest(request.RequestId);
            }

            // Rebuild graph connections and MST edges.
            for (int i = 0; i < allRequests.Count; i++)
            {
                for (int j = i + 1; j < allRequests.Count; j++)
                {
                    ServiceRequest firstRequest = allRequests[i];
                    ServiceRequest secondRequest = allRequests[j];

                    // Connect requests that share the same category.
                    if (firstRequest.Category == secondRequest.Category)
                    {
                        requestGraph.AddConnection(
                            firstRequest.RequestId,
                            secondRequest.RequestId);
                    }

                    // Recalculate the relationship weight.
                    int weight = CalculateRelationshipWeight(
                        firstRequest,
                        secondRequest);

                    requestMST.AddEdge(
                        firstRequest.RequestId,
                        secondRequest.RequestId,
                        weight);
                }
            }
        }


        // Calculate the weight between two related requests.
        private static int CalculateRelationshipWeight(
            ServiceRequest firstRequest,
            ServiceRequest secondRequest)
        {
            int weight = 3;

            // Same category represents a stronger relationship.
            if (firstRequest.Category == secondRequest.Category)
                weight--;

            // Same location represents a stronger relationship.
            if (firstRequest.Location == secondRequest.Location)
                weight--;

            // Ensure that the weight is at least 1.
            return Math.Max(weight, 1);
        }


        // ==========================================
        // BINARY SEARCH TREE
        // ==========================================

        // Search for a request using the BST.
        public static ServiceRequest FindRequest(int requestId)
        {
            return requestTree.Search(requestId);
        }

        // Retrieve all requests in ascending Request ID order.
        public static List<ServiceRequest> GetAllRequests()
        {
            return requestTree.GetRequestsInOrder();
        }

        // Validate the Binary Search Tree.
        public static bool ValidateBST()
        {
            return requestTree.ValidateBST();
        }


        // ==========================================
        // AVL TREE
        // ==========================================

        // Search using the AVL Tree.
        public static ServiceRequest FindRequestAVL(int requestId)
        {
            return requestAVLTree.Search(requestId);
        }

        // Validate AVL Tree structure and balancing.
        public static bool ValidateAVLTree()
        {
            return requestAVLTree.ValidateAVL();
        }


        // ==========================================
        // RED-BLACK TREE
        // ==========================================

        // Search using the Red-Black Tree.
        public static ServiceRequest FindRequestRedBlack(int requestId)
        {
            return requestRedBlackTree.Search(requestId);
        }

        // Validate Red-Black Tree properties.
        public static bool ValidateRedBlackTree()
        {
            return requestRedBlackTree.ValidateRedBlackTree();
        }


        // ==========================================
        // PRIORITY MAX-HEAP
        // ==========================================

        // Retrieve the highest-priority request.
        public static ServiceRequest GetHighestPriorityRequest()
        {
            return priorityHeap.Peek();
        }

        // Retrieve the number of requests in the heap.
        public static int GetPriorityHeapCount()
        {
            return priorityHeap.Count;
        }

        // Validate the Priority Max-Heap.
        public static bool ValidatePriorityHeap()
        {
            return priorityHeap.ValidateHeap();
        }


        // ==========================================
        // SORTED DICTIONARY AND QUEUE
        // ==========================================

        // Retrieve requests belonging to a particular status.
        public static List<ServiceRequest> GetRequestsByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return new List<ServiceRequest>();

            if (requestsByStatus.TryGetValue(
                status,
                out List<ServiceRequest> requests))
            {
                return new List<ServiceRequest>(requests);
            }

            return new List<ServiceRequest>();
        }

        // Return a copy of the submission Queue.
        public static Queue<ServiceRequest> GetRequestQueue()
        {
            return new Queue<ServiceRequest>(requestQueue);
        }


        // ==========================================
        // GRAPH: BFS AND DFS
        // ==========================================

        // Breadth-First Search.
        public static List<int> GetRelatedRequestsBFS(int requestId)
        {
            return requestGraph.BreadthFirstTraversal(requestId);
        }

        // Depth-First Search.
        public static List<int> GetRelatedRequestsDFS(int requestId)
        {
            return requestGraph.DepthFirstTraversal(requestId);
        }

        // Validate graph structure and connections.
        public static bool ValidateRequestGraph()
        {
            return requestGraph.ValidateGraph();
        }


        // ==========================================
        // MINIMUM SPANNING TREE
        // ==========================================

        // Generate the Minimum Spanning Tree.
        public static List<ServiceRequestMinimumSpanningTree.Edge>
            GetMinimumSpanningTree()
        {
            return requestMST.GenerateMST();
        }

        // Validate the Minimum Spanning Tree.
        public static bool ValidateMinimumSpanningTree()
        {
            return requestMST.ValidateMST();
        }

        // Calculate the total MST weight.
        public static int GetMinimumSpanningTreeTotalWeight()
        {
            return requestMST.GetMSTTotalWeight();
        }
    }
}
