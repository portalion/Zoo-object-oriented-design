using static Zoo.Zoo;
using Zoo;
using Collections;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Collections
{
    interface Iterator<T>
    {
        T? MoveNext();
    }
    interface ICollection<T>
    {
        void Add(T toAdd);
        void Remove(Iterator<T> toRemove);
        Iterator<T> GetIterator();
        Iterator<T> GetReverseIterator();
    }
    interface Predicate <T>
    {
        bool fulfills(T toCheck);
    }
    interface Function<T>
    {
        void operations(T toCheck);
    }

    static class Algorithms
    {
        static public T? Find<T>(Iterator<T> toSearch, Predicate<T> predicate)
        {
            T? toCheck;
            while ((toCheck = toSearch.MoveNext()) != null)
            {
                if (predicate.fulfills(toCheck)) return toCheck;
            }
            return default;
        }
        static public void Print<T>(Iterator<T> toSearch, Predicate<T> predicate)
        {
            T? toCheck;
            while ((toCheck = toSearch.MoveNext()) != null)
            {
                if (predicate.fulfills(toCheck))
                    PrintObject(toCheck);
            }
        }
        static public void ForEach<T>(Iterator<T> toSearch, Function<T> function)
        {
            T? toCheck;
            while ((toCheck = toSearch.MoveNext()) != null)
                function.operations(toCheck);
        }
        static public int CountIf<T>(Iterator<T> toSearch, Predicate<T> predicate)
        {
            int counter = 0;
            T? toCheck;
            while ((toCheck = toSearch.MoveNext()) != null)
                if (predicate.fulfills(toCheck)) counter++;
            return counter;        
        }
    }  
    
    class DoubleLinkList<T> : ICollection<T>
    {
        class Node
        {
            public T? value { get; set; }
            public Node? next { get; set; }
            public Node? prev { get; set; }
            public Node(T? value = default, Node? prev = null, Node? next = null)
            {
                this.value = value;
                this.prev = prev;
                this.next = next;
            }
        }
        class ForwardDoubleLinkListIterator : Iterator<T>
        {
            Node? actual;
            public ForwardDoubleLinkListIterator(Node? ListStart)
            {
                actual = new Node(default, null, ListStart);
            }
            public T? MoveNext()
            {
                actual = actual?.next;
                if (actual == null) return default;
                return actual.value;
            }
        }
        class ReverseDoubleLinkListIterator : Iterator<T>
        {
            Node? actual;
            public ReverseDoubleLinkListIterator(Node? ListEnd)
            {
                actual = new Node(default, ListEnd, null);
            }
            public T? MoveNext()
            {
                actual = actual?.prev;
                if (actual == null) return default;
                return actual.value;
            }
        }
        Node? Head = null;
        Node? Tail = null;
        public void Add(T toAdd) 
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
        public void Remove(Iterator<T> val) 
        {
            T? toRemove = val.MoveNext();
            if (toRemove == null) return;
            Node? actual = Head;
            if (actual == null) return;
            while (actual.next != null && !actual.value.Equals(toRemove)) actual = actual.next;

            if (!actual.value.Equals(toRemove)) return;

            if (actual.prev == null) Head = actual.next;
            else actual.prev.next = actual.next;
            if (actual.next == null) Tail = actual.prev;
            else actual.next.prev = actual.prev;
        }
        public Iterator<T> GetIterator()
        {
            return new ForwardDoubleLinkListIterator(Head);
        }
        public Iterator<T> GetReverseIterator()
        {
            return new ReverseDoubleLinkListIterator(Tail);
        }
    }

    class Vector : ICollection<IEnclosure>
    {
        class ForwardVectorIterator : Iterator<IEnclosure>
        {
            int index;
            Vector vector;
            public ForwardVectorIterator(Vector vector)
            {
                index = -1;
                this.vector = vector;
            }
            public IEnclosure? MoveNext()
            {
                return index == vector.size ? null : vector.values[++index];
            }
        }
        class ReverseVectorIterator : Iterator<IEnclosure>
        {
            int index;
            Vector vector;
            public ReverseVectorIterator(Vector vector)
            {
                index = vector.size;
                this.vector = vector;
            }

            public IEnclosure? MoveNext()
            {
                return index == 0 ? null : vector.values[--index];
            }
        }
        int size;
        IEnclosure?[] values;
        public Vector(int size = 2)
        {
            if (size < 2) throw new ArgumentException("size");
            values = new IEnclosure?[size];
            this.size = 0;
        }

        public void Add(IEnclosure value)
        {
            if(values.Length == size)
            {
                var tmp = values;
                values = new IEnclosure?[tmp.Length * 2];
                for (int i = 0; i < size; i++)
                    values[i] = tmp[i];
            }
           
            values[size++] = value;
        }

        public void Remove(Iterator<IEnclosure> iterator)
        {
            IEnclosure? toDeleteEnclosure = iterator.MoveNext();
            if (toDeleteEnclosure == null) return;
            int toDelete = 0;
            for (toDelete = 0; toDelete < size; toDelete++)
                if (values[toDelete] == toDeleteEnclosure) break;

            for (int i = toDelete; i < size - 1; i++)
                values[i] = values[i + 1];

            values[size--] = null;
        }

        public Iterator<IEnclosure> GetIterator()
        {
            return new ForwardVectorIterator(this);
        }
        public Iterator<IEnclosure> GetReverseIterator()
        {
            return new ReverseVectorIterator(this);
        }
    }
    
    class BinaryTree : ICollection<IEnclosure>
    {
        class Node
        {
            public Node? parent;
            public Node? left; 
            public Node? right;
            public IEnclosure value;
            public Node(IEnclosure value, Node? parent = null, Node? left = null, Node? right = null)
            {
                this.value = value;
                this.parent = parent;
                this.left = left;
                this.right = right;                
            }
        }

        class ForwardBinaryTreeIterator : Iterator<IEnclosure>
        {
            Node? root;
            int state;
            ForwardBinaryTreeIterator? it;
            public ForwardBinaryTreeIterator(Node? root)
            {
                this.root = root;
                if (this.root == null)
                    state = 0;
                else
                {
                    state = 1;
                    it = new ForwardBinaryTreeIterator(this.root.left);
                }
            }
            public IEnclosure? MoveNext()
            {
                if (state == 0) return null;
                else if (state == 1)
                {
                    IEnclosure? toCheck = it?.MoveNext();
                    if (toCheck != null) return toCheck;

                    state = 2;
                    return MoveNext();
                }
                else if (state == 2)
                {
                    state = 3;
                    it = new ForwardBinaryTreeIterator(root?.right);
                    return root?.value;
                }
                else return it?.MoveNext();
            }
        }
        class ReverseBinaryTreeIterator : Iterator<IEnclosure>
        {
            Node? root;
            int state;
            ReverseBinaryTreeIterator? it;
            public ReverseBinaryTreeIterator(Node? root)
            {
                this.root = root;
                if (this.root == null) state = 0;
                else
                {
                    state = 1;
                    it = new ReverseBinaryTreeIterator(this.root.right);
                }
            }
            public IEnclosure? MoveNext()
            {
                if (state == 0) return null;
                else if (state == 1)
                {
                    IEnclosure? toCheck = it?.MoveNext();
                    if (toCheck != null) return toCheck;

                    state = 2;
                    return MoveNext();
                }
                else if (state == 2)
                {
                    state = 3;
                    it = new ReverseBinaryTreeIterator(root?.left);
                    return root?.value;
                }
                else return it?.MoveNext();
            }
        }

        Node? root;
        public BinaryTree()
        {
            root = null;
        }

        public Iterator<IEnclosure> GetIterator()
        {
            return new ForwardBinaryTreeIterator(root);
        }
        public Iterator<IEnclosure> GetReverseIterator()
        {
            return new ReverseBinaryTreeIterator(root);
        }

        Node? getNodeWithValue(Node? node, IEnclosure value)
        {
            if (node == null)
                return null;
            if (node.value == value)
                return node;

            Node? result = getNodeWithValue(node.left, value);
            if (result != null) return result;
            return getNodeWithValue(node.right, value);
        }

        void deleteFromParent(Node toDelete)
        {
            if (toDelete.parent == null)
            {
                root = null;
                return;
            }

            Node tmp = toDelete.parent;
            if (tmp.right == toDelete) tmp.right = null;
            else tmp.left = null;
        }

        public void Add(IEnclosure value)
        {
            if(root == null)
            {
                root = new Node(value);
                return;
            }

            Node actual = root;

            while (actual.right != null && actual.left != null)
            {
                Random randomNumberGenerator = new Random();
                if (randomNumberGenerator.Next(2) == 1) actual = actual.left;
                else actual = actual.right;
            }
            if (actual.left == null && actual.right == null)
            {
                Random randomNumberGenerator = new Random();
                if (randomNumberGenerator.Next(2) == 1) actual.left = new Node(value, actual);
                else actual.right = new Node(value, actual);
            }
            else if (actual.left == null)
                actual.left = new Node(value, actual);
            else actual.right = new Node(value, actual);
        }
        public void Remove(Iterator<IEnclosure> iter)
        {
            if (root == null) return;
            IEnclosure? value = iter.MoveNext();

            if (value == null) return;

            Node? toDelete = getNodeWithValue(root, value);
            if (toDelete == null) return;

            if (toDelete.parent == null && toDelete.left == null && toDelete.right == null)
            {
                root = null;
                return;
            }

            Node? toSwap = root;
            while (toSwap.right != null && toSwap.left != null)
            {
                if (toSwap.right != null) toSwap = toSwap.right;
                else toSwap = toSwap.left;
            }
            var parent = toSwap.parent;
            toSwap.parent = toDelete.parent;
            toSwap.left = toDelete.left;
            toSwap.right = toDelete.right;
            toDelete.right = null;
            toDelete.left = null;
            toDelete.parent = parent;
            deleteFromParent(toDelete);
        }
    }
}