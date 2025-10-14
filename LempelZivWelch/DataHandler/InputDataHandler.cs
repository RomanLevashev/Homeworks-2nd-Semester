// <copyright file="InputDataHandler.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace DataHandler;

/// <summary>
/// Abstract base class for handling input data operations with buffered reading capabilities.
/// </summary>
/// <param name="bufferSize">The size of the buffer used for reading operations.</param>
/// <param name="inputFileStream">The input stream to read data from.</param>
public abstract class InputDataHandler(int bufferSize, FileStream inputFileStream)
{
    private readonly FileStream inputFileStream = inputFileStream;

    private byte[] inputBuffer = new byte[bufferSize];

    private int bytesRead = 0;

    private int currentByteIndex = 0;

    /// <summary>
    /// Gets a value indicating whether the entire input file has been read.
    /// </summary>
    /// <value>
    /// <c>true</c> if the file has been completely read; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsFileReadComplete { get; private set; } = false;

    /// <summary>
    /// Reads the next byte from the input buffer, refilling the buffer if necessary.
    /// </summary>
    /// <returns>The next byte from the input stream.</returns>
    /// <exception cref="EndOfStreamException">
    /// Thrown when attempting to read beyond the end of the stream or when the source is empty.
    /// </exception>
    protected byte GetNextByte()
    {
        if (this.IsFileReadComplete)
        {
            throw new EndOfStreamException("No data was read: end of stream.");
        }

        if (this.currentByteIndex == this.bytesRead)
        {
            bool isFill = this.FillInputBuffer();

            if (!isFill)
            {
                throw new EndOfStreamException("No data was read: end of stream or empty source.");
            }
        }

        if (this.currentByteIndex == this.bytesRead - 1 && this.inputFileStream.Position >= this.inputFileStream.Length)
        {
            this.IsFileReadComplete = true;
        }

        return this.inputBuffer[this.currentByteIndex++];
    }

    /// <summary>
    /// Moves the buffer position back by one byte, allowing the last read byte to be read again.
    /// </summary>
    /// <exception cref="IndexOutOfRangeException">
    /// Thrown when attempting to move before the start of the buffer.
    /// </exception>
    protected void ReturnToPreviousByte()
    {
        if (this.currentByteIndex == 0)
        {
            throw new IndexOutOfRangeException();
        }

        if (this.IsFileReadComplete)
        {
            this.IsFileReadComplete = false;
        }

        this.currentByteIndex--;
    }

    private bool FillInputBuffer()
    {
        this.bytesRead = this.inputFileStream.Read(this.inputBuffer, 0, this.inputBuffer.Length);
        this.currentByteIndex = 0;
        return this.bytesRead != 0;
    }
}
