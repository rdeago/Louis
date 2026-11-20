// Copyright (c) Tenacom and contributors. Licensed under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using CommunityToolkit.Diagnostics;

namespace Louis.Fluency;

partial class FluentExtensions
{
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="this">The object on which this method was called.</param>
    extension<T>(T @this)
    {
        /// <summary>
        /// Invokes an action on an object if a reference is not <see langword="null"/>, then returns the result of the action.
        /// </summary>
        /// <typeparam name="T1">The type of the additional argument to pass to <paramref name="then"/>.</typeparam>
        /// <param name="arg">The additional argument to pass to <paramref name="then"/>.</param>
        /// <param name="then">The action to perform on <paramref name="this"/> and <paramref name="arg"/>
        /// if <paramref name="arg"/> is not <see langword="null"/>.</param>
        /// <returns>The result of <paramref name="then"/> if <paramref name="arg"/> is not <see langword="null"/>;
        /// otherwise, a reference to <paramref name="this"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="this"/> is <see langword="null"/>.</para>
        /// <para>- or -</para>
        /// <para><paramref name="then"/> is <see langword="null"/>.</para>
        /// </exception>
        public T IfNotNull<T1>(T1? arg, FluentAction<T, T1> then)
            where T1 : class
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsNotNull(then);

            return arg is null ? @this : then(@this, arg);
        }

        /// <summary>
        /// Invokes an action on an object if a reference is not <see langword="null"/>, then returns the same object.
        /// </summary>
        /// <typeparam name="T1">The type of the additional argument to pass to <paramref name="then"/>.</typeparam>
        /// <param name="arg">The additional argument to pass to <paramref name="then"/>.</param>
        /// <param name="then">The action to perform on <paramref name="this"/> and <paramref name="arg"/>
        /// if <paramref name="arg"/> is not <see langword="null"/>.</param>
        /// <returns>A reference to <paramref name="this"/> after <paramref name="then"/>, if called, returns.</returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="this"/> is <see langword="null"/>.</para>
        /// <para>- or -</para>
        /// <para><paramref name="then"/> is <see langword="null"/>.</para>
        /// </exception>
        public T IfNotNull<T1>(T1? arg, Action<T, T1> then)
            where T1 : class
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsNotNull(then);

            if (arg is not null)
            {
                then(@this, arg);
            }

            return @this;
        }
    }
}
