using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Practice.LinkedListExercise
{
    internal class LinkedList
    {
        public Node Head { get; set; }
        private Node Last { get; set; } // Helper Property - O(1) in appending new Node to the end of the LinkedList
        private Node MaxValueNode { get; set; } // Helper Property - O(1) in returning the Node with the Max Value
        private Node MinValueNode { get; set; } // Helper Property - O(1) in returning the Node with the Min Value


        public LinkedList(Node head)
        {
            Head = head;
            Last = head;
            MaxValueNode = head;
            MinValueNode = head;
        }

        /// <summary>
        /// Appends a new node with the specified value to the end of the linkedlist.
        /// </summary>
        /// <param name="item">The value of the node to be appended.</param>
        public void Append(int item)
        {
            if (Head == null)
            {
                Head = new Node(item);
                return;
            }

            Last.Next = new Node(item);
            Last = Last.Next;

            UpdateMinMax(Last, true);
        }

        /// <summary>
        /// Prepends a new node with the specified value to the beginning of the linkedlist.
        /// </summary>
        /// <param name="item">The value of the node to be prepended.</param>
        public void Prepend(int item)
        {
            Node newHead = new Node(item);
            newHead.Next = Head;
            Head = newHead;

            UpdateMinMax(Head, true);
        }

        /// <summary>
        /// Removes and returns the value of the last node in the linkedlist.
        /// </summary>
        /// <returns>The value of the last node.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int Pop()
        {
            if (Head == null)
                throw new InvalidOperationException("The LinkedList is empty.");

            UpdateMinMax(Last, false);

            Node node = Head;
            while (node.Next != Last)
            {
                node = node.Next;
            }
            int lastValue = Last.Value;

            // If the last Node is the Head itself, nullify the Head pointer. Else, nullify node.Next:
            if (node == Head)
                Head = null;
            else
                node.Next = null;

            return lastValue;
        }

        /// <summary>
        /// Removes and returns the value of the first node in the linkedlist.
        /// </summary>
        /// <returns>The value of the first node.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int Unqueue()
        {
            if (Head == null)
                throw new InvalidOperationException("The LinkedList is empty.");

            UpdateMinMax(Head, false);

            int headValue = Head.Value;
            Head = Head.Next;
            return headValue;
        }

        // <summary>
        /// Returns an enumerable collection of all the values in the linkedlist.
        /// </summary>
        /// <returns>An enumerable collection of integers representing the values of the nodes.</returns>
        public IEnumerable<int> ToList()
        {
            Node node = Head;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        /// <summary>
        /// Checks if the linkedlist contains a cycle.
        /// </summary>
        /// <returns>True if the linked list contains a cycle; otherwise, false.</returns>
        public bool IsCircular()
        {
            HashSet<Node> pastNodes = new HashSet<Node>();
            Node node = Head;

            while (node != Last)
            {
                pastNodes.Add(node);
                node = node.Next;
            }

            return pastNodes.Contains(Last.Next);
        }

        /// <summary>
        /// Sorts the linkedlist in ascending order.
        /// </summary>
        public void Sort()
        {
            IEnumerable<int> list = this.ToList();
            list = list.OrderBy(x => x);

            this.ToLinkedList(list.ToList());
        }

        /// <summary>
        /// Returns the node with the maximum value in the linkedlist.
        /// </summary>
        /// <returns>The node with the maximum value.</returns>
        public Node GetMaxNode()
        {
            return MaxValueNode;
        }

        /// <summary>
        /// Returns the node with the minimum value in the linkedlist.
        /// </summary>
        /// <returns>The node with the minimum value.</returns>
        public Node GetMinNode()
        {
            return MinValueNode;
        }

        // ------------------------------------------------ HELPER METHODS ------------------------------------------------
        
        /// <summary>
        /// Converts the linkedlist to match the specified list of integers.
        /// </summary>
        /// <param name="list">The list of integers to be used to update the linkedlist.</param>
        private void ToLinkedList(List<int> list)
        {
            Node node = Head;
            int index = 0;
            while (node != null)
            {
                node.Value = list.ElementAt(index);
                index++;
                node = node.Next;
            }
        }

        /// <summary>
        /// Updates the minimum and maximum value nodes in the linkedlist.
        /// </summary>
        /// <param name="node">The node that has been added or removed.</param>
        /// <param name="isNewNode">A flag indicating whether the node is newly added (true) or about to be removed (false).</param>
        private void UpdateMinMax(Node node, bool isNewNode)
        {
            // If it's a newly added node - max and min nodes will be updated at O(1):
            if (isNewNode)
            {
                if (node.Value > MaxValueNode.Value)
                    MaxValueNode = node;
                if (node.Value < MinValueNode.Value)
                    MinValueNode = node;
            } else
            {
                // If the given node is about to get deleted (due to Pop or Unqueue),
                // we'll need to iterate and find the new min/max (if the given node was the min/max)
                if (node.Value == MinValueNode.Value)
                    (MinValueNode, _) = IterativeGet2ndMinMax();
                if (node.Value == MaxValueNode.Value)
                    (_, MaxValueNode) = IterativeGet2ndMinMax();
            }
        }

        /// <summary>
        /// Finds the nodes with the second minimum and maximum values in the linkedlist iteratively.
        /// </summary>
        /// <returns>A tuple containing the node with the second minimum value and the node with the second maximum value.</returns>
        private (Node min, Node max) IterativeGet2ndMinMax()
        {
            int min = Int32.MaxValue;
            int max = Int32.MinValue;

            Node minNode = null;
            Node maxNode = null;

            Node node = Head;
            while (node != null)
            {
                if (node.Value > max && node.Value < MaxValueNode.Value)
                {
                    maxNode = node;
                    max = node.Value;
                }
                if (node.Value < min && node.Value > MinValueNode.Value)
                {
                    minNode = node;
                    min = node.Value;
                }

                node = node.Next;
            }

            return (minNode, maxNode);
        }
    }
}
