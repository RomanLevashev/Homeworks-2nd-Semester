// <copyright file="Node.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace Trie;

using System.Collections.Generic;

/// <summary>
/// Represents a node in a Trie (prefix tree) data structure.
/// </summary>
public class Node(char value)
{
    /// <summary>
    /// Gets the character value stored in this node.
    /// </summary>
    public char Value { get; } = value;

    /// <summary>
    /// Gets the collection of child nodes.
    /// </summary>
    public Dictionary<char, Node> Children { get; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether this node represents the end of a complete word.
    /// </summary>
    public bool IsTerminal { get; set; }
}
