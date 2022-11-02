﻿// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Text;
using Louis.Diagnostics;
using Louis.Text.Internal;

namespace Louis.Text;

partial class StringBuilderExtensions
{
    /// <summary>
    /// Appends the specified string, expressed as a C# quoted string literal, to the end
    /// of this instance.
    /// </summary>
    /// <param name="this">The <see cref="StringBuilder"/> on which this method is called.</param>
    /// <param name="str">The string to append.</param>
    /// <returns>A reference to this instance after the append operation has completed..</returns>
    /// <remarks>
    /// <para>If <paramref name="str"/> is <see langword="null"/>, the string <c>null</c>
    /// (without surrounding quotes) will be appended to <paramref name="this"/>.</para>
    /// </remarks>
    /// <seealso cref="AppendVerbatimLiteral(StringBuilder,string?)"/>
    /// <seealso cref="AppendLiteral(StringBuilder,StringLiteralKind,string?)"/>
    public static StringBuilder AppendQuotedLiteral(this StringBuilder @this, string? str)
        => str is null ? @this.Append(InternalConstants.QuotedNull) : AppendQuotedLiteral(@this, str.AsSpan());

    /// <summary>
    /// Appends the specified span of characters, expressed as a C# quoted string literal,
    /// to the end of this instance.
    /// </summary>
    /// <param name="this">The <see cref="StringBuilder"/> on which this method is called.</param>
    /// <param name="chars">The characters to append.</param>
    /// <returns>A reference to this instance after the append operation has completed..</returns>
    /// <seealso cref="AppendVerbatimLiteral(StringBuilder,ReadOnlySpan{char})"/>
    /// <seealso cref="AppendLiteral(StringBuilder,StringLiteralKind,ReadOnlySpan{char})"/>
    public static StringBuilder AppendQuotedLiteral(this StringBuilder @this, ReadOnlySpan<char> chars)
        => @this.Append('"')
                .AppendQuotedLiteralCore(chars)
                .Append('"');

    /// <summary>
    /// Appends the specified string, expressed as a C# verbatim string literal, to the end
    /// of this instance.
    /// </summary>
    /// <param name="this">The <see cref="StringBuilder"/> on which this method is called.</param>
    /// <param name="str">The string to append.</param>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    /// <remarks>
    /// <para>If <paramref name="str"/> is <see langword="null"/>, the string <c>null</c>
    /// (without surrounding quotes) will be appended to <paramref name="this"/>.</para>
    /// </remarks>
    /// <seealso cref="AppendQuotedLiteral(StringBuilder,string?)"/>
    /// <seealso cref="AppendLiteral(StringBuilder,StringLiteralKind,string?)"/>
    public static StringBuilder AppendVerbatimLiteral(this StringBuilder @this, string? str)
        => str is null ? @this.Append(InternalConstants.QuotedNull) : AppendVerbatimLiteral(@this, str.AsSpan());

    /// <summary>
    /// Appends the specified span of characters, expressed as a C# verbatim string literal,
    /// to the end of this instance.
    /// </summary>
    /// <param name="this">The <see cref="StringBuilder"/> on which this method is called.</param>
    /// <param name="chars">The characters to append.</param>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    /// <seealso cref="AppendQuotedLiteral(StringBuilder,ReadOnlySpan{char})"/>
    /// <seealso cref="AppendLiteral(StringBuilder,StringLiteralKind,ReadOnlySpan{char})"/>
    public static StringBuilder AppendVerbatimLiteral(this StringBuilder @this, ReadOnlySpan<char> chars)
        => @this.Append(@"@""")
                .AppendVerbatimLiteralCore(chars)
                .Append('"');

    /// <summary>
    /// Appends the specified string, expressed as a C# string literal, to the end of this instance.
    /// </summary>
    /// <param name="this">The <see cref="StringBuilder"/> on which this method is called.</param>
    /// <param name="literalKind">A <see cref="StringLiteralKind"/> constant specifying the kind of string literal
    /// to append.</param>
    /// <param name="str">The string to append.</param>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    /// <remarks>
    /// <para>If <paramref name="str"/> is <see langword="null"/>, the string <c>null</c>
    /// (without surrounding quotes) will be appended to <paramref name="this"/>.</para>
    /// </remarks>
    /// <seealso cref="AppendQuotedLiteral(System.Text.StringBuilder,string?)"/>
    /// <seealso cref="AppendVerbatimLiteral(System.Text.StringBuilder,string?)"/>
    /// <seealso cref="StringLiteralKind"/>
    public static StringBuilder AppendLiteral(this StringBuilder @this, StringLiteralKind literalKind, string? str)
        => str is null ? @this.Append(InternalConstants.QuotedNull) : AppendLiteral(@this, literalKind, str.AsSpan());

    /// <summary>
    /// Appends the specified span of characters, expressed as a C# string literal, to the end of this instance.
    /// </summary>
    /// <param name="this">The <see cref="StringBuilder"/> on which this method is called.</param>
    /// <param name="literalKind">A <see cref="StringLiteralKind"/> constant specifying the kind of string literal
    /// to append.</param>
    /// <param name="chars">The characters to append.</param>
    /// <returns>A reference to this instance after the append operation has completed.</returns>
    /// <seealso cref="AppendQuotedLiteral(System.Text.StringBuilder,System.ReadOnlySpan{char})"/>
    /// <seealso cref="AppendVerbatimLiteral(System.Text.StringBuilder,System.ReadOnlySpan{char})"/>
    /// <seealso cref="StringLiteralKind"/>
    public static StringBuilder AppendLiteral(this StringBuilder @this, StringLiteralKind literalKind, ReadOnlySpan<char> chars)
        => literalKind switch {
            StringLiteralKind.Quoted => AppendQuotedLiteral(@this, chars),
            StringLiteralKind.Verbatim => AppendVerbatimLiteral(@this, chars),
            _ => Throw.Argument<StringBuilder>($"{literalKind} is not a valid {nameof(StringLiteralKind)}.", nameof(literalKind)),
        };
}
