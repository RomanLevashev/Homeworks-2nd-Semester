namespace Trie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Представляет узел дерева Trie.
    /// </summary>
    public class Node(char value)
    {
        /// <summary>
        /// Символ, хранящийся в узле.
        /// </summary>
        public char Value { get; } = value;

        /// <summary>
        /// Дочерние узлы текущего узла.
        /// </summary>
        public Dictionary<char, Node> Children { get; } = [];

        /// <summary>
        /// Является ли узел терминальным (оканчивается ли на нем слово).
        /// </summary>
        public bool IsTerminal { get; set; }
    }
}
