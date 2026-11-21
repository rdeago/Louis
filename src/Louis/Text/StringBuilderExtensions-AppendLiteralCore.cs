// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Text;

namespace Louis.Text;

partial class StringBuilderExtensions
{
    /// <param name="this">The <see cref="StringBuilder"/> on which this method is called.</param>
    extension(StringBuilder @this)
    {
        private StringBuilder AppendQuotedLiteralCore(ReadOnlySpan<char> chars)
        {
            foreach (var c in chars)
            {
                _ = @this.AppendQuoteChar(c);
            }

            return @this;
        }

        private StringBuilder AppendVerbatimLiteralCore(ReadOnlySpan<char> chars)
        {
            foreach (var c in chars)
            {
                _ = c == '"' ? @this.Append("\"\"") : @this.Append(c);
            }

            return @this;
        }

        private StringBuilder AppendQuoteChar(char c)
            => c switch {
                '\x00' => @this.Append(@"\0"),
                '\x07' => @this.Append(@"\a"),
                '\x08' => @this.Append(@"\b"),
                '\x09' => @this.Append(@"\t"),
                '\x0A' => @this.Append(@"\n"),
                '\x0B' => @this.Append(@"\v"),
                '\x0C' => @this.Append(@"\f"),
                '\x0D' => @this.Append(@"\r"),
                '"' => @this.Append(@"\"""),
                '\\' => @this.Append(@"\\"),

                // A \x escape takes up to four hex digits, so a hex digit after it would change the character.
                // A \u escape always takes four.
                '\x7F' or < '\x20' => @this.AppendUnicodeEscape(c),
                < '\x7F' => @this.Append(c),
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
                        or UnicodeCategory.OtherSymbol => @this.Append(c),
                    _ => @this.AppendUnicodeEscape(c),
                },
            };

        private StringBuilder AppendUnicodeEscape(char c)
        {
            var n = (int)c;
            return @this.Append(@"\u")
                        .Append(HexDigits[(n >> 12) & 0xF])
                        .Append(HexDigits[(n >> 8) & 0xF])
                        .Append(HexDigits[(n >> 4) & 0xF])
                        .Append(HexDigits[n & 0xF]);
        }
    }
}
