// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using CommunityToolkit.Diagnostics;

namespace Louis;

/// <summary>
/// Provides extension methods for objects implementing <see cref="IAsyncDisposable"/>.
/// </summary>
public static class AsyncDisposableExtensions
{
    /// <param name="this">The <see cref="IAsyncDisposable"/> interface on which this method is called.</param>
    extension(IAsyncDisposable @this)
    {
        /// <summary>
        /// Disposes an <see cref="IAsyncDisposable"/> synchronously by awaiting the result of <see cref="IAsyncDisposable.DisposeAsync"/>.
        /// </summary>
        /// <remarks>
        /// <para>The most common use case for this method is add an implementation of <see cref="IDisposable.Dispose"/>
        /// to a type that already implements <see cref="IAsyncDisposable.DisposeAsync"/>, without duplicating any code:</para>
        /// <code>
        ///     public void Dispose() => this.DisposeSynchronously();
        /// </code>
        /// </remarks>
        public void DisposeSynchronously()
        {
            Guard.IsNotNull(@this, nameof(@this));

#pragma warning disable CA2012 // Use ValueTasks correctly - This is correct usage because we consume the ValueTask only once.
            @this.DisposeAsync().GetAwaiter().GetResult();
#pragma warning restore CA2012 // Use ValueTasks correctly
        }
    }
}
