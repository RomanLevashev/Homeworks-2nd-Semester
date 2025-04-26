namespace CalculatorLogic
{
    /// <summary>
    /// Represents the type of a button pressed in the calculator.
    /// Determines how the calculator should interpret and process the input.
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// A numeric digit (0–9) button.
        /// </summary>
        Digit,

        /// <summary>
        /// An arithmetic operator button (such as +, −, ×, ÷).
        /// </summary>
        Operator,

        /// <summary>
        /// A delete button, used to remove the last character or operator from the expression.
        /// </summary>
        Delete,

        /// <summary>
        /// A comma (decimal point) button, used for inputting floating-point numbers.
        /// </summary>
        Comma,
    }
}
