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
        if (_size >= _array.Length - 1) return false;
        _size++;
        _array[_size - 1] = item;
        return true;
    }

    //remove function
    public void Remove(T item)
    {
        if (_array.Length == 0)
        {
            return;
        }

        for (int i = 0; i < _size; i++)
        {
            if (_array[i].Equals(item))
            {
                // Shift elements to the left
                for (int j = i; j < _size - 1; j++)
                {
                    _array[j] = _array[j + 1];
                }
                _size--;
                return;
            }
        }
    }
    //findby index function
    public T? FindBy<K>(K key, Func<T, K, int> comparer)
    {
        for (int i = 0; i < _size; i++)
        {
            if (comparer(_array[i], key) == 0)
            {
                return _array[i];
            }
        }
        return default;
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
        for (int i = 0; i < _size - 1; i++)
        {
            T key = _array[i];
            int j = i - 1;

            while (j >= 0 && comparison(_array[j], key) > 0)
            {
                _array[j + 1] = _array[j];
                j--;
            }

            _array[j + 1] = key;
        }
    }

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
        Array.Clear(_array, 0, _array.Length);
    }

    public R Reduce<R>(Func<R, T, R> accumulator)
    {
        R acc = default; ;
        for (int i = 0; i < _array.Length; i++)
        {
            acc = accumulator(acc, _array[i]);
        }
        return acc;

    }

    public R Reduce<R>(R initial, Func<R, T, R> accumulator)
    {

        R acc = initial;
        for (int i = 0; i < _size; i++)
        {
            acc = accumulator(acc, _array[i]);
        }

        return acc;

    }

    public IMyIterator<T> GetMyIterator()
    {
        return new MyIterator<T>(_array, _size);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _size; i++)
        {
            yield return _array[i];
        }
    }
}
public class MyIterator<T> : IMyIterator<T>
{
    private T[] _array;
    private int _size;
    private int _currentIndex;

    public MyIterator(T[] array, int size)
    {
        _array = array;
        _size = size;
        _currentIndex = 0;
    }
    //has next function
    public bool HasNext()
    {
        return _currentIndex < _size;
    }
    //next function
    public T Next()
    {
        if (!HasNext())
        {
            throw new InvalidOperationException("No more elements in the collection.");
        }
        return _array[_currentIndex++];
    }
    //reset function
    public void Reset()
    {
        _currentIndex = 0;
    }
}
