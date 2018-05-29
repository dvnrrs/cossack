//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using System;
using System.Globalization;
using System.Windows.Data;

namespace Cossack.Wpf.Converters
{
	/// <summary>
	/// A value converter which tests whether an input value of an enumeration type matches the
	/// enumeration value specified as the converter parameter. Backward conversion is partially
	/// supported, whereby an input value of <c>true</c> backward-converts to the enumeration
	/// value specified as the converter parameter, while any other input value results in a
	/// "do-nothing" return.
	/// </summary>

	public class EnumToBooleanConverter : IValueConverter
	{
		/// <summary>
		/// Converts the input value by testing whether or not the value equals the given
		/// converter parameter value.
		/// </summary>
		///
		/// <param name="value">The input value.</param>
		/// <param name="targetType">Ignored.</param>
		/// <param name="parameter">The enumeration value to test for.</param>
		/// <param name="culture">Ignored.</param>
		///
		/// <returns>The converted value.</returns>

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value.Equals(parameter);
		}

		/// <summary>
		/// Backward-converts the input value by returning the given converter parameter value if
		/// the input is <c>true</c>, and returning <see cref="Binding.DoNothing"/> otherwise.
		/// </summary>
		///
		/// <param name="value">The input value to back-convert</param>
		/// <param name="targetType">Ignored.</param>
		/// <param name="parameter"></param>
		/// <param name="culture">Ignored.</param>
		///
		/// <returns>The back-converted value.</returns>

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return true.Equals(value) ? parameter : Binding.DoNothing;
		}
	}
}
