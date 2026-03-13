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
        throw new NotImplementedException();
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
