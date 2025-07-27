// <copyright file="Program.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

using ParseTree;

Console.WriteLine("Enter the full path to file:");
string? filePath = Console.ReadLine();

if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
{
    Console.WriteLine("Error: File not found. Please check the path and try again.");
    return;
}

using var streamReader = new StreamReader(filePath);
var expression = streamReader.ReadToEnd();
DataHandler.EvaluateAndPrintExpression(expression);