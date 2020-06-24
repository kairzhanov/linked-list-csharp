using System;
using System.Collections;

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
            linkedList.Print(true);

            foreach (object elem in linkedList) {
                Console.WriteLine(elem.ToString());
            }

            Console.ReadKey();
        }
    }

    internal class Node<T>
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

        //END


    }
}
