
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    public class ServiceRequestAVLTree
    {
        private class Node
        {
            public ServiceRequest Request { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }

            public Node(ServiceRequest request)
            {
                Request = request;
                Height = 1;
            }
        }

        private Node root;

        // Insert a service request while maintaining AVL balance.
        public void Insert(ServiceRequest request)
        {
            if (request == null)
                return;

            root = InsertRecursive(root, request);
        }

        private Node InsertRecursive(Node node, ServiceRequest request)
        {
            if (node == null)
                return new Node(request);

            if (request.RequestId < node.Request.RequestId)
                node.Left = InsertRecursive(node.Left, request);
            else if (request.RequestId > node.Request.RequestId)
                node.Right = InsertRecursive(node.Right, request);
            else
            {
                node.Request = request;
                return node;
            }

            UpdateHeight(node);

            int balance = GetBalance(node);

            // Left Left case.
            if (balance > 1 &&
                request.RequestId < node.Left.Request.RequestId)
                return RotateRight(node);

            // Right Right case.
            if (balance < -1 &&
                request.RequestId > node.Right.Request.RequestId)
                return RotateLeft(node);

            // Left Right case.
            if (balance > 1 &&
                request.RequestId > node.Left.Request.RequestId)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            // Right Left case.
            if (balance < -1 &&
                request.RequestId < node.Right.Request.RequestId)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // Search using the unique Request ID.
        public ServiceRequest Search(int requestId)
        {
            Node current = root;

            while (current != null)
            {
                if (requestId == current.Request.RequestId)
                    return current.Request;

                if (requestId < current.Request.RequestId)
                    current = current.Left;
                else
                    current = current.Right;
            }

            return null;
        }

        // Return requests in ascending Request ID order.
        public List<ServiceRequest> GetRequestsInOrder()
        {
            List<ServiceRequest> requests =
                new List<ServiceRequest>();

            InOrderTraversal(root, requests);

            return requests;
        }

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

        private int GetHeight(Node node)
        {
            return node == null ? 0 : node.Height;
        }

        private void UpdateHeight(Node node)
        {
            node.Height = 1 +
                System.Math.Max(
                    GetHeight(node.Left),
                    GetHeight(node.Right));
        }

        private int GetBalance(Node node)
        {
            if (node == null)
                return 0;

            return GetHeight(node.Left) -
                   GetHeight(node.Right);
        }

        // Perform a right rotation.
        private Node RotateRight(Node y)
        {
            Node x = y.Left;
            Node subtree = x.Right;

            x.Right = y;
            y.Left = subtree;

            UpdateHeight(y);
            UpdateHeight(x);

            return x;
        }

        // Perform a left rotation.
        private Node RotateLeft(Node x)
        {
            Node y = x.Right;
            Node subtree = y.Left;

            y.Left = x;
            x.Right = subtree;

            UpdateHeight(x);
            UpdateHeight(y);

            return y;
        }

        // Validate the AVL ordering, heights and balance factors.
        public bool ValidateAVL()
        {
            int calculatedHeight;

            return ValidateNode(
                root,
                long.MinValue,
                long.MaxValue,
                out calculatedHeight);
        }

        private bool ValidateNode(
            Node node,
            long minId,
            long maxId,
            out int calculatedHeight)
        {
            calculatedHeight = 0;

            if (node == null)
                return true;

            if (node.Request == null)
                return false;

            long requestId = node.Request.RequestId;

            // Check the Binary Search Tree ordering.
            if (requestId <= minId || requestId >= maxId)
                return false;

            int leftHeight;
            int rightHeight;

            if (!ValidateNode(
                node.Left,
                minId,
                requestId,
                out leftHeight))
                return false;

            if (!ValidateNode(
                node.Right,
                requestId,
                maxId,
                out rightHeight))
                return false;

            // Calculate the actual height.
            calculatedHeight = 1 +
                System.Math.Max(leftHeight, rightHeight);

            // Check the stored height.
            if (node.Height != calculatedHeight)
                return false;

            // Check the AVL balance factor.
            if (System.Math.Abs(leftHeight - rightHeight) > 1)
                return false;

            return true;
        }
    }
}
