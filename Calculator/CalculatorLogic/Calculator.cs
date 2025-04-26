namespace CalculatorLogic
{
    using System.Globalization;
    using System.Numerics;
    using System.Reflection.Metadata.Ecma335;
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

        /// <summary>
        /// Gets the current expression being built by the calculator.
        /// </summary>
        public StringBuilder Expression { get; private set; } = new StringBuilder();

        /// <summary>
        /// Gets the current calculated result that is displayed to the user.
        /// </summary>
        public string DisplayResult { get; private set; } = string.Empty;

        /// <summary>
        /// Updates the current expression based on the provided button type and character input,
        /// and recalculates the result if necessary.
        /// </summary>
        /// <param name="elementType">The type of the button that was pressed (digit, operator, etc.).</param>
        /// <param name="element">The character representing the input value.</param>
        /// <returns>
        /// /// A tuple containing:
        /// - The updated expression string.
        /// - The updated result string.
        /// - A boolean indicating whether the expression changed.
        /// </returns>
        public (string newExpression, string displayResult, bool isChanged) UpdateExpression(ButtonType elementType, char element)
        {
            switch (elementType)
            {
                case ButtonType.Digit:

                    this.Expression.Append(element);
                    this.isPreviousANumber = true;
                    this.isWaitingForOperator = true;
                    this.CalculateExpression();
                    break;

                case ButtonType.Operator:
                    if (!this.isWaitingForOperator)
                    {
                        return (string.Empty, string.Empty, false);
                    }

                    this.Expression.Append(' ');
                    this.Expression.Append(element);
                    this.Expression.Append(' ');
                    this.isPreviousANumber = false;
                    this.isWaitingForOperator = false;
                    break;

                case ButtonType.Comma:
                    if (!this.isPreviousANumber)
                    {
                        return (string.Empty, string.Empty, false);
                    }

                    this.isDoubleMode = true;
                    this.Expression.Append(element);
                    this.isPreviousANumber = false;
                    this.isWaitingForOperator = false;
                    break;

                case ButtonType.Delete:
                    int deleteCount = this.Expression.Length > 1 && this.Expression[^1] == ' ' ? 2 : 1;
                    if (this.Expression.Length > 0)
                    {
                        this.Expression.Remove(this.Expression.Length - deleteCount, deleteCount);
                        this.CalculateExpression();
                    }

                    break;
            }

            return (this.Expression.ToString(), this.DisplayResult, true);
        }

        private void CalculateExpression()
        {
            string expression = this.Expression.ToString();

            if (expression == string.Empty)
            {
                this.DisplayResult = string.Empty;
                return;
            }

            string[] tokens = expression.Split(' ');
            double currentResult = 0;
            char previousOperator = '\0';

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double temp))
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
}
