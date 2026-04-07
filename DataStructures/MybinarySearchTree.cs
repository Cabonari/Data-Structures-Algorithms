public class MyBinarySearchTree<T> : IMyCollection<T>
{
    public T value;
    public MyBinarySearchTree<T>? left;
    public MyBinarySearchTree<T>? right;

    public MyBinarySearchTree(T data)
    {
        value = data;
    }

    public MyBinarySearchTree()
    {
        //voor lege tree
    }

    public int Count => 1 + (left?.Count ?? 0) + (right?.Count ?? 0);

    private bool dirty;
    public bool Dirty { get => dirty; set => dirty = value; }

    // Julian 
    public void Add(T item)
    {
        if (value == null)
        {
            value = item;
            return;
        }

        if (Comparer<T>.Default.Compare(item, value) < 0)
        {
            if (left == null)
                left = new MyBinarySearchTree<T>(item);
            else
                left.Add(item);
        }
        else
        {
            if (right == null)
                right = new MyBinarySearchTree<T>(item);
            else
                right.Add(item);
        }
    }

    // Julian 
    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        MyBinarySearchTree<T>? result = null;

        void AddIfMatch(T item)
        {
            if (predicate(item))
            {
                if (result == null)
                    result = new MyBinarySearchTree<T>(item);
                else
                    result.Add(item);
            }
        }

        void Traverse(MyBinarySearchTree<T>? node)
        {
            if (node == null) return;

            AddIfMatch(node.value);
            Traverse(node.left);
            Traverse(node.right);
        }

        Traverse(this);

        return result!;
    }

    // Julian 
    public T? FindBy<K>(K key, Func<T, K, int> comparer)
    {
        int cmp = comparer(value, key);

        if (cmp == 0)
            return value;
        else if (cmp > 0)
            return left.FindBy(key, comparer);
        else
            return right.FindBy(key, comparer);
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

        while (current != null && current.value!.Equals(item))
        {
            parent = current;

            if (Comparer<T>.Default.Compare(item, current.value) < 0) current = current.left;
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

            current.value = successor.value;

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





