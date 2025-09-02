// <copyright file="DataHandler.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace ParseTree;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParseTree.Nodes;

/// <summary>
/// Provides functionality for parsing and evaluating expressions using parse trees.
/// </summary>
public static class DataHandler
{
    /// <summary>
    /// Parses a string input into a parse tree structure.
    /// </summary>
    /// <param name="input">The string expression to parse.</param>
    /// <returns>The root <see cref="Node"/> of the constructed parse tree.</returns>
    public static Node Parse(string input)
    {
        var tokens = Tokenize(input);
        var enumerator = tokens.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            throw new FormatException("Empty expression");
        }

        var result = ParseExpression(enumerator);

        if (result.ResultEnumerator.Current != null)
        {
            throw new FormatException($"Unexpected extra tokens: '{enumerator.Current}'");
        }

        return result.Root;
    }

    /// <summary>
    /// Evaluates and prints both the parse tree structure and the expression result.
    /// </summary>
    /// /// <param name="expression">The mathematical expression to evaluate.</param>
    public static void EvaluateAndPrintExpression(string expression)
    {
        var root = Parse(expression);
        Console.Write("Parse Tree: ");
        root.Print();
        Console.WriteLine();
        Console.WriteLine($"Expression result: {root.Evaluate()}");
    }

    private static (Node Root, IEnumerator<string> ResultEnumerator) ParseExpression(IEnumerator<string> enumerator)
    {
        var token = enumerator.Current;

        if (int.TryParse(token, out var value))
        {
            enumerator.MoveNext();
            return (new NumberNode(value), enumerator);
        }
        else if (token == '('.ToString())
        {
            if (!enumerator.MoveNext())
            {
                throw new FormatException("Missing operator after (");
            }

            var operatorToken = enumerator.Current;
            var operation = ParseOperator(operatorToken);

            if (!enumerator.MoveNext())
            {
                throw new FormatException("Missing operand");
            }

            var left = ParseExpression(enumerator);
            var right = ParseExpression(enumerator);

            if (enumerator.Current != ')'.ToString())
            {
                throw new FormatException("Expected )");
            }

            enumerator.MoveNext();
            return (new OperationNode(left.Root, right.Root, operation), enumerator);
        }

        throw new FormatException("Invalid format");
    }

    private static Operators ParseOperator(string token)
    {
        var validOperators = Enum.GetValues(typeof(Operators)).Cast<int>();
        if (!validOperators.Contains<int>(token[0]))
        {
            throw new FormatException($"Unknown operator: {token}");
        }

        return (Operators)token[0];
    }

    private static List<string> Tokenize(string input)
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));

        var tokens = new List<string>();
        var currentToken = new StringBuilder();

        foreach (char c in input)
        {
            if (char.IsWhiteSpace(c))
            {
                if (currentToken.Length > 0)
                {
                    tokens.Add(currentToken.ToString());
                    currentToken.Clear();
                }
            }
            else if (c == '(' || c == ')')
            {
                if (currentToken.Length > 0)
                {
                    tokens.Add(currentToken.ToString());
                    currentToken.Clear();
                }

                tokens.Add(c.ToString());
            }
            else
            {
                currentToken.Append(c);
            }
        }

        if (currentToken.Length > 0)
        {
            var temp = currentToken.ToString();
            if (int.TryParse(temp, out int value))
            {
                tokens.Add(temp);
            }
        }

        return tokens;
    }
}
