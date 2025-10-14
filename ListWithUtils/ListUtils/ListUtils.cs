namespace ListUtils
{
    using TList;

    /// <summary>
    /// Provides utility methods for working with <c>TList&lt;T&gt;</c> collections.
    /// </summary>
    public static class ListUtils
    {
        /// <summary>
        /// Sorts the elements in the specified <see cref="TList{T}"/> using the specified comparer.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to sort. Must not be <c>null</c>.</param>
        /// <param name="comparer">The comparer used to compare list elements. Must not be <c>null</c>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="list"/> or <paramref name="comparer"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// This method uses the bubble sort algorithm and has a time complexity of O(n²).
        /// It modifies the original list in-place.
        /// </remarks>
        public static void Sort<T>(this TList<T> list, IComparer<T> comparer)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (comparer.Compare(list[j], list[j + 1]) > 0)
                    {
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
    }
}
