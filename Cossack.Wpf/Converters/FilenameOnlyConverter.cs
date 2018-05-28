//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;

namespace Cossack.Wpf.Converters
{
	/// <summary>
	/// A value converter which removes any drive/directory names from a path and returns the name
	/// only. The extension, if any, may either be included or removed.
	/// </summary>

	public class FilenameOnlyConverter : IValueConverter
	{
		/// <summary>
		/// Converts the input path by removing the drive and directory names.
		/// </summary>
		///
		/// <param name="value">The input path to convert.</param>
		/// <param name="targetType">Ignored.</param>
		/// <param name="parameter">Ignored.</param>
		/// <param name="culture">Ignored.</param>
		///
		/// <returns>The filename, or <see cref="DependencyProperty.UnsetValue"/> if the input
		///     <paramref name="culture"/> is null or not a string.</returns>

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string path = value as string;
			if (path == null) return DependencyProperty.UnsetValue;
			return IncludeExtension ? Path.GetFileName(path) : Path.GetFileNameWithoutExtension(path);
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
		/// A flag specifying whether or not to include the file's extension, if any, in the
		/// converted value.
		/// </summary>

		public bool IncludeExtension { get; set; } = true;
	}
}