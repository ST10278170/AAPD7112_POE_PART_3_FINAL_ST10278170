
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    public class ServiceRequestRedBlackTree
    {
        private enum NodeColor
        {
            Red,
            Black
        }

        private class Node
        {
            public ServiceRequest Request { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node Parent { get; set; }
            public NodeColor Color { get; set; }

            public Node(ServiceRequest request)
            {
                Request = request;
                Color = NodeColor.Red;
            }
        }

        private Node root;

        public void Insert(ServiceRequest request)
        {
            // Prevent null requests from being inserted.
            if (request == null)
                return;

            Node newNode = new Node(request);
            Node parent = null;
            Node current = root;

            // Find the correct insertion position.
            while (current != null)
            {
                parent = current;

                if (request.RequestId < current.Request.RequestId)
                    current = current.Left;
                else if (request.RequestId > current.Request.RequestId)
                    current = current.Right;
                else
                {
                    // Update an existing request with the same ID.
                    current.Request = request;
                    return;
                }
            }

            newNode.Parent = parent;

            if (parent == null)
                root = newNode;
            else if (request.RequestId < parent.Request.RequestId)
                parent.Left = newNode;
            else
                parent.Right = newNode;

            FixInsertion(newNode);
        }

        private void FixInsertion(Node node)
        {
            while (node != root &&
                   GetColor(node.Parent) == NodeColor.Red)
            {
                Node parent = node.Parent;
                Node grandparent = parent.Parent;

                if (parent == grandparent.Left)
                {
                    Node uncle = grandparent.Right;

                    // Case 1: Uncle is red.
                    if (GetColor(uncle) == NodeColor.Red)
                    {
                        parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        grandparent.Color = NodeColor.Red;
                        node = grandparent;
                    }
                    else
                    {
                        // Case 2: Left-right configuration.
                        if (node == parent.Right)
                        {
                            node = parent;
                            RotateLeft(node);
                            parent = node.Parent;
                            grandparent = parent.Parent;
                        }

                        // Case 3: Left-left configuration.
                        parent.Color = NodeColor.Black;
                        grandparent.Color = NodeColor.Red;
                        RotateRight(grandparent);
                    }
                }
                else
                {
                    Node uncle = grandparent.Left;

                    // Case 1: Uncle is red.
                    if (GetColor(uncle) == NodeColor.Red)
                    {
                        parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        grandparent.Color = NodeColor.Red;
                        node = grandparent;
                    }
                    else
                    {
                        // Case 2: Right-left configuration.
                        if (node == parent.Left)
                        {
                            node = parent;
                            RotateRight(node);
                            parent = node.Parent;
                            grandparent = parent.Parent;
                        }

                        // Case 3: Right-right configuration.
                        parent.Color = NodeColor.Black;
                        grandparent.Color = NodeColor.Red;
                        RotateLeft(grandparent);
                    }
                }
            }

            // The root must always be black.
            root.Color = NodeColor.Black;
        }

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

        private NodeColor GetColor(Node node)
        {
            return node == null ? NodeColor.Black : node.Color;
        }

        private void RotateLeft(Node node)
        {
            Node rightChild = node.Right;

            node.Right = rightChild.Left;

            if (rightChild.Left != null)
                rightChild.Left.Parent = node;

            rightChild.Parent = node.Parent;

            if (node.Parent == null)
                root = rightChild;
            else if (node == node.Parent.Left)
                node.Parent.Left = rightChild;
            else
                node.Parent.Right = rightChild;

            rightChild.Left = node;
            node.Parent = rightChild;
        }

        private void RotateRight(Node node)
        {
            Node leftChild = node.Left;

            node.Left = leftChild.Right;

            if (leftChild.Right != null)
                leftChild.Right.Parent = node;

            leftChild.Parent = node.Parent;

            if (node.Parent == null)
                root = leftChild;
            else if (node == node.Parent.Right)
                node.Parent.Right = leftChild;
            else
                node.Parent.Left = leftChild;

            leftChild.Right = node;
            node.Parent = leftChild;
        }

        // Validate the Red-Black Tree properties.
        public bool ValidateRedBlackTree()
        {
            // An empty tree is valid.
            if (root == null)
                return true;

            // The root must be black.
            if (root.Color != NodeColor.Black)
                return false;

            // The root must not have a parent.
            if (root.Parent != null)
                return false;

            int blackHeight;

            return ValidateNode(
                root,
                null,
                long.MinValue,
                long.MaxValue,
                out blackHeight);
        }

        // Recursively validate ordering, colours, parent links and black height.
        private bool ValidateNode(
            Node node,
            Node expectedParent,
            long minId,
            long maxId,
            out int blackHeight)
        {
            // Null leaves are treated as black.
            if (node == null)
            {
                blackHeight = 1;
                return true;
            }

            blackHeight = 0;

            if (node.Request == null)
                return false;

            // Check the parent reference.
            if (node.Parent != expectedParent)
                return false;

            long requestId = node.Request.RequestId;

            // Check Binary Search Tree ordering.
            if (requestId <= minId || requestId >= maxId)
                return false;

            // A red node cannot have a red child.
            if (node.Color == NodeColor.Red)
            {
                if (GetColor(node.Left) == NodeColor.Red ||
                    GetColor(node.Right) == NodeColor.Red)
                {
                    return false;
                }
            }

            int leftBlackHeight;
            int rightBlackHeight;

            // Validate the left subtree.
            if (!ValidateNode(
                node.Left,
                node,
                minId,
                requestId,
                out leftBlackHeight))
            {
                return false;
            }

            // Validate the right subtree.
            if (!ValidateNode(
                node.Right,
                node,
                requestId,
                maxId,
                out rightBlackHeight))
            {
                return false;
            }

            // Both subtrees must have equal black height.
            if (leftBlackHeight != rightBlackHeight)
                return false;

            // Include this node if it is black.
            blackHeight = leftBlackHeight;

            if (node.Color == NodeColor.Black)
                blackHeight++;

            return true;
        }


    }
}
