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
        if (left != null)
        {
            foreach (var item in left)
                yield return item;
        }

        yield return value;

        if (right != null)
        {
            foreach (var item in right)
                yield return item;
        }
    }
    // Rushil 
    public IMyIterator<T> GetMyIterator()
    {
        return new MyIterator(this);
    }

    // Rushil 
    public R Reduce<R>(Func<R, T, R> accumulator)
    {
        using var enumerator = GetEnumerator();

        if (!enumerator.MoveNext())
            throw new InvalidOperationException("Tree is empty");

        R result = (R)(object)enumerator.Current!;

        while (enumerator.MoveNext())
        {
            result = accumulator(result, enumerator.Current);
        }

        return result;
    }

    // Rushil 
    public R Reduce<R>(R initial, Func<R, T, R> accumulator)
    {
        R result = initial;

        foreach (var item in this)
        {
            result = accumulator(result, item);
        }

        return result;
    }

    // Jing 
    public void Remove(T item)
    {
        MyBinarySearchTree<T>? current = this;
        MyBinarySearchTree<T>? parent = null;

        while (current != null && !current.value!.Equals(item))
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

            return;
        }

        MyBinarySearchTree<T>? child = current.left ?? current.right;

        if (parent == null)
        {
            if (child == null)
            {
                value = default!;
                left = null;
                right = null;
            }

            else
            {
                value = child.value;
                left = child.left;
                right = child.right;
            }
        }

        else
        {
            if (parent.left == current) parent.left = child;
            else parent.right = child;
        }
    }

    // rushil 
    public void Sort(Comparison<T> comparison)
    {
        int count = Count;
        T[] items = new T[count];
        int index = 0;


        void FillArray(MyBinarySearchTree<T>? node)
        {
            if (node == null) return;

            FillArray(node.left);
            items[index++] = node.value;
            FillArray(node.right);
        }
        FillArray(this);


        void MergeSort(T[] array, int left, int right)
        {
            if (left >= right) return;

            int mid = (left + right) / 2;

            MergeSort(array, left, mid);

            MergeSort(array, mid + 1, right);
            Merge(array, left, mid, right);
        }

        void Merge(T[] array, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            T[] L = new T[n1];
            T[] R = new T[n2];

            for (int i = 0; i < n1; i++)
                L[i] = array[left + i];

            for (int j = 0; j < n2; j++)
                R[j] = array[mid + 1 + j];

            int iL = 0, iR = 0, k = left;

            while (iL < n1 && iR < n2)
            {
                if (comparison(L[iL], R[iR]) <= 0)
                    array[k++] = L[iL++];
                else
                    array[k++] = R[iR++];
            }

            while (iL < n1)
                array[k++] = L[iL++];

            while (iR < n2)
                array[k++] = R[iR++];
        }

        MergeSort(items, 0, items.Length - 1);

        left = null;
        right = null;
        value = default!;

        foreach (var item in items)
        {
            Add(item);
        }

    }


    public class MyIterator : IMyIterator<T>
    {
        private T[] items;
        private int index;

        public MyIterator(MyBinarySearchTree<T> tree)
        {
            int count = tree.Count;
            items = new T[count];
            index = 0;

            int i = 0;

            void Fill(MyBinarySearchTree<T>? node)
            {
                if (node == null) return;

                Fill(node.left);
                items[i++] = node.value;
                Fill(node.right);
            }

            Fill(tree);
        }

        public bool HasNext()
        {
            return index < items.Length;
        }

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("No more elements");

            return items[index++];
        }

        public void Reset()
        {
            index = 0;
        }
    }
}


