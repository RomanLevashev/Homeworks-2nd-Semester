namespace Trie
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    /// <summary>
    /// Представляет собой префиксное дерево (бор) для хранения строк.
    /// </summary>
    public class Trie
    {
        /// <summary>
        /// Корневой узел бора, имеет в себе значение "r", все первые буквы слов его потомки.
        /// </summary>
        public Node Root { get;  } = new Node('r');

        /// <summary>
        /// Добавляет слово в бор.
        /// </summary>
        /// <param name="element">Слово для добавления.</param>
        /// <returns>True, если слово добавлено, иначе false.</returns>
        public bool Add(string element)
        {
            if (string.IsNullOrEmpty(element))
            {
                throw new ArgumentException("String cannot be null or empty", nameof(element));
            }

            var (endPrefixNode, nextPosition, pathStack) = this.FindLongestPrefix(element, false);

            if (nextPosition == element.Length)
            {
                if (!endPrefixNode.IsTerminal)
                {
                    endPrefixNode.IsTerminal = true;
                    return true;
                }

                return false;
            }

            endPrefixNode.Children[element[nextPosition]] = CreateSuffix(element.Substring(nextPosition));
            return true;
        }

        /// <summary>
        /// Удаляет слово из бора.
        /// </summary>
        /// <param name="element">Слово для удаления.</param>
        /// <returns>True, если такое слово присутствовало и было удалено, иначе false.</returns>
        public bool Remove(string element)
        {
            if (string.IsNullOrEmpty(element))
            {
                throw new ArgumentException("String cannot be null or empty", nameof(element));
            }

            var (endPrefixNode, nextPosition, pathStack) = this.FindLongestPrefix(element, true);

            if (nextPosition != element.Length || endPrefixNode.IsTerminal == false)
            {
                return false;
            }

            char previousSymbol = '\0';
            bool isPreviousLeaf = false;
            endPrefixNode.IsTerminal = false;

            while (pathStack!.Count > 0)
            {
                Node last = pathStack.Pop();

                if (isPreviousLeaf)
                {
                    last.Children.Remove(previousSymbol);
                    isPreviousLeaf = false;
                }

                if (last.Children.Count == 0 && !last.IsTerminal)
                {
                    isPreviousLeaf = true;
                    previousSymbol = last.Value;
                }
                else
                {
                    return true;
                }
            }

            if (isPreviousLeaf)
            {
                this.Root.Children.Remove(previousSymbol);
            }

            return true;
        }

        /// <summary>
        /// Проверяет, содержится ли слово в боре.
        /// </summary>
        /// <param name="element">Слово для поиска.</param>
        /// <returns>True, если слово найдено, иначе false.</returns>
        public bool Contains(string element)
        {
            var (endPrefixNode, nextPosition, pathStack) = this.FindLongestPrefix(element, false);

            return nextPosition == element.Length && endPrefixNode.IsTerminal == true;
        }

        /// <summary>
        /// Возвращает количество потомков у данного префикса.
        /// </summary>
        /// <param name="prefix">Префикс для поиска.</param>
        /// <returns>Количество потомков у данного префикса.</returns>
        public int HowManyStartsWithPrefix(string prefix)
        {
            var (endPrefixNode, nextPosition, pathStack) = this.FindLongestPrefix(prefix, false);

            return nextPosition == prefix.Length ? endPrefixNode.Children.Count : 0;
        }

        private static Node CreateSuffix(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("String cannot be null or empty", nameof(str));
            }

            Node source = new Node(str[0]);
            Node previous = source;

            for (int i = 1; i < str.Length; i++)
            {
                Node newNode = new Node(str[i]);
                previous.Children[str[i]] = newNode;
                previous = newNode;
            }

            previous.IsTerminal = true;
            return source;
        }

        private (Node endPrefixNode, int nextPosition, Stack<Node>? pathStack) FindLongestPrefix(string element, bool needPathStack)
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
                    if (needPathStack)
                    {
                        stack.Push(currentNode);
                    }

                    currentPosition++;
                }
                else
                {
                    return needPathStack ? (currentNode, currentPosition, stack) : (currentNode, currentPosition, null);
                }
            }

            return needPathStack ? (currentNode, currentPosition, stack) : (currentNode, currentPosition, null);
        }
    }
}
