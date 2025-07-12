// <copyright file="Options.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace BurrowsWheelerTransform
{
    /// <summary>
    /// Перечисление с возможными операциями для преобразования строки.
    /// </summary>
    public enum Options
    {
        /// <summary>
        /// Выйти из программы.
        /// </summary>
        Exit,

        /// <summary>
        /// Операция преобразования строки.
        /// </summary>
        Transform,

        /// <summary>
        /// Операция восстановления строки после преобразования.
        /// </summary>
        Invert,
    }
}
