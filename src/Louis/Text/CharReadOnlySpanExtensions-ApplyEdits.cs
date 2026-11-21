// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using CommunityToolkit.Diagnostics;
using Louis.Text.Internal;

namespace Louis.Text;

partial class CharReadOnlySpanExtensions
{
    /// <param name="this">The <see cref="ReadOnlySpan{T}">ReadOnlySpan&lt;char&gt;</see>
    /// on which this method is called.</param>
    extension(ReadOnlySpan<char> @this)
    {
        /// <summary>
        /// Applies a set of edits to a span of characters, and returns the resulting string.
        /// </summary>
        /// <param name="edits">The edits to apply. The <see cref="TextEdit.Start"/> of every edit is an index
        /// in <paramref name="this"/>, whatever the other edits do.</param>
        /// <returns>A newly-constructed string, in which <see cref="TextEdit.Text"/> has replaced the characters of each edit.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="edits"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">An edit has a negative <see cref="TextEdit.Start"/>,
        /// a <see cref="TextEdit.Start"/> greater than the length of <paramref name="this"/>,
        /// a negative <see cref="TextEdit.Length"/>, or characters to replace after the end of <paramref name="this"/>.</exception>
        /// <exception cref="ArgumentException">Two edits overlap.</exception>
        /// <remarks>
        /// <para>The order of <paramref name="edits"/> matters only for insertions at one index:
        /// their texts appear in the result in the order of <paramref name="edits"/>.</para>
        /// <para>Two edits overlap when both replace one character, or when one inserts a text between two characters
        /// that the other replaces.</para>
        /// <para>An insertion at the first character that another edit replaces overlaps nothing:
        /// the inserted text precedes the replacing text in the result.
        /// An insertion just after the last character that another edit replaces follows the replacing text.</para>
        /// </remarks>
        /// <seealso cref="StringExtensions.ApplyEdits"/>
        public string ApplyEdits(IEnumerable<TextEdit> edits)
        {
            Guard.IsNotNull(edits);

            return TextEditHelper.Apply(@this, TextEditHelper.SortAndCheck(edits, @this.Length));
        }
    }
}
