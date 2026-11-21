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
        /// of this instance. If the string is longer than the sum of <paramref name="headLength"/>,
        /// <paramref name="tailLength"/>, and the length of an ellipsis (three dots <c>...</c>), then it is clipped,
        /// taking only the first <paramref name="headLength"/> characters, followed by the ellipsis
        /// and the last <paramref name="tailLength"/> characters.
        /// </summary>
        /// <param name="str">The string to append.</param>
        /// <param name="headLength">The number of characters to leave before clipping.</param>
        /// <param name="tailLength">The number of characters to leave after clipping.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLength"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLength"/> is less than 0.</para>
        /// </exception>
        /// <remarks>
        /// <para>If <paramref name="str"/> is <see langword="null"/>, the string <c>null</c>
        /// (without surrounding quotes) will be appended to <paramref name="this"/>.</para>
        /// </remarks>
        public StringBuilder AppendClippedQuotedLiteral(string? str, int headLength, int tailLength)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsGreaterThanOrEqualTo(headLength, 0);
            Guard.IsGreaterThanOrEqualTo(tailLength, 0);

            return str is null
                    ? @this.Append(InternalConstants.QuotedNull)
                    : @this.UnsafeAppendClippedQuotedLiteral(str.AsSpan(), headLength, tailLength, false);
        }

        /// <summary>
        /// Appends the specified string, expressed as a C# quoted string literal, to the end
        /// of this instance. If the string is longer than the sum of <paramref name="headLength"/>,
        /// <paramref name="tailLength"/>, and the length of an ellipsis (1 if <paramref name="useUnicodeEllipsis"/>
        /// is <see langword="true"/>, 3 otherwise), then it is clipped, taking only the first <paramref name="headLength"/>
        /// characters, followed by an ellipsis (either the Unicode character <c>'\x2026'</c> (<c>…</c>), or three dots <c>...</c>)
        /// and the last <paramref name="tailLength"/> characters.
        /// </summary>
        /// <param name="str">The string to append.</param>
        /// <param name="headLength">The number of characters to leave before clipping.</param>
        /// <param name="tailLength">The number of characters to leave after clipping.</param>
        /// <param name="useUnicodeEllipsis">
        /// <see langword="true"/> to use a single Unicode character Ellipsis (<c>'\x2026'</c>, <c>…</c>) to replace the clipped part;
        /// <see langword="false"/> to use three dots (<c>...</c>) instead.
        /// </param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLength"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLength"/> is less than 0.</para>
        /// </exception>
        /// <remarks>
        /// <para>If <paramref name="str"/> is <see langword="null"/>, the string <c>null</c>
        /// (without surrounding quotes) will be appended to <paramref name="this"/>.</para>
        /// </remarks>
        public StringBuilder AppendClippedQuotedLiteral(
            string? str,
            int headLength,
            int tailLength,
            bool useUnicodeEllipsis)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsGreaterThanOrEqualTo(headLength, 0);
            Guard.IsGreaterThanOrEqualTo(tailLength, 0);

            return str is null
                    ? @this.Append(InternalConstants.QuotedNull)
                    : @this.UnsafeAppendClippedQuotedLiteral(str.AsSpan(), headLength, tailLength, useUnicodeEllipsis);
        }

        /// <summary>
        /// Appends the specified span of characters, expressed as a C# quoted string literal, to the end
        /// of this instance. If the span is longer than the sum of <paramref name="headLength"/>,
        /// <paramref name="tailLength"/>, and the length of an ellipsis (three dots <c>...</c>), then it is clipped,
        /// taking only the first <paramref name="headLength"/> characters, followed by the ellipsis
        /// and the last <paramref name="tailLength"/> characters.
        /// </summary>
        /// <param name="chars">The characters to append.</param>
        /// <param name="headLength">The number of characters to leave before clipping.</param>
        /// <param name="tailLength">The number of characters to leave after clipping.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLength"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLength"/> is less than 0.</para>
        /// </exception>
        public StringBuilder AppendClippedQuotedLiteral(ReadOnlySpan<char> chars, int headLength, int tailLength)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsGreaterThanOrEqualTo(headLength, 0);
            Guard.IsGreaterThanOrEqualTo(tailLength, 0);

            return @this.UnsafeAppendClippedQuotedLiteral(chars, headLength, tailLength, false);
        }

        /// <summary>
        /// Appends the specified span of characters, expressed as a C# quoted string literal, to the end
        /// of this instance. If the span is longer than the sum of <paramref name="headLength"/>,
        /// <paramref name="tailLength"/>, and the length of an ellipsis (1 if <paramref name="useUnicodeEllipsis"/>
        /// is <see langword="true"/>, 3 otherwise), then it is clipped, taking only the first <paramref name="headLength"/>
        /// characters, followed by an ellipsis (either the Unicode character <c>'\x2026'</c> (<c>…</c>), or three dots <c>...</c>)
        /// and the last <paramref name="tailLength"/> characters.
        /// </summary>
        /// <param name="chars">The characters to append.</param>
        /// <param name="headLength">The number of characters to leave before clipping.</param>
        /// <param name="tailLength">The number of characters to leave after clipping.</param>
        /// <param name="useUnicodeEllipsis">
        /// <see langword="true"/> to use a single Unicode character Ellipsis (<c>'\x2026'</c>, <c>…</c>) to replace the clipped part;
        /// <see langword="false"/> to use three dots (<c>...</c>) instead.
        /// </param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLength"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLength"/> is less than 0.</para>
        /// </exception>
        public StringBuilder AppendClippedQuotedLiteral(
            ReadOnlySpan<char> chars,
            int headLength,
            int tailLength,
            bool useUnicodeEllipsis)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsGreaterThanOrEqualTo(headLength, 0);
            Guard.IsGreaterThanOrEqualTo(tailLength, 0);

            return @this.UnsafeAppendClippedQuotedLiteral(chars, headLength, tailLength, useUnicodeEllipsis);
        }

        /// <summary>
        /// Appends the specified string, expressed as a C# verbatim string literal, to the end
        /// of this instance. If the string is longer than the sum of <paramref name="headLength"/>,
        /// <paramref name="tailLength"/>, and the length of an ellipsis (three dots <c>...</c>), then it is clipped,
        /// taking only the first <paramref name="headLength"/> characters, followed by the ellipsis
        /// and the last <paramref name="tailLength"/> characters.
        /// </summary>
        /// <param name="str">The string to append.</param>
        /// <param name="headLength">The number of characters to leave before clipping.</param>
        /// <param name="tailLength">The number of characters to leave after clipping.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLength"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLength"/> is less than 0.</para>
        /// </exception>
        /// <remarks>
        /// <para>If <paramref name="str"/> is <see langword="null"/>, the string <c>null</c>
        /// (without surrounding quotes) will be appended to <paramref name="this"/>.</para>
        /// </remarks>
        public StringBuilder AppendClippedVerbatimLiteral(string? str, int headLength, int tailLength)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsGreaterThanOrEqualTo(headLength, 0);
            Guard.IsGreaterThanOrEqualTo(tailLength, 0);

            return str is null
                    ? @this.Append(InternalConstants.QuotedNull)
                    : @this.UnsafeAppendClippedVerbatimLiteral(str.AsSpan(), headLength, tailLength, false);
        }

        /// <summary>
        /// Appends the specified string, expressed as a C# verbatim string literal, to the end
        /// of this instance. If the string is longer than the sum of <paramref name="headLength"/>,
        /// <paramref name="tailLength"/>, and the length of an ellipsis (1 if <paramref name="useUnicodeEllipsis"/>
        /// is <see langword="true"/>, 3 otherwise), then it is clipped, taking only the first <paramref name="headLength"/>
        /// characters, followed by an ellipsis (either the Unicode character <c>'\x2026'</c> (<c>…</c>), or three dots <c>...</c>)
        /// and the last <paramref name="tailLength"/> characters.
        /// </summary>
        /// <param name="str">The string to append.</param>
        /// <param name="headLength">The number of characters to leave before clipping.</param>
        /// <param name="tailLength">The number of characters to leave after clipping.</param>
        /// <param name="useUnicodeEllipsis">
        /// <see langword="true"/> to use a single Unicode character Ellipsis (<c>'\x2026'</c>, <c>…</c>) to replace the clipped part;
        /// <see langword="false"/> to use three dots (<c>...</c>) instead.
        /// </param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLength"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLength"/> is less than 0.</para>
        /// </exception>
        /// <remarks>
        /// <para>If <paramref name="str"/> is <see langword="null"/>, the string <c>null</c>
        /// (without surrounding quotes) will be appended to <paramref name="this"/>.</para>
        /// </remarks>
        public StringBuilder AppendClippedVerbatimLiteral(
            string? str,
            int headLength,
            int tailLength,
            bool useUnicodeEllipsis)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsGreaterThanOrEqualTo(headLength, 0);
            Guard.IsGreaterThanOrEqualTo(tailLength, 0);

            return str is null
                    ? @this.Append(InternalConstants.QuotedNull)
                    : @this.UnsafeAppendClippedVerbatimLiteral(str.AsSpan(), headLength, tailLength, useUnicodeEllipsis);
        }

        /// <summary>
        /// Appends the specified span of characters, expressed as a C# verbatim string literal, to the end
        /// of this instance. If the span is longer than the sum of <paramref name="headLength"/>,
        /// <paramref name="tailLength"/>, and the length of an ellipsis (three dots <c>...</c>), then it is clipped,
        /// taking only the first <paramref name="headLength"/> characters, followed by the ellipsis
        /// and the last <paramref name="tailLength"/> characters.
        /// </summary>
        /// <param name="chars">The characters to append.</param>
        /// <param name="headLength">The number of characters to leave before clipping.</param>
        /// <param name="tailLength">The number of characters to leave after clipping.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLength"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLength"/> is less than 0.</para>
        /// </exception>
        public StringBuilder AppendClippedVerbatimLiteral(ReadOnlySpan<char> chars, int headLength, int tailLength)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsGreaterThanOrEqualTo(headLength, 0);
            Guard.IsGreaterThanOrEqualTo(tailLength, 0);

            return @this.UnsafeAppendClippedVerbatimLiteral(chars, headLength, tailLength, false);
        }

        /// <summary>
        /// Appends the specified span of characters, expressed as a C# verbatim string literal, to the end
        /// of this instance. If the span is longer than the sum of <paramref name="headLength"/>,
        /// <paramref name="tailLength"/>, and the length of an ellipsis (1 if <paramref name="useUnicodeEllipsis"/>
        /// is <see langword="true"/>, 3 otherwise), then it is clipped, taking only the first <paramref name="headLength"/>
        /// characters, followed by an ellipsis (either the Unicode character <c>'\x2026'</c> (<c>…</c>), or three dots <c>...</c>)
        /// and the last <paramref name="tailLength"/> characters.
        /// </summary>
        /// <param name="chars">The characters to append.</param>
        /// <param name="headLength">The number of characters to leave before clipping.</param>
        /// <param name="tailLength">The number of characters to leave after clipping.</param>
        /// <param name="useUnicodeEllipsis">
        /// <see langword="true"/> to use a single Unicode character Ellipsis (<c>'\x2026'</c>, <c>…</c>) to replace the clipped part;
        /// <see langword="false"/> to use three dots (<c>...</c>) instead.
        /// </param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLength"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLength"/> is less than 0.</para>
        /// </exception>
        public StringBuilder AppendClippedVerbatimLiteral(
            ReadOnlySpan<char> chars,
            int headLength,
            int tailLength,
            bool useUnicodeEllipsis)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsGreaterThanOrEqualTo(headLength, 0);
            Guard.IsGreaterThanOrEqualTo(tailLength, 0);

            return @this.UnsafeAppendClippedVerbatimLiteral(chars, headLength, tailLength, useUnicodeEllipsis);
        }

        /// <summary>
        /// Appends the specified string, expressed as a C# string literal, to the end
        /// of this instance. If the string is longer than the sum of <paramref name="headLength"/>,
        /// <paramref name="tailLength"/>, and the length of an ellipsis (three dots <c>...</c>), then it is clipped,
        /// taking only the first <paramref name="headLength"/> characters, followed by the ellipsis
        /// and the last <paramref name="tailLength"/> characters.
        /// </summary>
        /// <param name="literalKind">A <see cref="StringLiteralKind"/> constant specifying the kind of string literal
        /// to append.</param>
        /// <param name="str">The string to append.</param>
        /// <param name="headLength">The number of characters to leave before clipping.</param>
        /// <param name="tailLength">The number of characters to leave after clipping.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="literalKind"/> is neither <see cref="StringLiteralKind.Quoted"/>
        /// nor <see cref="StringLiteralKind.Verbatim"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLength"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLength"/> is less than 0.</para>
        /// </exception>
        /// <remarks>
        /// <para>If <paramref name="str"/> is <see langword="null"/>, the string <c>null</c>
        /// (without surrounding quotes) will be appended to <paramref name="this"/>.</para>
        /// </remarks>
        public StringBuilder AppendClippedLiteral(
            StringLiteralKind literalKind,
            string? str,
            int headLength,
            int tailLength)
        {
            Guard.IsNotNull(@this, nameof(@this));
            InternalGuard.IsDefinedStringLiteralKind(literalKind);
            Guard.IsGreaterThanOrEqualTo(headLength, 0);
            Guard.IsGreaterThanOrEqualTo(tailLength, 0);

            return str is null
                ? @this.Append(InternalConstants.QuotedNull)
                : @this.UnsafeAppendClippedLiteral(literalKind, str.AsSpan(), headLength, tailLength, false);
        }

        /// <summary>
        /// Appends the specified string, expressed as a C# string literal, to the end
        /// of this instance. If the string is longer than the sum of <paramref name="headLength"/>,
        /// <paramref name="tailLength"/>, and the length of an ellipsis (1 if <paramref name="useUnicodeEllipsis"/>
        /// is <see langword="true"/>, 3 otherwise), then it is clipped, taking only the first <paramref name="headLength"/>
        /// characters, followed by an ellipsis (either the Unicode character <c>'\x2026'</c> (<c>…</c>), or three dots <c>...</c>)
        /// and the last <paramref name="tailLength"/> characters.
        /// </summary>
        /// <param name="literalKind">A <see cref="StringLiteralKind"/> constant specifying the kind of string literal
        /// to append.</param>
        /// <param name="str">The string to append.</param>
        /// <param name="headLength">The number of characters to leave before clipping.</param>
        /// <param name="tailLength">The number of characters to leave after clipping.</param>
        /// <param name="useUnicodeEllipsis">
        /// <see langword="true"/> to use a single Unicode character Ellipsis (<c>'\x2026'</c>, <c>…</c>) to replace the clipped part;
        /// <see langword="false"/> to use three dots (<c>...</c>) instead.
        /// </param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="literalKind"/> is neither <see cref="StringLiteralKind.Quoted"/>
        /// nor <see cref="StringLiteralKind.Verbatim"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLength"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLength"/> is less than 0.</para>
        /// </exception>
        /// <remarks>
        /// <para>If <paramref name="str"/> is <see langword="null"/>, the string <c>null</c>
        /// (without surrounding quotes) will be appended to <paramref name="this"/>.</para>
        /// </remarks>
        public StringBuilder AppendClippedLiteral(
            StringLiteralKind literalKind,
            string? str,
            int headLength,
            int tailLength,
            bool useUnicodeEllipsis)
        {
            Guard.IsNotNull(@this, nameof(@this));
            InternalGuard.IsDefinedStringLiteralKind(literalKind);
            Guard.IsGreaterThanOrEqualTo(headLength, 0);
            Guard.IsGreaterThanOrEqualTo(tailLength, 0);

            return str is null
                ? @this.Append(InternalConstants.QuotedNull)
                : @this.UnsafeAppendClippedLiteral(literalKind, str.AsSpan(), headLength, tailLength, useUnicodeEllipsis);
        }

        /// <summary>
        /// Appends the specified span of characters, expressed as a C# string literal, to the end
        /// of this instance. If the span is longer than the sum of <paramref name="headLength"/>,
        /// <paramref name="tailLength"/>, and the length of an ellipsis (three dots <c>...</c>), then it is clipped,
        /// taking only the first <paramref name="headLength"/> characters, followed by the ellipsis
        /// and the last <paramref name="tailLength"/> characters.
        /// </summary>
        /// <param name="literalKind">A <see cref="StringLiteralKind"/> constant specifying the kind of string literal
        /// to append.</param>
        /// <param name="chars">The characters to append.</param>
        /// <param name="headLength">The number of characters to leave before clipping.</param>
        /// <param name="tailLength">The number of characters to leave after clipping.</param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="literalKind"/> is neither <see cref="StringLiteralKind.Quoted"/>
        /// nor <see cref="StringLiteralKind.Verbatim"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLength"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLength"/> is less than 0.</para>
        /// </exception>
        public StringBuilder AppendClippedLiteral(
            StringLiteralKind literalKind,
            ReadOnlySpan<char> chars,
            int headLength,
            int tailLength)
        {
            Guard.IsNotNull(@this, nameof(@this));
            InternalGuard.IsDefinedStringLiteralKind(literalKind);
            Guard.IsGreaterThanOrEqualTo(headLength, 0);
            Guard.IsGreaterThanOrEqualTo(tailLength, 0);

            return @this.UnsafeAppendClippedLiteral(literalKind, chars, headLength, tailLength, false);
        }

        /// <summary>
        /// Appends the specified span of characters, expressed as a C# string literal, to the end
        /// of this instance. If the span is longer than the sum of <paramref name="headLength"/>,
        /// <paramref name="tailLength"/>, and the length of an ellipsis (1 if <paramref name="useUnicodeEllipsis"/>
        /// is <see langword="true"/>, 3 otherwise), then it is clipped, taking only the first <paramref name="headLength"/>
        /// characters, followed by an ellipsis (either the Unicode character <c>'\x2026'</c> (<c>…</c>), or three dots <c>...</c>)
        /// and the last <paramref name="tailLength"/> characters.
        /// </summary>
        /// <param name="literalKind">A <see cref="StringLiteralKind"/> constant specifying the kind of string literal
        /// to append.</param>
        /// <param name="chars">The characters to append.</param>
        /// <param name="headLength">The number of characters to leave before clipping.</param>
        /// <param name="tailLength">The number of characters to leave after clipping.</param>
        /// <param name="useUnicodeEllipsis">
        /// <see langword="true"/> to use a single Unicode character Ellipsis (<c>'\x2026'</c>, <c>…</c>) to replace the clipped part;
        /// <see langword="false"/> to use three dots (<c>...</c>) instead.
        /// </param>
        /// <returns>A reference to this instance after the append operation has completed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="literalKind"/> is neither <see cref="StringLiteralKind.Quoted"/>
        /// nor <see cref="StringLiteralKind.Verbatim"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLength"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLength"/> is less than 0.</para>
        /// </exception>
        public StringBuilder AppendClippedLiteral(
            StringLiteralKind literalKind,
            ReadOnlySpan<char> chars,
            int headLength,
            int tailLength,
            bool useUnicodeEllipsis)
        {
            Guard.IsNotNull(@this, nameof(@this));
            InternalGuard.IsDefinedStringLiteralKind(literalKind);
            Guard.IsGreaterThanOrEqualTo(headLength, 0);
            Guard.IsGreaterThanOrEqualTo(tailLength, 0);

            return @this.UnsafeAppendClippedLiteral(literalKind, chars, headLength, tailLength, useUnicodeEllipsis);
        }

        internal StringBuilder UnsafeAppendClippedQuotedLiteral(
            ReadOnlySpan<char> chars,
            int headLength,
            int tailLength,
            bool useUnicodeEllipsis)
        {
            var unsignedLength = (uint)chars.Length;
            var unclippedLength = (uint)headLength + (uint)tailLength;
            if (unclippedLength >= int.MaxValue)
            {
                return @this.UnsafeAppendQuotedLiteral(chars);
            }

            var ellipsisLength = useUnicodeEllipsis ? 1U : 3U;
            if (unsignedLength <= unclippedLength + ellipsisLength)
            {
                return @this.UnsafeAppendQuotedLiteral(chars);
            }

            var tailStart = chars.Length - tailLength;
            @this.EnsureCapacity(@this.Length + 2 + headLength + (int)ellipsisLength + tailLength);
            return @this.Append('"')
                        .AppendQuotedLiteralCore(chars[0..headLength])
                        .AppendEllipsis(useUnicodeEllipsis)
                        .AppendQuotedLiteralCore(chars[tailStart..])
                        .Append('"');
        }

        internal StringBuilder UnsafeAppendClippedVerbatimLiteral(
            ReadOnlySpan<char> chars,
            int headLength,
            int tailLength,
            bool useUnicodeEllipsis)
        {
            var unsignedLength = (uint)chars.Length;
            var unclippedLength = (uint)headLength + (uint)tailLength;
            if (unclippedLength >= int.MaxValue)
            {
                return @this.UnsafeAppendVerbatimLiteral(chars);
            }

            var ellipsisLength = useUnicodeEllipsis ? 1U : 3U;
            if (unsignedLength <= unclippedLength + ellipsisLength)
            {
                return @this.UnsafeAppendVerbatimLiteral(chars);
            }

            var tailStart = chars.Length - tailLength;
            @this.EnsureCapacity(@this.Length + 3 + headLength + (int)ellipsisLength + tailLength);
            return @this.Append(@"@""")
                        .AppendVerbatimLiteralCore(chars[0..headLength])
                        .AppendEllipsis(useUnicodeEllipsis)
                        .AppendVerbatimLiteralCore(chars[tailStart..])
                        .Append('"');
        }

        internal StringBuilder UnsafeAppendClippedLiteral(
            StringLiteralKind literalKind,
            ReadOnlySpan<char> chars,
            int headLength,
            int tailLength,
            bool useUnicodeEllipsis)
            => literalKind switch {
                StringLiteralKind.Quoted => @this.UnsafeAppendClippedQuotedLiteral(chars, headLength, tailLength, useUnicodeEllipsis),
                StringLiteralKind.Verbatim => @this.UnsafeAppendClippedVerbatimLiteral(chars, headLength, tailLength, useUnicodeEllipsis),
                _ => @this,
            };

        private StringBuilder AppendEllipsis(bool useUnicodeEllipsis)
            => useUnicodeEllipsis ? @this.Append('\x2026') : @this.Append("...");
    }
}
