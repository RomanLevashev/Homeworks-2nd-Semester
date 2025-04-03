using ParseTree;

Console.WriteLine("Enter the full path to file:");
string? filePath = Console.ReadLine();

if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
{
    Console.WriteLine("Error: File not found. Please check the path and try again.");
    return;
}

var sr = new StreamReader(filePath);
var expression = sr.ReadToEnd();
DataHandler.EvaluateAndPrintExpression(expression);