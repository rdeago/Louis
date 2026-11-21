// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using Louis.Text;
using BlankLinesRow = (
    string Before,
    string Text,
    string After,
    int HeadLines,
    int TailLines,
    (string Head, string Tail, int HeadLineCount, int TailLineCount, int OmittedLineCount) Expected);
using LineClippingRow = (
    string Input,
    int HeadLines,
    int TailLines,
    (string Head, string Tail, int HeadLineCount, int TailLineCount, int OmittedLineCount) Expected);

/// <summary>
/// Holds the inputs that drive the tests of the <c>ComputeLineClipping</c> methods of <see cref="StringExtensions"/>
/// and <see cref="CharReadOnlySpanExtensions"/>. The expected values of each row are the text of the head,
/// the text of the tail, and the three line counts.
/// The expected values are a tuple of their own, because the test framework does not unpack a row of eight values
/// on .NET Framework.
/// </summary>
internal static class LineClippingRows
{
    /// <summary>
    /// Returns the rows whose input neither starts nor ends with a blank line.
    /// The head is at the start of the input, and the tail is at its end.
    /// </summary>
    public static IEnumerable<LineClippingRow> All() => [
        // An empty text has no lines.
        (string.Empty, 2, 2, (string.Empty, string.Empty, 0, 0, 0)),

        // A line terminator at the end of the text starts no line.
        ("a", 2, 2, ("a", string.Empty, 1, 0, 0)),
        ("a\n", 2, 2, ("a\n", string.Empty, 1, 0, 0)),

        // A text of at most headLines + tailLines + 1 lines is not clipped: the head holds all of its lines.
        ("a\nb\nc", 2, 2, ("a\nb\nc", string.Empty, 3, 0, 0)),
        ("a\nb\nc\nd", 2, 2, ("a\nb\nc\nd", string.Empty, 4, 0, 0)),
        ("a\nb\nc\nd\ne", 2, 2, ("a\nb\nc\nd\ne", string.Empty, 5, 0, 0)),
        ("a\nb\nc\nd\ne\n", 2, 2, ("a\nb\nc\nd\ne\n", string.Empty, 5, 0, 0)),
        ("a", 0, 0, ("a", string.Empty, 1, 0, 0)),
        ("a\nb\nc", 0, 2, ("a\nb\nc", string.Empty, 3, 0, 0)),
        ("a\nb\nc", 2, 0, ("a\nb\nc", string.Empty, 3, 0, 0)),

        // A text of more lines is clipped.
        ("a\nb\nc\nd\ne\nf", 2, 2, ("a\nb\n", "e\nf", 2, 2, 2)),
        ("a\nb\nc\nd\ne\nf\n", 2, 2, ("a\nb\n", "e\nf\n", 2, 2, 2)),
        ("a\nb\nc\nd\ne\nf\ng", 1, 2, ("a\n", "f\ng", 1, 2, 4)),

        // A head or a tail of no lines is an empty range, at the start or at the end of the text.
        ("a\nb\nc\nd", 0, 2, (string.Empty, "c\nd", 0, 2, 2)),
        ("a\nb\nc\nd", 2, 0, ("a\nb\n", string.Empty, 2, 0, 2)),
        ("a\nb", 0, 0, (string.Empty, string.Empty, 0, 0, 2)),

        // A head and a tail of int.MaxValue lines do not overflow.
        ("a\nb\nc", int.MaxValue, int.MaxValue, ("a\nb\nc", string.Empty, 3, 0, 0)),
        ("a\nb\nc", 0, int.MaxValue, ("a\nb\nc", string.Empty, 3, 0, 0)),

        // A line ends at '\r', at '\n', or at "\r\n".
        ("a\rb\rc\rd\re", 1, 1, ("a\r", "e", 1, 1, 3)),
        ("a\r\nb\r\nc\r\nd\r\n", 1, 1, ("a\r\n", "d\r\n", 1, 1, 2)),
        ("a\nb\r\nc\rd", 1, 1, ("a\n", "d", 1, 1, 2)),

        // "\n\r" is two line terminators, and "\r\r\n" is a '\r' followed by a "\r\n".
        ("a\n\rb", 0, 1, (string.Empty, "b", 0, 1, 2)),
        ("a\r\r\nb", 0, 1, (string.Empty, "b", 0, 1, 2)),
        ("a\nb\n\rc", 0, 2, (string.Empty, "\rc", 0, 2, 2)),
        ("a\rb\r\r\nc", 0, 2, (string.Empty, "\r\nc", 0, 2, 2)),

        // A blank line between two other lines is a line.
        ("a\n\n\n\nb", 1, 1, ("a\n", "b", 1, 1, 3)),
        ("a\r\n\r\n\r\n\r\nb\r\n", 1, 1, ("a\r\n", "b\r\n", 1, 1, 3)),
        ("a\n \n\t\nb", 0, 1, (string.Empty, "b", 0, 1, 3)),
    ];

    /// <summary>
    /// Returns the rows whose input is a text between blank lines.
    /// The head is at the start of the text, and the tail is at its end.
    /// </summary>
    public static IEnumerable<BlankLinesRow> BetweenBlankLines() => [
        // The blank lines at the start and at the end of the input are in neither range, and in no line count.
        ("\n", "a\nb\nc", string.Empty, 2, 2, ("a\nb\nc", string.Empty, 3, 0, 0)),
        (string.Empty, "a\nb\nc\n", "\n", 2, 2, ("a\nb\nc\n", string.Empty, 3, 0, 0)),
        ("\n\n", "a\nb\nc\nd\n", "\n\n", 1, 1, ("a\n", "d\n", 1, 1, 2)),
        ("\r\n\r\n", "a\r\nb\r\nc\r\nd\r\n", "\r\r", 1, 1, ("a\r\n", "d\r\n", 1, 1, 2)),

        // A line of white space alone is a blank line, also without a line terminator.
        (" \n\t\n", "a\nb\nc\nd\n", " \t \n", 1, 1, ("a\n", "d\n", 1, 1, 2)),
        (string.Empty, "a\nb\n", "  ", 2, 2, ("a\nb\n", string.Empty, 2, 0, 0)),

        // White space is every character that char.IsWhiteSpace accepts, such as '\v' and '\f'.
        ("\v\n", "a\n", "\f\n", 2, 2, ("a\n", string.Empty, 1, 0, 0)),

        // The white space of a line that is not blank stays in the line.
        ("\n", "  a  \n  b  ", string.Empty, 2, 2, ("  a  \n  b  ", string.Empty, 2, 0, 0)),
        ("\n", "  a  \nb\nc\n  d  \n", "\n", 1, 1, ("  a  \n", "  d  \n", 1, 1, 2)),

        // A blank line between two other lines is a line.
        ("\n", "a\n\n\n\nb\n", "\n", 1, 1, ("a\n", "b\n", 1, 1, 3)),

        // A head or a tail of no lines is an empty range, at the start or at the end of the text.
        ("\n", "a\nb\nc\nd\n", "\n", 0, 2, (string.Empty, "c\nd\n", 0, 2, 2)),
        ("\n", "a\nb\nc\nd\n", "\n", 2, 0, ("a\nb\n", string.Empty, 2, 0, 2)),
        ("\n", "a\nb\n", "\n", 0, 0, (string.Empty, string.Empty, 0, 0, 2)),
    ];
}
