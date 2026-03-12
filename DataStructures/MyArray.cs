public class MyArray<T> : IMyCollection<T>
{
    private T[] _array;
    private int _size;
    public MyArray(int capacity = 10)
    {
        _array = new T[capacity];
        _size = 0;
    }
    public int Count => _size;

    public bool Dirty { get; set; }


    //add function
    public void Add(T item)
    {
        throw new ArgumentException();
    }

    //resize function


    //remove function
    public void Remove(T item)
    {
        throw new ArgumentException();
    }

    //findby index function
    public T? FindBy<K>(K key, Func<T, K, int> comparer)
    {
        throw new ArgumentException();
    }
    // filter function
    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        int counter = 0;
        for (int i = 0; i < _array.Length; i++)
        {
            if (predicate(_array[i]) == true)
            {
                counter++;
            }
        }

        var filteredarray = new T[counter];
        int index = 0;
        for (int i = 0; i < _array.Length; i++)
        {
            if (predicate(_array[i]) == true)
            {
                filteredarray[index++] = _array[i];
            }

        }
        var result = new MyArray<T>(counter);
        result._array = filteredarray;
        result._size = Count;
        return result;
        
    }

    //sort function
    public void Sort(Comparison<T> comparison)
    {
        throw new ArgumentException();
    }

    //Class iterator 

    //has next function
    public bool HasNext()
    {
        throw new ArgumentException();
    }
    //next function
    public T Next()
    {
        throw new ArgumentException();
    }
    //reset function
    public void Reset()
    {
        throw new ArgumentException();
    }

    public R Reduce<R>(Func<R, T, R> accumulator)
    {
        throw new ArgumentException();

    }

    public R Reduce<R>(R initial, Func<R, T, R> accumulator)
    {
        throw new ArgumentException();
    }

    public IMyIterator<T> GetMyIterator()
    {
        throw new ArgumentException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new ArgumentException();
    }
}
