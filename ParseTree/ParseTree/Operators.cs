// <copyright file="Operators.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace ParseTree;

/// <summary>
/// Represents arithmetic operators supported by the parse tree.
/// </summary>
public enum Operators
{
    /// <summary>
    /// Addition operator '+'.
    /// </summary>
    Add = '+',

    /// <summary>
    /// Subtraction operator '-' (Note: Corrected spelling from 'Substract' to 'Subtract').
    /// </summary>
    Subtract = '-',

    /// <summary>
    /// Multiplication operator '*'.
    /// </summary>
    Multiply = '*',

    /// <summary>
    /// Division operator '/'.
    /// </summary>
    Divide = '/',
}
