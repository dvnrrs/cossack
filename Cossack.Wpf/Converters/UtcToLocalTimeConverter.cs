//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cossack.Wpf.Converters
{
	/// <summary>
	/// A value converter which converts an input <see cref="DateTime"/> to local time.
	/// </summary>

	public class UtcToLocalTimeConverter : IValueConverter
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
			if (!(value is DateTime dateTime)) return DependencyProperty.UnsetValue;
			return dateTime.ToLocalTime();
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
	}
}