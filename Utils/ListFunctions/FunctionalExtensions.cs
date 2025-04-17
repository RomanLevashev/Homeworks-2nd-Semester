namespace ListFunctions
{
    /// <summary>
    /// Provides functional extensions for IEnumerable.
    /// </summary>
    public static class FunctionalExtensions
    {
        /// <summary>
        /// Transforms each element of a sequence into a new form (Projection operation).
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the result sequence.</typeparam>
        /// <param name="source">The source sequence to transform.</param>
        /// <param name="func">A transform function to apply to each element.</param>
        /// <returns>A List whose elements are the result of invoking the transform function on each element of source.</returns>
        public static List<TResult> Map<T, TResult>(this IEnumerable<T> source, Func<T, TResult> func)
        {
            List<TResult> result = [];

            foreach (var item in source)
            {
                result.Add(func(item));
            }

            return result;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence to filter.</param>
        /// <param name="filter">A function to test each element for a condition.</param>
        /// <returns>A List that contains elements from the input sequence that satisfy the condition.</returns>
        public static List<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> filter)
        {
            List<T> result = [];

            foreach (var item in source)
            {
                if (filter(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Performs a reduction operation on a sequence (also known as aggregate/fold operation).
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="source">The source sequence to reduce.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <returns>The final accumulator value.</returns>
        public static TAccumulate Fold<T, TAccumulate>(this IEnumerable<T> source, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func)
        {
            TAccumulate accumulator = seed;

            foreach (var item in source)
            {
                accumulator = func(accumulator, item);
            }

            return accumulator;
        }
    }
}
