// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using Louis.Text;

internal sealed class CharReadOnlySpanExtensionsComputeLineClippingTests
{
    [Test]
    [MethodDataSource(typeof(LineClippingRows), nameof(LineClippingRows.All))]
    public async Task ComputeLineClipping_ReturnsRangesAndLineCounts(
        string input,
        int headLines,
        int tailLines,
        (string Head, string Tail, int HeadLineCount, int TailLineCount, int OmittedLineCount) expected)
    {
        var result = input.AsSpan().ComputeLineClipping(headLines, tailLines);

        await Assert.That(result.Head).IsEqualTo(new Range(0, expected.Head.Length));
        await Assert.That(result.Tail).IsEqualTo(new Range(input.Length - expected.Tail.Length, input.Length));
        await Assert.That(input[result.Head]).IsEqualTo(expected.Head);
        await Assert.That(input[result.Tail]).IsEqualTo(expected.Tail);
        await Assert.That(result.HeadLineCount).IsEqualTo(expected.HeadLineCount);
        await Assert.That(result.TailLineCount).IsEqualTo(expected.TailLineCount);
        await Assert.That(result.OmittedLineCount).IsEqualTo(expected.OmittedLineCount);
    }

    [Test]
    [MethodDataSource(typeof(LineClippingRows), nameof(LineClippingRows.BetweenBlankLines))]
    public async Task ComputeLineClipping_OnTextBetweenBlankLines_ReturnsRangesAndLineCountsOfTheText(
        string before,
        string text,
        string after,
        int headLines,
        int tailLines,
        (string Head, string Tail, int HeadLineCount, int TailLineCount, int OmittedLineCount) expected)
    {
        var input = before + text + after;
        var textEnd = before.Length + text.Length;

        var result = input.AsSpan().ComputeLineClipping(headLines, tailLines);

        await Assert.That(result.Head).IsEqualTo(new Range(before.Length, before.Length + expected.Head.Length));
        await Assert.That(result.Tail).IsEqualTo(new Range(textEnd - expected.Tail.Length, textEnd));
        await Assert.That(input[result.Head]).IsEqualTo(expected.Head);
        await Assert.That(input[result.Tail]).IsEqualTo(expected.Tail);
        await Assert.That(result.HeadLineCount).IsEqualTo(expected.HeadLineCount);
        await Assert.That(result.TailLineCount).IsEqualTo(expected.TailLineCount);
        await Assert.That(result.OmittedLineCount).IsEqualTo(expected.OmittedLineCount);
    }

    [Test]
    [Arguments("\n")]
    [Arguments("\n\n\n\n")]
    [Arguments("\r\n\r\n\r\n\r\n")]
    [Arguments(" ")]
    [Arguments(" \n\t\r\n  ")]
    public async Task ComputeLineClipping_OnBlankLinesAlone_ReturnsDefault(string input)
    {
        var result = input.AsSpan().ComputeLineClipping(1, 1);

        await Assert.That(result).IsEqualTo(default(LineClipping));
    }

    [Test]
    public async Task ComputeLineClipping_OnEveryShortText_FindsTheLinesOfAForwardScan()
    {
        const int MaxLines = 4;
        var failures = new List<string>();
        foreach (var text in BuildShortTexts(maxLength: 6))
        {
            var allBoundaries = FindLineBoundaries(text);
            if (allBoundaries.Count - 1 != CountLinesRead(text))
            {
                failures.Add($"{text.ToQuotedLiteral()}: the forward scan and ReadLine disagree");
            }

            var boundaries = TrimBlankLines(text, allBoundaries);
            var lineCount = boundaries.Count - 1;
            for (var headLines = 0; headLines <= MaxLines; headLines++)
            {
                for (var tailLines = 0; tailLines <= MaxLines; tailLines++)
                {
                    var isClipped = lineCount > headLines + tailLines + 1;
                    var headLineCount = isClipped ? headLines : lineCount;
                    var tailLineCount = isClipped ? tailLines : 0;
                    var expected = (
                        new Range(boundaries[0], boundaries[headLineCount]),
                        new Range(boundaries[lineCount - tailLineCount], boundaries[lineCount]),
                        headLineCount,
                        tailLineCount,
                        lineCount - headLineCount - tailLineCount);

                    var result = text.AsSpan().ComputeLineClipping(headLines, tailLines);
                    var actual = (result.Head, result.Tail, result.HeadLineCount, result.TailLineCount, result.OmittedLineCount);
                    if (!actual.Equals(expected))
                    {
                        failures.Add($"{text.ToQuotedLiteral()}, {headLines}, {tailLines}: expected {expected}, got {actual}");
                    }
                }
            }
        }

        await Assert.That(failures).IsEmpty();
    }

    [Test]
    [Arguments(-1, 0, "headLines")]
    [Arguments(0, -1, "tailLines")]
    public async Task ComputeLineClipping_WithNegativeLineCount_ThrowsArgumentOutOfRangeException(
        int headLines,
        int tailLines,
        string paramName)
    {
        void Act() => "abc".AsSpan().ComputeLineClipping(headLines, tailLines);

        await Assert.That(Act).Throws<ArgumentOutOfRangeException>().WithParameterName(paramName);
    }

    // Returns every text of at most maxLength characters, each of them a letter, a space, a '\r', or a '\n'.
    private static List<string> BuildShortTexts(int maxLength)
    {
        List<string> texts = [string.Empty];
        for (var i = 0; i < texts.Count; i++)
        {
            if (texts[i].Length < maxLength)
            {
                texts.AddRange([texts[i] + "a", texts[i] + " ", texts[i] + "\r", texts[i] + "\n"]);
            }
        }

        return texts;
    }

    // Returns the start of the text, then the index just past each of its lines, line terminator included.
    private static List<int> FindLineBoundaries(string text)
    {
        List<int> boundaries = [0];
        for (var i = 0; i < text.Length; i++)
        {
            var isLastCharacter = i == text.Length - 1;
            var isCarriageReturnOfCrLf = text[i] == '\r' && !isLastCharacter && text[i + 1] == '\n';
            var isEndOfLine = !isCarriageReturnOfCrLf && (text[i] is '\r' or '\n' || isLastCharacter);
            if (isEndOfLine)
            {
                boundaries.Add(i + 1);
            }
        }

        return boundaries;
    }

    private static int CountLinesRead(string text)
    {
        using var reader = new StringReader(text);
        var count = 0;
        while (reader.ReadLine() is not null)
        {
            count++;
        }

        return count;
    }

    // Returns the boundaries of the lines that remain without the blank lines at the start and at the end of the text.
    // A text of blank lines alone has no lines, and one boundary, at 0, as an empty text has.
    private static List<int> TrimBlankLines(string text, List<int> boundaries)
    {
        var lineCount = boundaries.Count - 1;
        var first = 0;
        while (first < lineCount && IsBlankLine(text, boundaries, first))
        {
            first++;
        }

        if (first == lineCount)
        {
            return [0];
        }

        var last = lineCount - 1;
        while (IsBlankLine(text, boundaries, last))
        {
            last--;
        }

        return boundaries.GetRange(first, last - first + 2);
    }

    private static bool IsBlankLine(string text, List<int> boundaries, int line)
        => text[boundaries[line]..boundaries[line + 1]].All(char.IsWhiteSpace);
}
