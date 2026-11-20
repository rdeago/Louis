// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Louis.Diagnostics;

namespace Louis.Collections;

partial class EnumerableExtensions
{
    /// <param name="this">The <see cref="IEnumerable"/> interface on which this method is called.</param>
    extension(IEnumerable @this)
    {
        /// <summary>
        /// <para>Asynchronously disposes all items that implement either <see cref="IAsyncDisposable"/>
        /// or <see cref="IDisposable"/>.</para>
        /// <para>If an item implements both, only <see cref="IAsyncDisposable"/> is considered.</para>
        /// </summary>
        /// <returns>A <see cref="ValueTask"/> that represents the ongoing operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="AggregateException">
        /// One or more exceptions were raised by the <see cref="IAsyncDisposable.DisposeAsync"/>
        /// and/or <see cref="IDisposable.Dispose"/> methods of items.</exception>
        /// <remarks>
        /// <para>This method will not dispose
        /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/ref-struct">disposable ref structs</see>.</para>
        /// </remarks>
        public async ValueTask DisposeAllAsync()
        {
            Guard.IsNotNull(@this, nameof(@this));

            List<Exception>? exceptions = null;
            List<Task>? pendingTasks = null;
            foreach (var item in @this)
            {
                switch (item)
                {
                    case IAsyncDisposable asyncDisposable:
                        var valueTask = asyncDisposable.DisposeAsync();
                        if (!valueTask.IsCompletedSuccessfully)
                        {
                            pendingTasks ??= [];
                            pendingTasks.Add(valueTask.AsTask());
                        }

                        break;
                    case IDisposable disposable:
                        try
                        {
                            disposable.Dispose();
                        }
                        catch (Exception e) when (!e.IsCriticalError())
                        {
                            exceptions ??= [];
                            exceptions.Add(e);
                        }

                        break;
                }
            }

            if (pendingTasks is not null)
            {
                foreach (var pendingTask in pendingTasks)
                {
                    try
                    {
                        await pendingTask.ConfigureAwait(false);
                    }
                    catch (Exception e) when (!e.IsCriticalError())
                    {
                        exceptions ??= [];
                        exceptions.Add(e);
                    }
                }
            }

            if (exceptions is not null)
            {
                throw new AggregateException(exceptions);
            }
        }

        /// <summary>
        /// <para>Disposes all items that implement either <see cref="IDisposable"/> or <see cref="IAsyncDisposable"/>.</para>
        /// <para>If an item implements both, only <see cref="IDisposable"/> is considered.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        /// <exception cref="AggregateException">
        /// One or more exceptions were raised by the <see cref="IDisposable.Dispose"/>
        /// and/or <see cref="IAsyncDisposable.DisposeAsync"/> methods of items.</exception>
        /// <remarks>
        /// <para>This method will not dispose
        /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/ref-struct">disposable ref structs</see>.</para>
        /// </remarks>
        public void DisposeAll()
        {
            Guard.IsNotNull(@this, nameof(@this));

            List<Exception>? exceptions = null;
            List<Task>? pendingTasks = null;
            foreach (var item in @this)
            {
                switch (item)
                {
                    case IDisposable disposable:
                        try
                        {
                            disposable.Dispose();
                        }
                        catch (Exception e) when (!e.IsCriticalError())
                        {
                            exceptions ??= [];
                            exceptions.Add(e);
                        }

                        break;
                    case IAsyncDisposable asyncDisposable:
                        var valueTask = asyncDisposable.DisposeAsync();
                        if (!valueTask.IsCompletedSuccessfully)
                        {
                            pendingTasks ??= [];
                            pendingTasks.Add(valueTask.AsTask());
                        }

                        break;
                }
            }

            if (pendingTasks is not null)
            {
                foreach (var pendingTask in pendingTasks)
                {
                    try
                    {
                        pendingTask.GetAwaiter().GetResult();
                    }
                    catch (Exception e) when (!e.IsCriticalError())
                    {
                        exceptions ??= [];
                        exceptions.Add(e);
                    }
                }
            }

            if (exceptions is not null)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
