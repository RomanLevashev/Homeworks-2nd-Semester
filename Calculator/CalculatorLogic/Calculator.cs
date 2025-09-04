// <copyright file="Calculator.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace CalculatorLogic;

using System.Globalization;
using System.Numerics;
using System.Text;

/// <summary>
/// Represents the core logic of a simple calculator that builds
/// an expression and calculates its result in real-time.
/// </summary>
public class Calculator
{
    private bool isDoubleMode = false;
    private bool isWaitingForOperator = false;
    private bool isPreviousANumber = false;
    private List<string> tokens = [];

    /// <summary>
    /// Gets the current expression being built by the calculator.
    /// </summary>
    public StringBuilder Expression { get; private set; } = new();

    /// <summary>
    /// Gets the current calculated result that is displayed to the user.
    /// </summary>
    public string DisplayResult { get; private set; } = string.Empty;

    /// <summary>
    /// Adds a digit character to the current expression.
    /// </summary>
    /// <param name="digit">The digit character to add. Must be between '0' and '9'.</param>
    /// <returns>
    /// <c>true</c> if the digit was successfully added to the display;
    /// <c>false</c> if the digit is invalid or display length limit is exceeded.
    /// </returns>
    public bool AddDigit(char digit)
    {
        if (!(digit >= '0' && digit <= '9'))
        {
            return false;
        }

        this.Expression.Append(digit);
        this.isWaitingForOperator = true;
        if (this.isPreviousANumber || (this.tokens.Count > 0 && this.tokens[^1].Contains(',')))
        {
            this.tokens[^1] = this.tokens[^1] + digit;
        }
        else
        {
            this.tokens.Add(digit.ToString());
        }

        this.isPreviousANumber = true;
        this.CalculateExpression();
        return true;
    }

    /// <summary>
    /// Adds a decimal point to the current expression.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the decimal point was successfully added;
    /// <c>false</c> if a decimal point already exists or display is at maximum length.
    /// </returns>
    public bool AddComma()
    {
        if (!this.isPreviousANumber)
        {
            return false;
        }

        this.Expression.Append(',');
        this.tokens[^1] = this.tokens[^1] + ',';
        this.isPreviousANumber = false;
        this.isWaitingForOperator = false;
        this.isDoubleMode = true;

        return true;
    }

    /// <summary>
    /// Adds an arithmetic operator for pending calculation.
    /// </summary>
    /// <param name="op">The operator character. Valid values: '+', '-', '*', '/'.</param>
    /// <returns>
    /// <c>true</c> if the operator was successfully set;
    /// <c>false</c> if the operator is invalid or there's an existing pending operation.
    /// </returns>
    public bool AddOperator(char op)
    {
        if (!"×−+÷".Contains(op))
        {
            return false;
        }

        if (!this.isWaitingForOperator)
        {
            return false;
        }

        this.Expression.Append(' ');
        this.Expression.Append(op);
        this.Expression.Append(' ');
        this.isPreviousANumber = false;
        this.isWaitingForOperator = false;
        this.tokens.Add(op.ToString());

        return true;
    }

    /// <summary>
    /// Deletes the last character from the current display value.
    /// </summary>
    /// <returns>
    /// <c>true</c> if a character was successfully deleted;
    /// <c>false</c> if the display is already at minimum length or cannot be deleted further.
    /// </returns>
    public bool Delete()
    {
        if (this.Expression.Length == 0)
        {
            return false;
        }

        int deleteCount = this.Expression[^1] == ' ' ? 3 : 1;
        this.Expression.Remove(this.Expression.Length - deleteCount, deleteCount);

        if (this.tokens[^1].Length > 1)
        {
            this.tokens[^1] = this.tokens[^1].Substring(0, this.tokens[^1].Length - 1);
        }
        else
        {
            if (this.tokens[^1][^1] >= '0' && this.tokens[^1][^1] <= '9')
            {
                this.isWaitingForOperator = false;
                this.isPreviousANumber = false;
            }
            else
            {
                this.isPreviousANumber = true;
                this.isWaitingForOperator = true;
            }

            this.tokens.RemoveAt(this.tokens.Count - 1);
        }

        this.CalculateExpression();
        return true;
    }

    private void CalculateExpression()
    {
        if (this.tokens.Count == 0)
        {
            this.DisplayResult = string.Empty;
            return;
        }

        double currentResult = 0;
        char previousOperator = '\0';

        foreach (string token in this.tokens)
        {
            if (double.TryParse(token, new CultureInfo("ru-RU"), out double temp))
            {
                if (currentResult == 0 && previousOperator == '\0')
                {
                    currentResult = temp;
                    continue;
                }

                switch (previousOperator)
                {
                    case '+':
                        currentResult += temp;
                        break;

                    case '−':
                        currentResult -= temp;
                        break;

                    case '×':
                        currentResult *= temp;
                        break;

                    case '÷':
                        if (temp == 0)
                        {
                            this.DisplayResult = "ZeroDivisionError";
                            return;
                        }

                        currentResult /= temp;
                        if (Math.Floor(currentResult) != currentResult)
                        {
                            this.isDoubleMode = true;
                        }

                        break;
                }
            }

            if (token.Length == 1 && "×+−÷".Contains(token))
            {
                previousOperator = token[0];
            }
        }

        if (!this.isDoubleMode)
        {
            BigInteger result = (BigInteger)currentResult;
            this.DisplayResult = result.ToString(new CultureInfo("ru-RU"));
            return;
        }

        this.DisplayResult = currentResult.ToString(new CultureInfo("ru-RU"));
    }
}
