namespace ParseTree.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an abstract node in an parse tree.
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Evaluates the node's numeric value.
        /// </summary>
        /// <returns>
        /// The integer value stored in this node.
        /// </returns>
        public abstract int Evaluate();

        /// <summary>
        /// Prints the node's structure.
        /// </summary>
        public abstract void Print();
    }
}
