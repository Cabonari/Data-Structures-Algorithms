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
        if (_size >= _linkedList.Length)
        {
            return;
        }
        _linkedList[_size] = new Node(item);
        _size++;
    }

    //remove function
    public void Remove(T item)
    {
        if (_size == 0) return;

        // Handle removal of first node
        if (_linkedList[0].Data.Equals(item))
        {
            _linkedList[0] = _linkedList[0].Next;
            _size--;
            return;
        }

        // Handle removal of subsequent nodes
        Node current = _linkedList[0];
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
        throw new NotImplementedException();
    }

    // filter function
    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }

    //sort function
    public void Sort(Comparison<T> comparison)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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
