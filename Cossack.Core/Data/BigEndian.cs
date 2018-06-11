//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using System;
using System.Diagnostics;

namespace Cossack.Core.Data
{
	/// <summary>
	/// Contains utility methods for encoding and decoding primitive numeric values as
	/// big-endian byte sequences.
	/// </summary>
	///
	/// <remarks>
	/// For performance reasons, all method arguments are checked only via debug assertions, and
	/// will not throw standard argument exceptions. The caller is responsible for ensuring that
	/// all provided buffer arrays are non-null and that any specified offsets and counts lie
	/// within the array bounds.
	/// </remarks>

	[CLSCompliant(false)]
	public static class BigEndian
	{
		/// <summary>
		/// Decodes a big-endian byte sequence as a 16-bit unsigned integer.
		/// </summary>
		///
		/// <param name="buffer">The array containing the bytes to decode.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     decode.</param>
		///
		/// <returns>The decoded value.</returns>

		public static ushort ReadU16(byte[] buffer, int offset)
		{
			Debug.Assert(buffer != null);
			Debug.Assert(offset >= 0);
			Debug.Assert(offset + 2 <= buffer.Length);

			return (ushort) (
				(buffer[offset + 0] << 8) |
				(buffer[offset + 1] << 0)
			);
		}

		/// <summary>
		/// Decodes a big-endian byte sequence as a 32-bit unsigned integer.
		/// </summary>
		///
		/// <param name="buffer">The array containing the bytes to decode.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     decode.</param>
		///
		/// <returns>The decoded value.</returns>

		public static uint ReadU32(byte[] buffer, int offset)
		{
			Debug.Assert(buffer != null);
			Debug.Assert(offset >= 0);
			Debug.Assert(offset + 4 <= buffer.Length);

			return
				((uint) buffer[offset + 0] << 24) |
				((uint) buffer[offset + 1] << 16) |
				((uint) buffer[offset + 2] << 8) |
				((uint) buffer[offset + 3] << 0);
		}

		/// <summary>
		/// Decodes a big-endian byte sequence as a 64-bit unsigned integer.
		/// </summary>
		///
		/// <param name="buffer">The array containing the bytes to decode.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     decode.</param>
		///
		/// <returns>The decoded unsigned value.</returns>

		public static ulong ReadU64(byte[] buffer, int offset)
		{
			Debug.Assert(buffer != null);
			Debug.Assert(offset >= 0);
			Debug.Assert(offset + 8 <= buffer.Length);

			return
				((ulong) buffer[offset + 0] << 56) |
				((ulong) buffer[offset + 1] << 48) |
				((ulong) buffer[offset + 2] << 40) |
				((ulong) buffer[offset + 3] << 32) |
				((ulong) buffer[offset + 4] << 24) |
				((ulong) buffer[offset + 5] << 16) |
				((ulong) buffer[offset + 6] << 8) |
				((ulong) buffer[offset + 7] << 0);
		}

		/// <summary>
		/// Encodes a 32-bit unsigned integer as a big-endian byte sequence.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store the encoded bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="value">The value to encode.</param>

		public static void WriteU16(byte[] buffer, int offset, ushort value)
		{
			Debug.Assert(buffer != null);
			Debug.Assert(offset >= 0);
			Debug.Assert(offset + 2 <= buffer.Length);

			buffer[offset + 0] = (byte) (value >> 8);
			buffer[offset + 1] = (byte) (value >> 0);
		}

		/// <summary>
		/// Encodes a 32-bit unsigned integer as a big-endian byte sequence.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store the encoded bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="value">The value to encode.</param>

		public static void WriteU32(byte[] buffer, int offset, uint value)
		{
			Debug.Assert(buffer != null);
			Debug.Assert(offset >= 0);
			Debug.Assert(offset + 4 <= buffer.Length);

			buffer[offset + 0] = (byte) (value >> 24);
			buffer[offset + 1] = (byte) (value >> 16);
			buffer[offset + 2] = (byte) (value >> 8);
			buffer[offset + 3] = (byte) (value >> 0);
		}

		/// <summary>
		/// Encodes a 64-bit unsigned integer as a big-endian byte sequence.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store the encoded bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="value">The value to encode.</param>

		public static void WriteU64(byte[] buffer, int offset, ulong value)
		{
			Debug.Assert(buffer != null);
			Debug.Assert(offset >= 0);
			Debug.Assert(offset + 8 <= buffer.Length);

			buffer[offset + 0] = (byte) (value >> 56);
			buffer[offset + 1] = (byte) (value >> 48);
			buffer[offset + 2] = (byte) (value >> 40);
			buffer[offset + 3] = (byte) (value >> 32);
			buffer[offset + 4] = (byte) (value >> 24);
			buffer[offset + 5] = (byte) (value >> 16);
			buffer[offset + 6] = (byte) (value >> 8);
			buffer[offset + 7] = (byte) (value >> 0);
		}
	}
}