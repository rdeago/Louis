// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Text;
using CommunityToolkit.Diagnostics;
using Louis.Text.Internal;

namespace Louis.Text;

partial class StringBuilderExtensions
{
    /// <param name="this">The <see cref="StringBuilder"/> on which this method is called.</param>
    extension(StringBuilder @this)
    {
        /// <summary>
        /// Appends the specified string, expressed as a C# quoted string literal, to the end
        /// of this instance.
        /// </summary>
        /// <param name="str">The string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// <para>If <paramref name="str"/> is <see langword="null"/>, the string <c>null</c>
        /// (without surrounding quotes) will be appended to <paramref name="this"/>.</para>
        /// </remarks>
        /// <seealso cref="AppendVerbatimLiteral(StringBuilder,string?)"/>
        /// <seealso cref="AppendLiteral(StringBuilder,StringLiteralKind,string?)"/>
        public StringBuilder AppendQuotedLiteral(string? str)
        {
            Guard.IsNotNull(@this, nameof(@this));

            return str is null
                ? @this.Append(InternalConstants.QuotedNull)
                : @this.UnsafeAppendQuotedLiteral(str.AsSpan());
        }

        /// <summary>
        /// Appends the specified span of characters, expressed as a C# quoted string literal,
        /// to the end of this instance.
        /// </summary>
        /// <param name="chars">The characters to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <seealso cref="AppendVerbatimLiteral(StringBuilder,ReadOnlySpan{char})"/>
        /// <seealso cref="AppendLiteral(StringBuilder,StringLiteralKind,ReadOnlySpan{char})"/>
        public StringBuilder AppendQuotedLiteral(ReadOnlySpan<char> chars)
        {
            Guard.IsNotNull(@this, nameof(@this));

            return @this.UnsafeAppendQuotedLiteral(chars);
        }

        /// <summary>
        /// Appends the specified string, expressed as a C# verbatim string literal, to the end
        /// of this instance.
        /// </summary>
        /// <param name="str">The string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// <para>If <paramref name="str"/> is <see langword="null"/>, the string <c>null</c>
        /// (without surrounding quotes) will be appended to <paramref name="this"/>.</para>
        /// </remarks>
        /// <seealso cref="AppendQuotedLiteral(StringBuilder,string?)"/>
        /// <seealso cref="AppendLiteral(StringBuilder,StringLiteralKind,string?)"/>
        public StringBuilder AppendVerbatimLiteral(string? str)
        {
            Guard.IsNotNull(@this, nameof(@this));

            return str is null
                ? @this.Append(InternalConstants.QuotedNull)
                : @this.UnsafeAppendVerbatimLiteral(str.AsSpan());
        }

        /// <summary>
        /// Appends the specified span of characters, expressed as a C# verbatim string literal,
        /// to the end of this instance.
        /// </summary>
        /// <param name="chars">The characters to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <seealso cref="AppendQuotedLiteral(StringBuilder,ReadOnlySpan{char})"/>
        /// <seealso cref="AppendLiteral(StringBuilder,StringLiteralKind,ReadOnlySpan{char})"/>
        public StringBuilder AppendVerbatimLiteral(ReadOnlySpan<char> chars)
        {
            Guard.IsNotNull(@this, nameof(@this));

            return @this.UnsafeAppendVerbatimLiteral(chars);
        }

        /// <summary>
        /// Appends the specified string, expressed as a C# string literal, to the end of this instance.
        /// </summary>
        /// <param name="literalKind">A <see cref="StringLiteralKind"/> constant specifying the kind of string literal
        /// to append.</param>
        /// <param name="str">The string to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="literalKind"/> is neither <see cref="StringLiteralKind.Quoted"/>
        /// nor <see cref="StringLiteralKind.Verbatim"/>.</exception>
        /// <remarks>
        /// <para>If <paramref name="str"/> is <see langword="null"/>, the string <c>null</c>
        /// (without surrounding quotes) will be appended to <paramref name="this"/>.</para>
        /// </remarks>
        /// <seealso cref="AppendQuotedLiteral(StringBuilder,string?)"/>
        /// <seealso cref="AppendVerbatimLiteral(StringBuilder,string?)"/>
        /// <seealso cref="StringLiteralKind"/>
        public StringBuilder AppendLiteral(StringLiteralKind literalKind, string? str)
        {
            Guard.IsNotNull(@this, nameof(@this));
            InternalGuard.IsDefinedStringLiteralKind(literalKind);

            return str is null
                ? @this.Append(InternalConstants.QuotedNull)
                : @this.UnsafeAppendLiteral(literalKind, str.AsSpan());
        }

        /// <summary>
        /// Appends the specified span of characters, expressed as a C# string literal, to the end of this instance.
        /// </summary>
        /// <param name="literalKind">A <see cref="StringLiteralKind"/> constant specifying the kind of string literal
        /// to append.</param>
        /// <param name="chars">The characters to append.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="literalKind"/> is neither <see cref="StringLiteralKind.Quoted"/>
        /// nor <see cref="StringLiteralKind.Verbatim"/>.</exception>
        /// <seealso cref="AppendQuotedLiteral(StringBuilder,ReadOnlySpan{char})"/>
        /// <seealso cref="AppendVerbatimLiteral(StringBuilder,ReadOnlySpan{char})"/>
        /// <seealso cref="StringLiteralKind"/>
        public StringBuilder AppendLiteral(StringLiteralKind literalKind, ReadOnlySpan<char> chars)
        {
            Guard.IsNotNull(@this, nameof(@this));
            InternalGuard.IsDefinedStringLiteralKind(literalKind);

            return @this.UnsafeAppendLiteral(literalKind, chars);
        }

        internal StringBuilder UnsafeAppendQuotedLiteral(ReadOnlySpan<char> chars)
        {
            @this.EnsureCapacity(@this.Length + 2 + chars.Length);
            return @this.Append('"')
                        .AppendQuotedLiteralCore(chars)
                        .Append('"');
        }

        internal StringBuilder UnsafeAppendVerbatimLiteral(ReadOnlySpan<char> chars)
        {
            @this.EnsureCapacity(@this.Length + 3 + chars.Length);
            return @this.Append(@"@""")
                        .AppendVerbatimLiteralCore(chars)
                        .Append('"');
        }

        internal StringBuilder UnsafeAppendLiteral(StringLiteralKind literalKind, ReadOnlySpan<char> chars)
            => literalKind switch {
                StringLiteralKind.Quoted => @this.UnsafeAppendQuotedLiteral(chars),
                StringLiteralKind.Verbatim => @this.UnsafeAppendVerbatimLiteral(chars),
                _ => @this,
            };
    }
}
