using System;

namespace DataStructures
{
    public class LinkedList
    {
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
            var current = First;
            for (int i = 0; i < index - 1; i++)
                current = current.Next;
            var temp = current.Next;
            current.Next = element;
            element.Next = temp.Next;
        }

        private void CheckIndexWithinBounds(int index)
        {
            if (index >= Count || index < 0)
                throw new ArgumentOutOfRangeException();
        }
    }
}
