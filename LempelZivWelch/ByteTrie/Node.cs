namespace ByteTrie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Представляет узел дерева Trie.
    /// </summary>
    public class Node(byte value)
    {
        /// <summary>
        /// Символ, хранящийся в узле.
        /// </summary>
        public byte Value { get; } = value;

        /// <summary>
        /// Дочерние узлы текущего узла.
        /// </summary>
        public Dictionary<byte, Node> Children { get; } = [];

        /// <summary>
        /// Является ли узел терминальным (оканчивается ли на нем слово).
        /// </summary>
        public bool IsTerminal { get; set; }

        /// <summary>
        /// Индекс последовательности.
        /// </summary>
        public uint Index { get; set; }
    }
}
