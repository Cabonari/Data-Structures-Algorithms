
public class MyBinarySearchTree<T> : IMyCollection<T>
{

    public int label;
    public MyBinarySearchTree<T> left;
    public MyBinarySearchTree<T> right;

    public MyBinarySearchTree(int data)
    {
        label = data;
        left = null;
        right = null;
    }

    public int Count => throw new NotImplementedException();

    public bool Dirty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Add(T item)
    {
        throw new NotImplementedException();
    }

    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public T? FindBy<K>(K key, Func<T, K, int> comparer)
    {
        throw new NotImplementedException();
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