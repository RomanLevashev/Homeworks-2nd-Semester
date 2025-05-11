namespace SkipListTests
{
    using SkipList;

    /// <summary>
    /// Contains unit tests for the <see cref="SkipList{T}"/> class,
    /// verifying its correctness, robustness, and compliance with the <see cref="IList{T}"/> interface.
    /// </summary>
    [TestClass]
    public sealed class SkipListTests
    {
        /// <summary>
        /// Tests the <see cref="SkipList{T}.Add(T)"/> and <see cref="SkipList{T}.Remove(T)"/> methods
        /// by adding multiple elements, including duplicates, and then removing one occurrence.
        /// </summary>
        /// <remarks>
        /// Verifies that removing a value that has multiple occurrences only removes one instance,
        /// and that the remaining value is still found in the skip list.
        /// </remarks>
        [TestMethod]
        public void AddAndRemoveTests()
        {
            SkipList<int> slist = new();
            slist.Add(5);
            slist.Add(1);
            slist.Add(7);
            slist.Add(2);
            slist.Add(4);
            slist.Add(5);
            slist.Remove(5);
            Assert.IsTrue(slist.Contains(5));
        }

        /// <summary>
        /// Tests the <see cref="SkipList{T}"/> by adding a large range of duplicate values,
        /// then verifying the correctness of search and deletion operations.
        /// </summary>
        /// <remarks>
        /// Adds two of each integer from 1 to <c>rangeMax</c>, checks that all values are present,
        /// and then removes them one at a time, verifying count and content correctness after each deletion.
        /// </remarks>
        [TestMethod]
        public void AddSearchAndDeleteManyElementsWithDuplicates()
        {
            int rangeMax = 1000;
            var slist = new SkipList<int>();
            int[] values = Enumerable.Range(1, rangeMax).ToArray();

            foreach (int value in values)
            {
                slist.Add(value);
                slist.Add(value);
            }

            Assert.IsTrue(this.InList(values, slist));
            Assert.IsTrue(slist.Count == rangeMax * 2);
            int i = 0;

            foreach (int value in values)
            {
                slist.Remove(value);
                Assert.IsTrue(slist.Count == ((rangeMax * 2) - (i * 2) - 1));
                Assert.IsTrue(this.InList(values[i..rangeMax], slist));
                slist.Remove(value);
                Assert.IsTrue(slist.Count == ((rangeMax * 2) - (i * 2) - 2));
                Assert.IsTrue(this.InList(values[(++i)..rangeMax], slist));
            }
        }

        /// <summary>
        /// Verifies that the <see cref="SkipList{T}"/> enumeration iterates over elements in sorted order.
        /// </summary>
        /// <remarks>
        /// Adds a range of integers from 1 to <c>rangeMax</c> into the skip list and uses a foreach loop
        /// to ensure that the enumerator returns them in the same sorted order.
        /// </remarks>
        [TestMethod]
        public void ForeachTest()
        {
            int rangeMax = 1000;
            var slist = new SkipList<int>();
            int[] values = Enumerable.Range(1, rangeMax).ToArray();

            foreach (int value in values)
            {
                slist.Add(value);
            }

            int i = 0;

            foreach (var value in slist)
            {
                Assert.AreEqual(value, values[i++]);
            }
        }

        /// <summary>
        /// Tests the <see cref="SkipList{T}.RemoveAt(int)"/> method by removing all elements in reverse order.
        /// </summary>
        /// <remarks>
        /// Verifies that the count is updated correctly after each removal, the removed element is no longer contained in the list,
        /// and the remaining elements match the expected subset of the original input.
        /// </remarks>
        [TestMethod]
        public void RemoveAtTest()
        {
            int rangeMax = 1000;
            var slist = new SkipList<int>();
            int[] values = Enumerable.Range(1, rangeMax).ToArray();

            foreach (int value in values)
            {
                slist.Add(value);
            }

            for (int i = rangeMax - 1; i >= 0; i--)
            {
                slist.RemoveAt(i);
                Assert.AreEqual(i, slist.Count);
                Assert.IsFalse(slist.Contains(i + 1));
                if (i > 0)
                {
                    Assert.IsTrue(this.InList(values[0..(i - 1)], slist));
                }
            }
        }

        /// <summary>
        /// Tests index-based access and lookup operations in <see cref="SkipList{T}"/>.
        /// </summary>
        [TestMethod]
        public void IndexTest()
        {
            int rangeMax = 1000;
            var slist = new SkipList<int>();
            int[] values = Enumerable.Range(1, rangeMax).ToArray();

            foreach (int value in values)
            {
                slist.Add(value);
            }

            for (int i = 0; i < rangeMax; i++)
            {
                Assert.IsTrue(slist[i] == i + 1);
                Assert.IsTrue(slist.IndexOf(i + 1) == i);
            }
        }

        /// <summary>
        /// Tests the <see cref="SkipList{T}.CopyTo"/> method for correct element copying and proper exception handling.
        /// </summary>
        [TestMethod]
        public void CopyToTest()
        {
            int rangeMax = 1000;
            var slist = new SkipList<int>();
            int[] values = Enumerable.Range(1, rangeMax / 2).ToArray();

            foreach (int value in values)
            {
                slist.Add(value);
            }

            int[] destinationArray = new int[rangeMax];
            Assert.ThrowsException<IndexOutOfRangeException>(() => slist.CopyTo(destinationArray, (rangeMax / 2) + 1));
            Assert.ThrowsException<IndexOutOfRangeException>(() => slist.CopyTo(destinationArray, -1));
            Assert.ThrowsException<ArgumentNullException>(() => slist.CopyTo(null!, 1));

            slist.CopyTo(destinationArray, rangeMax / 2);

            for (int i = 0; i < (rangeMax / 2); i++)
            {
                Assert.IsTrue(values[i] == destinationArray[i + (rangeMax / 2)]);
            }
        }

        /// <summary>
        /// Verifies that the <see cref="SkipList{T}.Contains(T)"/> method returns <c>false</c>
        /// for elements that are not present in the list.
        /// </summary>
        [TestMethod]
        public void SearchShouldReturnFalseForNonExistentElement()
        {
            var slist = new SkipList<int>();
            slist.Add(3);
            slist.Add(7);
            slist.Add(9);
            slist.Add(5);
            slist.Remove(5);

            Assert.IsFalse(slist.Contains(5));
            Assert.IsFalse(slist.Contains(4));
        }

        /// <summary>
        /// Verifies that adding or removing <c>null</c> values throws <see cref="ArgumentNullException"/>
        /// when the skip list is used with reference types.
        /// </summary>
        [TestMethod]
        public void AddAndRemoveNullThrowsForReferenceTypes()
        {
            var slist = new SkipList<string>();

            Assert.ThrowsException<ArgumentNullException>(() => slist.Add(null!));
            Assert.ThrowsException<ArgumentNullException>(() => slist.Remove(null!));
        }

        /// <summary>
        /// Verifies that modifying the <see cref="SkipList{T}"/> after retrieving its enumerator
        /// causes the enumerator to throw an <see cref="InvalidOperationException"/> when used.
        /// </summary>
        [TestMethod]
        public void EnumeratorInvalidationTest()
        {
            var slist = new SkipList<int>();
            slist.Add(1);

            var enumerator = ((IEnumerable<int>)slist).GetEnumerator();
            SkipList<int> list = new();

            slist.Add(3);
            Assert.ThrowsException<InvalidOperationException>(() => enumerator.MoveNext());
        }

        private bool InList(int[] array, SkipList<int> list)
        {
            foreach (var element in array)
            {
                if (!list.Contains(element))
                {
                    return false;
                }
            }

            return true;
        }
    }
}