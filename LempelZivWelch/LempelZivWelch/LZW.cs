// <copyright file="LZW.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace LempelZivWelch;

using ByteTrie;

/// <summary>
/// Implements the LZW compression algorithm for encoding and decoding data.
/// </summary>
public class LZW
{
    /// <summary>
    /// Compresses the specified file using the LZW algorithm.
    /// The compressed data is written to a new file with the same name and a ".zipped" extension.
    /// </summary>
    /// <param name="inputPath">The path of the file to be compressed.</param>
    /// <returns>
    /// The compression ratio, which is the ratio of the original file size to the compressed file size.
    /// </returns>
    public static long CompressFile(string inputPath)
    {
        FileInfo fileInfo = new FileInfo(inputPath);
        long inputSize = fileInfo.Length;
        string outputPath = inputPath + ".zipped";
        int bufferSize = 4 * (int)Math.Pow(2, 20);

        using (FileStream inputFileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputFileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            EncodeBytes(inputFileStream, outputFileStream, bufferSize);
        }

        FileInfo encodeFileInfo = new FileInfo(outputPath);
        long outputSize = encodeFileInfo.Length;

        return inputSize / outputSize;
    }

    /// <summary>
    /// Decompresses the specified LZW-compressed file.
    /// The decompressed data is written to a new file with the original extension before compression.
    /// </summary>
    /// <param name="inputPath">The path of the file to be decompressed.</param>
    public static void DecompressFile(string inputPath)
    {
        string outputPath = inputPath[..^7];
        int bufferSize = 4 * (int)Math.Pow(2, 20);

        using (FileStream inputFileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (FileStream outputFileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            DecodeBytes(inputFileStream, outputFileStream, bufferSize);
        }
    }

    private static void EncodeBytes(FileStream inputFileStream, FileStream outputFileStream, int bufferSize)
    {
        Trie trie = new Trie();
        DataHandler.EncodeDataHandler handler = new(inputFileStream, outputFileStream, bufferSize, 9);
        TrieBytesInit(trie);
        List<byte> byteSequence = [];

        while (!handler.IsFileReadComplete)
        {
            byte currentByte = handler.GetNextByte();
            byteSequence.Add(currentByte);
            if (!trie.Contains(byteSequence.ToArray()))
            {
                (Node? terminal, bool isSuccess) = trie.Add(byteSequence.ToArray());
                byteSequence.Clear();

                if (!isSuccess)
                {
                    throw new Exception("Encode error");
                }

                uint index = terminal!.Index;
                handler.WriteToBuffer(index);
                handler.ReturnToPreviousByte();
            }

            if (trie.Size == Math.Pow(2, handler.ChunkSize))
            {
                handler.ChunkSize++;
            }
        }

        if (byteSequence.Count > 0)
        {
            uint index = trie.GetIndex(byteSequence.ToArray());
            handler.WriteToBuffer(index);
        }

        handler.FinalizeEncode();
    }

    private static void DecodeBytes(FileStream inputFileStream, FileStream outputFileStream, int bufferSize)
    {
        DataHandler.DecodeDataHandler handler = new(inputFileStream, outputFileStream, bufferSize, 9);
        Dictionary<uint, byte[]> sequencesDictionary = [];
        DictionaryBytesInit(sequencesDictionary);

        byte[]? previousSequence = null;

        while (!handler.IsFileReadComplete)
        {
            uint code = handler.GetNextCode();

            if (sequencesDictionary.ContainsKey(code))
            {
                handler.WriteSequence(sequencesDictionary[code]);
                if (previousSequence != null)
                {
                    byte[] newSequence = new byte[previousSequence.Length + 1];
                    Array.Copy(previousSequence, newSequence, previousSequence.Length);
                    newSequence[^1] = sequencesDictionary[code][0];
                    sequencesDictionary[(uint)sequencesDictionary.Count] = newSequence;
                }

                previousSequence = sequencesDictionary[code];
            }
            else
            {
                if (previousSequence == null)
                {
                    throw new InvalidDataException("File is corrupted: expected byte sequence is missing");
                }

                byte[] newSequence = new byte[previousSequence.Length + 1];
                Array.Copy(previousSequence, newSequence, previousSequence.Length);
                newSequence[^1] = newSequence[0];
                sequencesDictionary[(uint)sequencesDictionary.Count] = newSequence;
                handler.WriteSequence(newSequence);
                previousSequence = newSequence;
            }

            if (sequencesDictionary.Count == Math.Pow(2, handler.ChunkSize) - 1)
            {
                handler.ChunkSize++;
            }
        }

        handler.FinalizeDecode();
    }

    private static void TrieBytesInit(Trie trie)
    {
        for (int i = 0; i < 256; ++i)
        {
            trie.Add((byte)i);
        }
    }

    private static void DictionaryBytesInit(Dictionary<uint, byte[]> dict)
    {
        for (uint i = 0; i < 256; ++i)
        {
            byte[] unitByteArray = { (byte)i };
            dict[i] = unitByteArray;
        }
    }
}
