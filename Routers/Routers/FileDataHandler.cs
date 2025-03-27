namespace Routers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
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
        /// <returns>Parsed UndirectedGraph object.</returns>
        public static UndirectedGraph ParseFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            UndirectedGraph graph = new UndirectedGraph();
            var line = sr.ReadLine();

            while (line != null)
            {
                string[] sourcesAndDestinations = line.Split(":", StringSplitOptions.TrimEntries);

                if (sourcesAndDestinations.Length != 2)
                {
                    throw new FormatException("Invalid file format: expected 'source: destination (capacity), ...'");
                }

                var sourceString = sourcesAndDestinations[0];

                if (!int.TryParse(sourceString, out int sourceInt))
                {
                    throw new FormatException($"Invalid file format: expected integer, but got '{sourceString}'");
                }

                var destinations = sourcesAndDestinations[1].Split(",", StringSplitOptions.TrimEntries);

                if (destinations.Length == 0)
                {
                    throw new FormatException("Invalid file format: source without destinations");
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
                        throw new FormatException("Invalid file format: expected 'source: destination (capacity), ...'");
                    }
                }

                line = sr.ReadLine();
            }

            sr.Close();
            return graph;
        }

        /// <summary>
        /// Writes an undirected graph to the specified file.
        /// </summary>
        /// <param name="path">Path to the output file.</param>
        /// <param name="graph">Graph to be written.</param>
        public static void WriteToFile(string path, UndirectedGraph graph)
        {
            StreamWriter sw = new(path);
            Dictionary<int, List<(int destination, int capacity)>> sourceAndDestinations = [];

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
                    sw.Write($"{current.destination} ({current.capacity})");
                    if (i != sourceAndDestinations[key].Count - 1)
                    {
                        sw.Write(", ");
                    }
                }

                sw.Write('\n');
            }

            sw.Close();
        }
    }
}