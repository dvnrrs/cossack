//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System;
using System.Globalization;
using System.Windows.Data;

namespace Cossack.Wpf.Converters
{
	/// <summary>
	/// A value converter which converts an input number into a prettified byte size string with
	/// an appropriate unit suffix (e.g., "2.0 KiB").
	/// </summary>

	public class PrettyByteSizeConverter : IValueConverter
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
			double size = System.Convert.ToDouble(value);

			if (size == 1) return "1 byte";
			else if (size < 1000) return string.Format("{0:0.} bytes", size);
			else if (size < 1000 * 1024) return string.Format("{0:0.00} KiB", size / 1024);
			else if (size < 1000 * 1024 * 1024) return string.Format("{0:0.000} MiB", size / 1024 / 1024);
			else if (size < 1000L * 1024 * 1024 * 1024) return string.Format("{0:0.0000} GiB", size / 1024 / 1024 / 1024);
			return string.Format("{0:0.0000} TiB", size / 1024 / 1024 / 1024 / 1024);
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