// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;

namespace Louis.Text;

/// <summary>
/// Describes the first and the last lines of a text, as the <c>ComputeLineClipping</c> methods find them,
/// with the number of lines between them.
/// </summary>
/// <remarks>
/// <para><see cref="Head"/> and <see cref="Tail"/> are ranges of the text that <c>ComputeLineClipping</c> was called on.
/// Each range includes the line terminators of its lines, as they appear in the text.</para>
/// <para>The <c>ComputeLineClipping</c> methods ignore the blank lines at the start and at the end of the text.
/// The documentation of this type does not count them among the lines of the text.</para>
/// <para>When <see cref="OmittedLineCount"/> is 0, the text is not clipped: <see cref="Head"/> covers every line of the text,
/// and <see cref="Tail"/> is an empty range at the end of <see cref="Head"/>.
/// <see cref="OmittedLineCount"/> is never 1, because no text is clipped by a single line.</para>
/// <para>The default value describes a text that has no lines.</para>
/// </remarks>
/// <seealso cref="StringExtensions.ComputeLineClipping"/>
/// <seealso cref="CharReadOnlySpanExtensions.ComputeLineClipping"/>
public readonly struct LineClipping : IEquatable<LineClipping>
{
    internal LineClipping(Range head, Range tail, int headLineCount, int tailLineCount, int omittedLineCount)
    {
        Head = head;
        Tail = tail;
        HeadLineCount = headLineCount;
        TailLineCount = tailLineCount;
        OmittedLineCount = omittedLineCount;
    }

    /// <summary>
    /// Gets the range of the first lines of the text.
    /// </summary>
    /// <value>
    /// A range that starts at the first line of the text and holds <see cref="HeadLineCount"/> lines.
    /// Both of its indexes count from the start of the text.
    /// </value>
    public Range Head { get; }

    /// <summary>
    /// Gets the range of the last lines of the text.
    /// </summary>
    /// <value>
    /// A range that ends after the last line of the text and holds <see cref="TailLineCount"/> lines.
    /// Both of its indexes count from the start of the text.
    /// </value>
    public Range Tail { get; }

    /// <summary>
    /// Gets the number of lines in <see cref="Head"/>.
    /// </summary>
    /// <value>
    /// The <c>headLines</c> parameter of <c>ComputeLineClipping</c> when the text is clipped;
    /// otherwise, the number of lines of the text.
    /// </value>
    public int HeadLineCount { get; }

    /// <summary>
    /// Gets the number of lines in <see cref="Tail"/>.
    /// </summary>
    /// <value>
    /// The <c>tailLines</c> parameter of <c>ComputeLineClipping</c> when the text is clipped;
    /// otherwise, 0.
    /// </value>
    public int TailLineCount { get; }

    /// <summary>
    /// Gets the number of lines of the text that are neither in <see cref="Head"/> nor in <see cref="Tail"/>.
    /// </summary>
    /// <value>
    /// The number of lines between the end of <see cref="Head"/> and the start of <see cref="Tail"/>,
    /// which is 0 when the text is not clipped, and at least 2 when it is.
    /// </value>
    public int OmittedLineCount { get; }

    /// <summary>
    /// Determines whether two specified <see cref="LineClipping"/> values are equal.
    /// </summary>
    /// <param name="a">The first value to compare.</param>
    /// <param name="b">The second value to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="a"/> and <paramref name="b"/> have the same ranges and the same line counts;
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(LineClipping a, LineClipping b) => a.Equals(b);

    /// <summary>
    /// Determines whether two specified <see cref="LineClipping"/> values are different.
    /// </summary>
    /// <param name="a">The first value to compare.</param>
    /// <param name="b">The second value to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="a"/> and <paramref name="b"/> differ in a range or in a line count;
    /// otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(LineClipping a, LineClipping b) => !a.Equals(b);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is LineClipping other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(LineClipping other)
        => (Head, Tail, HeadLineCount, TailLineCount, OmittedLineCount)
            .Equals((other.Head, other.Tail, other.HeadLineCount, other.TailLineCount, other.OmittedLineCount));

    /// <inheritdoc/>
    public override int GetHashCode() => (Head, Tail, HeadLineCount, TailLineCount, OmittedLineCount).GetHashCode();
}
