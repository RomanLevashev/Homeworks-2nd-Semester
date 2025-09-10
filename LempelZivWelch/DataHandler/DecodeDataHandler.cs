// <copyright file="DecodeDataHandler.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace DataHandler;

/// <summary>
/// Handles decoding of compressed data by reading bits from an input stream and writing decoded sequences to an output stream.
/// </summary>
/// <param name="inputFileStream">The input stream containing compressed data.</param>
/// <param name="outputFileStream">The output stream to write decoded data to.</param>
/// <param name="bufferSize">The size of the buffer used for reading and writing operations.</param>
/// <param name="chunkSize">The number of bits to read for each code during decoding.</param>
public class DecodeDataHandler(FileStream inputFileStream, FileStream outputFileStream, int bufferSize, int chunkSize) : InputDataHandler(bufferSize, inputFileStream)
{
    private readonly FileStream outputFileStream = outputFileStream;

    private int bufferSize = bufferSize;

    private List<byte> outputBuffer = [];

    private Queue<bool> bitBuffer = [];

    /// <summary>
    /// Gets a value indicating whether both the file read operation is complete and the bit buffer contains less data than the chunk size.
    /// </summary>
    public override bool IsFileReadComplete => base.IsFileReadComplete && this.bitBuffer.Count < this.ChunkSize;

    /// <summary>
    /// Gets or sets the number of bits to read for each code during decoding.
    /// </summary>
    public int ChunkSize { get; set; } = chunkSize;

    /// <summary>
    /// Reads the next code from the bit buffer with the specified chunk size.
    /// </summary>
    /// <returns>The decoded code as an unsigned integer.</returns>
    /// <exception cref="EndOfStreamException">Thrown when attempting to read beyond the end of the stream.</exception>
    public uint GetNextCode()
    {
        this.FillBitBuffer();
        uint code = 0;

        for (int i = 0; i < this.ChunkSize; i++)
        {
            bool bit = this.bitBuffer.Dequeue();
            code = (code << 1) | (uint)(bit ? 1 : 0);
        }

        return code;
    }

    /// <summary>
    /// Writes a decoded sequence to the output buffer and flushes to the output stream if the buffer is full.
    /// </summary>
    /// <param name="sequence">The byte sequence to write to the output.</param>
    public void WriteSequence(byte[] sequence)
    {
        for (int i = 0; i < sequence.Length; i++)
        {
            this.outputBuffer.Add(sequence[i]);

            if (this.outputBuffer.Count > this.bufferSize)
            {
                this.outputFileStream.Write(this.outputBuffer.ToArray());
                this.outputBuffer.Clear();
            }
        }
    }

    /// <summary>
    /// Flushes any remaining data in the output buffer to the output stream.
    /// </summary>
    public void FinalizeDecode()
    {
        this.outputFileStream.Write(this.outputBuffer.ToArray());
        this.outputBuffer.Clear();
    }

    private void FillBitBuffer()
    {
        while (this.bitBuffer.Count < this.ChunkSize)
        {
            if (this.IsFileReadComplete)
            {
                throw new EndOfStreamException("Invalid file format: The input file appears to be corrupted or not properly compressed");
            }

            byte nextByte = this.GetNextByte();

            for (int i = 7; i >= 0; i--)
            {
                bool bit = (nextByte & (1 << i)) != 0;
                this.bitBuffer.Enqueue(bit);
            }
        }
    }
}
