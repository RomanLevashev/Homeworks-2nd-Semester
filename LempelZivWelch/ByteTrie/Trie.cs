// <copyright file="Trie.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace ByteTrie;

/// <summary>
/// Represents a prefix tree (trie) for byte sequences storage.
/// </summary>
public class Trie
{
    /// <summary>
    /// Gets the number of sequences stored in the trie.
    /// </summary>
    public uint Size { get; private set;  } = 0;

    /// <summary>
    /// Gets the root node of the trie.
    /// </summary>
    private Node Root { get; } = new Node(0);

    /// <summary>
    /// Adds a single byte as a sequence to the trie.
    /// </summary>
    /// <param name="element">The byte to add.</param>
    /// <returns>A tuple containing the terminal node and a boolean indicating success.</returns>
    public (Node? Terminal, bool IsSuccess) Add(byte element) => this.Add([element]);

    /// <summary>
    /// Adds a byte sequence to the trie.
    /// </summary>
    /// <param name="element">The byte array representing the sequence to add.</param>
    /// <returns>A tuple containing the terminal node if the sequence was added successfully, otherwise null, and a boolean indicating success.</returns>
    public (Node? Terminal, bool IsSuccess) Add(byte[] element)
    {
        ArgumentNullException.ThrowIfNull(element);
        if (element.Length == 0)
        {
            throw new ArgumentException("The element cannot be empty.", nameof(element));
        }

        var (endPrefixNode, nextPosition) = this.FindLongestPrefix(element);

        if (nextPosition == element.Length)
        {
            if (!endPrefixNode.IsTerminal)
            {
                endPrefixNode.IsTerminal = true;
                endPrefixNode.Index = this.Size;
                this.Size++;
                return (endPrefixNode, true);
            }

            return (endPrefixNode, false);
        }

        endPrefixNode.Children[element[nextPosition]] = this.CreateSuffix(element[nextPosition..]);
        this.Size++;

        return (endPrefixNode, true);
    }

    /// <summary>
    /// Checks if the given byte sequence exists in the trie.
    /// </summary>
    /// <param name="element">The byte array representing the sequence to check.</param>
    /// <returns>True if the sequence exists in the trie, otherwise false.</returns>
    public bool Contains(byte[] element)
    {
        var (endPrefixNode, nextPosition) = this.FindLongestPrefix(element);

        return nextPosition == element.Length && endPrefixNode.IsTerminal;
    }

    /// <summary>
    /// Checks if the given byte element exists in the trie.
    /// </summary>
    /// <param name="element">The byte element to check for existence in the trie.</param>
    /// <returns>True if the byte element exists in the trie, otherwise false.</returns>
    public bool Contains(byte element) => this.Contains([element]);

    /// <summary>
    /// Retrieves the index of the given byte sequence in the trie.
    /// </summary>
    /// <param name="element">The byte sequence for which to get the index.</param>
    /// <returns>The index of the sequence in the trie, or Argument Exception if the sequence is not found.</returns>
    public uint GetIndex(byte[] element)
    {
        (Node sequenceEnd, int nextPosition) = this.FindLongestPrefix(element);
        if (nextPosition != element.Length)
        {
            throw new ArgumentException("The element is not present in trie");
        }

        return sequenceEnd.Index;
    }

    private Node CreateSuffix(byte[] element)
    {
        Node source = new(element[0]);
        Node previous = source;

        for (int i = 1; i < element.Length; i++)
        {
            Node newNode = new(element[i]);
            previous.Children[element[i]] = newNode;
            previous = newNode;
        }

        previous.Index = this.Size;
        previous.IsTerminal = true;
        return source;
    }

    private (Node EndPrefixNode, int NextPosition) FindLongestPrefix(byte[] element)
    {
        int currentPosition = 0;
        Node currentNode = this.Root;

        while (currentPosition < element.Length)
        {
            var currentElement = element[currentPosition];
            if (currentNode.Children.TryGetValue(currentElement, out Node? value))
            {
                currentNode = value;
                currentPosition++;
            }
            else
            {
               return (currentNode, currentPosition);
            }
        }

        return (currentNode, currentPosition);
    }
}
