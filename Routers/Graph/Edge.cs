namespace Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a weighted edge in a graph between two vertices.
    /// </summary>
    public class Edge(int from, int to, int weight)
    {
        /// <summary>
        /// Gets the starting vertex of the edge.
        /// </summary>
        /// <value>The identifier of the source vertex.</value>
        public int From { get; } = from;

        /// <summary>
        /// Gets the ending vertex of the edge.
        /// </summary>
        /// <value>The identifier of the destination vertex.</value>
        public int To { get; } = to;

        /// <summary>
        /// Gets the weight associated with the edge.
        /// </summary>
        /// <value>The numerical value representing edge weight.</value>
        public int Weight { get; } = weight;
    }
}
