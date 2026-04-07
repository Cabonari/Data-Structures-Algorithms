
using System.Transactions;

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
        MyBinarySearchTree<T>? current = this;
        MyBinarySearchTree<T>? parent = null;

        while (current != null && current.label!.Equals(item))
        {
            parent = current;

            if (Comparer<T>.Default.Compare(item, current.label) < 0) current = current.left;
            else current = current.right;
        }

        if (current == null) return;

        if (current.left == null || current.right == null)
        {
            MyBinarySearchTree<T>? child = current.left ?? current.right;

            if(parent.left == current) parent.left = child;
            else parent.right = child;
        }
        else
        {
            MyBinarySearchTree<T> successorParent = current;
            MyBinarySearchTree<T> successor = current.right;

            while (successor.left != null)
            {
                successorParent = successor;
                successor = successor.left;
            }

            current.label = successor.label;

            if (successorParent.left == successor) successorParent.left = successor.right;
            else successorParent.right = successor.right;
        }
    }
    
    // rushil 
    public void Sort(Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }
}





