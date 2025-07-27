// <copyright file="Node.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace ParseTree.Nodes;

/// <summary>
/// Represents an abstract node in an parse tree.
/// </summary>
public abstract class Node
{
    /// <summary>
    /// Evaluates the node's numeric value.
    /// </summary>
    /// <returns>
    /// The integer value stored in this node.
    /// </returns>
    public abstract int Evaluate();

    /// <summary>
    /// Prints the node's structure.
    /// </summary>
    public abstract void Print();
}
