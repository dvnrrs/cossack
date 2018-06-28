//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using Cossack.Core.Miscellaneous;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Cossack.Wpf.Media
{
	/// <summary>
	/// Contains image-related utility methods.
	/// </summary>

	public static class ImageUtilities
	{
		/// <summary>
		/// Parses the specified data and returns a <see cref="BitmapImage"/>.
		/// </summary>
		///
		/// <param name="data">An array containing the image data bytes.</param>
		/// <param name="offset">The index in <paramref name="data"/> of the first image data
		///     byte.</param>
		/// <param name="count">The number of image data bytes.</param>
		///
		/// <returns>A new <see cref="BitmapImage"/> object.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="data"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> and
		///     <paramref name="count"/> are outside the bounds of the array.</exception>
		/// <exception cref="InvalidDataException">The image data cannot be
		///     parsed.</exception>

		public static BitmapImage ParseBitmap(byte[] data, int offset, int count)
		{
			ValidationUtilities.ValidateArraySlice(data, offset, count,
				nameof(data), nameof(offset), nameof(count));

			using (MemoryStream memory = new MemoryStream(data, offset, count))
			{
				try
				{
					BitmapImage image = new BitmapImage();
					image.BeginInit();
					image.CacheOption = BitmapCacheOption.OnLoad;
					image.StreamSource = memory;
					image.EndInit();
					return image;
				}

				catch (Exception ex)
				{
					throw new InvalidDataException("Failed to parse image data", ex);
				}
			}
		}
	}
}