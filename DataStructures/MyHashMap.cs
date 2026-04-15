public class MyHashMap<T> : IMyCollection<T> where T : notnull
{
    public Dictionary<T, T> Value { get; private set; }

    public MyHashMap(T data)
    {
        var properties = data.GetType().GetProperties();

        foreach (var p in properties)
        {
            if (p.PropertyType == typeof(T))
            {
                var raw = p.GetValue(data);

                if (raw is T typed) Value[typed] = typed;
            }
        }
    }

    public MyHashMap()
    {
        Value = new Dictionary<T, T>();
    }

    public int Count => Value.Count;

    public bool Dirty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Add(T item)
    {
        Value.Add(item, item);
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
        foreach (var value in Value.Values)
        {
            yield return value;
        }
    }

    public IMyIterator<T> GetMyIterator()
    {
        return new MyIterator(Value.Values.ToArray());
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

    private class MyIterator : IMyIterator<T>
    {
        private readonly T[] _values;
        private int _currentIndex;

        public MyIterator(T[]? array)
        {
            _values = array ?? Array.Empty<T>();
            _currentIndex = -1;
        }

        public bool HasNext()
        {
            return _currentIndex + 1 < _values.Length;
        }

        public T Next()
        {
            if (!HasNext()) throw new InvalidOperationException("No more elements.");

            _currentIndex++;
            return _values[_currentIndex];
        }

        public void Reset()
        {
            _currentIndex = -1;
        }
    }
}

