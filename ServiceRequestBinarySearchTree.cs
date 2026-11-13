
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    public class ServiceRequestBinarySearchTree
    {
        private class Node
        {
            public ServiceRequest Request { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(ServiceRequest request)
            {
                Request = request;
                Left = null;
                Right = null;
            }
        }

        private Node root;

        // Insert a service request using its unique Request ID.
        public void Insert(ServiceRequest request)
        {
            // Prevent null requests from being inserted.
            if (request == null)
                return;

            root = InsertRecursive(root, request);
        }

        private Node InsertRecursive(Node node, ServiceRequest request)
        {
            if (node == null)
                return new Node(request);

            if (request.RequestId < node.Request.RequestId)
            {
                node.Left = InsertRecursive(node.Left, request);
            }
            else if (request.RequestId > node.Request.RequestId)
            {
                node.Right = InsertRecursive(node.Right, request);
            }
            else
            {
                // Update the existing request if the ID already exists.
                node.Request = request;
            }

            return node;
        }

        // Search for a service request by Request ID.
        public ServiceRequest Search(int requestId)
        {
            return SearchRecursive(root, requestId);
        }

        private ServiceRequest SearchRecursive(Node node, int requestId)
        {
            if (node == null)
                return null;

            if (requestId == node.Request.RequestId)
                return node.Request;

            if (requestId < node.Request.RequestId)
                return SearchRecursive(node.Left, requestId);

            return SearchRecursive(node.Right, requestId);
        }

        // Return all requests in ascending Request ID order.
        public List<ServiceRequest> GetRequestsInOrder()
        {
            List<ServiceRequest> requests =
                new List<ServiceRequest>();

            InOrderTraversal(root, requests);

            return requests;
        }

        // Perform an in-order traversal.
        private void InOrderTraversal(
            Node node,
            List<ServiceRequest> requests)
        {
            if (node == null)
                return;

            InOrderTraversal(node.Left, requests);

            requests.Add(node.Request);

            InOrderTraversal(node.Right, requests);
        }

        // Validate the BST ordering throughout the tree.
        public bool ValidateBST()
        {
            return ValidateNode(
                root,
                long.MinValue,
                long.MaxValue);
        }

        private bool ValidateNode(
            Node node,
            long minId,
            long maxId)
        {
            if (node == null)
                return true;

            if (node.Request == null)
                return false;

            long requestId = node.Request.RequestId;

            if (requestId <= minId || requestId >= maxId)
                return false;

            return ValidateNode(
                       node.Left,
                       minId,
                       requestId)
                   &&
                   ValidateNode(
                       node.Right,
                       requestId,
                       maxId);
        }
    }
}
