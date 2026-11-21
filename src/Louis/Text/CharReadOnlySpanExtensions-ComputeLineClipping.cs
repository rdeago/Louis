// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using CommunityToolkit.Diagnostics;

namespace Louis.Text;

partial class CharReadOnlySpanExtensions
{
    /// <param name="this">The <see cref="ReadOnlySpan{T}">ReadOnlySpan&lt;char&gt;</see>
    /// on which this method is called.</param>
    extension(ReadOnlySpan<char> @this)
    {
        /// <summary>
        /// Finds the first and the last lines of a span of characters, and counts the lines between them.
        /// </summary>
        /// <param name="headLines">The number of lines to keep at the start of the span, when the span is clipped.</param>
        /// <param name="tailLines">The number of lines to keep at the end of the span, when the span is clipped.</param>
        /// <returns>A <see cref="LineClipping"/> value, whose ranges refer to <paramref name="this"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLines"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLines"/> is less than 0.</para>
        /// </exception>
        /// <remarks>
        /// <para>A line ends at a line feed (<c>\n</c>), at a carriage return (<c>\r</c>), at a CR-LF pair
        /// (<c>\r\n</c>), or at the end of the span, as in <see cref="System.IO.TextReader.ReadLine"/>.</para>
        /// <para>A line terminator at the end of the span does not start a new line.</para>
        /// <para>A blank line is a line that is empty, or holds white-space characters alone,
        /// as <see cref="char.IsWhiteSpace(char)"/> defines them.
        /// This method ignores the blank lines at the start and at the end of the span,
        /// and the rest of these remarks does not count them among the lines of the span.
        /// A blank line between two other lines is not ignored.</para>
        /// <para>An empty span and a span of blank lines alone have no lines.</para>
        /// <para>A span of more than (<paramref name="headLines"/> + <paramref name="tailLines"/> + 1) lines is clipped:
        /// <see cref="LineClipping.Head"/> holds its first <paramref name="headLines"/> lines,
        /// and <see cref="LineClipping.Tail"/> its last <paramref name="tailLines"/> lines.
        /// The reason for the +1 above is that a clipped text is usually displayed with a placeholder line
        /// (e.g., <c>"--- more... ---"</c>) between head and tail.</para>
        /// <para>When a span is not clipped, <see cref="LineClipping.Head"/> holds all of its lines,
        /// and <see cref="LineClipping.Tail"/> is empty.</para>
        /// <para>This method copies no characters and allocates no memory.</para>
        /// </remarks>
        /// <seealso cref="StringExtensions.ComputeLineClipping"/>
        public LineClipping ComputeLineClipping(int headLines, int tailLines)
        {
            Guard.IsGreaterThanOrEqualTo(headLines, 0);
            Guard.IsGreaterThanOrEqualTo(tailLines, 0);

            var start = FindStartOfFirstNonBlankLine(@this);
            if (start < 0)
            {
                return default;
            }

            var end = FindEndOfLastNonBlankLine(@this);
            var headEnd = start;
            var headLineCount = 0;
            while (headLineCount < headLines && headEnd < end)
            {
                headEnd += FindEndOfFirstLine(@this[headEnd..end]);
                headLineCount++;
            }

            var tailStart = end;
            var tailLineCount = 0;
            while (tailLineCount < tailLines && tailStart > headEnd)
            {
                tailStart = headEnd + FindStartOfLastLine(@this[headEnd..tailStart]);
                tailLineCount++;
            }

            var omittedLines = @this[headEnd..tailStart];
            var omittedLineCount = 0;
            while (!omittedLines.IsEmpty)
            {
                omittedLines = omittedLines[FindEndOfFirstLine(omittedLines)..];
                omittedLineCount++;
            }

            // A caller shows one line in place of the omitted lines, so omitting a single line gains nothing.
            if (omittedLineCount <= 1)
            {
                var lineCount = headLineCount + omittedLineCount + tailLineCount;
                return new(new(start, end), new(end, end), lineCount, 0, 0);
            }

            return new(new(start, headEnd), new(tailStart, end), headLineCount, tailLineCount, omittedLineCount);
        }
    }
}
