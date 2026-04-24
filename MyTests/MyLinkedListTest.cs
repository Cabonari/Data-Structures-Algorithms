using Xunit;

public class MyLinkedListTest
{
    
    [Fact]
    public void Add_IncreaseCount()
    {
        var list = new MyLinkedList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);

        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void Remove_DecreaseCount()
    {
        var list = new MyLinkedList<int>();

        list.Add(10);
        list.Add(20);
        list.Add(30);

        list.Remove(20);

        Assert.Equal(2, list.Count);
    }

    [Fact]
    public void FindBy_CorrectValue()
    {
        var list = new MyLinkedList<int>();

        list.Add(5);
        list.Add(10);
        list.Add(15);

        var result = list.FindBy(10, (item, key) => item.CompareTo(key));

        Assert.Equal(10, result);
    }

    
    [Fact]
    public void Filter_OnlyEvenNumbers()
    {
        var list = new MyLinkedList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);

        var filtered = list.Filter(x => x % 2 == 0);

        Assert.Equal(2, filtered.Count);
    }

    [Fact]
    public void Sort_OrderElements()
    {
        var list = new MyLinkedList<int>();

        list.Add(3);
        list.Add(1);
        list.Add(2);

        list.Sort((a, b) => a.CompareTo(b));

        int[] result = new int[3];
        int index = 0;

        foreach (var item in list)
        {
            result[index++] = item;
        }

        Assert.Equal(1, result[0]);
        Assert.Equal(2, result[1]);
        Assert.Equal(3, result[2]);
    }

}