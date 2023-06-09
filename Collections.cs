﻿using static Zoo.Zoo;
using Zoo;

namespace Collections
{
    public interface Iterator
    {
        IEditableByUser? MoveNext();
    }
    public interface ICollection
    {
        void Add(IEditableByUser toAdd);
        void Remove(Iterator toRemove);
        void Remove(IEditableByUser? toRemove);
        Iterator GetIterator();
        Iterator GetReverseIterator();
    }
    public interface Predicate
    {
        bool fulfills(IEditableByUser toCheck);
    }
    public interface Function
    {
        void operations(IEditableByUser toCheck);
    }

    public class TruePredicate : Predicate
    {
        public bool fulfills(IEditableByUser toCheck) { return true; }
    }

    public static class Algorithms
    {
        static public IEditableByUser? Find(Iterator toSearch, Predicate predicate)
        {
            IEditableByUser? toCheck;
            while ((toCheck = toSearch.MoveNext()) != null)
            {
                if (predicate.fulfills(toCheck)) return toCheck;
            }
            return null;
        }
        static public void Print(Iterator toSearch, Predicate predicate)
        {
            IEditableByUser? toCheck;
            while ((toCheck = toSearch.MoveNext()) != null)
            {
                if (predicate.fulfills(toCheck))
                    PrintToUser(toCheck);
            }
        }
        static public void ForEach(Iterator toSearch, Function function)
        {
            IEditableByUser? toCheck;
            while ((toCheck = toSearch.MoveNext()) != null)
                function.operations(toCheck);
        }
        static public int CountIf(Iterator toSearch, Predicate predicate)
        {
            int counter = 0;
            IEditableByUser? toCheck;
            while ((toCheck = toSearch.MoveNext()) != null)
                if (predicate.fulfills(toCheck)) counter++;
            return counter;
        }
    }

    public class DoubleLinkList : ICollection
    {
        class Node
        {
            public IEditableByUser? value { get; set; }
            public Node? next { get; set; }
            public Node? prev { get; set; }
            public Node(IEditableByUser? value = null, Node? prev = null, Node? next = null)
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
                actual = new Node(null, null, ListStart);
            }
            public IEditableByUser? MoveNext()
            {
                actual = actual?.next;
                return actual?.value;
            }
        }
        class ReverseDoubleLinkListIterator : Iterator
        {
            Node? actual;
            public ReverseDoubleLinkListIterator(Node? ListEnd)
            {
                actual = new Node(null, ListEnd, null);
            }
            public IEditableByUser? MoveNext()
            {
                actual = actual?.prev;
                return actual?.value;
            }
        }
        Node? Head = null;
        Node? Tail = null;
        public void Add(IEditableByUser toAdd)
        {
            if (Head == null)
            {
                Head = new Node(toAdd);
                Tail = Head;
                return;
            }
            if (Head.next == null)
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
            IEditableByUser? toRemove = val.MoveNext();
            Remove(toRemove);
        }
        public void Remove(IEditableByUser? val)
        {
            IEditableByUser? toRemove = val;
            if (toRemove == null) return;
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
    public class Vector : ICollection
    {
        class ForwardVectorIterator : Iterator
        {
            int index;
            Vector vector;
            public ForwardVectorIterator(Vector vector)
            {
                index = -1;
                this.vector = vector;
            }
            public IEditableByUser? MoveNext()
            {
                return index == vector.size ? null : vector.values[++index];
            }
        }
        class ReverseVectorIterator : Iterator
        {
            int index;
            Vector vector;
            public ReverseVectorIterator(Vector vector)
            {
                index = vector.size;
                this.vector = vector;
            }

            public IEditableByUser? MoveNext()
            {
                return index == 0 ? null : vector.values[--index];
            }
        }
        int size;
        IEditableByUser?[] values;
        public Vector(int size = 2)
        {
            if (size < 2) throw new ArgumentException("size");
            values = new IEditableByUser?[size];
            this.size = 0;
        }

        public void Add(IEditableByUser value)
        {
            if (values.Length == size)
            {
                var tmp = values;
                values = new IEditableByUser?[tmp.Length * 2];
                for (int i = 0; i < size; i++)
                    values[i] = tmp[i];
            }

            values[size++] = value;
        }

        public void Remove(Iterator iterator)
        {
            IEditableByUser? toDeleteEnclosure = iterator.MoveNext();
            Remove(toDeleteEnclosure);
        }

        public void Remove(IEditableByUser? toRemove)
        {
            IEditableByUser? toDeleteEnclosure = toRemove;
            if (toDeleteEnclosure == null) return;
            int toDelete = 0;
            for (toDelete = 0; toDelete < size; toDelete++)
                if (values[toDelete] == toDeleteEnclosure) break;

            for (int i = toDelete; i < size - 1; i++)
                values[i] = values[i + 1];

            values[size--] = null;
        }

        public Iterator GetIterator()
        {
            return new ForwardVectorIterator(this);
        }
        public Iterator GetReverseIterator()
        {
            return new ReverseVectorIterator(this);
        }
    }
    public class BinaryTree : ICollection
    {
        class Node
        {
            public Node? parent;
            public Node? left;
            public Node? right;
            public IEditableByUser value;
            public Node(IEditableByUser value, Node? parent = null, Node? left = null, Node? right = null)
            {
                this.value = value;
                this.parent = parent;
                this.left = left;
                this.right = right;
            }
        }

        class ForwardBinaryTreeIterator : Iterator
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
            public IEditableByUser? MoveNext()
            {
                if (state == 0) return null;
                else if (state == 1)
                {
                    IEditableByUser? toCheck = it?.MoveNext();
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
        class ReverseBinaryTreeIterator : Iterator
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
            public IEditableByUser? MoveNext()
            {
                if (state == 0) return null;
                else if (state == 1)
                {
                    IEditableByUser? toCheck = it?.MoveNext();
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

        public Iterator GetIterator()
        {
            return new ForwardBinaryTreeIterator(root);
        }
        public Iterator GetReverseIterator()
        {
            return new ReverseBinaryTreeIterator(root);
        }

        Node? getNodeWithValue(Node? node, IEditableByUser value)
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

        public void Add(IEditableByUser value)
        {
            if (root == null)
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
        public void Remove(Iterator iter)
        {
            if (root == null) return;
            IEditableByUser? value = iter.MoveNext();

            Remove(value);
        }
        public void Remove(IEditableByUser? iter)
        {
            if (root == null) return;
            IEditableByUser? value = iter;

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