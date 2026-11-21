// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;

namespace Louis.Text;

partial class StringExtensions
{
    /// <param name="this">The <see langword="string"/> on which this method is called.</param>
    extension(string? @this)
    {
        /// <summary>
        /// Finds the first and the last lines of a string, and counts the lines between them.
        /// </summary>
        /// <param name="headLines">The number of lines to keep at the start of the string, when the string is clipped.</param>
        /// <param name="tailLines">The number of lines to keep at the end of the string, when the string is clipped.</param>
        /// <returns>A <see cref="LineClipping"/> value, whose ranges refer to <paramref name="this"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <para><paramref name="headLines"/> is less than 0.</para>
        /// <para>- or -</para>
        /// <para><paramref name="tailLines"/> is less than 0.</para>
        /// </exception>
        /// <remarks>
        /// <para>A line ends at a line feed (<c>\n</c>), at a carriage return (<c>\r</c>), at a CR-LF pair
        /// (<c>\r\n</c>), or at the end of the string, as in <see cref="System.IO.TextReader.ReadLine"/>.</para>
        /// <para>A line terminator at the end of the string does not start a new line.</para>
        /// <para><see langword="null"/> and the empty string have no lines.</para>
        /// <para>A string of more than (<paramref name="headLines"/> + <paramref name="tailLines"/> + 1) lines is clipped:
        /// <see cref="LineClipping.Head"/> holds its first <paramref name="headLines"/> lines,
        /// and <see cref="LineClipping.Tail"/> its last <paramref name="tailLines"/> lines.
        /// The reason for the +1 above is that a clipped text is usually displayed with a placeholder line
        /// (e.g., <c>"--- more... ---"</c>) between head and tail.</para>
        /// <para>When a string is not clipped, <see cref="LineClipping.Head"/> holds all of its lines,
        /// and <see cref="LineClipping.Tail"/> is empty.</para>
        /// <para>This method copies no characters and allocates no memory.</para>
        /// </remarks>
        /// <seealso cref="CharReadOnlySpanExtensions.ComputeLineClipping"/>
        public LineClipping ComputeLineClipping(int headLines, int tailLines)
            => @this.AsSpan().ComputeLineClipping(headLines, tailLines);
    }
}
