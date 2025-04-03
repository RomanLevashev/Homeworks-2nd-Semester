namespace ParseTree.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an operation node in an expression tree that combines two child nodes with a specified operator.
    /// </summary>
    public class OperationNode(Node left, Node right, Operators operatorSymbol) : Node
    {
        /// <summary>
        /// Gets the left child node of the operation.
        /// </summary>
        public Node Left { get; } = left;

        /// <summary>
        /// Gets the right child node of the operation.
        /// </summary>
        public Node Right { get; } = right;

        /// <summary>
        /// Gets the operator that will be applied to the child nodes.
        /// </summary>
        public Operators Operator { get; } = operatorSymbol;

        private static Dictionary<Operators, Func<int, int, int>> OperationsDict { get; } = new()
        {
            [Operators.Add] = (a, b) => a + b,
            [Operators.Subtract] = (a, b) => a - b,
            [Operators.Multiply] = (a, b) => a * b,
            [Operators.Divide] = (a, b) => b == 0 ? throw new DivideByZeroException() : a / b,
        };

        /// <summary>
        /// Evaluates the result of applying the operator to the child nodes.
        /// </summary>
        /// /// <returns>
        /// The integer result of the operation.
        /// </returns>
        public override int Evaluate()
        {
            if (OperationsDict.TryGetValue(this.Operator, out var operation))
            {
                return operation(this.Left.Evaluate(), this.Right.Evaluate());
            }

            throw new InvalidOperationException($"Unknown operator: {this.Operator}");
        }

        /// <summary>
        /// Prints the operation and its operands in S-expression format.
        /// </summary>
        public override void Print()
        {
            Console.Write($"({(char)this.Operator} ");
            this.Left.Print();
            Console.Write(" ");
            this.Right.Print();
            Console.Write(")");
        }
    }
}
