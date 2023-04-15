using static Zoo.Zoo;
using Zoo;
using Collections;

namespace Collections
{
    interface Iterator
    {
        IEnclosure current { get; }
        void MoveNext();
        bool HasMore();
    }
    interface ICollection
    {
        void Add(IEnclosure toAdd);
        void Remove(Iterator toRemove);
        Iterator GetIterator();
        Iterator GetReverseIterator();
    }

    interface Predicate 
    {
        bool fulfills(IEnclosure toCheck);
    }
    interface Function
    {
        void operations(IEnclosure toCheck);
    }

    static class Algorithms
    {
        static public IEnclosure? Find(Iterator toSearch, Predicate predicate)
        {
            while (toSearch.HasMore())
            {
                IEnclosure toCheck = toSearch.current;
                if (predicate.fulfills(toCheck)) return toCheck;
            }
            return null;
        }
        static public void Print(Iterator toSearch, Predicate predicate)
        {
            while (toSearch.HasMore())
            {
                if (predicate.fulfills(toSearch.current))
                    PrintEnclosure(toSearch.current);
                toSearch.MoveNext();
            }
            if (predicate.fulfills(toSearch.current)) 
                PrintEnclosure(toSearch.current);
        }
        static public void ForEach(Iterator toSearch, Function function)
        {
            while (toSearch.HasMore())
            {
                function.operations(toSearch.current);
                toSearch.MoveNext();
            }
            function.operations(toSearch.current);
        }
        static public int CountIf(Iterator toSearch, Predicate predicate)
        {
            int counter = 0;
            while (toSearch.HasMore())
            {
                if (predicate.fulfills(toSearch.current)) counter++;
                toSearch.MoveNext();
            }
            if (predicate.fulfills(toSearch.current)) counter++;
            return counter;
        }
    }  
    
    class DoubleLinkList : ICollection
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
            Node actual;
            public ForwardDoubleLinkListIterator(Node? ListStart)
            {
                actual = new Node(null, null, ListStart);
                MoveNext();
            }
            public IEnclosure current 
            { 
                get 
                {
                    if (actual.value == null) throw new NullReferenceException();
                    return actual.value;
                } 
            }
            public void MoveNext()
            {
                if (!HasMore()) throw new IndexOutOfRangeException();

                actual = actual.next;
                
            }
            public bool HasMore()
            {
                return actual.next != null;
            }
        }
        class ReverseDoubleLinkListIterator : Iterator
        {
            Node actual;
            public ReverseDoubleLinkListIterator(Node? ListEnd)
            {
                actual = new Node(null, ListEnd, null);
                MoveNext();
            }

            public IEnclosure current
            {
                get
                {
                    if (actual.value == null) throw new NullReferenceException();
                    return actual.value;
                }
            }

            public void MoveNext()
            {
                if (!HasMore()) throw new IndexOutOfRangeException();

                actual = actual.prev;
            }
            public bool HasMore()
            {
                return actual.prev != null;
            }
        }
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
        public void Remove(Iterator val) 
        {
            var toRemove = val.current;
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