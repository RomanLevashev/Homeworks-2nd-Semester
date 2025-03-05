namespace BurrowsWheelerTest
{
    using BurrowsWheelerTransform;
    [TestClass]
    public sealed class BWTTest
    {

        [TestMethod]
        public void DefaultString()
        {
            TestTransformAndInvert("banana", "nnbaaa");
        }

        [TestMethod]
        public void ConstantString()
        {
            TestTransformAndInvert("aaaa", "aaaa");
        }

        [TestMethod]
        public void OneCharacterString()
        {
            TestTransformAndInvert("a", "a");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmptyStr()
        {
            TestTransformAndInvert(string.Empty, string.Empty);
        }

        private static void TestTransformAndInvert(string testStr, string expectedTransformResult)
        {
            var transformedResult = BurrowsWheeller.Transform(testStr);
            Assert.AreEqual(transformedResult.transformed, expectedTransformResult);
            var invertResult = BurrowsWheeller.InverseTransform(transformedResult.transformed, transformedResult.position);
            Assert.AreEqual(invertResult, testStr);
        }
    }
}
