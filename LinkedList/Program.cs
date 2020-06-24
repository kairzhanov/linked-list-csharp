using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.Push(15);
            linkedList.Push(10);
            linkedList.Push(40);
            linkedList.Push(20);
            linkedList.Push(64);
            linkedList.Insert(0, 1);
            linkedList.Insert(2, 2);
            linkedList.Insert(6, 3);
            int number = linkedList.GetAt(3);
            Console.WriteLine(number.ToString());
            linkedList.DeleteAt(3);


            linkedList.Pop();
            linkedList.Print();
            //linkedList.Print(true);

            //foreach (object elem in linkedList) {
            //    Console.WriteLine(elem.ToString());
            //}

            linkedList.QuickSort();
            linkedList.Print();

            LinkedList<int> newList = linkedList.Where(i => i > 10).Select(i => i * i).Reverse();
            LinkedList<int> newList2 = linkedList.Where(i => i > 10).Select(i => i * i);

            newList.Print();
            newList2.Print();

            Console.ReadKey();
        }
    }

    internal class Node<T> : IComparable<Node<T>>
    {
        internal T data;
        internal Node<T> next;
        internal Node<T> prev;

        public Node(T data)
        {
            this.data = data;
            this.next = null;
            this.prev = null;
        }

        public int CompareTo(Node<T> other)
        {
            if (other == null) return 1;

            return Comparer<T>.Default.Compare(data, other.data);
        }

        public static bool operator >(Node<T> operand1, Node<T> operand2)
        {
            return operand1.CompareTo(operand2) == 1;
        }

        public static bool operator <(Node<T> operand1, Node<T> operand2)
        {
            return operand1.CompareTo(operand2) == -1;
        }

        public static bool operator >=(Node<T> operand1, Node<T> operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        public static bool operator <=(Node<T> operand1, Node<T> operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }
    }

    internal class LinkedList<T> : IEnumerator, IEnumerable
    {
        private int size;
        public int Size {
            get
            {
                return size;
            }
        }

        public Node<T> head;
        int position = -1;

        public LinkedList() {
            this.size = 0;
            this.head = null;
        }

        public void Push(T data) {
            Node<T> newNode = new Node<T>(data);
            if (this.head != null)
            {
                Node<T> endNode = this.head.prev;
                endNode.next = newNode;
                newNode.prev = endNode;
                newNode.next = this.head;
                this.head.prev = newNode;
            }
            else
            {
                this.head = newNode;
                this.head.next = newNode;
                this.head.prev = newNode;
            }
            this.size++;
        }

        public T Pop() {
            if (this.size == 0)
            {
                return default(T);
            }
            else
            {
                Node<T> popNode = this.head;
                if (this.size > 1)
                {
                    Node<T> nextNode = this.head.next;
                    Node<T> prevNode = this.head.prev;
                    this.head = nextNode;
                    nextNode.prev = prevNode;
                    prevNode.next = nextNode;
                }
                else
                {
                    this.head = null;
                }
                this.size--;
                return popNode.data;
            }
        }

        public void Insert(int index, T data)
        {
            if (index > size)
            {
                Console.WriteLine("Segmentation fault: index cannot be larger than size of LinkedList.");
                return;
            }
            if (index < 0)
            {
                Console.WriteLine("Segmentation fault: index cannot be less than zero.");
                return;
            }
            if (this.head == null)
            {
                Node<T> node = new Node<T>(data);
                this.head = node;
                this.head.next = node;
                this.head.prev = node;
                this.size++;
            }
            else
            {
                //Node<T> prevNode = this.head;
                Node<T> nextNode = this.head;
                int i = index;
                while(i > 0)
                {
                    nextNode = nextNode.next;
                    i--;
                }

                Node<T> prevNode = nextNode.prev;
                Node<T> newNode = new Node<T>(data);
                prevNode.next = newNode;
                nextNode.prev = newNode;
                newNode.next = nextNode;
                newNode.prev = prevNode;
                if (nextNode == this.head && index == 0)
                {
                    this.head = newNode;
                }
                this.size++;
            }
        }

        private Node<T> GetNodeAt(int index)
        {
            if (index >= size)
            {
                Console.WriteLine("Segmentation fault: index cannot be larger than size of LinkedList.");
                return null;
            }
            if (index < 0)
            {
                Console.WriteLine("Segmentation fault: index cannot be less than zero.");
                return null;
            }
            Node<T> nextNode = this.head;
            while (index > 0)
            {
                nextNode = nextNode.next;
                index--;
            }
            return nextNode;
        }

        public T GetAt(int index)
        {
            Node<T> node = this.GetNodeAt(index);
            if (node != null)
                return node.data;
            return default(T);
        }

        public void DeleteAt(int index)
        {
            Node<T> node = this.GetNodeAt(index);
            if (node != null)
            {
                Node<T> prevNode = node.prev;
                Node<T> nextNode = node.next;
                node.next = null;
                node.prev = null;
                prevNode.next = nextNode;
                nextNode.prev = prevNode;
                this.size--;
            }
        }

        // PRINT - START

        public void Print(bool reverse = false)
        {
            int index = this.size;
            Node<T> nextNode = this.head;
            if (this.head == null)
            {
                Console.WriteLine("Empty Linked list");
                return;
            }

            Console.WriteLine("Print out Linked list. Size = " + this.size.ToString());
            while (index > 0)
            {
                Console.Write(nextNode.data.ToString() + " -> ");

                if (reverse)
                    nextNode = nextNode.prev;
                else
                    nextNode = nextNode.next;

                index--;
            }
            Console.WriteLine("FINISH\r\n");

            return;
        }

        // PRINT - END

        // FOREACH IMPLEMENTATION - START

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public bool MoveNext()
        {
            this.position++;
            return (this.position < this.size);
        }

        public void Reset()
        {
            this.position = 0;
        }

        public object Current
        {
            get
            {
                return GetAt(position);
            }
        }

        // END

        // LINQ - START


        // Where: определяет фильтр выборки
        public LinkedList<T> Where(Func<T, bool> condition)
        {
            LinkedList<T> newList = new LinkedList<T>();
            Node<T> nextNode = this.head;
            int i = this.size;
            while (i > 0)
            {
                if (condition(nextNode.data))
                    newList.Push(nextNode.data);
                nextNode = nextNode.next;
                i--;
            }
            return newList;
        }

        // Select: определяет проекцию выбранных значений
        public LinkedList<T> Select(Func<T, T> operation)
        {
            LinkedList<T> newList = new LinkedList<T>();
            Node<T> nextNode = this.head;
            int i = this.size;
            while (i > 0)
            {
                T newData = operation(nextNode.data);
                newList.Push(newData);
                nextNode = nextNode.next;
                i--;
            }
            return newList;
        }

        // Reverse: располагает элементы в обратном порядке
        public LinkedList<T> Reverse()
        {
            LinkedList<T> newList = new LinkedList<T>();
            Node<T> nextNode = this.head.prev;
            int i = this.size;
            while (i > 0)
            {
                newList.Push(nextNode.data);
                nextNode = nextNode.prev;
                i--;
            }
            return newList;
        }

        // All: определяет, все ли элементы коллекции удовлятворяют определенному условию
        public bool All(Func<T, bool> condition)
        {
            Node<T> nextNode = this.head;
            int i = this.size;
            while (i > 0)
            {
                if (!condition(nextNode.data))
                    return false;
                nextNode = nextNode.next;
                i--;
            }
            return true;
        }


        // Any: определяет, удовлетворяет хотя бы один элемент коллекции определенному условию
        public bool Any(Func<T, bool> condition)
        {
            Node<T> nextNode = this.head;
            int i = this.size;
            while (i > 0)
            {
                if (condition(nextNode.data))
                    return true;
                nextNode = nextNode.next;
                i--;
            }
            return false;
        }

        // LINQ - END


        // QUICKSORT - START
        private void Swap(int i, int j)
        {
            Node<T> node1 = this.GetNodeAt(i);
            Node<T> node2 = this.GetNodeAt(j);
            T temp = node1.data;
            node1.data = node2.data;
            node2.data = temp;
        }

        public void QuickSort()
        {
            QuickSort(0, size - 1);
        }
        

        private void QuickSort(int start, int end)
        {
            int i;
            if (start < end)
            {
                i = Partition(start, end);

                QuickSort(start, i - 1);
                QuickSort(i + 1, end);
            }
        }

        private int Partition(int start, int end)
        {
            Node<T> p = this.GetNodeAt(end);
            int i = start - 1;

            for (int j = start; j <= end - 1; j++)
            {
                if (GetNodeAt(j) <= p)
                {
                    i++;
                    Swap(i, j);
                }
            }
            Swap(i + 1, end);
            return i + 1;
        }

        // QUICKSORT - END

    }
}
