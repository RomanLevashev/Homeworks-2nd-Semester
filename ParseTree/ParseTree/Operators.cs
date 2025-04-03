namespace ParseTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents arithmetic operators supported by the parse tree.
    /// </summary>
    public enum Operators
    {
        /// <summary>
        /// Addition operator '+'
        /// </summary>
        Add = '+',

        /// <summary>
        /// Subtraction operator '-' (Note: Corrected spelling from 'Substract' to 'Subtract')
        /// </summary>
        Subtract = '-',

        /// <summary>
        /// Multiplication operator '*'
        /// </summary>
        Multiply = '*',

        /// <summary>
        /// Division operator '/'
        /// </summary>
        Divide = '/',
    }
}
