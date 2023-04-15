using Zoo;
using Collections;
using System.ComponentModel;

namespace Collections
{
    interface Iterator
    {
        IEnclosure? MoveNext();
        bool HasMore();
    }
    interface ICollection
    {
        void Add(IEnclosure toAdd);
        void Remove(IEnclosure toRemove);
        Iterator GetIterator();
        Iterator GetReverseIterator();
    }
}

namespace DoubleLinkList
{
    class Node
    {
        public IEnclosure? value { get; set; }
        public Node? next { get; set; }
        public Node? prev { get; set; }
        public Node(IEnclosure? value = null, Node? prev = null, Node? next = null) 
        {
            this.value = value;
            this.prev = prev;
            this.next = next;
        }
    }

    class ForwardDoubleLinkListIterator : Iterator 
    {
        Node? actual;
        public ForwardDoubleLinkListIterator(Node? ListStart)
        {
            actual = new Node();
            actual.next = ListStart;
        }
        public IEnclosure? MoveNext()
        {
            if (!HasMore()) throw new IndexOutOfRangeException();

            actual = actual.next;
            return actual.value;
        }
        public bool HasMore() 
        {
            return actual.next != null;
        }
    }
    class ReverseDoubleLinkListIterator : Iterator
    {
        Node? actual;
        public ReverseDoubleLinkListIterator(Node? ListEnd)
        {
            actual = new Node();
            actual.prev = ListEnd;
        }
        public IEnclosure? MoveNext()
        {
            if (!HasMore()) throw new IndexOutOfRangeException();

            actual = actual.prev;
            return actual.value;
        }
        public bool HasMore()
        {
            return actual.prev != null;
        }
    }

    class DoubleLinkList
    {
        Node? Head = null;
        Node? Tail = null;
        public void Add(IEnclosure toAdd) 
        {
            if (Head == null)
            {
                Head = new Node(toAdd);
                Tail = Head;
                return;
            }
            if(Head.next == null)
            {
                Head = new Node(toAdd, null, Head);
                Tail.prev = Head;
                return;
            }
            Head = new Node(toAdd, null, Head);
            Head.next.prev = Head;
        }
        public void Remove(IEnclosure toRemove) 
        {
            Node? actual = Head;
            if (actual == null) return;
            while (actual.next != null && actual.value != toRemove) actual = actual.next;

            if (actual.value != toRemove) return;

            if (actual.prev == null) Head = actual.next;
            else actual.prev.next = actual.next;
            if (actual.next == null) Tail = actual.prev;
            else actual.next.prev = actual.prev;
        }
        public Iterator GetIterator()
        {
            return new ForwardDoubleLinkListIterator(Head);
        }
        public Iterator GetReverseIterator()
        {
            return new ReverseDoubleLinkListIterator(Tail);
        }
    }
}