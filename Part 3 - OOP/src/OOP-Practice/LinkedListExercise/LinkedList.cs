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
            Last.Next = new Node(item);
            Last = Last.Next;

            UpdateMinMax(Last);
        }

        public void Prepend(int item)
        {
            Node newHead = new Node(item);
            newHead.Next = Head;
            Head = newHead;

            UpdateMinMax(Head);
        }

        public int Pop()
        {
            Node node = Head;
            while (node.Next != null && node.Next != Last)
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
            return Last.Next == Head;
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

        private void UpdateMinMax(Node newNode)
        {
            if (newNode.Value > MaxValueNode.Value)
                MaxValueNode = newNode;
            if (newNode.Value < MinValueNode.Value)
                MinValueNode = newNode;
        }
    }
}
