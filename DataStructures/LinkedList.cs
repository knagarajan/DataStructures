using System;

namespace DataStructures
{
    public class LinkedList
    {
        internal const string Seperator = ", ";

        public LinkedList(int capacity)
        {
            for (int i = 0; i < capacity; i++)
                Add(0);
        }

        public LinkedList()
            : this(0)
        {
        }

        internal Node First { get; private set; }

        public int Count { get; private set; }

        public Node this[int index]
        {
            get { return GetAt(index); }
            set { SetAt(index, value); }
        }

        public void Add(int value)
        {
            if (ReferenceEquals(First, null))
                First = new Node { Value = value };
            else
            {
                var current = First;
                while (!ReferenceEquals(current.Next, null))
                    current = current.Next;
                current.Next = new Node { Value = value };
            }
            Count++;
        }

        private Node GetAt(int index)
        {
            CheckIndexWithinBounds(index);
            var current = First;
            for (int i = 0; i < index; i++)
                current = current.Next;
            return current;
        }

        private void SetAt(int index, Node element)
        {
            CheckIndexWithinBounds(index);
            if (index == 0)
            {
                if (ReferenceEquals(element, null))
                {
                    First = First.Next;
                    Count--;
                }
                else
                {
                    element.Next = First.Next;
                    First = element;
                }
            }
            else
            {
                var current = First;
                for (int i = 0; i < index - 1; i++)
                    current = current.Next;
                if (ReferenceEquals(element, null))
                {
                    current.Next = current.Next.Next;
                    Count--;
                }
                else
                {
                    element.Next = current.Next.Next;
                    current.Next = element;
                }
            }
        }

        public override string ToString()
        {
            var result = string.Empty;
            for (int i = 0; i < Count; i++)
            {
                if (i == Count - 1)
                    result += this[i].Value;
                else
                    result += this[i].Value + Seperator;
            }
            return result;
        }

        private void CheckIndexWithinBounds(int index)
        {
            if (index >= Count || index < 0)
                throw new ArgumentOutOfRangeException();
        }

        public void Remove(int element)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Value.Equals(element))
                {
                    this[i] = null;
                    break;
                }
            }
        }
    }
}
