// <copyright file="BurrowsWheeler.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace BurrowsWheelerTransform;

using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Provides methods for encoding and decoding text using the Burrows-Wheeler Transform algorithm.
/// </summary>
public static class BurrowsWheeler
{
    /// <summary>
    /// Performs Burrows-Wheeler Transform on the input string.
    /// </summary>
    /// <param name="input">The string to transform. Cannot be null or empty.</param>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    /// <item><description>Transformed - The BWT-transformed string</description></item>
    /// <item><description>Position - The zero-based index of original string in sorted rotations table</description></item>
    /// </list>
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when input is null or empty.</exception>
    public static (string Transformed, int Position) Transform(string input)
    {
        ArgumentException.ThrowIfNullOrEmpty(input, "Input string can't be null or empty.");

        int length = input.Length;
        var rotations = new string[length];

        for (int i = 0; i < length; i++)
        {
            rotations[i] = input.Substring(length - i, i) + input.Substring(0, length - i);
        }

        Array.Sort(rotations);
        StringBuilder resultBuilder = new();
        int position = 0;

        for (int i = 0; i < length; i++)
        {
            if (rotations[i] == input)
            {
                position = i;
            }

            resultBuilder.Append(rotations[i][length - 1]);
        }

        return (resultBuilder.ToString(), position);
    }

    /// <summary>
    /// Reverses the Burrows-Wheeler Transform to reconstruct the original string.
    /// </summary>
    /// <param name="transformed">The BWT-transformed string to process. Cannot be null or empty.</param>
    /// <param name="position">The zero-based index of the original string in the sorted rotations table (range: 0 to transformed.Length - 1).</param>
    /// <returns>The original string before transformation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when transformed string is null or empty.</exception>
    /// <exception cref="IndexOutOfRangeException"> Thrown when: position is outside valid range.</exception>
    public static string InverseTransform(string transformed, int position)
    {
        ArgumentNullException.ThrowIfNull(transformed, "Transformed string can't be null.");
        if (transformed.Length == 0)
        {
            throw new ArgumentException("Input string can't be empty.");
        }

        if (position >= transformed.Length || position < 0)
        {
            throw new IndexOutOfRangeException(nameof(position));
        }

        var countBefore = new int[transformed.Length];
        Dictionary<char, int> charCurrentCount = [];

        for (int i = 0; i < transformed.Length; ++i)
        {
            char current = transformed[i];
            var count = 0;
            if (charCurrentCount.ContainsKey(current))
            {
                count = charCurrentCount[current]++;
            }
            else
            {
                charCurrentCount[current] = 1;
            }

            countBefore[i] = count;
        }

        HashSet<char> uniqueChars = new (transformed);
        Dictionary<char, int> countSmaller = [];

        foreach (char current in uniqueChars)
        {
            countSmaller[current] = 0;
            foreach (char other in uniqueChars)
            {
                if (current.CompareTo(other) > 0)
                {
                    countSmaller[current] += charCurrentCount[other];
                }
            }
        }

        StringBuilder stringBuilder = new();
        stringBuilder.Append(transformed[position]);
        int previousPosition = position;

        for (int i = transformed.Length - 2; i >= 0; i--)
        {
            char previous = transformed[previousPosition];
            int currentCharPosition = countSmaller[previous] + countBefore[previousPosition];
            stringBuilder.Insert(0, transformed[currentCharPosition]);
            previousPosition = currentCharPosition;
        }

        return stringBuilder.ToString();
    }
}