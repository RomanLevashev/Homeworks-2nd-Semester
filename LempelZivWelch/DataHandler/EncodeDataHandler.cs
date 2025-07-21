// <copyright file="EncodeDataHandler.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace DataHandler;

/// <summary>
/// Handles encoding of data by converting input bytes to bit sequences and writing them in chunks to an output stream.
/// </summary>
/// <param name="inputFileStream">The input stream containing data to be encoded.</param>
/// <param name="outputFileStream">The output stream to write encoded data to.</param>
/// <param name="bufferSize">The size of the buffer used for reading and writing operations.</param>
/// <param name="chunkSize">The number of bits used for each encoded chunk.</param>
public class EncodeDataHandler(FileStream inputFileStream, FileStream outputFileStream, int bufferSize, int chunkSize) : InputDataHandler(bufferSize, inputFileStream)
{
    private readonly FileStream outputFileStream = outputFileStream;

    private List<byte> outputBuffer = [];

    private Queue<bool> bitBuffer = [];

    private int bufferSize = bufferSize;

    /// <summary>
    /// Gets or sets the number of bits used for each encoded chunk.
    /// </summary>
    public int ChunkSize { get; set; } = chunkSize;

    /// <summary>
    /// Reads the next byte from the input stream.
    /// </summary>
    /// <returns>The next byte from the input stream.</returns>
    public new byte GetNextByte() => base.GetNextByte();

    /// <summary>
    /// Moves the input stream position back by one byte, allowing the last read byte to be read again.
    /// </summary>
    public new void ReturnToPreviousByte() => base.ReturnToPreviousByte();

    /// <summary>
    /// Encodes a number and writes it to the bit buffer using the specified chunk size.
    /// </summary>
    /// <param name="num">The number to encode and write to the buffer.</param>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the number requires more bits than the current chunk size allows.
    /// </exception>
    public void WriteToBuffer(uint num)
    {
        int numBitsNeeded = 0;
        uint temp = num;

        while (temp > 0)
        {
            temp >>= 1;
            numBitsNeeded++;
        }

        if (numBitsNeeded > this.ChunkSize)
        {
            throw new InvalidOperationException(
            $"Number {num} requires {numBitsNeeded} bits but current chunk size is only {this.ChunkSize} bits. " +
            "The number is too large for the current encoding settings.");
        }

        int leadingZeros = this.ChunkSize - numBitsNeeded;
        for (int i = 0; i < leadingZeros; i++)
        {
            this.bitBuffer.Enqueue(false);
        }

        if (num > 0)
        {
            for (int i = numBitsNeeded - 1; i >= 0; i--)
            {
                bool bit = (num & (1u << i)) != 0;
                this.bitBuffer.Enqueue(bit);
            }
        }
        else
        {
            for (int i = 0; i < this.ChunkSize; i++)
            {
                this.bitBuffer.Enqueue(false);
            }
        }

        this.FillOutputBuffer();
    }

    /// <summary>
    /// Finalizes the encoding process by padding the bit buffer to a full byte and writing any remaining data.
    /// </summary>
    public void FinalizeEncode()
    {
        while (this.bitBuffer.Count % 8 != 0)
        {
            this.bitBuffer.Enqueue(false);
        }

        this.FillOutputBuffer();
        this.outputFileStream.Write(this.outputBuffer.ToArray());
    }

    private void FillOutputBuffer()
    {
        while (this.bitBuffer.Count >= 8)
        {
            if (this.outputBuffer.Count >= this.bufferSize)
            {
                this.outputFileStream.Write(this.outputBuffer.ToArray());
                this.outputBuffer.Clear();
            }

            bool[] bitArray = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                bitArray[i] = this.bitBuffer.Dequeue();
            }

            this.outputBuffer.Add(this.ConvertToByte(bitArray));
        }
    }

    private byte ConvertToByte(bool[] bitArray)
    {
        if (bitArray.Length != 8)
        {
            throw new ArgumentException("Array length must be exactly 8 bits to convert to a byte.");
        }

        int temp = 0;
        int multiplier = 1;

        for (int i = bitArray.Length - 1; i >= 0; --i)
        {
            temp += bitArray[i] ? multiplier : 0;
            multiplier *= 2;
        }

        return (byte)temp;
    }
}
