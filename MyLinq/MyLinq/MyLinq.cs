namespace MyLinq
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a set of custom LINQ-like extension methods and sequence generators.
    /// </summary>
    public static class MyLinq
    {
        /// <summary>
        /// Generates an infinite sequence of prime numbers using a lazy evaluation approach.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Int32}"/> that yields prime numbers in ascending order.</returns>
        public static IEnumerable<int> GetPrimes()
        {
            yield return 2;

            for (int candidate = 3; ; candidate += 2)
            {
                if (IsPrime(candidate))
                {
                    yield return candidate;
                }
            }
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from the start of a sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="seq">The sequence to return elements from.</param>
        /// <param name="n">The number of elements to return.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the specified number of elements from the start of the input sequence.</returns>
        public static IEnumerable<T> Take<T>(this IEnumerable<T> seq, int n)
        {
            if (seq is null)
            {
                throw new ArgumentNullException(nameof(seq));
            }

            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            int currentNumber = 1;

            foreach (T item in seq)
            {
                if (currentNumber++ > n)
                {
                    break;
                }

                yield return item;
            }
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="seq">The sequence to skip elements from.</param>
        /// <param name="n">The number of elements to skip.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the elements that occur after the specified index in the input sequence.</returns>
        public static IEnumerable<T> Skip<T>(this IEnumerable<T> seq, int n)
        {
            if (seq is null)
            {
                throw new ArgumentNullException(nameof(seq));
            }

            if (n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            int currentNumber = 1;

            foreach (T item in seq)
            {
                if (currentNumber++ > n)
                {
                    yield return item;
                }
            }
        }

        private static bool IsPrime(int number)
        {
            if (number <= 1)
            {
                return false;
            }

            if (number == 2)
            {
                return true;
            }

            if (number % 2 == 0)
            {
                return false;
            }

            int bound = (int)Math.Sqrt(number);

            for (int i = 3; i <= bound; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
