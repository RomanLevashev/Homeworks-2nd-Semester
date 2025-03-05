namespace BurrowsWheelerTransform;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Класс, реализующий преобразование Барроуза-Уилера.
/// </summary>
public static class BurrowsWheeller
{
    /// <summary>
    /// Выполняет преобразование Барроуза-Уилера над входной строкой.
    /// </summary>
    /// <param name="input">Входная строка.</param>
    /// <returns>Кортеж, содержащий преобразованную строку и позицию исходной строки (Индексация с нуля).</returns>
    public static (string transformed, int position) Transform(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentNullException("input");
        }

        int length = input.Length;
        var rotations = new string[length];

        for (int i = 0; i < length; i++)
        {
            rotations[i] = input.Substring(length - i, i) + input.Substring(0, length - i);
        }

        Array.Sort(rotations);
        StringBuilder resultBuilder = new();
        int position = 0;

        for (int i = 0; i < length; i++)
        {
            if (rotations[i] == input)
            {
                position = i;
            }

            resultBuilder.Append(rotations[i][length - 1]);
        }

        return (resultBuilder.ToString(), position);
    }

    /// <summary>
    /// Выполняет обратное преобразование Барроуза-Уилера.
    /// </summary>
    /// <param name="transformed">Преобразованная строка.</param>
    /// <param name="position">Позиция оригинальной строки в отсортированной таблице сдвигов (индексация с нуля).</param>
    /// <returns>Исходная строка.</returns>
    public static string InverseTransform(string transformed, int position)
    {
        if (string.IsNullOrEmpty(transformed))
        {
            throw new ArgumentNullException("input");
        }

        int[] countBefore = new int[transformed.Length];
        Dictionary<char, int> charCurrentCount = [];

        for (int i = 0; i < transformed.Length; ++i)
        {
            char current = transformed[i];
            int count = 0;
            if (charCurrentCount.ContainsKey(current))
            {
                count = charCurrentCount[current]++;
            }
            else
            {
                charCurrentCount[current] = 1;
            }

            countBefore[i] = count;
        }

        HashSet<char> uniqueChars = new (transformed);
        Dictionary<char, int> countSmaller = [];

        foreach (char current in uniqueChars)
        {
            countSmaller[current] = 0;
            foreach (char other in uniqueChars)
            {
                if (current.CompareTo(other) > 0)
                {
                    countSmaller[current] += charCurrentCount[other];
                }
            }
        }

        StringBuilder stringBuilder = new();
        stringBuilder.Append(transformed[position]);
        int previousPosition = position;

        for (int i = transformed.Length - 2; i >= 0; i--)
        {
            char previous = transformed[previousPosition];
            int currentCharPosition = countSmaller[previous] + countBefore[previousPosition];
            stringBuilder.Insert(0, transformed[currentCharPosition]);
            previousPosition = currentCharPosition;
        }

        return stringBuilder.ToString();
    }
}