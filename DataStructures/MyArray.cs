

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
    //add function
    public void Add(T item)
    {
        if (_index >= _data.Length - 1) return false;
        _index++;
        _data[_index] = key;
        return true;
    }

    //resize function

    //remove function
    public void Remove(T item)
    {
        for (int i = 0; i < _size; i++)
        {
            if (_array[i] == item)
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

    //sort function

    //Class iterator 

    //has next function

    //next function

    //reset function

}
