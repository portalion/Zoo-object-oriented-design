using static Zoo.Zoo;
using Zoo;
using Collections;

namespace Collections
{
    interface Iterator
    {
        IEnclosure MoveNext();
        bool HasMore();
    }
    interface ICollection
    {
        void Add(IEnclosure toAdd);
        void Remove(IEnclosure toRemove);
        Iterator GetIterator();
        Iterator GetReverseIterator();
    }

    interface Predicate 
    {
        bool fulfills(IEnclosure toCheck);
    }

    static class Algorithms
    {
        static public IEnclosure? Find(ICollection collection, Predicate predicate, bool reverse)
        {
            Iterator toSearch = reverse ? collection.GetReverseIterator() : collection.GetIterator();

            while (toSearch.HasMore())
            {
                IEnclosure toCheck = toSearch.MoveNext();
                if (predicate.fulfills(toCheck)) return toCheck;
            }
            return null;
        }

        static public void Print(ICollection collection, Predicate predicate, bool reverse)
        {
            Iterator toSearch = reverse ? collection.GetReverseIterator() : collection.GetIterator();

            while (toSearch.HasMore())
            {
                IEnclosure toCheck = toSearch.MoveNext();
                if (predicate.fulfills(toCheck)) PrintEnclosure(toCheck);
            }
        }
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
        public IEnclosure MoveNext()
        {
            if (!HasMore()) throw new IndexOutOfRangeException();

            actual = actual.next;
            if (actual.value == null) throw new NullReferenceException();
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
        public IEnclosure MoveNext()
        {
            if (!HasMore()) throw new IndexOutOfRangeException();

            actual = actual.prev;
            if (actual.value == null) throw new NullReferenceException();
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