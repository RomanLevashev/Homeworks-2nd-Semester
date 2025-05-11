namespace SkipList
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a node in a skip list. Each node contains a value and a set of forward references to other nodes at different levels.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value stored in the node. It can be any type that is either a reference type or a nullable value type.
    /// </typeparam>
    public class SkipListNode<T>(T? value)
    {
        /// <summary>
        /// Gets the value stored in the node.
        /// </summary>
        public T? Value { get; } = value;

        /// <summary>
        /// Gets a dictionary of forward references to nodes at different levels in the skip list.
        /// The key is the level index, and the value is the node at that level.
        /// </summary>
        public Dictionary<int, SkipListNode<T>> Forward { get; } = new();
    }
}
