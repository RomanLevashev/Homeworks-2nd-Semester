// <copyright file="Program.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

using Graph;
using Routers;

Console.Write("Enter input path: ");
string? inputPath = Console.ReadLine();

ArgumentException.ThrowIfNullOrEmpty(inputPath, nameof(inputPath));

string directory = Path.GetDirectoryName(inputPath)!;
string fileName = Path.GetFileNameWithoutExtension(inputPath);
string extension = Path.GetExtension(inputPath);
string newFileName = $"{fileName}_new{extension}";
string outputPath = Path.Combine(directory, newFileName);

UndirectedGraph? originalTopology = FileDataHandler.ParseFile(inputPath);

if (originalTopology is null)
{
    Environment.Exit(1);
}

UndirectedGraph? newTopology = SpanningTreeFinder.GetMaximumSpanningTree(originalTopology);

if (newTopology is null)
{
    Environment.Exit(2);
}

FileDataHandler.WriteToFile(outputPath, newTopology);