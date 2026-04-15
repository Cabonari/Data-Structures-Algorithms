
public class MyHashMap<T> : IMyCollection<T> where T : notnull
{
    public Dictionary<T, T> Value { get; private set; }

    public MyHashMap(T data)
    {
        Value = data.GetType()
            .GetProperties()
            .Where(p => p.PropertyType == typeof(T))
            .ToDictionary(p => (T)p.GetValue(data)!, p => (T)p.GetValue(data)!);
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
        var result = new MyHashMap<T>();

        foreach (var item in Value.Values)
        {
            if (predicate(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    public T? FindBy<K>(K key, Func<T, K, int> comparer)
    {
        foreach (var item in Value.Values)
        {
            if (comparer(item, key) == 0)
            {
                return item;
            }
        }

        return default;
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
        if (Value.Count == 0)
            throw new InvalidOperationException("Collection is empty.");

        var enumerator = Value.Values.GetEnumerator();
        enumerator.MoveNext();

        R result = (R)(object)enumerator.Current;
        while (enumerator.MoveNext())
        {
            result = accumulator(result, enumerator.Current);
        }
        return result;
    }

    public R Reduce<R>(R initial, Func<R, T, R> accumulator)
    {
        R result = initial;
        foreach (var value in Value.Values)
        {
            result = accumulator(result, value);
        }
        return result;
    }

    public void Remove(T item)
    {
        Value.Remove(item);
    }

    public void Sort(Comparison<T> comparison)
    {
        var sortedEntries = Value
            .OrderBy(item => item.Key, Comparer<T>.Create(comparison))
            .ToList();

        Value = new Dictionary<T, T>();
        foreach (var item in sortedEntries)
        {
            Value[item.Key] = item.Value;
        }
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

