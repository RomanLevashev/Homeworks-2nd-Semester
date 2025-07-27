// <copyright file="FileDataHandler.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace Routers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Graph;

/// <summary>
/// Provides static methods for reading and writing graph data to/from files.
/// </summary>
public static class FileDataHandler
{
    /// <summary>
    /// Parses an undirected graph from the specified file.
    /// </summary>
    /// <param name="path">Path to the input file.</param>
    /// <returns>Parsed UndirectedGraph object;
    /// <c>null</c> if the file contains invalid data format or is corrupted.
    /// </returns>
    public static UndirectedGraph? ParseFile(string path)
    {
        using StreamReader sr = new(path);
        UndirectedGraph graph = new();
        var line = sr.ReadLine();

        while (line != null)
        {
            string[] sourcesAndDestinations = line.Split(":", StringSplitOptions.TrimEntries);

            if (sourcesAndDestinations.Length != 2)
            {
                Console.Error.WriteLine("Invalid file format: expected 'source: destination (capacity), ...'");
                return null;
            }

            var sourceString = sourcesAndDestinations[0];

            if (!int.TryParse(sourceString, out int sourceInt))
            {
                Console.Error.WriteLine($"Invalid file format: expected integer, but got '{sourceString}'");
                return null;
            }

            var destinations = sourcesAndDestinations[1].Split(",", StringSplitOptions.TrimEntries);

            if (destinations.Length == 0)
            {
                Console.Error.WriteLine("Invalid file format: source without destinations");
                return null;
            }

            foreach(var destination in destinations)
            {
                var match = Regex.Match(destination, @"(\d+)\s+\((\d+)\)");

                if (match.Success)
                {
                    int destinationNumber = int.Parse(match.Groups[1].Value);
                    int capacity = int.Parse(match.Groups[2].Value);
                    graph.AddVertex(sourceInt);
                    graph.AddVertex(destinationNumber);
                    graph.AddEdge(sourceInt, destinationNumber, capacity);
                }
                else
                {
                    Console.Error.WriteLine("Invalid file format: expected 'source: destination (capacity), ...'");
                    return null;
                }
            }

            line = sr.ReadLine();
        }

        return graph;
    }

    /// <summary>
    /// Writes an undirected graph to the specified file.
    /// </summary>
    /// <param name="path">Path to the output file.</param>
    /// <param name="graph">Graph to be written.</param>
    public static void WriteToFile(string path, UndirectedGraph graph)
    {
        using StreamWriter sw = new(path);
        Dictionary<int, List<(int Destination, int Capacity)>> sourceAndDestinations = [];

        foreach (var edge in graph.Edges)
        {
            if (!sourceAndDestinations.ContainsKey(edge.From))
            {
                sourceAndDestinations[edge.From] = [];
            }

            sourceAndDestinations[edge.From].Add((edge.To, edge.Weight));
        }

        foreach (var key in sourceAndDestinations.Keys.OrderBy(key => key).ToList())
        {
            if (!sourceAndDestinations.ContainsKey(key))
            {
                continue;
            }

            sw.Write($"{key}: ");

            for (int i = 0; i < sourceAndDestinations[key].Count; ++i)
            {
                var current = sourceAndDestinations[key][i];
                sw.Write($"{current.Destination} ({current.Capacity})");
                if (i != sourceAndDestinations[key].Count - 1)
                {
                    sw.Write(", ");
                }
            }

            sw.Write('\n');
        }
    }
}