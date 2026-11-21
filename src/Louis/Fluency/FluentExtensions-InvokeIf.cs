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
        /// Invokes an action on an object if a condition is satisfied, then returns the same object.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="action">The action to perform on <paramref name="this"/>.</param>
        /// <returns>A reference to <paramref name="this"/> after <paramref name="action"/> returns.</returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="this"/> is <see langword="null"/>.</para>
        /// <para>- or -</para>
        /// <para><paramref name="action"/> is <see langword="null"/>.</para>
        /// </exception>
        public T InvokeIf(bool condition, Action<T> action)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsNotNull(action);

            if (condition)
            {
                action(@this);
            }

            return @this;
        }

        /// <summary>
        /// Invokes an action on an object and an additional argument if a condition is satisfied, then returns the same object.
        /// </summary>
        /// <typeparam name="TArg">The type of the additional parameter to <paramref name="action"/>.</typeparam>
        /// <param name="condition">The condition to check.</param>
        /// <param name="arg">The additional argument to pass to <paramref name="action"/>.</param>
        /// <param name="action">The action to perform on <paramref name="this"/> and <paramref name="arg"/>.</param>
        /// <returns>A reference to <paramref name="this"/> after <paramref name="action"/> returns.</returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="this"/> is <see langword="null"/>.</para>
        /// <para>- or -</para>
        /// <para><paramref name="action"/> is <see langword="null"/>.</para>
        /// </exception>
        public T InvokeIf<TArg>(bool condition, TArg arg, Action<T, TArg> action)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsNotNull(action);

            if (condition)
            {
                action(@this, arg);
            }

            return @this;
        }

        /// <summary>
        /// Invokes an action on an object and additional arguments if a condition is satisfied, then returns the same object.
        /// </summary>
        /// <typeparam name="TArg1">The type of the first additional parameter to <paramref name="action"/>.</typeparam>
        /// <typeparam name="TArg2">The type of the second additional parameter to <paramref name="action"/>.</typeparam>
        /// <param name="condition">The condition to check.</param>
        /// <param name="arg1">The first additional argument to pass to <paramref name="action"/>.</param>
        /// <param name="arg2">The second additional argument to pass to <paramref name="action"/>.</param>
        /// <param name="action">The action to perform on <paramref name="this"/> and the additional arguments.</param>
        /// <returns>A reference to <paramref name="this"/> after <paramref name="action"/> returns.</returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="this"/> is <see langword="null"/>.</para>
        /// <para>- or -</para>
        /// <para><paramref name="action"/> is <see langword="null"/>.</para>
        /// </exception>
        public T InvokeIf<TArg1, TArg2>(bool condition, TArg1 arg1, TArg2 arg2, Action<T, TArg1, TArg2> action)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsNotNull(action);

            if (condition)
            {
                action(@this, arg1, arg2);
            }

            return @this;
        }

        /// <summary>
        /// Invokes an action on an object and additional arguments if a condition is satisfied, then returns the same object.
        /// </summary>
        /// <typeparam name="TArg1">The type of the first additional parameter to <paramref name="action"/>.</typeparam>
        /// <typeparam name="TArg2">The type of the second additional parameter to <paramref name="action"/>.</typeparam>
        /// <typeparam name="TArg3">The type of the third additional parameter to <paramref name="action"/>.</typeparam>
        /// <param name="condition">The condition to check.</param>
        /// <param name="arg1">The first additional argument to pass to <paramref name="action"/>.</param>
        /// <param name="arg2">The second additional argument to pass to <paramref name="action"/>.</param>
        /// <param name="arg3">The third additional argument to pass to <paramref name="action"/>.</param>
        /// <param name="action">The action to perform on <paramref name="this"/> and the additional arguments.</param>
        /// <returns>A reference to <paramref name="this"/> after <paramref name="action"/> returns.</returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="this"/> is <see langword="null"/>.</para>
        /// <para>- or -</para>
        /// <para><paramref name="action"/> is <see langword="null"/>.</para>
        /// </exception>
        public T InvokeIf<TArg1, TArg2, TArg3>(
            bool condition,
            TArg1 arg1,
            TArg2 arg2,
            TArg3 arg3,
            Action<T, TArg1, TArg2, TArg3> action)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsNotNull(action);

            if (condition)
            {
                action(@this, arg1, arg2, arg3);
            }

            return @this;
        }

        /// <summary>
        /// Invokes an action on an object and additional arguments if a condition is satisfied, then returns the same object.
        /// </summary>
        /// <typeparam name="TArg1">The type of the first additional parameter to <paramref name="action"/>.</typeparam>
        /// <typeparam name="TArg2">The type of the second additional parameter to <paramref name="action"/>.</typeparam>
        /// <typeparam name="TArg3">The type of the third additional parameter to <paramref name="action"/>.</typeparam>
        /// <typeparam name="TArg4">The type of the fourth additional parameter to <paramref name="action"/>.</typeparam>
        /// <param name="condition">The condition to check.</param>
        /// <param name="arg1">The first additional argument to pass to <paramref name="action"/>.</param>
        /// <param name="arg2">The second additional argument to pass to <paramref name="action"/>.</param>
        /// <param name="arg3">The third additional argument to pass to <paramref name="action"/>.</param>
        /// <param name="arg4">The fourth additional argument to pass to <paramref name="action"/>.</param>
        /// <param name="action">The action to perform on <paramref name="this"/> and the additional arguments.</param>
        /// <returns>A reference to <paramref name="this"/> after <paramref name="action"/> returns.</returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="this"/> is <see langword="null"/>.</para>
        /// <para>- or -</para>
        /// <para><paramref name="action"/> is <see langword="null"/>.</para>
        /// </exception>
        public T InvokeIf<TArg1, TArg2, TArg3, TArg4>(
            bool condition,
            TArg1 arg1,
            TArg2 arg2,
            TArg3 arg3,
            TArg4 arg4,
            Action<T, TArg1, TArg2, TArg3, TArg4> action)
        {
            Guard.IsNotNull(@this, nameof(@this));
            Guard.IsNotNull(action);

            if (condition)
            {
                action(@this, arg1, arg2, arg3, arg4);
            }

            return @this;
        }
    }
}
