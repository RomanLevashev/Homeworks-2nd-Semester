// <copyright file="UnitTests.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace TrieTest;

/// <summary>
/// Contains unit tests for the <see cref="Trie.Trie"/> class.
/// </summary>
[TestClass]
public sealed class UnitTests
{
    /// <summary>
    /// Tests basic Add and Contains functionality of the Trie.
    /// </summary>
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

    /// <summary>
    /// Tests Remove functionality and Trie state after deletions.
    /// </summary>
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
        Assert.AreEqual(0, trie.Root.Children.Count);
    }
}
