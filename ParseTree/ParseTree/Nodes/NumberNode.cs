namespace ParseTree.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a numeric value node in an expression tree.
    /// </summary>
    public class NumberNode(int value) : Node
    {
        private int Value { get; } = value;

        /// <summary>
        /// Evaluates the numeric value of this node.
        /// </summary>
        /// <returns>
        /// The integer value stored in this node.
        /// </returns>
        public override int Evaluate()
        {
            return this.Value;
        }

        /// <summary>
        /// Prints the numeric value to standard output.
        /// </summary>
        public override void Print()
        {
            Console.Write(this.Value);
        }
    }
}
