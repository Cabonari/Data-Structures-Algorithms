public class MyBinarySearchTree<T> : IMyCollection<T> where T : class, IComparable<T>
{
    public T value;
    public MyBinarySearchTree<T>? left;
    public MyBinarySearchTree<T>? right;

    public MyBinarySearchTree(T data)
    {
        value = data;
    }
    public int Count => 1 + (left?.Count ?? 0) + (right?.Count ?? 0);

    private bool dirty;
    public bool Dirty { get => dirty; set => dirty = value; }

    public void Add(T item)
    {
        if (item.CompareTo(value) < 0)
        {
            if (left == null)
                left = new MyBinarySearchTree<T>(item);
            else
                left.Add(item);
        }
        else
        {
            if (right == null)
                right = new MyBinarySearchTree<T>(item);
            else
                right.Add(item);
        }
    }

    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public T? FindBy<K>(K key, Func<T, K, int> comparer)
    {
        int cmp = comparer(value, key);

        if (cmp == 0)
            return value;
        else if (cmp > 0)
            return left.FindBy(key, comparer);
        else
            return right.FindBy(key, comparer);
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public IMyIterator<T> GetMyIterator()
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

    public void Remove(T item)
    {
        throw new NotImplementedException();
    }

    public void Sort(Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }
}