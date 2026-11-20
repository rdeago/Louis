// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using CommunityToolkit.Diagnostics;

namespace Louis.Diagnostics;

partial class ExceptionExtensions
{
    /// <param name="this">The <see cref="Exception"/> on which this method is called.</param>
    extension(Exception @this)
    {
        /// <summary>
        /// Rethrows an exception, preserving the original stack trace. This method never returns.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <see langword="null"/>.</exception>
        [DoesNotReturn]
#pragma warning disable CS8763 // A method marked [DoesNotReturn] should not return - ExceptionDispatchInfo.Throw has no DoesNotReturn.
        public void Rethrow()
        {
            Guard.IsNotNull(@this, nameof(@this));

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER
            ExceptionDispatchInfo.Throw(@this);
#else
            ExceptionDispatchInfo.Capture(@this).Throw();
#endif
        }
#pragma warning restore CS8763 // A method marked [DoesNotReturn] should not return
    }
}
