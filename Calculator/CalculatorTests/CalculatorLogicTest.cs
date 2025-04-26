namespace CalculatorTests
{
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
            Calculator calculator = new();

            (ButtonType, char)[] expressionSteps = new (ButtonType, char)[]
            {
                (ButtonType.Digit, '5'),
                (ButtonType.Operator, '+'),
                (ButtonType.Digit, '3'),
                (ButtonType.Operator, '−'),
                (ButtonType.Digit, '2'),
                (ButtonType.Operator, '×'),
                (ButtonType.Digit, '4'),
                (ButtonType.Operator, '÷'),
                (ButtonType.Digit, '2'),
                (ButtonType.Comma, ','),
                (ButtonType.Digit, '5'),
            };

            foreach (var (type, symbol) in expressionSteps)
            {
                var tempResult = calculator.UpdateExpression(type, symbol);
                Assert.IsTrue(tempResult.isChanged);
            }

            Assert.AreEqual(double.Parse(calculator.DisplayResult, new CultureInfo("ru-RU")), 9.6);
        }

        /// <summary>
        /// Tests division by zero handling and checks if the calculator returns the expected "ZeroDivisionError" message.
        /// </summary>
        [TestMethod]
        public void ZeroDivisonTest()
        {
            Calculator calculator = new();
            calculator.UpdateExpression(ButtonType.Digit, '7');
            calculator.UpdateExpression(ButtonType.Operator, '÷');
            calculator.UpdateExpression(ButtonType.Digit, '0');

            Assert.AreEqual(calculator.DisplayResult, "ZeroDivisionError");
        }

        /// <summary>
        /// Tests adding and then deleting a digit before continuing the expression, ensuring deletion is handled correctly.
        /// </summary>
        [TestMethod]
        public void DeleteAndEvaluateTest()
        {
            Calculator calculator = new();

            (ButtonType, char)[] expressionSteps = new (ButtonType, char)[]
            {
                (ButtonType.Digit, '7'),
                (ButtonType.Digit, '8'),
                (ButtonType.Operator, '+'),
                (ButtonType.Digit, '2'),
                (ButtonType.Digit, '1'),
                (ButtonType.Digit, '5'),
                (ButtonType.Delete, '\0'),
                (ButtonType.Operator, '−'),
                (ButtonType.Digit, '3'),
                (ButtonType.Comma, ','),
                (ButtonType.Digit, '1'),
                (ButtonType.Operator, '×'),
                (ButtonType.Digit, '2'),
                (ButtonType.Operator, '÷'),
                (ButtonType.Digit, '4'),
            };

            foreach (var (type, symbol) in expressionSteps)
            {
                var tempResult = calculator.UpdateExpression(type, symbol);
                Assert.IsTrue(tempResult.isChanged);
            }

            Assert.AreEqual(double.Parse(calculator.DisplayResult, new CultureInfo("ru-RU")), 47.95);
        }

        /// <summary>
        /// Tests how the calculator behaves when the expression starts incorrectly
        /// (e.g., starts with an operator), and ensures invalid operations do not modify the expression.
        /// </summary>
        [TestMethod]
        public void WrongFormatTest()
        {
            Calculator calculator = new();
            var tempResult = calculator.UpdateExpression(ButtonType.Operator, '+');
            Assert.IsFalse(tempResult.isChanged);
            Assert.AreEqual(string.Empty, calculator.DisplayResult);
            Assert.AreEqual(string.Empty, calculator.Expression.ToString());

            calculator.UpdateExpression(ButtonType.Digit, '7');
            calculator.UpdateExpression(ButtonType.Operator, '+');

            foreach (var op in new char[] { '×', '+', '−', '÷' })
            {
                tempResult = calculator.UpdateExpression(ButtonType.Operator, op);
                Assert.IsFalse(tempResult.isChanged);
                Assert.AreEqual(calculator.Expression.ToString(), "7 + ");
            }

            calculator.UpdateExpression(ButtonType.Delete, '\0');
            calculator.UpdateExpression(ButtonType.Delete, '\0');
            Assert.AreEqual(calculator.Expression.ToString(), string.Empty);
            Assert.AreEqual(calculator.DisplayResult, string.Empty);
        }
    }
}
