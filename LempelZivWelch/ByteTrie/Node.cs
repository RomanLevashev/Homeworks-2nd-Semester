namespace ByteTrie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a node in the trie data structure.
    /// </summary>
    /// <param name="value">The byte value associated with the node.</param>
    public class Node(byte value)
    {
        /// <summary>
        /// Gets the byte value associated with this node.
        /// </summary>
        public byte Value { get; } = value;

        /// <summary>
        /// Gets the dictionary of child nodes, where the key is the byte value.
        /// </summary>
        public Dictionary<byte, Node> Children { get; } = [];

        /// <summary>
        /// Gets or sets a value indicating whether this node is a terminal node in the Trie.
        /// A terminal node marks the end of a valid sequence.
        /// </summary>
        public bool IsTerminal { get; set; }

        /// <summary>
        /// Gets or sets the index of this node in the Trie.
        /// </summary>
        public uint Index { get; set; }
    }
}
