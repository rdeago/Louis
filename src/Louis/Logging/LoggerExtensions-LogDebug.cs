﻿// ---------------------------------------------------------------------------------------
// Copyright (C) Tenacom and L.o.U.I.S. contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.
//
// Part of this file may be third-party code, distributed under a compatible license.
// See the THIRD-PARTY-NOTICES file in the project root for third-party copyright notices.
// ---------------------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace Louis.Logging;

#pragma warning disable CA1848 // Use the LoggerMessage delegates
#pragma warning disable CA2254 // Template should be a static expression

partial class LoggerExtensions
{
    /// <summary>
    /// Writes a debug log message.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="message">The log message to write.</param>
    public static void LogDebug(this ILogger @this, string message)
    {
        if (@this.IsEnabled(LogLevel.Debug))
        {
            @this.Log(LogLevel.Debug, message);
        }
    }

    /// <summary>
    /// Writes a debug log message.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="eventId">The event id associated with the log.</param>
    /// <param name="message">The log message to write.</param>
    public static void LogDebug(this ILogger @this, EventId eventId, string message)
    {
        if (@this.IsEnabled(LogLevel.Debug))
        {
            @this.Log(LogLevel.Debug, eventId, message);
        }
    }

    /// <summary>
    /// Writes a debug log message.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The log message to write.</param>
    public static void LogDebug(this ILogger @this, Exception? exception, string message)
    {
        if (@this.IsEnabled(LogLevel.Debug))
        {
            @this.Log(LogLevel.Debug, exception, message);
        }
    }

    /// <summary>
    /// Writes a debug log message.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="eventId">The event id associated with the log.</param>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The log message to write.</param>
    public static void LogDebug(this ILogger @this, EventId eventId, Exception? exception, string message)
    {
        if (@this.IsEnabled(LogLevel.Debug))
        {
            @this.Log(LogLevel.Debug, eventId, exception, message);
        }
    }

    /// <summary>
    /// Formats and writes a debug log message.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="message">The log message to write. This parameter must be an interpolated string.</param>
    public static void LogDebug(
        this ILogger @this,
        [InterpolatedStringHandlerArgument("this")]
        ref LogInterpolatedStringHandler.Debug message)
    {
        if (message.IsEnabled)
        {
            var (template, arguments) = message.GetDataAndDispose();
            @this.Log(LogLevel.Debug, template, arguments);
        }
    }

    /// <summary>
    /// Formats and writes a debug log message.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="eventId">The event id associated with the log.</param>
    /// <param name="message">The log message to write. This parameter must be an interpolated string.</param>
    public static void LogDebug(
        this ILogger @this,
        EventId eventId,
        [InterpolatedStringHandlerArgument("this")]
        ref LogInterpolatedStringHandler.Debug message)
    {
        if (message.IsEnabled)
        {
            var (template, arguments) = message.GetDataAndDispose();
            @this.Log(LogLevel.Debug, eventId, template, arguments);
        }
    }

    /// <summary>
    /// Formats and writes a debug log message.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The log message to write. This parameter must be an interpolated string.</param>
    public static void LogDebug(
        this ILogger @this,
        Exception? exception,
        [InterpolatedStringHandlerArgument("this")]
        ref LogInterpolatedStringHandler.Debug message)
    {
        if (message.IsEnabled)
        {
            var (template, arguments) = message.GetDataAndDispose();
            @this.Log(LogLevel.Debug, exception, template, arguments);
        }
    }

    /// <summary>
    /// Formats and writes a debug log message.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="eventId">The event id associated with the log.</param>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The log message to write. This parameter must be an interpolated string.</param>
    public static void LogDebug(
        this ILogger @this,
        EventId eventId,
        Exception? exception,
        [InterpolatedStringHandlerArgument("this")]
        ref LogInterpolatedStringHandler.Debug message)
    {
        if (message.IsEnabled)
        {
            var (template, arguments) = message.GetDataAndDispose();
            @this.Log(LogLevel.Debug, eventId, exception, template, arguments);
        }
    }
}
