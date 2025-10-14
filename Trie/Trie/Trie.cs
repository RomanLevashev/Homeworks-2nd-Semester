// <copyright file="Trie.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace Trie;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a Trie (prefix tree) data structure for storing and retrieving strings efficiently.
/// </summary>
public class Trie
{
    /// <summary>
    /// Gets the root node of the Trie.
    /// </summary>
    public Node Root { get; } = new Node('\0');

    /// <summary>
    /// Adds a string to the Trie.
    /// </summary>
    /// <param name="element">The string to add to the Trie.</param>
    /// <returns>
    /// <c>true</c> if the string was successfully added;
    /// <c>false</c> if the string already exists in the Trie.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the input string is null or empty.
    /// </exception>
    public bool Add(string element)
    {
        ArgumentException.ThrowIfNullOrEmpty(element, nameof(element));

        var (endPrefixNode, nextPosition, _) = this.FindLongestPrefix(element);

        if (nextPosition == element.Length)
        {
            if (!endPrefixNode.IsTerminal)
            {
                endPrefixNode.IsTerminal = true;
                return true;
            }

            return false;
        }

        endPrefixNode.Children[element[nextPosition]] = CreateSuffix(element.Substring(nextPosition));
        return true;
    }

    /// <summary>
    /// Removes a string from the Trie.
    /// </summary>
    /// <param name="element">The string to remove from the Trie.</param>
    /// <returns>
    /// <c>true</c> if the string was successfully removed;
    /// <c>false</c> if the string wasn't found in the Trie.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the input string is null or empty.
    /// </exception>
    public bool Remove(string element)
    {
        ArgumentException.ThrowIfNullOrEmpty(element, nameof(element));

        var (endPrefixNode, nextPosition, pathStack) = this.FindLongestPrefix(element);

        if (nextPosition != element.Length || !endPrefixNode.IsTerminal)
        {
            return false;
        }

        char previousSymbol = '\0';
        bool isPreviousLeaf = false;
        endPrefixNode.IsTerminal = false;

        while (pathStack!.Count > 0)
        {
            Node last = pathStack.Pop();

            if (isPreviousLeaf)
            {
                last.Children.Remove(previousSymbol);
                isPreviousLeaf = false;
            }

            if (last.Children.Count == 0 && !last.IsTerminal)
            {
                isPreviousLeaf = true;
                previousSymbol = last.Value;
            }
            else
            {
                return true;
            }
        }

        if (isPreviousLeaf)
        {
            this.Root.Children.Remove(previousSymbol);
        }

        return true;
    }

    /// <summary>
    /// Determines whether the Trie contains the specified string.
    /// </summary>
    /// <param name="element">The string to locate in the Trie.</param>
    /// <returns>
    /// <c>true</c> if the Trie contains the string;
    /// <c>false</c> otherwise.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the input string is null or empty.
    /// </exception>
    public bool Contains(string element)
    {
        ArgumentException.ThrowIfNullOrEmpty(element, nameof(element));

        var (endPrefixNode, nextPosition, _) = this.FindLongestPrefix(element);

        return nextPosition == element.Length && endPrefixNode.IsTerminal;
    }

    /// <summary>
    /// Counts how many complete strings in the Trie start with the specified prefix.
    /// </summary>
    /// <param name="prefix">The prefix to search for.</param>
    /// <returns>
    /// The number of complete strings that start with the prefix.
    /// Returns 0 if the prefix doesn't exist in the Trie.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the input string is null or empty.
    /// </exception>
    public int HowManyStartsWithPrefix(string prefix)
    {
        ArgumentException.ThrowIfNullOrEmpty(prefix, nameof(prefix));

        var (endPrefixNode, nextPosition, _) = this.FindLongestPrefix(prefix);

        return nextPosition == prefix.Length ? endPrefixNode.Children.Count : 0;
    }

    private static Node CreateSuffix(string str)
    {
        ArgumentException.ThrowIfNullOrEmpty(str, nameof(str));

        Node source = new(str[0]);
        Node previous = source;

        for (int i = 1; i < str.Length; i++)
        {
            Node newNode = new(str[i]);
            previous.Children[str[i]] = newNode;
            previous = newNode;
        }

        previous.IsTerminal = true;
        return source;
    }

    private (Node EndPrefixNode, int NextPosition, Stack<Node> PathStack) FindLongestPrefix(string element)
    {
        int currentPosition = 0;
        Node currentNode = this.Root;
        Stack<Node> stack = new();

        while (currentPosition < element.Length)
        {
            var currentElement = element[currentPosition];
            if (currentNode.Children.TryGetValue(currentElement, out Node? value))
            {
                currentNode = value;
                stack.Push(currentNode);

                currentPosition++;
            }
            else
            {
                return (currentNode, currentPosition, stack);
            }
        }

        return (currentNode, currentPosition, stack);
    }
}
