// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using Louis.Text;

internal sealed class StringExtensionsComputeLineClippingTests
{
    [Test]
    [MethodDataSource(typeof(LineClippingRows), nameof(LineClippingRows.All))]
    public async Task ComputeLineClipping_ReturnsRangesAndLineCounts(
        string input,
        int headLines,
        int tailLines,
        (string Head, string Tail, int HeadLineCount, int TailLineCount, int OmittedLineCount) expected)
    {
        var result = input.ComputeLineClipping(headLines, tailLines);

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

        var result = input.ComputeLineClipping(headLines, tailLines);

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
        await Assert.That(input.ComputeLineClipping(1, 1)).IsEqualTo(default(LineClipping));
    }

    [Test]
    public async Task ComputeLineClipping_WithNullString_ReturnsDefault()
    {
        await Assert.That(((string?)null).ComputeLineClipping(2, 2)).IsEqualTo(default(LineClipping));
    }

    [Test]
    [Arguments(-1, 0, "headLines")]
    [Arguments(0, -1, "tailLines")]
    public async Task ComputeLineClipping_WithNegativeLineCount_ThrowsArgumentOutOfRangeException(
        int headLines,
        int tailLines,
        string paramName)
    {
        void Act() => "abc".ComputeLineClipping(headLines, tailLines);

        await Assert.That(Act).Throws<ArgumentOutOfRangeException>().WithParameterName(paramName);
    }
}
