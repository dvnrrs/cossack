//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System;

namespace Cossack.Core.Miscellaneous
{
	/// <summary>
	/// Contains utility methods related to method parameter validation.
	/// </summary>

	public static class ValidationUtilities
	{
		/// <summary>
		/// Validates method parameters consisting of an array, array offset and element count to
		/// ensure that the array reference is not null and that the specified offset and count do
		/// not exceed the bounds of the array.
		/// </summary>
		///
		/// <param name="array">The array to validate.</param>
		/// <param name="offset">The array offset to validate.</param>
		/// <param name="count">The element count to validate.</param>
		/// <param name="nameOfArray">The name of the caller's <paramref name="array"/>
		///     parameter.</param>
		/// <param name="nameOfOffset">The name of the caller's <paramref name="offset"/>
		///     parameter.</param>
		/// <param name="nameOfCount">The name of the caller's <paramref name="count"/>
		///     parameter.</param>

		public static void ValidateArraySlice<T>(T[] array, int offset, int count,
			string nameOfArray, string nameOfOffset, string nameOfCount)
		{
			if (array == null)
				throw new ArgumentNullException(nameOfArray);
			if (offset < 0 || offset > array.Length)
				throw new ArgumentOutOfRangeException(nameOfOffset);
			if (count < 0 || offset + count > array.Length)
				throw new ArgumentOutOfRangeException(nameOfCount);
		}
	}
}