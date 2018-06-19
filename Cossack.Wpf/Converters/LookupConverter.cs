//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Cossack.Wpf.Converters
{
	/// <summary>
	/// A converter that looks up its input value in a configurable lookup table and returns the
	/// result, or <c>null</c> if the input value does not exist in the lookup table.
	/// </summary>

	public class LookupConverter : IValueConverter
	{
		/// <summary>
		/// Converts the input value.
		/// </summary>
		///
		/// <param name="value">The input value to convert.</param>
		/// <param name="targetType">Ignored.</param>
		/// <param name="parameter">Ignored.</param>
		/// <param name="culture">Ignored.</param>
		///
		/// <returns>The converted value.</returns>

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object result = FallbackValue;
			Values?.TryGetValue(value, out result);
			return result;
		}

		/// <summary>
		/// Throws <see cref="NotSupportedException"/> because backward conversion is not
		/// supported.
		/// </summary>
		///
		/// <param name="value">Ignored.</param>
		/// <param name="targetType">Ignored.</param>
		/// <param name="parameter">Ignored.</param>
		/// <param name="culture">Ignored.</param>
		///
		/// <returns>This method does not return.</returns>
		///
		/// <exception cref="NotSupportedException">This method is not supported.</exception>

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Gets or sets the fallback value to use if an input value is not found in the lookup
		/// table.
		/// </summary>

		public object FallbackValue { get; set; }

		/// <summary>
		/// Gets the lookup table used by the converter.
		/// </summary>

		public Dictionary<object, object> Values { get; } = new Dictionary<object, object>();
	}
}