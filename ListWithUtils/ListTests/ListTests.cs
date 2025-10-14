namespace ListTests
{
    using ListUtils;
    using TList;

    /// <summary>
    /// Contains unit tests for the <see cref="TList{T}"/> class and its associated utilities.
    /// </summary>
    [TestClass]
    public sealed class ListTests
    {
        /// <summary>
        /// Tests the <see cref="TList{T}.Add(T)"/> method and automatic capacity expansion.
        /// Verifies that elements are correctly added and the internal storage expands when needed.
        /// </summary>
        [TestMethod]
        public void AddAndExpandingTest()
        {
            TList<int> lst = new(5);

            for (int i = 0; i < 7; i++)
            {
                lst.Add(i);
            }

            for (int i = 0; i < 7; i++)
            {
                Assert.AreEqual(i, lst[i]);
            }

            Assert.AreEqual(7, lst.Count);
        }

        /// <summary>
        /// Tests boundary conditions and exception throwing behavior.
        /// Verifies that:
        /// 1. Constructor throws <see cref="ArgumentOutOfRangeException"/> for negative size
        /// 2. Indexer throws <see cref="IndexOutOfRangeException"/> for out-of-range access.
        /// </summary>
        [TestMethod]
        public void IndexExceptionAndSizeExceptionTest()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new TList<int>(-1));
            TList<int> lst = new(2);
            Assert.ThrowsException<IndexOutOfRangeException>(() => lst[2]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => lst[3] = 4);
        }

        /// <summary>
        /// Tests the indexer's set and get functionality.
        /// Verifies that values can be properly stored and retrieved by index.
        /// </summary>
        [TestMethod]
        public void SetAndGetIndexTests()
        {
            TList<int> lst = new(5);
            lst.Add(0);
            lst[0] = 3;
            Assert.AreEqual(3, lst[0]);
        }

        /// <summary>
        /// Tests the sorting functionality using the default comparer.
        /// Verifies that elements are sorted in ascending order after calling <see cref="ListUtils.Sort{T}(TList{T}, IComparer{T})"/>.
        /// </summary>
        [TestMethod]
        public void SortTest()
        {
            TList<int> lst = new(10);

            for (int i = 10; i > 0; i--)
            {
                lst.Add(i);
            }

            lst.Sort(Comparer<int>.Default);

            for (int i = 0; i < 9; i++)
            {
                Assert.IsTrue(lst[i] <= lst[i + 1]);
            }
        }
    }
}
