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

        public void Prepend(int item)
        {
            Node newHead = new Node(item);
            newHead.Next = Head;
            Head = newHead;

            UpdateMinMax(Head, true);
        }

        public int Pop()
        {
            if (Head == null)
                throw new InvalidOperationException("The LinkedList is empty.");

            UpdateMinMax(Last, false);

            Node node = Head;
            while (node.Next != null)
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

        public int Unqueue()
        {
            if (Head == null)
                throw new InvalidOperationException("The LinkedList is empty.");

            UpdateMinMax(Head, false);

            int headValue = Head.Value;
            Head = Head.Next;
            return headValue;
        }

        public IEnumerable<int> ToList()
        {
            Node node = Head;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

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

        public void Sort()
        {
            IEnumerable<int> list = this.ToList();
            list = list.OrderBy(x => x);

            this.ToLinkedList(list.ToList());
        }

        public Node GetMaxNode()
        {
            return MaxValueNode;
        }

        public Node GetMinNode()
        {
            return MinValueNode;
        }

        // ------------------------------------------------ HELPER METHODS ------------------------------------------------
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
