namespace MyLinqTests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MyLinq"/> static class.
    /// </summary>
    [TestClass]
    public sealed class MyLinqTests
    {
        /// <summary>
        /// Tests the <see cref="MyLinq.GetPrimes"/> method to verify it generates
        /// the correct sequence of prime numbers.
        /// </summary>
        [TestMethod]
        public void GetPrimeNumbersTest()
        {
            int[] primes = [2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71];
            int i = 0;

            foreach (int prime in MyLinq.MyLinq.GetPrimes())
            {
                if (prime > 71)
                {
                    break;
                }

                 Assert.AreEqual(primes[i++], prime);
            }
        }

        /// <summary>
        /// Tests the <see cref="MyLinq.Take{T}"/> extension method to verify it correctly
        /// returns the specified number of elements from the start of a sequence.
        /// </summary>
        [TestMethod]
        public void TakeTest()
        {
            int[] testArray = [1, 2, 3, 4, 5, 6];
            int index = 0;

            foreach (int value in MyLinq.MyLinq.Take(testArray, 3))
            {
                Assert.AreEqual(testArray[index++], value);
            }
        }

        /// <summary>
        /// Tests the <see cref="MyLinq.Skip{T}"/> extension method to verify it correctly
        /// bypasses the specified number of elements and returns the remaining elements.
        /// </summary>
        [TestMethod]
        public void SkipTest()
        {
            int[] testArray = [1, 2, 3, 4, 5, 6];
            int index = 3;

            foreach (int value in testArray.Skip(3))
            {
                Assert.AreEqual(testArray[index++], value);
            }
        }

        /// <summary>
        /// Tests the chained operation of <see cref="MyLinq.Skip{T}"/> followed by <see cref="MyLinq.Take{T}"/>
        /// to verify correct behavior when methods are composed.
        /// </summary>
        [TestMethod]
        public void ComplexTest()
        {
            int[] testArray = [1, 2, 3, 4, 5, 6];
            int[] expectedArray = [2, 3];
            int index = 0;

            foreach (int value in testArray.Skip(1).Take(2))
            {
                Assert.AreEqual(expectedArray[index++], value);
            }
        }
    }
}
