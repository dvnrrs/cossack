//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System;

namespace Cossack.Core.Data
{
	/// <summary>
	/// Implements the 32-bit CRC-32 algorithm.
	/// </summary>

	public class CRC32
	{
		/// <summary>
		/// Updates the CRC value by processing the specified data.
		/// </summary>
		///
		/// <param name="buffer">An array containing the bytes to process.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     process.</param>
		/// <param name="count">The number of bytes to process.</param>
		///
		/// <exception cref="ArgumentNullException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The specified <paramref name="offset"/>
		///     and <paramref name="count"/> fall outside the bounds of the array.</exception>

		public void Update(byte[] buffer, int offset, int count)
		{
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));
			if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
			if (count < 0 || offset + count > buffer.Length)
				throw new ArgumentOutOfRangeException(nameof(count));

			for (int i = 0; i < count; ++i)
				_crc = LOOKUP[(_crc ^ buffer[offset + i]) & 0xFF] ^ (_crc >> 8);
		}

		/// <summary>
		/// Gets the current CRC value.
		/// </summary>

		public long Value => _crc ^ 0xffffffff;

		static CRC32()
		{
			GenerateLookupTable();
		}

		private static void GenerateLookupTable()
		{
			for (uint i = 0; i < 256; ++i)
			{
				uint c = i;

				for (int j = 0; j < 8; ++j)
				{
					if ((c & 1) != 0) c = 0xedb88320 ^ (c >> 1);
					else c = c >> 1;
				}

				LOOKUP[i] = c;
			}
		}

		private static readonly uint[] LOOKUP = new uint[256];

		private uint _crc = 0xffffffff;
	}
}