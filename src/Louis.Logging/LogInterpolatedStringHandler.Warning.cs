﻿// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace Louis.Logging;

#pragma warning disable CA1034 // Nested types should not be visible
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

partial struct LogInterpolatedStringHandler
{
    [InterpolatedStringHandler]
    public ref struct Warning
    {
        private LogInterpolatedStringHandler _handler;

        public Warning(int literalLength, int formattedCount, ILogger @this, out bool isEnabled)
        {
            _handler = new(literalLength, formattedCount, @this, LogLevel.Critical, out isEnabled);
        }

        internal bool IsEnabled => _handler.IsEnabled;

        public void AppendLiteral(string s) => _handler.AppendLiteral(s);

#pragma warning disable RS0026 // Do not add multiple public overloads with optional parameters - We need CallerArgumentExpression here
        public void AppendFormatted(object? value, int alignment = 0, string? format = null, [CallerArgumentExpression("value")] string name = "")
#pragma warning restore RS0026 // Do not add multiple public overloads with optional parameters
            => _handler.AppendFormatted(value, alignment, format, name);

#pragma warning disable RS0026 // Do not add multiple public overloads with optional parameters - We need CallerArgumentExpression here
        public void AppendFormatted<T>(T? value, int alignment = 0, string? format = null, [CallerArgumentExpression("value")] string name = "")
#pragma warning restore RS0026 // Do not add multiple public overloads with optional parameters
            where T : struct
            => _handler.AppendFormatted(value, alignment, format, name);

        internal (string Template, object?[] Arguments) GetDataAndDispose() => _handler.GetDataAndClear();
    }
}
