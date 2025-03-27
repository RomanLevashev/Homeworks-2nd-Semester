namespace Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
        /// A new UndirectedGraph representing the maximum spanning tree.
        /// </returns>
        public static UndirectedGraph GetMaximumSpanningTree(UndirectedGraph graph)
        {
            if (!graph.IsConnected())
            {
                throw new ArgumentException("Graph is disconnected");
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
