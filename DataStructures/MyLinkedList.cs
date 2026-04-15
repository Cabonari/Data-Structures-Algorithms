using System.Runtime.ExceptionServices;

public class MyLinkedList<T> : IMyCollection<T>
{
    private Node? head;
    private int _size;

    private class Node
    {
        public T Data;
        public Node? Next;

        // Constructor
        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public int Count => _size;

    public bool Dirty { get; set; }

    //add function
    public void Add(T item)
    {
        Node newNode = new Node(item);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        _size++;
    }

    //remove function
    public void Remove(T item)
    {
        if (_size == 0) return;

        // Handle removal of first node
        if (head?.Data.Equals(item) == true)
        {
            head = head.Next;
            _size--;
            return;
        }

        // Handle removal of subsequent nodes
        Node current = head;
        while (current?.Next != null)
        {
            if (current.Next.Data.Equals(item))
            {
                current.Next = current.Next.Next;
                _size--;
                return;
            }
            current = current.Next;
        }
    }

    //findby index function
    public T? FindBy<K>(K key, Func<T, K, int> comparer)
    {
        Node current = head;
        while (current != null)
        {
            if (comparer(current.Data, key) == 0)
            {
                return current.Data;
            }
            current = current.Next;
        }
        return default(T);
    }

    // filter function
    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        MyLinkedList<T> filtered = new MyLinkedList<T>();
        Node current = head;
        while (current != null)
        {
            if (predicate(current.Data))
            {
                filtered.Add(current.Data);
            }
            current = current.Next;
        }
        return filtered;
    }

    //sort function
    public void Sort(Comparison<T> comparison)
    {
        if (_size <= 1 || head == null) return;

        bool swapped;
        do
        {
            swapped = false;
            Node? current = head;

            while (current?.Next != null)
            {
                if (comparison(current.Data, current.Next.Data) > 0)
                {
                    T tempData = current.Data;
                    current.Data = current.Next.Data;
                    current.Next.Data = tempData;
                    swapped = true;
                }

                current = current.Next;
            }
        } while (swapped);

        Dirty = false;
    }

    //reset function
    public void Reset()
    {
        var current = head;
        while (current != null)
        {
            current.Data = default(T);
            current = current.Next;
        }

    }

    public R Reduce<R>(Func<R, T, R> accumulator)
    {
        if (head == null)
            throw new InvalidOperationException("Collection is empty");

        Node current = head;

        // Eerste element gebruiken als startwaarde
        R result = (R)(object)current.Data;

        current = current.Next;

        while (current != null)
        {
            result = accumulator(result, current.Data);
            current = current.Next;
        }

        return result;
    }

    public R Reduce<R>(R initial, Func<R, T, R> accumulator)
    {
        R result = initial;
        Node? current = head;

        while (current != null)
        {
            result = accumulator(result, current.Data);
            current = current.Next;
        }

        return result;

    }

    public IMyIterator<T> GetMyIterator()
    {
        // return new MyIterator<T>(_linkedList, _size);
        return new MyIterator(head);
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = head;

        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    private class MyIterator : IMyIterator<T>
    {
        private Node? _current;
        private readonly Node? _head;

        public MyIterator(Node? head)
        {
            _head = head;
            _current = null;
        }

        public bool HasNext()
        {
            if (_current == null)
                return _head != null;

            return _current.Next != null;
        }

        public T Next()
        {
            if (_current == null)
            {
                if (_head == null)
                    throw new InvalidOperationException("List is empty.");

                _current = _head;
                return _current.Data;
            }

            if (_current.Next == null)
                throw new InvalidOperationException("No more elements.");

            _current = _current.Next;
            return _current.Data;
        }

        public void Reset()
        {
            _current = null;
        }
    }
}
