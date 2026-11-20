// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using CommunityToolkit.Diagnostics;

namespace Louis.Diagnostics;

partial class ExceptionExtensions
{
    /// <param name="this">The <see cref="Exception"/> on which this method is called.</param>
    extension(Exception @this)
    {
        /// <summary>
        /// Determines whether any of the causing exceptions for this exception
        /// satisfies the given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A function to test each causing exception for a condition.</param>
        /// <returns><see langword="true"/> if <paramref name="predicate"/> returns <c>true</c> for at least one
        /// of the causing exceptions of <paramref name="this"/>; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="this"/> is <see langword="null"/>.</para>
        /// <para>- or -</para>
        /// <para><paramref name="predicate"/> is <see langword="null"/>.</para>
        /// </exception>
        /// <remarks>
        /// <para>For an explanation of what are considered causing exceptions of an exception, see the "Remarks"
        /// section of <see cref="GetCausingExceptions"/>.</para>
        /// <para>This method yields the same result as calling <c>exception.GetCausingExceptions().Any(predicate)</c>,
        /// but is faster and allocates less memory. It is used internally by the <see cref="IsCriticalError"/> method.</para>
        /// </remarks>
        public bool AnyCausingException(Func<Exception, bool> predicate)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsNotNull(predicate);

            return AnyCausingExceptionCore(@this, predicate);
        }
    }
}
