namespace Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an undirected graph data structure with weighted edges.
    /// </summary>
    public class UndirectedGraph
    {
        /// <summary>
        /// Gets the collection of all vertices in the graph.
        /// </summary>
        public HashSet<int> Vertices { get; private set; } = [];

        /// <summary>
        /// Gets the collection of all edges in the graph.
        /// </summary>
        public List<Edge> Edges { get; private set; } = [];

        /// <summary>
        /// Determines whether the graph is fully connected (all vertices are reachable from any other vertex).
        /// </summary>
        /// <returns>
        /// <c>true</c> if the graph is connected; otherwise, <c>false</c>.
        /// </returns>
        public bool IsConnected()
        {
            if (this.Vertices.Count == 0)
            {
                return true;
            }

            var visited = new HashSet<int>();
            this.DFS(this.Vertices.First(), visited);

            return visited.Count == this.Vertices.Count;
        }

        /// <summary>
        /// Adds a new vertex to the graph.
        /// </summary>
        /// <param name="vertex">The vertex identifier to add.</param>
        /// <returns>
        /// <c>true</c> if the vertex was added; <c>false</c> if the vertex already exists.
        /// </returns>
        public bool AddVertex(int vertex)
        {
            if (this.Vertices.Contains(vertex))
            {
                return false;
            }

            this.Vertices.Add(vertex);
            return true;
        }

        /// <summary>
        /// Adds a new weighted edge between two vertices in the graph.
        /// </summary>
        /// <param name="from">The source vertex identifier.</param>
        /// <param name="to">The target vertex identifier.</param>
        /// <param name="weight">The weight/cost of the edge.</param>
        /// <returns>
        /// <c>true</c> if the edge was added; <c>false</c> if either vertex doesn't exist.
        /// or the edge already exists.
        /// </returns>
        public bool AddEdge(int from, int to, int weight)
        {
            if (!this.Vertices.Contains(from) || !this.Vertices.Contains(to) || weight < 0 || from == to)
            {
                return false;
            }

            foreach (var edge in this.Edges)
            {
                if ((edge.From == from && edge.To == to) || (edge.To == from && edge.From == to))
                {
                    return false;
                }
            }

            Edge newEdge = new(from, to, weight);
            this.Edges.Add(newEdge);
            return true;
        }

        private void DFS(int currentVertex, HashSet<int> visited)
        {
            visited.Add(currentVertex);

            foreach(var edge in this.Edges)
            {
                if (edge.From == currentVertex && !visited.Contains(edge.To))
                {
                    this.DFS(edge.To, visited);
                }
                else if (edge.To == currentVertex && !visited.Contains(edge.From))
                {
                    this.DFS(edge.From, visited);
                }
            }
        }
    }
}
