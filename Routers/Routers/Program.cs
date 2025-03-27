using Graph;
using Routers;

Console.Write("Enter input path: ");
string? inputPath = Console.ReadLine();

if (inputPath == null || inputPath == string.Empty)
{
    throw new ArgumentException(inputPath);
}

string directory = Path.GetDirectoryName(inputPath)!;
string fileName = Path.GetFileNameWithoutExtension(inputPath);
string extension = Path.GetExtension(inputPath);
string newFileName = $"{fileName}_new{extension}";
string outputPath = Path.Combine(directory, newFileName);

UndirectedGraph oritinalTopology = FileDataHandler.ParseFile(inputPath);
UndirectedGraph newTopology = SpanningTreeFinder.GetMaximumSpanningTree(oritinalTopology);
FileDataHandler.WriteToFile(outputPath, newTopology);

Console.WriteLine();