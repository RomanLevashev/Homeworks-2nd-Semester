namespace GraphTests
{
    using Graph;

    /// <summary>
    /// Contains unit tests for the UndirectedGraph class.
    /// </summary>
    [TestClass]
    public sealed class UndirectedGraphTests
    {
        /// <summary>
        /// Tests the combined functionality of adding vertices and edges.
        /// </summary>
        [TestMethod]
        public void AddEdgeAndVertexTest()
        {
            UndirectedGraph graph = new();
            Assert.IsFalse(graph.AddEdge(1, 3, 5));
            Assert.IsTrue(graph.AddVertex(3));
            Assert.IsTrue(graph.AddVertex(9));
            Assert.IsTrue(graph.AddEdge(3, 9, 10));
        }

        /// <summary>
        /// Tests the IsConnected method.
        /// </summary>
        [TestMethod]
        public void IsConnectedTest()
        {
            UndirectedGraph graph = new();
            for (int i = 0; i < 4; i++)
            {
                graph.AddVertex(i);
            }

            graph.AddEdge(0, 1, 10);
            graph.AddEdge(0, 2, 6);
            graph.AddEdge(0, 3, 5);
            graph.AddEdge(1, 2, 15);
            graph.AddEdge(2, 3, 4);
            Assert.IsTrue(graph.IsConnected());
        }

        /// <summary>
        /// Tests the maximum spanning tree generation.
        /// </summary>
        [TestMethod]
        public void MaxSpanningTreeTest()
        {
            var graph = new UndirectedGraph();

            for (int i = 1; i <= 5; i++)
            {
                graph.AddVertex(i);
            }

            graph.AddEdge(1, 2, 9);
            graph.AddEdge(1, 3, 7);

            graph.AddEdge(2, 1, 9);
            graph.AddEdge(2, 4, 8);

            graph.AddEdge(3, 1, 7);
            graph.AddEdge(3, 4, 6);
            graph.AddEdge(3, 5, 5);

            graph.AddEdge(4, 2, 8);
            graph.AddEdge(4, 3, 6);
            graph.AddEdge(4, 5, 10);

            graph.AddEdge(5, 3, 5);
            graph.AddEdge(5, 4, 10);
            var expectedTotalWeight = 34;
            UndirectedGraph maxSt = SpanningTreeFinder.GetMaximumSpanningTree(graph);
            Assert.IsTrue(maxSt.IsConnected());
            Assert.IsTrue(maxSt.Vertices.Count == maxSt.Edges.Count + 1);
            Assert.IsTrue(maxSt.Edges.Sum(e => e.Weight) == expectedTotalWeight);
        }
     }
}
