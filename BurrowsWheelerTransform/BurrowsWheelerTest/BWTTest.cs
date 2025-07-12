// <copyright file="BurrowsWheelerTest.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace BurrowsWheelerTest;

using BurrowsWheelerTransform;

/// <summary>
/// Contains unit tests for the <see cref="BurrowsWheeler"/> class implementation.
/// Verifies both encoding (Transform) and decoding (InverseTransform) functionality.
/// </summary>
[TestClass]
public sealed class BWTTest
{
    /// <summary>
    /// Tests transformation and inversion of a typical string with varied characters.
    /// </summary>
    [TestMethod]
    public void DefaultString()
    {
        TestTransformAndInvert("banana", "nnbaaa");
    }

    /// <summary>
    /// Tests transformation of a string containing identical characters.
    /// </summary>
    [TestMethod]
    public void ConstantString()
    {
        TestTransformAndInvert("aaaa", "aaaa");
    }

    /// <summary>
    /// Tests transformation of a single-character string.
    /// </summary>
    [TestMethod]
    public void OneCharacterString()
    {
        TestTransformAndInvert("a", "a");
    }

    /// <summary>
    /// Verifies that empty string input throws <see cref="ArgumentNullException"/>.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void EmptyStr()
    {
        TestTransformAndInvert(string.Empty, string.Empty);
    }

    private static void TestTransformAndInvert(string testStr, string expectedTransformResult)
    {
        var transformedResult = BurrowsWheeller.Transform(testStr);
        Assert.AreEqual(expectedTransformResult, transformedResult.Transformed);
        var invertResult = BurrowsWheeller.InverseTransform(transformedResult.Transformed, transformedResult.Position);
        Assert.AreEqual(testStr, invertResult);
    }
}