
public class MyBinarySearchTree<T> : IMyCollection<T>
{

    public T label;
    public MyBinarySearchTree<T> left;
    public MyBinarySearchTree<T> right;

    public MyBinarySearchTree(T data)
    {
        label = data;
        left = null;
        right = null;
    }

    public int Count => throw new NotImplementedException();

    public bool Dirty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    // Julian 
    public void Add(T item)
    {
        throw new NotImplementedException();
    }

    // Julian 
    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }

    // Julian 
    public T? FindBy<K>(K key, Func<T, K, int> comparer)
    {
        throw new NotImplementedException();
    }

    // Rushil 
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }
    // Rushil 
    public IMyIterator<T> GetMyIterator()
    {
        throw new NotImplementedException();
    }

    // Rushil 
    public R Reduce<R>(Func<R, T, R> accumulator)
    {
        throw new NotImplementedException();
    }

    // Rushil 
    public R Reduce<R>(R initial, Func<R, T, R> accumulator)
    {
        throw new NotImplementedException();
    }

    // Jing 
    public void Remove(T item)
    {
        throw new NotImplementedException();
    }
    
    // rushil 
    public void Sort(Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }
}





