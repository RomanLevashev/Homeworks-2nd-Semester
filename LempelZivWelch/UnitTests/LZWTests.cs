// <copyright file="LZWTests.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace UnitTests;

using LempelZivWelch;

/// <summary>
/// A test class for the LZW compression and decompression algorithm.
/// </summary>
[TestClass]
public sealed class LZWTests
{
    private string directoryWithTestFiles = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName;

    /// <summary>
    /// Verifies the validity of the compression and decompression process for HTML.
    /// This test checks that a file, once compressed and then decompressed,
    /// matches the original input file to ensure that the compression algorithm
    /// works correctly and does not result in data loss or corruption.
    /// </summary>
    [TestMethod]
    public void TestCompressAndDecompressValidatyHTML() => this.TestFileEquality(this.directoryWithTestFiles + "\\lzwWiki.html");

    /// <summary>
    /// Verifies the validity of the compression and decompression process for PDF.
    /// This test checks that a file, once compressed and then decompressed,
    /// matches the original input file to ensure that the compression algorithm
    /// works correctly and does not result in data loss or corruption.
    /// </summary>
    [TestMethod]
    public void TestCompressAndDecompressValidatyPDF() => this.TestFileEquality(this.directoryWithTestFiles + "\\Memory_Barriers_a_Hardware_View_for_Software_Hacke.pdf");

    private void TestFileEquality(string filePath)
    {
        byte[] originalFileBytes = File.ReadAllBytes(filePath);
        LZW.CompressFile(filePath);
        LZW.DecompressFile(filePath + ".zipped");
        byte[] decompressedFileBytes = File.ReadAllBytes(filePath);

        Assert.AreEqual(originalFileBytes.Length, decompressedFileBytes.Length);

        for (int i = 0; i < originalFileBytes.Length; i++)
        {
            Assert.AreEqual(originalFileBytes[i], decompressedFileBytes[i]);
        }
    }
}
