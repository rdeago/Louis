// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;

namespace Louis.Text;

partial class CharReadOnlySpanExtensions
{
    // Returns the index of the first character of the first line that is not blank, or -1 when every line is blank.
    // Every line terminator is white space, so the white space at the start of the text holds the blank lines,
    // then the white space at the start of the first line that is not blank.
    private static int FindStartOfFirstNonBlankLine(ReadOnlySpan<char> text)
    {
        var whiteSpaceLength = text.Length - text.TrimStart().Length;
        return whiteSpaceLength == text.Length ? -1 : text[..whiteSpaceLength].LastIndexOfAny('\r', '\n') + 1;
    }

    // Returns the index just past the last line that is not blank, line terminator included.
    // The text must have such a line.
    private static int FindEndOfLastNonBlankLine(ReadOnlySpan<char> text)
    {
        var whiteSpaceStart = text.TrimEnd().Length;
        return whiteSpaceStart + FindEndOfFirstLine(text[whiteSpaceStart..]);
    }

    // Returns the index just past the first line of a text, line terminator included, or 0 for an empty text.
    private static int FindEndOfFirstLine(ReadOnlySpan<char> text)
    {
        var terminatorIndex = text.IndexOfAny('\r', '\n');
        return terminatorIndex < 0 ? text.Length
            : text[terminatorIndex..] is ['\r', '\n', ..] ? terminatorIndex + 2
            : terminatorIndex + 1;
    }

    // Returns the index of the first character of the last line of a non-empty text.
    // The text must end where a line ends: at its line terminator, or at the end of the whole text.
    // A '\n' that follows a '\r' is always the second character of one line terminator,
    // so a backward scan finds the same lines as a forward scan.
    private static int FindStartOfLastLine(ReadOnlySpan<char> text)
    {
        var terminatorLength = text is [.., '\r', '\n'] ? 2
            : text is [.., '\r' or '\n'] ? 1
            : 0;

        return text[..^terminatorLength].LastIndexOfAny('\r', '\n') + 1;
    }
}
