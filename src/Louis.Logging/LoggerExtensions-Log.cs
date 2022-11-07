﻿// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using CommunityToolkit.Diagnostics;
using Louis.Logging.Internal;
using Microsoft.Extensions.Logging;

namespace Louis.Logging;

#pragma warning disable CA1848 // Use the LoggerMessage delegates
#pragma warning disable CA2254 // Template should be a static expression

partial class LoggerExtensions
{
    /// <summary>
    /// Writes a log message at the specified log level.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="logLevel">Entry will be written on this level.</param>
    /// <param name="message">The log message to write.</param>
    /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="logLevel"/> is not a valid log level.</exception>
    public static void Log(this ILogger @this, LogLevel logLevel, string message)
    {
        Guard.IsNotNull(@this);
        InternalGuard.IsValidLogLevelForWriting(logLevel);

        if (@this.IsEnabled(logLevel))
        {
            @this.Log(logLevel, NullEventId, message, null, StaticMessageFormatter);
        }
    }

    /// <summary>
    /// Writes a log message at the specified log level.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="logLevel">Entry will be written on this level.</param>
    /// <param name="eventId">The event id associated with the log.</param>
    /// <param name="message">The log message to write.</param>
    /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="logLevel"/> is not a valid log level.</exception>
    public static void Log(this ILogger @this, LogLevel logLevel, EventId eventId, string message)
    {
        Guard.IsNotNull(@this);
        InternalGuard.IsValidLogLevelForWriting(logLevel);

        if (@this.IsEnabled(logLevel))
        {
            @this.Log(logLevel, eventId, message, null, StaticMessageFormatter);
        }
    }

    /// <summary>
    /// Writes a log message at the specified log level.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="logLevel">Entry will be written on this level.</param>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The log message to write.</param>
    /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="logLevel"/> is not a valid log level.</exception>
    public static void Log(this ILogger @this, LogLevel logLevel, Exception? exception, string message)
    {
        Guard.IsNotNull(@this);
        InternalGuard.IsValidLogLevelForWriting(logLevel);

        if (@this.IsEnabled(logLevel))
        {
            @this.Log(logLevel, NullEventId, message, exception, StaticMessageFormatter);
        }
    }

    /// <summary>
    /// Writes a log message at the specified log level.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="logLevel">Entry will be written on this level.</param>
    /// <param name="eventId">The event id associated with the log.</param>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The log message to write.</param>
    /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="logLevel"/> is not a valid log level.</exception>
    public static void Log(this ILogger @this, LogLevel logLevel, EventId eventId, Exception? exception, string message)
    {
        Guard.IsNotNull(@this);
        InternalGuard.IsValidLogLevelForWriting(logLevel);

        if (@this.IsEnabled(logLevel))
        {
            @this.Log(logLevel, eventId, message, exception, StaticMessageFormatter);
        }
    }

    /// <summary>
    /// Formats and writes a log message at the specified log level.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="logLevel">Entry will be written on this level.</param>
    /// <param name="message">The log message to write. This parameter must be an interpolated string.</param>
    /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="logLevel"/> is not a valid log level.</exception>
    public static void Log(
        this ILogger @this,
        LogLevel logLevel,
        [InterpolatedStringHandlerArgument("this", "logLevel")]
        ref LogInterpolatedStringHandler message)
    {
        // Arguments are validated in the constructor of LogInterpolatedStringHandler.
        if (message.IsEnabled)
        {
            var (template, arguments) = message.GetDataAndClear();
            @this.Log(logLevel, template, arguments);
        }
    }

    /// <summary>
    /// Formats and writes a log message at the specified log level.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="logLevel">Entry will be written on this level.</param>
    /// <param name="eventId">The event id associated with the log.</param>
    /// <param name="message">The log message to write. This parameter must be an interpolated string.</param>
    /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="logLevel"/> is not a valid log level.</exception>
    public static void Log(
        this ILogger @this,
        LogLevel logLevel,
        EventId eventId,
        [InterpolatedStringHandlerArgument("this", "logLevel")]
        ref LogInterpolatedStringHandler message)
    {
        // Arguments are validated in the constructor of LogInterpolatedStringHandler.
        if (message.IsEnabled)
        {
            var (template, arguments) = message.GetDataAndClear();
            @this.Log(logLevel, eventId, template, arguments);
        }
    }

    /// <summary>
    /// Formats and writes a log message at the specified log level.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="logLevel">Entry will be written on this level.</param>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The log message to write. This parameter must be an interpolated string.</param>
    /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="logLevel"/> is not a valid log level.</exception>
    public static void Log(
        this ILogger @this,
        LogLevel logLevel,
        Exception? exception,
        [InterpolatedStringHandlerArgument("this", "logLevel")]
        ref LogInterpolatedStringHandler message)
    {
        // Arguments are validated in the constructor of LogInterpolatedStringHandler.
        if (message.IsEnabled)
        {
            var (template, arguments) = message.GetDataAndClear();
            @this.Log(logLevel, exception, template, arguments);
        }
    }

    /// <summary>
    /// Formats and writes a log message at the specified log level.
    /// </summary>
    /// <param name="this">The <see cref="ILogger"/> to write to.</param>
    /// <param name="logLevel">Entry will be written on this level.</param>
    /// <param name="eventId">The event id associated with the log.</param>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The log message to write. This parameter must be an interpolated string.</param>
    /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="logLevel"/> is not a valid log level.</exception>
    public static void Log(
        this ILogger @this,
        LogLevel logLevel,
        EventId eventId,
        Exception? exception,
        [InterpolatedStringHandlerArgument("this", "logLevel")]
        ref LogInterpolatedStringHandler message)
    {
        // Arguments are validated in the constructor of LogInterpolatedStringHandler.
        if (message.IsEnabled)
        {
            var (template, arguments) = message.GetDataAndClear();
            @this.Log(logLevel, eventId, exception, template, arguments);
        }
    }
}
