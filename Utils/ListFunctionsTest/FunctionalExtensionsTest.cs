// <copyright file="FunctionalExtensionsTest.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace ListFunctionsTest;

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
    public void MapWithIntTest()
    {
        Func<int, int> plus = (x) => x + 2;
        List<int> testList = [1, 2, 3];
        List<int> expectedList = [3, 4, 5];

        Assert.IsTrue(expectedList.SequenceEqual(testList.Map(plus)));
    }

    /// <summary>
    /// Tests the Map extension method with string transformation.
    /// </summary>
    [TestMethod]
    public void MapWithStringTest()
    {
        Func<string, string> toUpper = (s) => s.ToUpper();
        List<string> testList = ["hello", "world", "test"];
        List<string> expectedList = ["HELLO", "WORLD", "TEST"];

        Assert.IsTrue(expectedList.SequenceEqual(testList.Map(toUpper)));
    }

    /// <summary>
    /// Tests the Map extension method with empty list.
    /// </summary>
    [TestMethod]
    public void MapWithEmptyListTest()
    {
        Func<int, int> plus = (x) => x + 2;
        List<int> testList = [];
        List<int> expectedList = [];

        Assert.IsTrue(expectedList.SequenceEqual(testList.Map(plus)));
    }

    /// <summary>
    /// Tests the Filter extension method by selecting elements that match a predicate.
    /// </summary>
    [TestMethod]
    public void FilterWithIntTest()
    {
        Func<int, bool> isEven = (x) => x % 2 == 0;
        List<int> testList = [1, 2, 3];
        List<int> expectedList = [2];

        Assert.IsTrue(expectedList.SequenceEqual(testList.Filter(isEven)));
    }

    /// <summary>
    /// Tests the Filter extension method with string predicate.
    /// </summary>
    [TestMethod]
    public void FilterWithStringTest()
    {
        Func<string, bool> startsWithH = (s) => s.StartsWith("h", StringComparison.OrdinalIgnoreCase);
        List<string> testList = ["hello", "world", "Hi", "test"];
        List<string> expectedList = ["hello", "Hi"];

        Assert.IsTrue(expectedList.SequenceEqual(testList.Filter(startsWithH)));
    }

    /// <summary>
    /// Tests the Filter extension method with empty list.
    /// </summary>
    [TestMethod]
    public void FilterWithEmptyListTest()
    {
        Func<int, bool> isEven = (x) => x % 2 == 0;
        List<int> testList = [];
        List<int> expectedList = [];

        Assert.IsTrue(expectedList.SequenceEqual(testList.Filter(isEven)));
    }

    /// <summary>
    /// Tests the Fold extension method by accumulating values with a reduction function.
    /// </summary>
    [TestMethod]
    public void FoldWithIntTest()
    {
        Func<int, int, int> multiply = (acc, elem) => acc * elem;
        List<int> testList = [1, 2, 3];
        int seed = 1;

        Assert.AreEqual(6, testList.Fold(seed, multiply));
    }

    /// <summary>
    /// Tests the Fold extension method with string concatenation.
    /// </summary>
    [TestMethod]
    public void FoldWithStringTest()
    {
        Func<string, string, string> concat = (acc, elem) => acc + elem;
        List<string> testList = ["a", "b", "c"];
        string seed = string.Empty;

        Assert.AreEqual("abc", testList.Fold(seed, concat));
    }

    /// <summary>
    /// Tests the Fold extension method with empty list.
    /// </summary>
    [TestMethod]
    public void FoldWithEmptyListTest()
    {
        Func<int, int, int> multiply = (acc, elem) => acc * elem;
        List<int> testList = [];
        int seed = 1;

        Assert.AreEqual(seed, testList.Fold(seed, multiply));
    }
}
