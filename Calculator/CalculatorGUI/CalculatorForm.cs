// <copyright file="CalculatorForm.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace CalculatorGUI;

using CalculatorLogic;

/// <summary>
/// Represents a calculator form that provides basic arithmetic operations.
/// This partial class contains the main form initialization and core functionality.
/// </summary>
public partial class CalculatorForm : Form
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CalculatorForm"/> class.
    /// Sets up the calculator form with default values and initial state.
    /// </summary>
    public CalculatorForm()
    {
        this.InitializeComponent();
        var screen = Screen.PrimaryScreen!.WorkingArea;

        int width = (int)(screen.Width * 0.25);
        int height = (int)(screen.Height * 0.6);

        this.Size = new Size(width, height);
        this.StartPosition = FormStartPosition.CenterScreen;
        Calculator calculator = new();

        Button[] buttons =
        {
            this.delete, this.digitZero, this.division, this.addition,
            this.digitThree, this.digitTwo, this.digitOne, this.subtraction,
            this.digitSix, this.digitFive, this.digitFour, this.multiplication,
            this.digitNine, this.digitEight, this.digitSeven, this.comma,
        };

        Font buttonFont = new("Segoe UI", 14, FontStyle.Regular);

        foreach (var btn in buttons)
        {
            btn.Font = buttonFont;
            btn.Click += (sender, e) =>
            {
                Button clickedButton = (Button)sender!;
                ButtonType clickedButtonType = (ButtonType)clickedButton.Tag!;
                char symbol = clickedButton.Text[0];
                bool isChanged = false;

                switch (clickedButtonType)
                {
                    case ButtonType.Digit:
                        isChanged = calculator.AddDigit(symbol);
                        break;

                    case ButtonType.Comma:
                        isChanged = calculator.AddComma();
                        break;

                    case ButtonType.Operator:
                        isChanged = calculator.AddOperator(symbol);
                        break;

                    case ButtonType.Delete:
                        isChanged = calculator.Delete();
                        break;
                }

                if (isChanged)
                {
                    string newExpression = calculator.Expression.ToString();
                    string displayResult = calculator.DisplayResult.ToString();

                    this.expressionLabel.Text = newExpression == string.Empty && displayResult == string.Empty ? "0" : newExpression + " = " + displayResult;
                }
            };
        }
    }
}
