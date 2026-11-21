// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using Louis.Text.Internal;

namespace Louis.Text;

partial class CharReadOnlySpanExtensions
{
    /// <param name="this">The <see cref="ReadOnlySpan{T}">ReadOnlySpan&lt;char&gt;</see>
    /// on which this method is called.</param>
    extension(ReadOnlySpan<char> @this)
    {
        /// <summary>
        /// Computes the length of the <see href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#quoted-string-literals">quoted literal</see>
        /// representation of a span of characters.
        /// </summary>
        /// <returns>The length of the string that would result from calling
        /// <see cref="ToQuotedLiteral"/> on <paramref name="this"/>.</returns>
        public int ComputeQuotedLiteralLength()
        {
            var result = 2;
            foreach (var c in @this)
            {
                result += c switch {
                    '\x00' or '\x07' or '\x08' or '\x09' or '\x0A' or '\x0B' or '\x0C' or '\x0D' or '"' or '\\' => 2,
                    '\x7F' or < '\x20' => 6,
                    < '\x7F' => 1,
                    _ => char.GetUnicodeCategory(c) switch {
                        UnicodeCategory.UppercaseLetter
                            or UnicodeCategory.LowercaseLetter
                            or UnicodeCategory.TitlecaseLetter
                            or UnicodeCategory.ModifierLetter
                            or UnicodeCategory.OtherLetter
                            or UnicodeCategory.DecimalDigitNumber
                            or UnicodeCategory.LetterNumber
                            or UnicodeCategory.OtherNumber
                            or UnicodeCategory.DashPunctuation
                            or UnicodeCategory.OpenPunctuation
                            or UnicodeCategory.ClosePunctuation
                            or UnicodeCategory.InitialQuotePunctuation
                            or UnicodeCategory.FinalQuotePunctuation
                            or UnicodeCategory.OtherPunctuation
                            or UnicodeCategory.MathSymbol
                            or UnicodeCategory.CurrencySymbol
                            or UnicodeCategory.OtherSymbol => 1,
                        _ => 6,
                    },
                };
            }

            return result;
        }

        /// <summary>
        /// Computes the length of the <see href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#verbatim-string-literals">verbatim literal</see>
        /// representation of a span of characters.
        /// </summary>
        /// <returns>The length of the string that would result from calling
        /// <see cref="ToVerbatimLiteral"/> on <paramref name="this"/>.</returns>
        public int ComputeVerbatimLiteralLength()
        {
            var result = 3 + @this.Length;
            foreach (var c in @this)
            {
                if (c == '"')
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Computes the length of the representation of a span of characters as a string literal.
        /// </summary>
        /// <param name="literalKind">A <see cref="StringLiteralKind"/> constant specifying
        /// the kind of string literal that would be built.</param>
        /// <returns>The length of the string that would result from calling
        /// <see cref="ToLiteral"/> on <paramref name="this"/>
        /// with <paramref name="literalKind"/> as last parameter.</returns>
        /// <exception cref="ArgumentException"><paramref name="literalKind"/> is neither <see cref="StringLiteralKind.Quoted"/>
        /// nor <see cref="StringLiteralKind.Verbatim"/>.</exception>
        public int ComputeLiteralLength(StringLiteralKind literalKind)
        {
            InternalGuard.IsDefinedStringLiteralKind(literalKind);

            return literalKind switch {
                StringLiteralKind.Quoted => @this.ComputeQuotedLiteralLength(),
                StringLiteralKind.Verbatim => @this.ComputeVerbatimLiteralLength(),
                _ => 0,
            };
        }
    }
}
