//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Cossack.Wpf.Converters
{
	/// <summary>
	/// A multi-value converter which returns the result of boolean logic on one or more boolean
	/// inputs. The converter can operate in boolean-AND or boolean-OR mode.
	/// </summary>

	public class BooleanLogicConverter : IMultiValueConverter
	{
		/// <summary>
		/// Converts the input path by applying the specified boolean logic to the inputs.
		/// </summary>
		///
		/// <param name="values">The input values to convert.</param>
		/// <param name="targetType">Ignored.</param>
		/// <param name="parameter">Ignored.</param>
		/// <param name="culture">Ignored.</param>

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool result;

			switch (Mode)
			{
				case BooleanLogicConverterMode.And:
					result = values.All(v => true.Equals(v));
					break;

				case BooleanLogicConverterMode.Or:
					result = values.Any(v => true.Equals(v));
					break;

				case BooleanLogicConverterMode.Nand:
					result = !values.All(v => true.Equals(v));
					break;

				case BooleanLogicConverterMode.Nor:
					result = !values.Any(v => true.Equals(v));
					break;

				case BooleanLogicConverterMode.Xor:
					result = values.Count(v => true.Equals(v)) % 2 == 1;
					break;

				case BooleanLogicConverterMode.Xnor:
					result = values.Count(v => true.Equals(v)) % 2 == 0;
					break;

				default:
					throw new NotSupportedException();
			}

			return result ? ValueIfTrue : ValueIfFalse;
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
		/// Gets or sets a flag specifying whether this converter should operate in AND, OR, NAND,
		/// NOR, XOR or XNOR mode.
		/// </summary>

		public BooleanLogicConverterMode Mode { get; set; } = BooleanLogicConverterMode.And;

		/// <summary>
		/// Gets or sets the value to return if the result of the boolean logic is <c>true</c>.
		/// </summary>

		public object ValueIfTrue { get; set; } = true;

		/// <summary>
		/// Gets or sets the value to return if the result of the boolean logic is <c>false</c>.
		/// </summary>

		public object ValueIfFalse { get; set; } = false;
	}
}