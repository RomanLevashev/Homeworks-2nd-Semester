namespace ParseTreeTests
{
    using ParseTree;

    /// <summary>
    /// Contains unit tests for the parse tree.
    /// </summary>
    [TestClass]
    public sealed class ParseTreeTests
    {
        /// <summary>
        /// Tests evaluation of correctly formatted expressions.
        /// </summary>
        [TestMethod]
        public void CorrectExpresions()
        {
            Assert.AreEqual(DataHandler.Parse("(+ 1 2)").Evaluate(), 3);
            Assert.AreEqual(DataHandler.Parse("(- 5 3)").Evaluate(), 2);
            Assert.AreEqual(DataHandler.Parse("(* 2 3)").Evaluate(), 6);
            Assert.AreEqual(DataHandler.Parse("(/ 10 2)").Evaluate(), 5);
            Assert.AreEqual(DataHandler.Parse("(+ (* 2 3) (/ 10 2))").Evaluate(), 11);
            Assert.AreEqual(DataHandler.Parse("(- (* 3 4) (+ 1 2))").Evaluate(), 9);
            Assert.AreEqual(DataHandler.Parse("(/ (* (+ 1 2) 5) 3)").Evaluate(), 5);
            Assert.AreEqual(DataHandler.Parse("(+ (* (- 5 1) (/ 6 2)) 3)").Evaluate(), 15);
            Assert.AreEqual(DataHandler.Parse("(* (+ 1 (/ 8 2)) (- 7 4))").Evaluate(), 15);
            Assert.AreEqual(DataHandler.Parse("(+ 0 0)").Evaluate(), 0);
            Assert.AreEqual(DataHandler.Parse("(* 1 10)").Evaluate(), 10);
            Assert.AreEqual(DataHandler.Parse("(- -5 -2)").Evaluate(), -3);
            Assert.AreEqual(DataHandler.Parse("(/ 10 1)").Evaluate(), 10);
            Assert.AreEqual(DataHandler.Parse("(+ (* 3 (+ 1 2)) (/ (- 10 2) 4))").Evaluate(), 11);
            Assert.AreEqual(DataHandler.Parse("(- (* (+ 1 3) (/ 10 5)) 2)").Evaluate(), 6);
        }

        /// <summary>
        /// Tests handling of expressions with missing whitespace between tokens.
        /// </summary>
        [TestMethod]
        public void MissingSpace()
        {
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("(+(* 23) (/ 10 2))"));
        }

        /// <summary>
        /// Tests parsing of single number expressions.
        /// </summary>
        [TestMethod]
        public void JustANumber()
        {
            Assert.AreEqual(DataHandler.Parse("5").Evaluate(), 5);
        }

        /// <summary>
        /// Tests handling of empty and null input strings.
        /// </summary>
        [TestMethod]
        public void EmptyAndNullExpression()
        {
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse(string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => DataHandler.Parse(null));
        }

        /// <summary>
        /// Tests detection of incomplete expressions with missing operands.
        /// </summary>
        [TestMethod]
        public void OperatorWithoutOperand()
        {
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("(+ 5)"));
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("(+ )"));
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("((+ 5 3 ) -)"));
        }

        /// <summary>
        /// Tests detection of syntax errors with missing parentheses.
        /// </summary>
        [TestMethod]
        public void MissingParenthesis()
        {
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("+ 5 9"));
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("(+ + 3 5 8)"));
        }

        /// <summary>
        /// Tests detection of expressions with extra trailing tokens.
        /// </summary>
        [TestMethod]
        public void ExtraOperators()
        {
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("(+(* 2 3) (/ 10 2)) 3 4"));
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("(+(* 2 3) (/ 10 2)) (+ 3 4)"));
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("(+ 1 2) 3"));
        }

        /// <summary>
        /// Tests handling of expressions containing invalid characters.
        /// </summary>
        [TestMethod]
        public void UnexpectedChars()
        {
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("(+(* 2 a) (/ 10 2)) 3 4"));
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("(+(* 2 3() (/ 10 2)) 3 4"));
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("(+ asd 5 4)"));
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("asd (+ 4 5)"));
            Assert.ThrowsException<FormatException>(() => DataHandler.Parse("(& 4 5)"));
        }
    }
}