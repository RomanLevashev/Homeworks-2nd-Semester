namespace TrieTest
{
    [TestClass]
    public sealed class UnitTests
    {
        [TestMethod]
        public void AddAndContainsTest()
        {
            Trie.Trie trie = new();
            trie.Add("a");
            trie.Add("asd");
            trie.Add("asdb");

            Assert.IsFalse(trie.Add("a"));
            Assert.IsTrue(trie.Contains("a"));
            Assert.IsTrue(trie.Contains("asd"));
            Assert.IsTrue(trie.Contains("asdb"));
            Assert.IsFalse(trie.Contains("as"));
        }

        [TestMethod]
        public void RemoveTest()
        {
            Trie.Trie trie = new();
            trie.Add("a");
            trie.Add("asd");
            trie.Add("asdb");
            Assert.IsFalse(trie.Remove("as"));
            Assert.IsTrue(trie.Remove("a"));
            Assert.IsFalse(trie.Contains("a"));
            Assert.IsTrue(trie.Contains("asd"));
            Assert.IsTrue(trie.Contains("asdb"));
            Assert.IsTrue(trie.Remove("asd"));
            Assert.IsTrue(trie.Contains("asdb"));
            Assert.IsFalse(trie.Contains("asd"));
            Assert.IsTrue(trie.Remove("asdb"));
            Assert.IsTrue(trie.Root.Children.Count == 0);
        }
    }
}
