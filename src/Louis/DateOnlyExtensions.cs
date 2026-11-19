// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

#if NET6_0_OR_GREATER

using System;

namespace Louis;

/// <summary>
/// <para>Provides extension methods for instances of <see cref="DateOnly"/>.</para>
/// <para>This type is not available in the <c>netstandard</c> and .NET Framework builds of Louis,
/// because their BCL has no <see cref="DateOnly"/>.</para>
/// </summary>
public static partial class DateOnlyExtensions
{
    /// <param name="this">The instance of <see cref="DateOnly"/> on which this method is called.</param>
    extension(DateOnly @this)
    {
        /// <summary>
        /// Given a date, returns the first day of the same month.
        /// </summary>
        /// <returns>An instance of <see cref="DateOnly"/> whose value represents the first day
        /// of the month of <paramref name="this"/>.</returns>
        public DateOnly GetStartOfMonth()
            => @this.AddDays(1 - @this.Day);

        /// <summary>
        /// Given a date, returns the last day of the same month.
        /// </summary>
        /// <returns>An instance of <see cref="DateOnly"/> whose value represents the last day
        /// of the month of <paramref name="this"/>.</returns>
        public DateOnly GetEndOfMonth()
            => @this.AddDays(DateTime.DaysInMonth(@this.Year, @this.Month) - @this.Day);
    }
}

#endif
