// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Diagnostics;

namespace Louis.Collections;

partial class EnumerableExtensions
{
    /// <typeparam name="T">The type of the elements of <paramref name="this"/>.</typeparam>
    /// <param name="this">An <see cref="IEnumerable{T}"/> to filter.</param>
    extension<T>(IEnumerable<T> @this)
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate, taking only those that do not satisfy the condition set by the predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that do not satisfy the condition
        /// defined by <paramref name="predicate"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="this"/> is <see langword="null"/>.</para>
        /// <para>- or -</para>
        /// <para><paramref name="predicate"/> is <see langword="null"/>.</para>
        /// </exception>
        public IEnumerable<T> WhereNot(Func<T, bool> predicate)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsNotNull(predicate);
            return @this.Where(x => !predicate(x));
        }
    }
}
