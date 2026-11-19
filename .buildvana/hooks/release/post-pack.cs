// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

/*
 * release/post-pack hook: at a stable release, rewrites each link to the preview part of the API reference,
 * in the new section of `CHANGELOG.md`, into a link to the stable part.
 * `bv release` runs the hook after it finalizes the new section, and the rewritten file joins the release commit.
 *
 * A bullet under "Unreleased changes" links the preview part, because the stable part lacks a type
 * that no stable release holds yet.
 * From a stable release on, the stable part describes that release.
 * The preview part describes every later prerelease in turn, where the linked page may change or disappear.
 *
 * The hook writes nothing at a prerelease, because the preview part then describes the version being released.
 * It writes nothing when `release.changelogUpdates` is `none` either, because the changelog then gets no section.
 *
 * To replay the hook by hand, after a `bv release` run or against an args file written by hand:
 *   `dotnet run .buildvana/hooks/release/post-pack.cs`
 */

#:package Buildvana.Runtime

using System;
using System.IO;
using Buildvana.Runtime;

// The URL of the stable part of the API reference, with its trailing slash.
// The preview part is the `preview/` folder of the same site.
const string StablePartUrl = "https://tenacom.github.io/Louis/";
const string PreviewPartUrl = StablePartUrl + "preview/";

const string ChangelogFile = "CHANGELOG.md";

var hookArgs = PostPackHookArgs.Load();
var hasNewSection = hookArgs.RuntimeInfo.Configuration.Release.ChangelogUpdates is not ChangelogUpdates.None;
if (hookArgs.Release.IsPrerelease || !hasNewSection)
{
    return 0;
}

// `bv` gives the new section the heading `## [<version>](<release URL>) (<date>)`.
var text = File.ReadAllText(ChangelogFile);
var heading = $"\n## [{hookArgs.Release.SemVer}]";
var start = text.IndexOf(heading, StringComparison.Ordinal);
if (start < 0)
{
    Console.Error.WriteLine($"{ChangelogFile} has no section for version {hookArgs.Release.SemVer}.");
    return 1;
}

var nextHeading = text.IndexOf("\n## ", start + heading.Length, StringComparison.Ordinal);
var end = nextHeading < 0 ? text.Length : nextHeading;
var section = text[start..end];
var rewritten = section.Replace(PreviewPartUrl, StablePartUrl, StringComparison.Ordinal);
if (rewritten != section)
{
    File.WriteAllText(ChangelogFile, string.Concat(text.AsSpan(0, start), rewritten, text.AsSpan(end)));
}

return 0;
