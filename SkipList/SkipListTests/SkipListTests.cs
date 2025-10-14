// <copyright file="SkipListTests.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace SkipListTests;

using System.Collections.Generic;
using SkipList;

/// <summary>
/// Contains unit tests for the <see cref="SkipList{T}"/> class,
/// verifying its correctness, robustness, and compliance with the <see cref="IList{T}"/> interface.
/// </summary>
[TestClass]
public sealed class SkipListTests
{
    private SkipList<int> slist = [];
    private int rangeMax = 1000;

    /// <summary>
    /// Initializes a new SkipList instance before each test method execution.
    /// Ensures test isolation and consistent initial state for all tests.
    /// </summary>
    [TestInitialize]
    public void TestInitialize()
    {
        this.slist = new();
    }

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
        this.slist.Add(5);
        this.slist.Add(1);
        this.slist.Add(7);
        this.slist.Add(2);
        this.slist.Add(4);
        this.slist.Add(5);
        this.slist.Remove(5);
        Assert.IsTrue(this.slist.Contains(5));
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
        int[] values = Enumerable.Range(1, this.rangeMax).ToArray();

        foreach (int value in values)
        {
            this.slist.Add(value);
            this.slist.Add(value);
        }

        Assert.IsTrue(this.InList(values, this.slist));
        Assert.IsTrue(this.slist.Count == this.rangeMax * 2);
        int i = 0;

        foreach (int value in values)
        {
            this.slist.Remove(value);
            Assert.AreEqual((this.rangeMax * 2) - (i * 2) - 1, this.slist.Count);
            Assert.IsTrue(this.InList(values[i..this.rangeMax], this.slist));
            this.slist.Remove(value);
            Assert.AreEqual((this.rangeMax * 2) - (i * 2) - 2, this.slist.Count);
            Assert.IsTrue(this.InList(values[(++i)..this.rangeMax], this.slist));
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
        int[] values = Enumerable.Range(1, this.rangeMax).ToArray();

        foreach (int value in values)
        {
            this.slist.Add(value);
        }

        int i = 0;

        foreach (var value in this.slist)
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
        int[] values = Enumerable.Range(1, this.rangeMax).ToArray();

        foreach (int value in values)
        {
            this.slist.Add(value);
        }

        for (int i = this.rangeMax - 1; i >= 0; i--)
        {
            this.slist.RemoveAt(i);
            Assert.AreEqual(i, this.slist.Count);
            Assert.IsFalse(this.slist.Contains(i + 1));
            if (i > 0)
            {
                Assert.IsTrue(this.InList(values[0..(i - 1)], this.slist));
            }
        }
    }

    /// <summary>
    /// Tests index-based access and lookup operations in <see cref="SkipList{T}"/>.
    /// </summary>
    [TestMethod]
    public void IndexTest()
    {
        int[] values = Enumerable.Range(1, this.rangeMax).ToArray();

        foreach (int value in values)
        {
            this.slist.Add(value);
        }

        for (int i = 0; i < this.rangeMax; i++)
        {
            Assert.AreEqual(this.slist[i], i + 1);
            Assert.AreEqual(this.slist.IndexOf(i + 1), i);
        }
    }

    /// <summary>
    /// Tests the <see cref="SkipList{T}.CopyTo"/> method for correct element copying and proper exception handling.
    /// </summary>
    [TestMethod]
    public void CopyToTest()
    {
        int[] values = Enumerable.Range(1, this.rangeMax / 2).ToArray();

        foreach (int value in values)
        {
            this.slist.Add(value);
        }

        int[] destinationArray = new int[this.rangeMax];
        Assert.ThrowsException<IndexOutOfRangeException>(() => this.slist.CopyTo(destinationArray, (this.rangeMax / 2) + 1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => this.slist.CopyTo(destinationArray, -1));
        Assert.ThrowsException<ArgumentNullException>(() => this.slist.CopyTo(null!, 1));

        this.slist.CopyTo(destinationArray, this.rangeMax / 2);

        for (int i = 0; i < (this.rangeMax / 2); i++)
        {
            Assert.AreEqual(values[i], destinationArray[i + (this.rangeMax / 2)]);
        }
    }

    /// <summary>
    /// Verifies that the <see cref="SkipList{T}.Contains(T)"/> method returns <c>false</c>
    /// for elements that are not present in the list.
    /// </summary>
    [TestMethod]
    public void SearchShouldReturnFalseForNonExistentElement()
    {
        this.slist.Add(3);
        this.slist.Add(7);
        this.slist.Add(9);
        this.slist.Add(5);
        this.slist.Remove(5);

        Assert.IsFalse(this.slist.Contains(5));
        Assert.IsFalse(this.slist.Contains(4));
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
        this.slist.Add(1);

        var enumerator = ((IEnumerable<int>)this.slist).GetEnumerator();
        SkipList<int> list = new();

        this.slist.Add(3);
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