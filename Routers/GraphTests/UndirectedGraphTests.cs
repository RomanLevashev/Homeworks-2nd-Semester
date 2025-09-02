// <copyright file="UndirectedGraphTests.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace GraphTests;

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
    public void FindMaximumSpanningTreeInConnectedGraph()
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
        var maxSpanningTree = SpanningTreeFinder.GetMaximumSpanningTree(graph);
        Assert.IsTrue(maxSpanningTree!.IsConnected());
        Assert.AreEqual(maxSpanningTree.Vertices.Count, maxSpanningTree.Edges.Count + 1);
        Assert.AreEqual(expectedTotalWeight, maxSpanningTree.Edges.Sum(e => e.Weight));
    }

    /// <summary>
    /// Verifies that the <see cref="SpanningTreeFinder.GetMaximumSpanningTree"/> method
    /// returns null when provided with a disconnected graph that cannot form a complete spanning tree.
    /// </summary>
    [TestMethod]
    public void ReturnNullWhenGraphIsDisconnected()
    {
        var graph = new UndirectedGraph();

        graph.AddEdge(1, 2, 3);
        graph.AddEdge(4, 5, 6);

        var maxSpanningTree = SpanningTreeFinder.GetMaximumSpanningTree(graph);

        Assert.AreEqual(null, maxSpanningTree);
    }
 }
