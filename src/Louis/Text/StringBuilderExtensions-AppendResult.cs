﻿// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Text;
using Louis.ArgumentValidation;

namespace Louis.Text;

partial class StringBuilderExtensions
{
    /// <summary>
    /// Appends the result of a method to the end of this builder.
    /// </summary>
    /// <param name="this">The <see cref="StringBuilder"/> on which this method is called.</param>
    /// <param name="provider">The method to call.</param>
    /// <returns>A reference to <paramref name="this"/> after the operation is completed.</returns>
    public static StringBuilder AppendResult(this StringBuilder @this, Func<string?> provider)
        => @this.Append(Validated.NotNull(provider)());

    /// <summary>
    /// Appends the result of a method to the end of this builder.
    /// </summary>
    /// <param name="this">The <see cref="StringBuilder"/> on which this method is called.</param>
    /// <param name="func">The method to call.</param>
    /// <returns>A reference to <paramref name="this"/> after the operation is completed.</returns>
    public static StringBuilder AppendResult(this StringBuilder @this, ReadOnlySpanFunc<char> func)
#if NETSTANDARD2_0 || NETFRAMEWORK
        => @this.Append(Validated.NotNull(func)().ToString());
#else
        => @this.Append(Validated.NotNull(func)());
#endif
}
