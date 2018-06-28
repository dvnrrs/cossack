//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System;
using System.Globalization;
using System.Windows.Data;

namespace Cossack.Wpf.Converters
{
	/// <summary>
	/// A multi-value converter which returns one value if all inputs are equal or another value
	/// if not. The returned values are specified with the <see cref="ValueIfEqual"/> and
	/// <see cref="ValueIfNotEqual"/>
	/// </summary>

	public class MultiEqualityConverter : IMultiValueConverter
	{
		/// <summary>
		/// Converts the input path by testing the equality of all of the inputs.
		/// </summary>
		///
		/// <param name="values">The input values to convert.</param>
		/// <param name="targetType">Ignored.</param>
		/// <param name="parameter">Ignored.</param>
		/// <param name="culture">Ignored.</param>

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			int i;

			for (i = 1; i < values.Length; ++i)
			{
				if (values[i - 1] == null)
				{
					if (values[i] != null) break;
				}

				else
				{
					if (!values[i - 1].Equals(values[i])) break;
				}
			}

			return i >= values.Length ? ValueIfEqual : ValueIfNotEqual;
		}

		/// <summary>
		/// Throws <see cref="NotSupportedException"/> because backward conversion is not
		/// supported.
		/// </summary>
		///
		/// <param name="value">Ignored.</param>
		/// <param name="targetTypes">Ignored.</param>
		/// <param name="parameter">Ignored.</param>
		/// <param name="culture">Ignored.</param>
		///
		/// <returns>This method does not return.</returns>
		///
		/// <exception cref="NotSupportedException">This method is not supported.</exception>

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Gets or sets the value to select if all input values are equal.
		/// </summary>

		public object ValueIfEqual { get; set; } = true;

		/// <summary>
		/// Gets or sets the value to select if all input values are not equal.
		/// </summary>

		public object ValueIfNotEqual { get; set; } = false;
	}
}