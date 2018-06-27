//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System;

namespace Cossack.Core.Errors
{
	/// <summary>
	/// Contains utility functions for dealing with exceptions.
	/// </summary>

	public static class Exceptions
    {
		/// <summary>
		/// Returns the result of a function, or a default value if that function throws an
		/// exception.
		/// </summary>
		///
		/// <typeparam name="T">The type of value to be returned.</typeparam>
		///
		/// <param name="func">The function to invoke.</param>
		/// <param name="defaultValue">The value to return if <paramref name="func"/> throws an
		///     exception.</param>
		///
		/// <returns>The result of <paramref name="func"/>, or <paramref name="defaultValue"/> if
		///     <paramref name="func"/> throws an exception.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="func"/> is
		///     <c>null</c>.</exception>

		public static T Default<T>(Func<T> func, T defaultValue = default(T))
		{
			if (func == null) throw new ArgumentNullException(nameof(func));

			try
			{
				return func();
			}

			catch (Exception)
			{
				return defaultValue;
			}
		}

		/// <summary>
		/// Invokes an action and silently discards any exceptions thrown by it.
		/// </summary>
		///
		/// <param name="action">The action to invoke.</param>
		///
		/// <exception cref="ArgumentNullException"><paramref name="action"/> is
		///     <c>null</c>.</exception>

		public static void Ignore(Action action)
		{
			if (action == null) throw new ArgumentNullException(nameof(action));

			try
			{
				action();
			}

			catch (Exception)
			{
			}
		}
	}
}