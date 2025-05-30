namespace TList
{
    /// <summary>
    /// Represents a strongly typed list of objects that can be accessed by index.
    /// Provides methods to add items and automatically handles capacity expansion.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class TList<T>
    {
        private T[] items;

        /// <summary>
        /// Initializes a new instance of the <see cref="TList{T}"/> class that is empty
        /// and has the specified initial capacity.
        /// </summary>
        /// <param name="defaultSize">The initial number of elements that the list can contain.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="defaultSize"/> is negative.
        /// </exception>
        public TList(int defaultSize)
        {
            if (defaultSize < 0)
            {
                throw new ArgumentOutOfRangeException("Size can't be negative");
            }

            this.items = new T[defaultSize];
            this.Count = 0;
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="TList{T}"/>.
        /// </summary>
        /// <value>The number of elements actually contained in the list.</value>
        public int Count { get; private set; }

        /// <summary>
        /// Gets  the total number of elements that the list can hold without resizing.
        /// </summary>
        public int Capacity => this.items.Length;

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <value>The element at the specified index.</value>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown when <paramref name="index"/> is less than 0 or equal to/greater than <see cref="Count"/>.
        /// </exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                return this.items[index];
            }

            set
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                this.items[index] = value;
            }
        }

        /// <summary>
        /// Adds an object to the end of the <see cref="TList{T}"/>.
        /// Automatically expands the capacity if needed.
        /// </summary>
        /// <param name="item">The object to be added to the end of the list.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="item"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the list cannot be expanded further (reached maximum capacity).
        /// </exception>
        public void Add(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException();
            }

            if (this.Count == this.Capacity)
            {
                this.ExpandItems(this.Capacity * 2);
            }

            this.items[this.Count++] = item;
        }

        private void ExpandItems(int newCapacity)
        {
            if (newCapacity < this.Capacity)
            {
                throw new ArgumentOutOfRangeException("A list with this capacity will not be able to accommodate all the elements.");
            }

            if (newCapacity > Array.MaxLength)
            {
                throw new InvalidOperationException("List has reached maximum capacity");
            }

            T[] newItems = new T[newCapacity];

            Array.Copy(this.items, newItems, this.Count);
            this.items = newItems;
        }
    }
}
