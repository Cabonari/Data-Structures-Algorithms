using System.Runtime.ExceptionServices;

public class MyLinkedList<T> : IMyCollection<T>
{
    private Node[] _linkedList;
    private int _size;
    public MyLinkedList(int capacity = 10)
    {
        _linkedList = new Node[capacity];
        _size = 0;
    }

    class Node(T data)
    {
        public T Data = data;
        public Node Next = null;
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
        for (int i = 0; i <= _linkedList.Length; i++)
        {
            if (comparer(_linkedList[i].Data, key) == 0)
            {
                return _linkedList[i].Data;
            }
        }
        return default(T);
    }

    // filter function
    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        MyLinkedList<T> filtered = new MyLinkedList<T>();
        for (int i = 0; i < _linkedList.Length; i++)
        {
            if (predicate(_linkedList[i].Data))
            {
                filtered.Add(_linkedList[i].Data);
            }
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
        throw new NotImplementedException();
    }

    public R Reduce<R>(Func<R, T, R> accumulator)
    {
        throw new NotImplementedException();
    }

    public R Reduce<R>(R initial, Func<R, T, R> accumulator)
    {
        throw new NotImplementedException();
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
