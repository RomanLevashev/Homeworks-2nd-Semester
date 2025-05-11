namespace SkipList
{
    public class DoublyLinkedListNode<T>
    {
        public T Value { get; }

        public DoublyLinkedListNode<T> Next { get; private set; }

        public DoublyLinkedListNode<T> Previous { get; private set; }
    }
}
