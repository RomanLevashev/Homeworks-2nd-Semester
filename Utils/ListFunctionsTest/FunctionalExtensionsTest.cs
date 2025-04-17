namespace ListFunctionsTest
{
    using ListFunctions;

    /// <summary>
    /// Contains unit tests for the FunctionalExtensions class.
    /// </summary>
    [TestClass]
    public sealed class FunctionalExtensionsTest
    {
        /// <summary>
        /// Tests the Map extension method by applying a transformation function to each element.
        /// </summary>
        [TestMethod]
        public void MapTest()
        {
            Func<int, int> plus = (x) => x + 2;
            List<int> testList = [1, 2, 3];
            List<int> expectedList = [3, 4, 5];

            Assert.IsTrue(expectedList.SequenceEqual(testList.Map(plus)));
        }

        /// <summary>
        /// Tests the Filter extension method by selecting elements that match a predicate.
        /// </summary>
        [TestMethod]
        public void FilterTest()
        {
            Func<int, bool> isEven = (x) => x % 2 == 0;
            List<int> testList = [1, 2, 3];
            List<int> expectedList = [2];

            Assert.IsTrue(expectedList.SequenceEqual(testList.Filter(isEven)));
        }

        /// <summary>
        /// Tests the Fold extension method by accumulating values with a reduction function.
        /// </summary>
        [TestMethod]
        public void FoldTest()
        {
            Func<int, int, int> multiply = (acc, elem) => acc * elem;
            List<int> testList = [1, 2, 3];
            int seed = 1;

            Assert.AreEqual(6, testList.Fold(seed, multiply));
        }
    }
}
