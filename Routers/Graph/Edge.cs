// <copyright file="Edge.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace Graph;

/// <summary>
/// Represents a weighted edge in a graph between two vertices.
/// </summary>
public record class Edge(int From, int To, int Weight)
{
}
