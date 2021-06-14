namespace CarDirectory
{
    public class DoublyLinkedListNode<T>
    {
        public T Key { get; internal set; }
        public DoublyLinkedListNode<T> Next { get; internal set; }
        public DoublyLinkedListNode<T> Prev { get; internal set; }

        internal DoublyLinkedListNode(T key, DoublyLinkedListNode<T> next = null, DoublyLinkedListNode<T> prev = null)
        {
            Key = key;
            Next = next;
            Prev = prev;
        }
    }

    public class DoublyLinkedList<T> : System.Collections.Generic.IEnumerable<DoublyLinkedListNode<T>>
    {
        public DoublyLinkedListNode<T> First { get; private set; } = null;
        public DoublyLinkedListNode<T> Last { get; private set; } = null;
        public int Size { get; private set; } = 0;

        public void AddFirst(T key)
        {
            if (First == null)
            {
                First = new DoublyLinkedListNode<T>(key);
                Last = First;
            }
            else
            {
                First.Prev = new DoublyLinkedListNode<T>(key, First);
                First = First.Prev;
            }
            Size++;
        }

        public void AddLast(T key)
        {
            if (First == null)
            {
                First = new DoublyLinkedListNode<T>(key);
                Last = First;
            }
            else
            {
                Last.Next = new DoublyLinkedListNode<T>(key, null, Last);
                Last = Last.Next;
            }
            Size++;
        }

        public bool Contains(T key)
        {
            if (First == null) return false;
            DoublyLinkedListNode<T> ptr = First;
            do
            {
                if (ptr.Key.Equals(key)) return true;
                ptr = ptr.Next;
            } while (ptr != null);
            return false;
        }

        public void Remove(T key)
        {
            DoublyLinkedListNode<T> node = GetNode(key);
            if (node == null) return;
            if (node.Prev == null) // первый
            {
                if (node.Next == null) // единственный
                {
                    First = null;
                }
                else
                {
                    First = First.Next;
                    First.Prev = null;
                }
            }
            else if (node.Next == null) // последний
            {
                Last = Last.Prev;
                Last.Next = null;
            }
            else
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }
            Size--;
        }

        public void Clear() => First = Last = null;

        public DoublyLinkedListNode<T> GetNode(T key)
        {
            if (First == null) return null;
            DoublyLinkedListNode<T> ptr = First;
            do
            {
                if (ptr.Key.ToString().Equals(key.ToString())) return ptr;
                ptr = ptr.Next;
            } while (ptr != null);
            return null;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            DoublyLinkedListNode<T> ptr = First;
            while (ptr != null)
            {
                yield return ptr.Key;
                ptr = ptr.Next;
            }
        }

        System.Collections.Generic.IEnumerator<DoublyLinkedListNode<T>> System.Collections.Generic.IEnumerable<DoublyLinkedListNode<T>>.GetEnumerator()
        {
            DoublyLinkedListNode<T> ptr = First;
            while (ptr != null)
            {
                yield return ptr;
                ptr = ptr.Next;
            }
        }
    }
}