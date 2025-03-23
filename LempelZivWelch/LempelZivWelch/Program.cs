using System.Collections;
using LempelZivWelch;

if (args.Length < 2)
{
    Console.WriteLine("Usage: LZWCompressor.exe -c|-u <file-path>");
    return;
}

string operation = args[0];
string filePath = args[1];

switch (operation)
{
    case "-c":

        Console.WriteLine($"Compressing file: {filePath}");
        long compressionRatio = LZW.CompressFile(filePath);
        Console.WriteLine($"Compression ratio: {compressionRatio}");
        break;
    case "-u":
        Console.WriteLine($"Decompressing file: {filePath}");
        LZW.DecompressFile(filePath);
        break;
    default:
        Console.WriteLine("Invalid operation. Use -c for compress or -u for decompress.");
        break;
}