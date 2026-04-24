using Xunit;

public class MyArrayTests
{
    [Fact]
    public void Add_IncreaseCount()
    {
        var arr = new MyArray<int>();

        arr.Add(10);
        arr.Add(20);

        Assert.Equal(2, arr.Count);
    }

    [Fact]
    public void Add_StoreValuesCorrectly()
    {
        var arr = new MyArray<int>();

        arr.Add(5);
        arr.Add(10);

        var first = arr.FindBy(5, (item, key) => item.CompareTo(key));

        Assert.Equal(5, first);
    }

    [Fact]
    public void Remove_DecreaseCount()
    {
        var arr = new MyArray<int>();

        arr.Add(1);
        arr.Add(2);
        arr.Add(3);

        arr.Remove(2);

        Assert.Equal(2, arr.Count);
    }

    [Fact]
    public void Remove_RemoveItem()
    {
        var arr = new MyArray<int>();

        arr.Add(1);
        arr.Add(2);
        arr.Add(3);

        arr.Remove(2);

        var result = arr.FindBy(2, (item, key) => item.CompareTo(key));

        Assert.Equal(default(int), result);
    }

    [Fact]
    public void Filter_OnlyMatchingItems()
    {
        var arr = new MyArray<int>();

        arr.Add(1);
        arr.Add(2);
        arr.Add(3);
        arr.Add(4);

        var filtered = arr.Filter(x => x % 2 == 0);

        Assert.Equal(2, filtered.Count);
    }

    [Fact]
    public void Sort_OrderElements()
    {
        var arr = new MyArray<int>();

        arr.Add(3);
        arr.Add(1);
        arr.Add(2);

        arr.Sort((a, b) => a.CompareTo(b));

        var iterator = arr.GetMyIterator();

        Assert.Equal(1, iterator.Next());
        Assert.Equal(2, iterator.Next());
        Assert.Equal(3, iterator.Next());
    }

    [Fact]
    public void Reduce_WithInitialValue_ShouldSumCorrectly()
    {
        var arr = new MyArray<int>();

        arr.Add(1);
        arr.Add(2);
        arr.Add(3);

        var result = arr.Reduce(0, (acc, item) => acc + item);

        Assert.Equal(6, result);
    }

    [Fact]
    public void Iterator_ShouldTraverseAllElements()
    {
        var arr = new MyArray<int>();

        arr.Add(10);
        arr.Add(20);

        var it = arr.GetMyIterator();

        Assert.True(it.HasNext());
        Assert.Equal(10, it.Next());
        Assert.Equal(20, it.Next());
        Assert.False(it.HasNext());
    }
}