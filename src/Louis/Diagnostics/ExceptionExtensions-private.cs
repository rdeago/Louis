// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace Louis.Diagnostics;

partial class ExceptionExtensions
{
    private static bool AnyCausingExceptionCore(Exception exception, Func<Exception, bool> predicate)
    {
        if (exception is AggregateException aggregateException)
        {
            return aggregateException.InnerExceptions.Any(e => AnyCausingExceptionCore(e, predicate));
        }

        if (exception.InnerException is { } innerException && AnyCausingExceptionCore(innerException, predicate))
        {
            return true;
        }

        return predicate(exception);
    }

    private static IEnumerator<Exception> GetCausingExceptionsCore(Exception exception)
    {
        if (exception is AggregateException aggregateException)
        {
            // A foreach would very probably be fine here, generating the very same code,
            // but I want to stress that we're dealing with enumerators directly.
            using var enumerator = aggregateException.InnerExceptions.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                // Early bailout for common cases - don't allocate an extra enumerator unless necessary.
#if !NETCOREAPP3_0_OR_GREATER
                // JetBrains annotations mark IEnumerator<T>.Current CanBeNull, whatever T is.
                // From .NET Core 3.0 on, the BCL gives Current the nullability of T, and ReSharper follows it.
                // AggregateException rejects null inner exceptions, so current is never null here.
                // ReSharper disable once PossibleNullReferenceException
#endif
                if (current is not AggregateException && current.InnerException is null)
                {
                    yield return current;
                }
                else
                {
                    using var innerEnumerator = GetCausingExceptionsCore(current);
                    while (innerEnumerator.MoveNext())
                    {
#if !NETCOREAPP3_0_OR_GREATER
                        // JetBrains annotations mark IEnumerator<T>.Current CanBeNull, whatever T is.
                        // From .NET Core 3.0 on, the BCL gives Current the nullability of T, and ReSharper follows it.
                        // GetCausingExceptionsCore yields no nulls, so innerEnumerator.Current is never null here.
                        // ReSharper disable once AssignNullToNotNullAttribute
#endif
                        yield return innerEnumerator.Current;
                    }
                }
            }
        }
        else
        {
            if (exception.InnerException is { } innerException)
            {
                // Early bailout for common cases - don't allocate an extra enumerator unless necessary.
                if (innerException is not AggregateException && innerException.InnerException is null)
                {
                    yield return innerException;
                }
                else
                {
                    using var enumerator = GetCausingExceptionsCore(innerException);
                    while (enumerator.MoveNext())
                    {
#if !NETCOREAPP3_0_OR_GREATER
                        // JetBrains annotations mark IEnumerator<T>.Current CanBeNull, whatever T is.
                        // From .NET Core 3.0 on, the BCL gives Current the nullability of T, and ReSharper follows it.
                        // GetCausingExceptionsCore yields no nulls, so enumerator.Current is never null here.
                        // ReSharper disable once AssignNullToNotNullAttribute
#endif
                        yield return enumerator.Current;
                    }
                }
            }

            yield return exception;
        }
    }

    private static bool IsCriticalErrorCore(Exception exception) => exception
        is NullReferenceException
        or StackOverflowException
        or AppDomainUnloadedException
        or BadImageFormatException
        or InvalidProgramException
        or ThreadAbortException
        or AccessViolationException
        or OutOfMemoryException
        or SEHException
        or SecurityException
        or ICriticalError;
}
