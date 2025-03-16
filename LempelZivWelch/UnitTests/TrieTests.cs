namespace UnitTests
{
    using ByteTrie;

    /// <summary>
    /// Contains unit tests for the Trie class.
    /// </summary>
    [TestClass]
    public sealed class TrieTests
    {
        /// <summary>
        /// Tests the <see cref="Trie.Add(byte[])"/> and <see cref="Trie.Contains(byte[])"/> methods.
        /// Verifies that elements can be successfully added to the Trie and then checked for existence.
        /// </summary>
        [TestMethod]
        public void AddAndContainsTest()
        {
            Trie trie = new();
            trie.Add((byte)0);
            trie.Add(new byte[] { 0, 1 });
            trie.Add(new byte[] { 0, 1, 2 });

            Assert.IsFalse(trie.Add((byte)0).isSuccess);
            Assert.IsTrue(trie.Contains((byte)0));
            Assert.IsTrue(trie.Contains(new byte[] { 0, 1, 2 }));
            Assert.IsTrue(trie.Contains(new byte[] { 0, 1 }));
        }
    }
}