namespace LempelZivWelch
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.Json.Serialization.Metadata;
    using System.Threading.Tasks;
    using ByteTrie;

    public class LZW
    {
        public static void CompressFile(string inputPath)
        {
            string outputPath = inputPath + ".zipped";
            int bufferSize = 13;

            using (FileStream inputFileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            using (FileStream outputFileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                EncodeBytes(inputFileStream, outputFileStream, bufferSize);
            }
        }

        public static void DecompressFile(string inputPath)
        {
            string outputPath = inputPath[..^7];
            int bufferSize = 13;

            using (FileStream inputFileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            using (FileStream outputFileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                DecodeBytes(inputFileStream, outputFileStream, bufferSize);
            }
        }

        private static void EncodeBytes(FileStream inputFileStream, FileStream outputFileStream, int bufferSize)
        {
            Trie trie = new Trie();
            TrieBytesInit(trie);
            int bufferLength = bufferSize;
            byte[] inputBuffer = new byte[bufferLength];
            List<byte> outputBuffer = [];
            Queue<bool> bitBuffer = [];
            int bytesRead = 0;
            int currentCodeSize = 9;
            List<byte> byteSequence = [];
            bool isFill = FillBuffer(inputFileStream, ref bytesRead, inputBuffer);

            if (!isFill)
            {
                throw new ArgumentException("The file is empty");
            }

            for (int i = 0; i < bytesRead; i++)
            {
                byte currentByte = inputBuffer[i];
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
                    bool[] binaryInterptitation = GetBinaryInterpritationArray(index, currentCodeSize);

                    AddBitsToBuffer(binaryInterptitation, bitBuffer);
                    WriteToBytes(outputBuffer, bitBuffer, false);
                    i--;
                }

                if (i == bytesRead - 1)
                {
                    isFill = FillBuffer(inputFileStream, ref bytesRead, inputBuffer);

                    if (!isFill && trie.Contains(byteSequence.ToArray()))
                    {
                        uint index = trie.GetIndex(byteSequence.ToArray());
                        bool[] binaryInterptitation = GetBinaryInterpritationArray(index, currentCodeSize);
                        AddBitsToBuffer(binaryInterptitation, bitBuffer);
                        WriteToBytes(outputBuffer, bitBuffer, true);
                    }
                    else
                    {
                        i = -1;
                    }

                    outputFileStream.Write(outputBuffer.ToArray(), 0, outputBuffer.Count);
                    outputBuffer.Clear();
                }

                if (trie.Size == Math.Pow(2, currentCodeSize))
                {
                    currentCodeSize++;
                }
            }
        }

        private static void DecodeBytes(FileStream inputFileStream, FileStream outputFileStream, int bufferSize)
        {
            int currentByteIndex = 0;
            Dictionary<uint, byte[]> sequencesDict = [];
            DictBytesInit(sequencesDict);

            Queue<bool> bitBuffer = [];
            byte[]? previousSequence = null;
            int bufferLength = bufferSize;
            byte[] inputBuffer = new byte[bufferLength];
            List<byte> outputBuffer = [];
            int bytesRead = 0;
            int currentCodeSize = 9;
            bool isFill = FillBuffer(inputFileStream, ref bytesRead, inputBuffer);

            if (!isFill)
            {
                throw new ArgumentException("The file is empty");
            }

            while (currentByteIndex < bytesRead || bitBuffer.Count >= currentCodeSize)
            {
                bool isRefil = ReadBytesUntilEnoughBits(inputBuffer, ref currentByteIndex, bitBuffer, currentCodeSize, ref bytesRead, inputFileStream);
                if (isRefil)
                {
                    outputFileStream.Write(outputBuffer.ToArray(), 0, outputBuffer.Count);
                    outputBuffer.Clear();
                }

                uint code = GetDecimalInterpritation(GetBitsArray(bitBuffer, currentCodeSize));

                if (sequencesDict.ContainsKey(code))
                {
                    WriteSequence(sequencesDict[code], outputBuffer);
                    if (previousSequence != null)
                    {
                        byte[] newSequence = new byte[previousSequence.Length + 1];
                        Array.Copy(previousSequence, newSequence, previousSequence.Length);
                        newSequence[^1] = sequencesDict[code][0];
                        sequencesDict[(uint)sequencesDict.Count] = newSequence;
                    }

                    previousSequence = sequencesDict[code];
                }
                else
                {
                    byte[] newSequence = new byte[previousSequence.Length + 1];
                    Array.Copy(previousSequence, newSequence, previousSequence.Length);
                    newSequence[^1] = newSequence[0];
                    sequencesDict[(uint)sequencesDict.Count] = newSequence;
                    WriteSequence(newSequence, outputBuffer);
                    previousSequence = newSequence;
                }

                if (currentByteIndex >= bytesRead)
                {
                    FillBuffer(inputFileStream, ref bytesRead, inputBuffer);
                    outputFileStream.Write(outputBuffer.ToArray(), 0, outputBuffer.Count);
                    outputBuffer.Clear();
                    currentByteIndex = 0;
                }

                if (sequencesDict.Count == Math.Pow(2, currentCodeSize) - 1)
                {
                    currentCodeSize++;
                }
            }
        }

        private static void WriteSequence(byte[] sequence, List<byte> outputBuffer)
        {
            for (int i = 0; i < sequence.Length; i++)
            {
                outputBuffer.Add(sequence[i]);
            }
        }

        private static uint GetDecimalInterpritation(bool[] bits)
        {
            uint temp = 0;
            uint multiplier = 1;

            for (int i = bits.Length - 1; i >= 0; --i)
            {
                temp += bits[i] ? multiplier : 0;
                multiplier *= 2;
            }

            return temp;
        }

        private static bool[] GetBitsArray(Queue<bool> bitBuffer, int count)
        {
            if (bitBuffer.Count < count)
            {
                throw new ArgumentException("There are currently fewer bits in the queue than required.");
            }

            bool[] bitArray = new bool[count];

            for (int i = 0; i < count; ++i)
            {
                bitArray[i] = bitBuffer.Dequeue();
            }

            return bitArray;
        }

        private static bool ReadBytesUntilEnoughBits(byte[] inputBuffer, ref int currentByteIndex, Queue<bool> bitBuffer, int currentCodeSize, ref int bytesRead, FileStream inputFileStream)
        {
            bool isRefill = false;
            while (bitBuffer.Count < currentCodeSize)
            {
                if (currentByteIndex == inputBuffer.Length)
                {
                    if (FillBuffer(inputFileStream, ref bytesRead, inputBuffer))
                    {
                        currentByteIndex = 0;
                        isRefill = true;
                        continue;
                    }

                    throw new Exception("Decode error");
                }

                AddBitsToBuffer(ByteToBits(inputBuffer[currentByteIndex++]), bitBuffer);
            }
            return isRefill;
        }

        private static bool FillBuffer(FileStream inputFileStream, ref int bytesRead, byte[] inputBuffer)
        {
            bytesRead = inputFileStream.Read(inputBuffer, 0, inputBuffer.Length);
            return bytesRead != 0;
        }

        private static bool[] ByteToBits(byte value)
        {
            bool[] bits = new bool[8];

            for (int i = 0; i < 8; i++)
            {
                bits[7 - i] = (value & (1 << i)) != 0;
            }

            return bits;
        }

        private static void AddBitsToBuffer(bool[] bits, Queue<bool> bitBuffer)
        {
            foreach (bool bit in bits)
            {
                bitBuffer.Enqueue(bit);
            }
        }

        private static void WriteToBytes(List<byte> outputBuffer, Queue<bool> bitBuffer, bool needToEmpty)
        {
            while (bitBuffer.Count >= 8)
            {
                bool[] bitArray = new bool[8];
                for (int i = 0; i < 8; i++)
                {
                    bitArray[i] = bitBuffer.Dequeue();
                }

                outputBuffer.Add(ConvertToByte(bitArray));
            }

            if (needToEmpty && bitBuffer.Count > 0)
            {
                bool[] bitArray = new bool[8];
                int index = 0;

                while (bitBuffer.Count > 0)
                {
                    bitArray[index++] = bitBuffer.Dequeue();
                }

                outputBuffer.Add(ConvertToByte(bitArray));
            }
        }

        private static byte ConvertToByte(bool[] bitsArray)
        {
            if (bitsArray.Length != 8)
            {
                throw new ArgumentException("Array length must be exactly 8 bits to convert to a byte.");
            }

            int temp = 0;
            int multiplier = 1;

            for (int i = bitsArray.Length - 1; i >= 0; --i)
            {
                temp += bitsArray[i] ? multiplier : 0;
                multiplier *= 2;
            }

            return (byte)temp;
        }

        private static bool[] GetBinaryInterpritationArray(uint num, int size)
        {
            bool[] binaryInterpritation = new bool[size];
            string binaryString = Convert.ToString(num, 2);

            if (binaryString.Length > size)
            {
                throw new ArgumentException("Can't code num with so small size");
            }

            for (int i = 0; i < binaryString.Length; ++i)
            {
                binaryInterpritation[size - i - 1] = binaryString[binaryString.Length - i - 1] == '1';
            }

            return binaryInterpritation;
        }

        private static void TrieBytesInit(Trie trie)
        {
            for (int i = 0; i < 256; ++i)
            {
                trie.Add((byte)i);
            }
        }

        private static void DictBytesInit(Dictionary<uint, byte[]> dict)
        {
            for (uint i = 0; i < 256; ++i)
            {
                byte[] unitByteArray = { (byte)i };
                dict[i] = unitByteArray;
            }
        }
    }
}
