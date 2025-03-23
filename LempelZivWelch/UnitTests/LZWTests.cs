using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    using LempelZivWelch;

    /// <summary>
    /// A test class for the LZW compression and decompression algorithm.
    /// </summary>
    [TestClass]
    public sealed class LZWTests
    {
        /// <summary>
        /// Verifies the validity of the compression and decompression process.
        /// This test checks that a file, once compressed and then decompressed,
        /// matches the original input file to ensure that the compression algorithm
        /// works correctly and does not result in data loss or corruption.
        /// </summary>
        [TestMethod]
        public void TestCompressAndDecompressValidaty()
        {
            string originalFilePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\lzwWiki.html";
            byte[] originalFileBytes = File.ReadAllBytes(originalFilePath);
            LZW.CompressFile(originalFilePath);
            LZW.DecompressFile(originalFilePath + ".zipped");
            byte[] decompressedFileBytes = File.ReadAllBytes(originalFilePath);

            Assert.IsTrue(originalFileBytes.Length == decompressedFileBytes.Length);

            for (int i = 0; i < originalFileBytes.Length; i++)
            {
                Assert.IsTrue(originalFileBytes[i] == decompressedFileBytes[i]);
            }
        }
    }
}
