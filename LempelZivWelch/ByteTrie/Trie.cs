namespace ByteTrie
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.AccessControl;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    /// <summary>
    /// Представляет собой префиксное дерево (бор) для хранения строк.
    /// </summary>
    public class Trie
    {
        /// <summary>
        /// Корневой узел бора, имеет в себе значение 0, все первые байты последовательностей - его потомки.
        /// </summary>
        public Node Root { get;  } = new Node(0);

        /// <summary>
        /// Количество последовательностей в боре.
        /// </summary>
        public uint Size { get; private set;  } = 0;

        /// <summary>
        /// Добавляет байт в бор.
        /// </summary>
        /// <param name="element">Последовательность байтов.</param>
        /// <returns>True, если слово добавлено, иначе false.</returns>
        public (Node terminal, bool isSuccess) Add(byte element)
        {
            return this.Add([element]);
        }

        /// <summary>
        /// Добавляет последовательность байтов в бор.
        /// </summary>
        /// <param name="element">Последовательность байтов.</param>
        /// <returns>True, если слово добавлено, иначе false.</returns>
        public (Node? terminal, bool isSuccess) Add(byte[] element)
        {
            if (element == null || element.Length == 0)
            {
                throw new ArgumentException("The element cannot be null or empty.", nameof(element));
            }

            var (endPrefixNode, nextPosition) = this.FindLongestPrefix(element);

            if (nextPosition == element.Length)
            {
                if (!endPrefixNode.IsTerminal)
                {
                    endPrefixNode.IsTerminal = true;
                    endPrefixNode.Index = this.Size;
                    this.Size++;
                    return (endPrefixNode, true);
                }

                return (endPrefixNode, false);
            }

            endPrefixNode.Children[element[nextPosition]] = CreateSuffix(element[nextPosition..]);
            this.Size++;

            return (endPrefixNode, true);
        }

        /// <summary>
        /// Проверяет, содержится ли последовательность байтов в боре.
        /// </summary>
        /// <param name="element">Последовательность байтов для поиска.</param>
        /// <returns>True, если последовательность байтов найдена, иначе false.</returns>
        public bool Contains(byte[] element)
        {
            var (endPrefixNode, nextPosition) = this.FindLongestPrefix(element);

            return nextPosition == element.Length && endPrefixNode.IsTerminal == true;
        }

        /// <summary>
        /// Проверяет, содержится ли байт в боре.
        /// </summary>
        /// <param name="element">Байт для поиска</param>
        /// <returns></returns>
        public bool Contains(byte element)
        {
            return this.Contains([element]);
        }

        public uint GetIndex(byte[] element)
        {
            (Node sequenceEnd, int nextPosition) = FindLongestPrefix(element);
            if (nextPosition != element.Length)
            {
                throw new ArgumentException("The element is not present in trie");
            }

            return sequenceEnd.Index;
        }

        private Node CreateSuffix(byte[] element)
        {
            Node source = new Node(element[0]);
            Node previous = source;

            for (int i = 1; i < element.Length; i++)
            {
                Node newNode = new Node(element[i]);
                previous.Children[element[i]] = newNode;
                previous = newNode;
            }

            previous.Index = this.Size;
            previous.IsTerminal = true;
            return source;
        }

        private (Node endPrefixNode, int nextPosition) FindLongestPrefix(byte[] element)
        {
            int currentPosition = 0;
            Node currentNode = this.Root;
            Stack<Node> stack = new Stack<Node>();

            while (currentPosition < element.Length)
            {
                var currentElement = element[currentPosition];
                if (currentNode.Children.TryGetValue(currentElement, out Node? value))
                {
                    currentNode = value;
                    currentPosition++;
                }
                else
                {
                   return (currentNode, currentPosition);
                }
            }

            return (currentNode, currentPosition);
        }
    }
}
