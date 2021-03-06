﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace Microsoft.DotNet.Cli.Build.Framework
{
    // Stupid-simple console manager
    internal class Reporter
    {
        private static readonly Reporter Null = new Reporter(console: null);
        private static object _lock = new object();

        private readonly AnsiConsole _console;

        private Reporter(AnsiConsole console)
        {
            _console = console;
        }

        public static Reporter Output { get; } = Create(AnsiConsole.GetOutput);
        public static Reporter Error { get; } = Create(AnsiConsole.GetOutput);
        public static Reporter Verbose { get; } = Create(AnsiConsole.GetOutput);

        public static Reporter Create(Func<bool, AnsiConsole> getter)
        {
            var stripColors = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                string.Equals(Environment.GetEnvironmentVariable("NO_COLOR"), "1");
            return new Reporter(getter(stripColors));
        }

        public void WriteLine(string message)
        {
            lock (_lock)
            {
                _console?.WriteLine(message);
            }
        }

        public void WriteLine()
        {
            lock (_lock)
            {
                _console?.Writer?.WriteLine();
            }
        }

        public void Write(string message)
        {
            lock (_lock)
            {
                _console?.Writer?.Write(message);
            }
        }
        
        public void WriteBanner(string content)
        {
            string border = new string('*', content.Length + 6);
            WriteLine($@"{border}
*  {content}  *
{border}".Green());
        }
    }
}
