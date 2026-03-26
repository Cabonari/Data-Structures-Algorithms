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
        throw new NotImplementedException();
    }

    //reset function
    public void Reset()
    {
        for (int i = 0; i < _size; i++)
        {
            _linkedList[i].Data = default(T);
        }
    }

    public R Reduce<R>(Func<R, T, R> accumulator)
    {
        throw new NotImplementedException();
    }

    public R Reduce<R>(R initial, Func<R, T, R> accumulator)
    {
        R res = initial;
        for (int i = 0; i < _size; i++)
        {
            res = accumulator(res, _linkedList[i].Data);
        }
        return res;
    }

    public IMyIterator<T> GetMyIterator()
    {
        // return new MyIterator<T>(_linkedList, _size);
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _size; i++)
        {
            yield return _linkedList[i].Data;
        }
    }
}
