// <copyright file="BWTTest.cs" company="Roman Levashev">
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
    public void TransformAndInvertStringWithVariedCharacters() => TransformAndInvertStringShouldReturnOriginalString("banana", "nnbaaa");

    /// <summary>
    /// Tests transformation of a string containing identical characters.
    /// </summary>
    [TestMethod]
    public void TransformStringWithIdenticalCharacters() => TransformAndInvertStringShouldReturnOriginalString("aaaa", "aaaa");

    /// <summary>
    /// Tests transformation of a single-character string.
    /// </summary>
    [TestMethod]
    public void TransformSingleCharacterString() => TransformAndInvertStringShouldReturnOriginalString("a", "a");

    /// <summary>
    /// Verifies that empty string input throws <see cref="ArgumentNullException"/>.
    /// </summary>
    [TestMethod]
    public void ThrowExceptionWhenTransformingEmptyString() =>
        Assert.ThrowsException<ArgumentException>(() => TransformAndInvertStringShouldReturnOriginalString(string.Empty, string.Empty));

    /// <summary>
    /// Verifies that the <see cref="BurrowsWheelerTransform.InverseTransform"/> method
    /// throws an appropriate exception when provided with a position value that exceeds
    /// the bounds of the transformed string or represents an invalid state for reconstruction.
    /// </summary>
    [TestMethod]
    public void ThrowExceptionWhenGivenImpossibleOriginalStringPosition()
    {
        Assert.ThrowsException<IndexOutOfRangeException>(() => BurrowsWheeler.InverseTransform("asd", 4));
        Assert.ThrowsException<IndexOutOfRangeException>(() => BurrowsWheeler.InverseTransform("asd", -1));
    }

    private static void TransformAndInvertStringShouldReturnOriginalString(string testStr, string expectedTransformResult)
    {
        var transformedResult = BurrowsWheeler.Transform(testStr);
        Assert.AreEqual(expectedTransformResult, transformedResult.Transformed);
        var invertResult = BurrowsWheeler.InverseTransform(transformedResult.Transformed, transformedResult.Position);
        Assert.AreEqual(testStr, invertResult);
    }
}