// <copyright file="SpanningTreeFinder.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace Graph
{
    using System;
    using System.Linq;

    /// <summary>
    /// Provides algorithms for finding spanning trees in undirected graphs.
    /// </summary>
    public static class SpanningTreeFinder
    {
        /// <summary>
        /// Computes the maximum spanning tree (MST) of the given undirected graph using Kruskal's algorithm.
        /// </summary>
        /// <param name="graph">The input undirected graph.</param>
        /// <returns>
        /// A new UndirectedGraph representing the maximum spanning tree or <c>null</c> if the input graph is disconnected, empty or null.
        /// </returns>
        public static UndirectedGraph? GetMaximumSpanningTree(UndirectedGraph graph)
        {
            if (graph is null)
            {
                Console.Error.WriteLine("Graph cannot be null");
                return null;
            }

            if (graph.Edges.Count == 0)
            {
                Console.Error.WriteLine("Graph must be connected to compute maximum spanning tree.");
                return null;
            }

            if (!graph.IsConnected())
            {
                Console.Error.WriteLine("Graph is disconnected.");
                return null;
            }

            var maximumSpanningTree = new UndirectedGraph();

            foreach (var vertex in graph.Vertices)
            {
                maximumSpanningTree.AddVertex(vertex);
            }

            var sortedEdges = graph.Edges.OrderByDescending(e => e.Weight).ToList();
            var dsu = new DSU();

            foreach (var edge in sortedEdges)
            {
                int rootFrom = dsu.Find(edge.From);
                int rootTo = dsu.Find(edge.To);

                if (rootFrom != rootTo)
                {
                    maximumSpanningTree.AddEdge(edge.From, edge.To, edge.Weight);
                    dsu.Union(rootFrom, rootTo);
                }
            }

            return maximumSpanningTree;
        }
    }
}
