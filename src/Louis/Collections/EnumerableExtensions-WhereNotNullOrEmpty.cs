// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Diagnostics;

namespace Louis.Collections;

partial class EnumerableExtensions
{
    /// <param name="this">An <see cref="IEnumerable{T}">IEnumerable&lt;string?&gt;</see> to filter.</param>
    extension(IEnumerable<string?> @this)
    {
        /// <summary>
        /// Filters a sequence of nullable strings, taking only those that are neither <see langword="null"/> nor the empty string.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}">IEnumerable&lt;string&gt;</see> that contains elements from the input sequence
        /// that are neither <see langword="null"/> nor the empty string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        public IEnumerable<string> WhereNotNullOrEmpty()
        {
            Guard.IsNotNull(@this, nameof(@this));
            return @this.Where(IsNotNullOrEmpty) as IEnumerable<string>;

            static bool IsNotNullOrEmpty(string? x) => !string.IsNullOrEmpty(x);
        }
    }
}
