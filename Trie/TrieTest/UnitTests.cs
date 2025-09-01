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

    /// <summary>
    /// Verifies that the <see cref="Trie.Add"/> method throws an <see cref="ArgumentException"/>
    /// when attempting to add either a null or empty string to the trie.
    /// </summary>
    [TestMethod]
    public void ThrowArgumenExceptionWhenAddingNullOrEmptyString()
    {
        Trie.Trie trie = new();
        this.NullOrEmptyStringTest(trie.Add);
    }

    /// <summary>
    /// Verifies that the <see cref="Trie.Contains"/> method throws an <see cref="ArgumentException"/>
    /// when checking for the presence of either a null or empty string in the trie.
    /// </summary>
    [TestMethod]
    public void ThrowArgumentExceptionWhenCheckingContainsForNullOrEmptyString()
    {
        Trie.Trie trie = new();
        this.NullOrEmptyStringTest(trie.Contains);
    }

    /// <summary>
    /// Verifies that the <see cref="Trie.Remove"/> method throws an <see cref="ArgumentException"/>
    /// when attempting to remove either a null or empty string from the trie.
    /// </
    [TestMethod]
    public void ThrowArgumentExceptionWhenRemovingNullOrEmptyString()
    {
        Trie.Trie trie = new();
        this.NullOrEmptyStringTest(trie.Remove);
    }

    private bool NullOrEmptyStringTest(Func<string, bool> func)
    {
        Assert.ThrowsException<ArgumentException>(() => func(string.Empty));
        Assert.ThrowsException<ArgumentNullException>(() => func(null!));

        return true;
    }
}
