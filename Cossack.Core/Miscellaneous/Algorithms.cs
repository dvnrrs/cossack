//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

namespace Cossack.Core.Miscellaneous
{
	/// <summary>
	/// Contains simple generic algorithms.
	/// </summary>

	public static class Algorithms
    {
		/// <summary>
		/// Swaps the values of <paramref name="a"/> and <paramref name="b"/>.
		/// </summary>
		///
		/// <typeparam name="T">The type of object to be swapped.</typeparam>
		///
		/// <param name="a">A reference to the first value to swap.</param>
		/// <param name="b">A reference to the second value to swap.</param>

		public static void Swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}

		/// <summary>
		/// Moves the value of <paramref name="from"/> to <paramref name="to"/> and sets
		/// <paramref name="from"/> to <typeparamref name="TFrom"/>'s default value.
		/// </summary>
		///
		/// <typeparam name="TFrom">The type of the source object.</typeparam>
		/// <typeparam name="TTo">The type of the destination object.</typeparam>
		///
		/// <param name="from">A reference to the value to move from.</param>
		/// <param name="to">A reference to the value to move to.</param>

		public static void Move<TFrom, TTo>(ref TFrom from, ref TTo to) where TFrom : TTo
		{
			to = from;
			from = default(TFrom);
		}
	}
}