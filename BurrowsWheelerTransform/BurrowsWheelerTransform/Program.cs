// <copyright file="Program.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

using System.Text;
using BurrowsWheelerTransform;

Options choice = Options.Transform;
Console.OutputEncoding = Encoding.UTF8;

while (choice != Options.Exit)
{
    Console.WriteLine("Select an action:");
    Console.WriteLine("0 - Exit");
    Console.WriteLine("1 - Transform string using Burrows-Wheeler");
    Console.WriteLine("2 - Reconstruct string using Burrows-Wheeler");
    string? input = Console.ReadLine();

    if (input == null)
    {
        Console.WriteLine("Invalid input!");
        continue;
    }

    try
    {
        choice = (Options)int.Parse(input);
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input!");
        continue;
    }

    if (choice == Options.Exit)
    {
        return;
    }

    if (choice == Options.Transform)
    {
        Console.WriteLine("Enter the string to transform:");
        string? str = Console.ReadLine();
        if (string.IsNullOrEmpty(str))
        {
            Console.WriteLine("Invalid input!");
        }

        var (transformed, position) = BurrowsWheeler.Transform(str!);
        Console.WriteLine($"Transformed string: {transformed}\nOriginal string position: {position}");
    }

    if (choice == Options.Invert)
    {
        Console.WriteLine("Enter the string to reconstruct");
        string? str = Console.ReadLine();
        if (string.IsNullOrEmpty(str))
        {
            Console.WriteLine("Invalid input!");
            continue;
        }

        Console.WriteLine("Enter the original string position");
        string? positionStr = Console.ReadLine();
        if (string.IsNullOrEmpty(positionStr))
        {
            Console.WriteLine("Invalid input!");
            continue;
        }

        try
        {
            int position = int.Parse(positionStr);
            string result = BurrowsWheeler.InverseTransform(str, position);
            Console.WriteLine(result);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input!");
        }
    }
}