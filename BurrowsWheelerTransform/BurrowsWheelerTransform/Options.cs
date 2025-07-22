// <copyright file="Options.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace BurrowsWheelerTransform
{
    /// <summary>
    /// Enumeration of possible operations for string transformation.
    /// </summary>
    public enum Options
    {
        /// <summary>
        /// Exit the program.
        /// </summary>
        Exit,

        /// <summary>
        /// String transformation operation.
        /// </summary>
        Transform,

        /// <summary>
        /// String inversion operation (reversing the transformation).
        /// </summary>
        Invert,
    }
}
