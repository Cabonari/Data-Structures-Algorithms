

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

    //resize function

    //remove function

    //findby index function

    // filter function

    //sort function

    //Class iterator 

    //has next function

    //next function

    //reset function

}
