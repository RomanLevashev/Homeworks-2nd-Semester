// <copyright file="NumberNode.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace ParseTree.Nodes;

using System;

/// <summary>
/// Represents a numeric value node in an expression tree.
/// </summary>
public class NumberNode(int value) : Node
{
    private int Value { get; } = value;

    /// <summary>
    /// Evaluates the numeric value of this node.
    /// </summary>
    /// <returns>
    /// The integer value stored in this node.
    /// </returns>
    public override int Evaluate()
    {
        return this.Value;
    }

    /// <summary>
    /// Prints the numeric value to standard output.
    /// </summary>
    public override void Print()
    {
        Console.Write(this.Value);
    }
}
