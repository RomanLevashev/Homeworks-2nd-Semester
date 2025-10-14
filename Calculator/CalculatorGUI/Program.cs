// <copyright file="Program.cs" company="Roman Levashev">
// Copyright (c) Roman Levashev. All rights reserved.
// Licensed under the MIT License.
// </copyright>

namespace CalculatorGUI;

/// <summary>
/// The main entry point class for the calculator application.
/// This class contains the application entry point and startup configuration.
/// </summary>
internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new CalculatorForm());
    }
}