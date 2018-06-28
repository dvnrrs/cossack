//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System;

namespace Cossack.Core.Miscellaneous
{
	/// <summary>
	/// Contains array-related extension methods.
	/// </summary>

	public static class ArrayExtensions
    {
		/// <summary>
		/// Creates a new array containing a copy of a slice of the elements in
		/// <paramref name="array"/>.
		/// </summary>
		///
		/// <typeparam name="T">The type of the array elements.</typeparam>
		///
		/// <param name="array">The array to create a slice of.</param>
		/// <param name="offset">The index in <paramref name="array"/> of the first byte to
		///     include in the slice.</param>
		/// <param name="count">The number of bytes to include in the slice.</param>
		///
		/// <returns>The slice.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="array"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> and
		///     <paramref name="count"/> are outside the bounds of the array.</exception>

		public static T[] Slice<T>(this T[] array, int offset, int count)
		{
			ValidationUtilities.ValidateArraySlice(array, offset, count,
				nameof(array), nameof(offset), nameof(count));

			T[] slice = new T[count];
			Array.Copy(array, offset, slice, 0, count);
			return slice;
		}
    }
}