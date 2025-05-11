namespace SkipList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a sorted collection of elements in a skip list data structure.
    /// A skip list is a probabilistic data structure that allows fast search, insertion, and deletion operations.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements in the skip list. It must implement the <see cref="IComparable{T}"/> interface
    /// for sorting and comparison purposes.
    /// </typeparam>
    public class SkipList<T> : IList<T>
        where T : IComparable<T>
    {
        private int version;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="SkipList{T}"/>.
        /// </summary>
        public int Count { get; private set; } = 0;

        /// <summary>
        /// Gets a value indicating whether the <see cref="SkipList{T}"/> is read-only.
        /// Always returns <c>false</c> as the collection is mutable.
        /// </summary>
        public bool IsReadOnly => false;

        private int CurrentLevel { get; set; } = 0;

        private SkipListNode<T> Head { get; set; } = new(default);

        private Random Random { get; } = new();

        /// <summary>
        /// Gets or sets the element at the specified index in the <see cref="SkipList{T}"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        /// <remarks>
        /// The getter retrieves the element at the specified position in the sorted skip enumeratedList.
        /// The setter inserts the new value into the skip enumeratedList, maintaining the sorted order;
        /// the index itself is ignored during insertion.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the specified <paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.
        /// </exception>
        public T this[int index]
        {
            get
            {
                return this.GetElementByIndex(index).SearchElement.Value!;
            }

            set
            {
                this.Insert(index, value);
            }
        }

        /// <summary>
        /// Adds a value to the <see cref="SkipList{T}"/> while maintaining its sorted order.
        /// </summary>
        /// <param name="value">The value to add to the skip enumeratedList.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is <c>null</c>.
        /// Note: This applies only if <typeparamref name="T"/> is a reference type.
        /// </exception>
        public void Add(T value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            (SkipListNode<T>[] path, bool isFound, SkipListNode<T> searchNode) = this.FindPath(value);
            int newNodeLevel = this.GetRandomLevel();

            if (newNodeLevel > this.CurrentLevel)
            {
                var tempPath = new SkipListNode<T>[newNodeLevel + 1];
                Array.Copy(path, tempPath, path.Length);

                for (int i = this.CurrentLevel + 1; i <= newNodeLevel; i++)
                {
                    tempPath[i] = this.Head;
                }

                path = tempPath;
                this.CurrentLevel = newNodeLevel;
            }

            SkipListNode<T> newNode = new(value);

            for (int i = 0; i <= newNodeLevel; i++)
            {
                if (path[i].Forward.ContainsKey(i))
                {
                    newNode.Forward[i] = path[i].Forward[i];
                }

                path[i].Forward[i] = newNode;
            }

            this.Count++;
            this.version++;
        }

        /// <summary>
        /// Inserts an item into the <see cref="SkipList{T}"/>.
        /// </summary>
        /// <param name="index">
        /// The index at which to insert the item. This parameter is accepted for interface compatibility
        /// but is ignored, as the skip enumeratedList maintains sorted order automatically.
        /// </param>
        /// <param name="item">The item to insert into the skip enumeratedList.</param>
        /// <remarks>
        /// This method adds the item to the skip enumeratedList at the appropriate position according to the sort order,
        /// not necessarily at the specified index.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="item"/> is a reference type and is null.
        /// </exception>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
            }

            this.Add(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="SkipList{T}"/> to a specified array, starting at a particular array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the elements copied from the skip enumeratedList.
        /// The array must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying begins.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="array"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown when <paramref name="arrayIndex"/> is less than 0, or there is not enough space from
        /// <paramref name="arrayIndex"/> to the end of the array to accommodate all elements from the skip enumeratedList.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new IndexOutOfRangeException();
            }

            if (array.Length - arrayIndex < this.Count)
            {
                throw new IndexOutOfRangeException(nameof(arrayIndex));
            }

            var current = this.Head;
            int i = 0;

            foreach (var item in this)
            {
                array[arrayIndex + i++] = item;
            }
        }

        /// <summary>
        /// Determines whether the <see cref="SkipList{T}"/> contains a specific value.
        /// </summary>
        /// <param name="value">The value to locate in the skip enumeratedList.</param>
        /// <returns><c>true</c> if the value is found in the skip enumeratedList; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is <c>null</c>.
        /// Note: This applies only if <typeparamref name="T"/> is a reference type.
        /// </exception>
        public bool Contains(T value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var searchResult = this.FindPath(value);

            return searchResult.IsFound;
        }

        /// <summary>
        /// Returns the zero-based index of the first occurrence of a specific item in the <see cref="SkipList{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the skip enumeratedList.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="item"/> within the skip enumeratedList,
        /// if found; otherwise, <c>-1</c>.
        /// </returns>
        public int IndexOf(T item)
        {
            var current = this.Head;
            int index = 0;

            foreach (var value in this)
            {
                if (value.CompareTo(item) == 0)
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Removes the first occurrence of a specific value from the <see cref="SkipList{T}"/>.
        /// </summary>
        /// <param name="value">The value to remove from the skip enumeratedList.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="value"/> was successfully removed from the skip enumeratedList;
        /// otherwise, <c>false</c>. This method also returns <c>false</c> if <paramref name="value"/> was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="value"/> is <c>null</c>.
        /// Note: This applies only if <typeparamref name="T"/> is a reference type.
        /// </exception>
        public bool Remove(T value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            (SkipListNode<T>[] path, bool isFound, SkipListNode<T> searchNode) = this.FindPath(value);

            if (!isFound)
            {
                return false;
            }

            this.RemoveWithPath(path, value);

            this.Count--;
            this.version++;
            return true;
        }

        /// <summary>
        /// Removes the element at the specified index from the <see cref="SkipList{T}"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the <paramref name="index"/> is less than 0 or greater than or equal to <see cref="Count"/>.
        /// </exception>
        public void RemoveAt(int index)
        {
            var path = new SkipListNode<T>[this.CurrentLevel + 1];
            (path[0], var expectedNode) = this.GetElementByIndex(index);

            for (int i = 1; i <= this.CurrentLevel; i++)
            {
                var current = this.Head;
                while (current.Forward.ContainsKey(i) && current.Forward[i].Value!.CompareTo(expectedNode.Value) <= 0)
                {
                    {
                        if (object.ReferenceEquals(current.Forward[i], expectedNode))
                        {
                            path[i] = current;
                            break;
                        }

                        current = current.Forward[i];
                    }
                }

                if (path[i] == null)
                {
                    for (int j = i; j <= this.CurrentLevel; j++)
                    {
                        path[j] = this.Head;
                    }

                    break;
                }
            }

            this.RemoveWithPath(path, expectedNode.Value!);
            this.Count--;
            this.version++;
        }

        /// <summary>
        /// Removes all elements from the <see cref="SkipList{T}"/>.
        /// </summary>
        /// <remarks>
        /// Resets the skip enumeratedList to its initial state by reinitializing the head node,
        /// resetting the level to 0, and setting the element count to zero.
        /// </remarks>
        public void Clear()
        {
            this.Head = new SkipListNode<T>(default);

            this.CurrentLevel = 0;
            this.Count = 0;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the skip list.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the elements of the skip list.
        /// </returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the skip list.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the elements of the skip list.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        private void RemoveWithPath(SkipListNode<T>[] path, T value)
        {
            for (int i = 0; i < path.Length; i++)
            {
                var currentPreviousElement = path[i];

                if (currentPreviousElement.Forward.TryGetValue(i, out var deleteNode))
                {
                    if (deleteNode.Value!.CompareTo(value) == 0)
                    {
                        if (deleteNode.Forward.TryGetValue(i, out var nextNode))
                        {
                            currentPreviousElement.Forward[i] = nextNode;
                            continue;
                        }

                        currentPreviousElement.Forward.Remove(i);
                    }
                }
            }
        }

        private (SkipListNode<T>[] Path, bool IsFound, SkipListNode<T> SearchNode) FindPath(T value)
        {
            var current = this.Head;
            var path = new SkipListNode<T>[this.CurrentLevel + 1];
            for (int i = this.CurrentLevel; i >= 0; i--)
            {
                while (current.Forward.ContainsKey(i) && current.Forward[i].Value!.CompareTo(value) <= 0)
                {
                    if (current.Forward[i].Value!.CompareTo(value) == 0)
                    {
                        path[i] = current;
                        SkipListNode<T> searchNode = current.Forward[i--];

                        while (i >= 0)
                        {
                            while (!object.ReferenceEquals(current.Forward[i], searchNode))
                            {
                                current = current.Forward[i];
                            }

                            path[i--] = current;
                        }

                        return (path, true, searchNode);
                    }

                    current = current.Forward[i];
                }

                path[i] = current;
            }

            return (path, false, this.Head);
        }

        private (SkipListNode<T> Previous, SkipListNode<T> SearchElement) GetElementByIndex(int index)
        {
            if (index < 0 || index > this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
            }

            int currentIndex = -1;
            var current = this.Head;
            SkipListNode<T> previous = current;

            while (currentIndex < index)
            {
                previous = current;
                current = current.Forward[0];
                currentIndex++;
            }

            return (previous, current);
        }

        private int GetRandomLevel()
        {
            int level = 0;
            while (this.Random.NextDouble() < 0.5)
            {
                level++;
            }

            return level;
        }

        /// <summary>
        /// Enumerates the elements of a <see cref="SkipList{T}"/>.
        /// </summary>
        /// <remarks>
        /// This enumerator supports the standard iteration protocol and detects modifications to the
        /// collection during enumeration by tracking the version.
        /// </remarks>
        public class Enumerator(SkipList<T> list) : IEnumerator<T>
        {
            private readonly SkipList<T> enumeratedList = list;

            private SkipListNode<T> currentNode = list.Head;

            private int expectedVersion = list.version;

            /// <summary>
            /// Gets the current element in the enumerator.
            /// </summary>
            /// <value>
            /// The current element of type <typeparamref name="T"/> in the skip enumeratedList.
            /// </value>
            /// <remarks>
            /// This property returns the value of the node that the enumerator is currently pointing to.
            /// It will throw an <see cref="InvalidOperationException"/> if the enumerator is before the first element,
            /// after the last element, or the collection has been modified.
            /// </remarks>
            public T Current => this.currentNode.Value!;

            /// <summary>
            /// Gets the current element in the enumerator.
            /// </summary>
            /// <value>
            /// The current element of type <typeparamref name="T"/> in the skip enumeratedList.
            /// </value>
            /// <remarks>
            /// This property returns the value of the node that the enumerator is currently pointing to.
            /// It will throw an <see cref="InvalidOperationException"/> if the enumerator is before the first element,
            /// after the last element, or the collection has been modified.
            /// </remarks>
            object System.Collections.IEnumerator.Current => this.Current!;

            /// <summary>
            /// Advances the enumerator to the next element in the skip list.
            /// </summary>
            /// <returns>
            /// <c>true</c> if the enumerator was successfully advanced to the next element;
            /// <c>false</c> if the enumerator has passed the end of the collection.
            /// </returns>
            /// <exception cref="InvalidOperationException">
            /// Thrown if the collection was modified after the enumerator was created.
            /// </exception>
            /// <remarks>
            /// This method advances the enumerator to the next element in the skip list.
            /// If the enumerator is at the end of the list, the method will return <c>false</c>.
            /// If the collection is modified during iteration, an <see cref="InvalidOperationException"/> will be thrown.
            /// </remarks>
            public bool MoveNext()
            {
                if (this.expectedVersion != this.enumeratedList.version)
                {
                    throw new InvalidOperationException("Collection was modified");
                }

                if (this.currentNode.Forward.TryGetValue(0, out var nextNode))
                {
                    this.currentNode = nextNode;
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Resets the enumerator to the initial position (before the first element) of the skip list.
            /// </summary>
            /// <exception cref="InvalidOperationException">
            /// Thrown if the collection was modified after the enumerator was created.
            /// </exception>
            /// <remarks>
            /// This method resets the enumerator to its initial state, where it points to the start of the skip list,
            /// before the first element. If the collection has been modified after the enumerator was created,
            /// an <see cref="InvalidOperationException"/> will be thrown.
            /// </remarks>
            public void Reset()
            {
                if (this.expectedVersion != this.enumeratedList.version)
                {
                    throw new InvalidOperationException("Collection was modified");
                }

                this.currentNode = this.enumeratedList.Head;
            }

            /// <summary>
            /// Releases all resources used by the enumerator.
            /// </summary>
            public void Dispose()
            {
            }
        }
    }
}
