//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cossack.Wpf.Converters
{
	/// <summary>
	/// A value converter which selects one or another value based on whether or not the input
	/// value is equal to a target value. The target value may be a statically-defined value (via
	/// the <see cref="TargetValue"/> property), or the converter parameter value may be used
	/// instead (via the <see cref="UseConverterParameterAsTargetValue"/> property). The returned
	/// values are specified with the <see cref="ValueIfEqual"/> and <see cref="ValueIfNotEqual"/>
	/// properties.
	/// </summary>

	public class EqualityConverter : IValueConverter
	{
		/// <summary>
		/// Converts the input value by returning <see cref="ValueIfEqual"/> (if the input value
		/// is equal to the target value) or <see cref="ValueIfNotEqual"/>.
		/// </summary>
		///
		/// <param name="value">The input value to convert.</param>
		/// <param name="targetType">Ignored.</param>
		/// <param name="parameter">If <see cref="UseConverterParameterAsTargetValue"/> is
		///     <c>true</c>, the equality target value; otherwise, ignored.</param>
		/// <param name="culture">Ignored.</param>
		///
		/// <returns>The converted value.</returns>

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object target = UseConverterParameterAsTargetValue ? parameter : TargetValue;
			bool equal = (value == null ? (target == null) : value.Equals(target));
			return equal ? ValueIfEqual : ValueIfNotEqual;
		}

		/// <summary>
		/// If the conversion involves only booleans (i.e., the input value, target value, and
		/// both selection values are all booleans), reverses the conversion by assuming that the
		/// original input value was also a boolean. Otherwise back-conversion is not possible,
		/// and <see cref="NotSupportedException"/> is thrown.
		/// </summary>
		///
		/// <param name="value">The input value to back-convert.</param>
		/// <param name="targetType">Ignored.</param>
		/// <param name="parameter">If <see cref="UseConverterParameterAsTargetValue"/> is
		///     <c>true</c>, the equality target value; otherwise, ignored.</param>
		/// <param name="culture">Ignored.</param>
		///
		/// <returns>The back-converted value.</returns>
		///
		/// <exception cref="NotSupportedException">This method is not supported because the
		///     forward conversion is irreversible.</exception>

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			object target = UseConverterParameterAsTargetValue ? parameter : TargetValue;

			if (target is bool && ValueIfEqual is bool && ValueIfNotEqual is bool)
			{
				bool equal = (value == null ? (target == null) : value.Equals(target));
				return equal ? ValueIfEqual : ValueIfNotEqual;
			}

			else throw new NotSupportedException();
		}

		/// <summary>
		/// Gets or sets the target object against which to compare for equality.
		/// </summary>

		public object TargetValue { get; set; }

		/// <summary>
		/// Gets or sets a flag that specifies whether to use the converter parameter as the
		/// equality target value (<c>true</c>) or to use the <see cref="TargetValue"/> property
		/// (<c>false</c>).
		/// </summary>

		public bool UseConverterParameterAsTargetValue { get; set; } = false;

		/// <summary>
		/// Gets or sets the <see cref="Visibility"/> value to select if the input value is equal
		/// to the target value.
		/// </summary>

		public object ValueIfEqual { get; set; } = true;

		/// <summary>
		/// Gets or sets the <see cref="Visibility"/> value to select if the input value is not
		/// equal to the target value.
		/// </summary>

		public object ValueIfNotEqual { get; set; } = false;
	}
}