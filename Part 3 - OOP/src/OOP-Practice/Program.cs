using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Practice.LinkedListExercise;
using OOP_Practice.NumericalExpressionExercise;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Testing LinkedList:
            LinkedList linkedList = new LinkedList(new Node(1));
            linkedList.Prepend(2);
            linkedList.Append(3);
            linkedList.Prepend(4);

            Console.WriteLine($"Is Circular? {linkedList.IsCircular()}");

            Console.WriteLine($"Max Node: {linkedList.GetMaxNode().Value}");
            Console.WriteLine($"Min Node: {linkedList.GetMinNode().Value}");

            Console.WriteLine(linkedList.Pop());

            Console.WriteLine($"Max Node: {linkedList.GetMaxNode().Value}");
            Console.WriteLine($"Min Node: {linkedList.GetMinNode().Value}");

            Console.WriteLine(linkedList.Unqueue());

            Console.WriteLine($"Max Node: {linkedList.GetMaxNode().Value}");
            Console.WriteLine($"Min Node: {linkedList.GetMinNode().Value}");

            linkedList.Sort();

            Node node = linkedList.Head;
            while (node != null)
            {
                Console.Write($"{node.Value} -> ");
                node = node.Next;
            }
            Console.Write("null");
            Console.WriteLine();

            Console.WriteLine(linkedList.GetMinNode().Value);
            Console.WriteLine(linkedList.GetMaxNode().Value);

            Console.WriteLine();
            Console.WriteLine();


            // Testing NumericalExpression:
            NumericalExpression n = new NumericalExpression(999000000000000, new EnglishLanguage());
            Console.WriteLine(n);
            Console.WriteLine(NumericalExpression.SumLetters(4));

            Console.ReadLine();
        }
    }
}
