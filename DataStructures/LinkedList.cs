using System;

namespace DataStructures
{
    public class LinkedList
    {
        internal const string Seperator = ", ";

        internal Node First { get; private set; }

        public int Count { get; private set; }

        public LinkedList()
        {
        }

        public Node this[int index]
        {
            get
            {
                if (!IsWithinBounds(index))
                    throw new ArgumentOutOfRangeException();
                return GetAt(index);
            }
            set
            {
                if (!IsWithinBounds(index))
                    throw new ArgumentOutOfRangeException();
                SetAt(index, value);
            }
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

        public Node GetAt(int index)
        {
            if (!IsWithinBounds(index))
                return null;
            var current = First;
            for (int i = 0; i < index; i++)
                current = current.Next;
            return current;
        }

        public void SetAt(int index, Node element)
        {
            if (!IsWithinBounds(index)) return;
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

        public int IndexOf(int element)
        {
            if (First.Value.Equals(element)) return 0;
            for (int i = 0; i < Count; i++)
                if (this[i].Value == element) return i;
            return -1;
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

        private bool IsWithinBounds(int index)
        {
            return index > -1 && index < Count;
        }
    }
}
