// <copyright file="CalculatorLogicTest.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace CalculatorTests;

using System.Globalization;
using CalculatorLogic;

/// <summary>
/// Contains unit tests for the Calculator class logic.
/// </summary>
[TestClass]
public sealed class CalculatorLogicTest
{
    /// <summary>
    /// Tests evaluating an expression with all possible operators
    /// and verifies that the final result is correct.
    /// </summary>
    [TestMethod]
    public void EvaluateExpressionTest()
    {
        string displayResult = this.CalculateExpression("5 + 3 − 2 × 4 ÷ 2,5");

        Assert.AreEqual(double.Parse(displayResult, new CultureInfo("ru-RU")), 9.6);
    }

    /// <summary>
    /// Tests division by zero handling and checks if the calculator returns the expected "ZeroDivisionError" message.
    /// </summary>
    [TestMethod]
    public void ZeroDivisonTest()
    {
        string displayResult = this.CalculateExpression("7 ÷ 0");

        Assert.AreEqual(displayResult, "ZeroDivisionError");
    }

    /// <summary>
    /// Tests adding and then deleting a digit before continuing the expression, ensuring deletion is handled correctly.
    /// </summary>
    [TestMethod]
    public void DeleteAndEvaluateTest()
    {
        string displayResult = this.CalculateExpression("78 + 215 ⌫ − 3,1 × 2 ÷ 4");

        Assert.AreEqual(double.Parse(displayResult, new CultureInfo("ru-RU")), 47.95);
    }

    /// <summary>
    /// Tests how the calculator behaves when the expression starts incorrectly
    /// (e.g., starts with an operator), and ensures invalid operations do not modify the expression.
    /// </summary>
    [TestMethod]
    public void WrongFormatTest()
    {
        Calculator calculator = new();
        bool isChanged = calculator.AddOperator('+');
        Assert.IsFalse(isChanged);
        Assert.AreEqual(string.Empty, calculator.DisplayResult);
        Assert.AreEqual(string.Empty, calculator.Expression.ToString());

        calculator.AddDigit('7');
        calculator.AddOperator('+');

        foreach (var op in new char[] { '×', '+', '−', '÷' })
        {
            isChanged = calculator.AddOperator(op);
            Assert.IsFalse(isChanged);
            Assert.AreEqual(calculator.Expression.ToString(), "7 + ");
        }

        calculator.Delete();
        calculator.Delete();
        Assert.AreEqual(calculator.Expression.ToString(), string.Empty);
        Assert.AreEqual(calculator.DisplayResult, string.Empty);
    }

    private string CalculateExpression(string expression)
    {
        Calculator calculator = new();
        string[] tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in tokens)
        {
            if (token.Length > 1)
            {
                foreach (var ch in token)
                {
                    if (ch >= '0' && ch <= '9')
                    {
                        calculator.AddDigit(ch);
                    }

                    if (ch == ',')
                    {
                        calculator.AddComma();
                    }
                }
            }
            else if (token[^1] >= '0' && token[^1] <= '9')
            {
                calculator.AddDigit(token[^1]);
            }

            if ("×−+÷".Contains(token[^1]))
            {
                calculator.AddOperator(token[^1]);
            }

            if (token[^1] == '⌫')
            {
                calculator.Delete();
            }
        }

        return calculator.DisplayResult;
    }
}