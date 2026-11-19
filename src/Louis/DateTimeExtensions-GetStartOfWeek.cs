// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using CommunityToolkit.Diagnostics;

namespace Louis;

partial class DateTimeExtensions
{
    /// <param name="this">The instance of <see cref="DateTime"/> on which this method is called.</param>
    extension(DateTime @this)
    {
        /// <summary>
        /// Given a date and time, returns midnight on the first day of the same week, according to the current culture.
        /// </summary>
        /// <returns>An instance of <see cref="DateTime"/> whose value represents the first day of the week of <paramref name="this"/>,
        /// according to the current culture.</returns>
        /// <seealso cref="CultureInfo.CurrentCulture"/>
        /// <seealso cref="CultureInfo.DateTimeFormat"/>
        /// <seealso cref="DateTimeFormatInfo.FirstDayOfWeek"/>
        public DateTime GetStartOfWeek()
            => @this.GetStartOfWeek(CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

        /// <summary>
        /// Given a date and time, returns midnight on the first day of the same week, according to the specified culture.
        /// </summary>
        /// <param name="culture">An object that supplies culture-specific information.</param>
        /// <returns>An instance of <see cref="DateTime"/> whose value represents the first day of the week of <paramref name="this"/>,
        /// according to <paramref name="culture"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="culture"/> is <see langword="null"/>.</exception>
        /// <seealso cref="CultureInfo"/>
        /// <seealso cref="CultureInfo.DateTimeFormat"/>
        /// <seealso cref="DateTimeFormatInfo.FirstDayOfWeek"/>
        public DateTime GetStartOfWeek(CultureInfo culture)
        {
            Guard.IsNotNull(culture);
            return @this.GetStartOfWeek(culture.DateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>
        /// Given a date and time, returns midnight on the first day of the same week, according to the specified formatting information.
        /// </summary>
        /// <param name="dateTimeFormat">An object that supplies culture-specific date formatting information.</param>
        /// <returns>An instance of <see cref="DateTime"/> whose value represents the first day of the week of <paramref name="this"/>,
        /// according to <paramref name="dateTimeFormat"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dateTimeFormat"/> is <see langword="null"/>.</exception>
        /// <seealso cref="DateTimeFormatInfo.FirstDayOfWeek"/>
        public DateTime GetStartOfWeek(DateTimeFormatInfo dateTimeFormat)
        {
            Guard.IsNotNull(dateTimeFormat);
            return @this.GetStartOfWeek(dateTimeFormat.FirstDayOfWeek);
        }

        /// <summary>
        /// Returns midnight on the most recent date, earlier or equal to a given date,
        /// whose day of the week is equal to a specified day of the week.
        /// </summary>
        /// <param name="firstDayOfWeek">A <see cref="DayOfWeek"/> value representing the day that starts the week.</param>
        /// <returns>An instance of <see cref="DateTime"/> whose value represents the first day of the week of <paramref name="this"/>,
        /// if weeks are considered starting with <paramref name="firstDayOfWeek"/>.</returns>
        /// <seealso cref="DayOfWeek"/>
        public DateTime GetStartOfWeek(DayOfWeek firstDayOfWeek)
            => @this.Date.AddDays(-DateUtility.GetDaysFromStartOfWeek(@this.DayOfWeek, firstDayOfWeek));
    }
}
