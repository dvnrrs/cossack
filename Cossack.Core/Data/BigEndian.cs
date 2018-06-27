//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System;

namespace Cossack.Core.Data
{
	/// <summary>
	/// Contains utility methods for encoding and decoding primitive numeric values as
	/// big-endian byte sequences.
	/// </summary>

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
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		[CLSCompliant(false)]
		public static ushort ReadU16(byte[] buffer, int offset)
		{
			return (ushort) (
				(buffer[offset + 0] << 8) |
				(buffer[offset + 1] << 0)
			);
		}

		/// <summary>
		/// Decodes a big-endian byte sequence as a 16-bit signed integer.
		/// </summary>
		///
		/// <param name="buffer">The array containing the bytes to decode.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     decode.</param>
		///
		/// <returns>The decoded value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		public static short ReadS16(byte[] buffer, int offset)
		{
			return (short) (
				(buffer[offset + 0] << 8) |
				(buffer[offset + 1] << 0)
			);
		}

		/// <summary>
		/// Decodes a big-endian byte sequence as a 24-bit unsigned integer.
		/// </summary>
		///
		/// <param name="buffer">The array containing the bytes to decode.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     decode.</param>
		///
		/// <returns>The decoded value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		[CLSCompliant(false)]
		public static uint ReadU24(byte[] buffer, int offset)
		{
			return
				((uint) buffer[offset + 0] << 16) |
				((uint) buffer[offset + 1] << 8) |
				((uint) buffer[offset + 2] << 0);
		}

		/// <summary>
		/// Decodes a big-endian byte sequence as a 24-bit signed integer.
		/// </summary>
		///
		/// <param name="buffer">The array containing the bytes to decode.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     decode.</param>
		///
		/// <returns>The decoded value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		public static int ReadS24(byte[] buffer, int offset)
		{
			return
				((buffer[offset + 0] << 24) >> 8) |
				((int) buffer[offset + 1] << 8) |
				((int) buffer[offset + 2] << 0);
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
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		[CLSCompliant(false)]
		public static uint ReadU32(byte[] buffer, int offset)
		{
			return
				((uint) buffer[offset + 0] << 24) |
				((uint) buffer[offset + 1] << 16) |
				((uint) buffer[offset + 2] << 8) |
				((uint) buffer[offset + 3] << 0);
		}

		/// <summary>
		/// Decodes a big-endian byte sequence as a 32-bit signed integer.
		/// </summary>
		///
		/// <param name="buffer">The array containing the bytes to decode.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     decode.</param>
		///
		/// <returns>The decoded value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		public static int ReadS32(byte[] buffer, int offset)
		{
			return
				(buffer[offset + 0] << 24) |
				(buffer[offset + 1] << 16) |
				(buffer[offset + 2] << 8) |
				(buffer[offset + 3] << 0);
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
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		[CLSCompliant(false)]
		public static ulong ReadU64(byte[] buffer, int offset)
		{
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
		/// Decodes a big-endian byte sequence as a 64-bit signed integer.
		/// </summary>
		///
		/// <param name="buffer">The array containing the bytes to decode.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     decode.</param>
		///
		/// <returns>The decoded unsigned value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		public static long ReadS64(byte[] buffer, int offset)
		{
			return
				((long) buffer[offset + 0] << 56) |
				((long) buffer[offset + 1] << 48) |
				((long) buffer[offset + 2] << 40) |
				((long) buffer[offset + 3] << 32) |
				((long) buffer[offset + 4] << 24) |
				((long) buffer[offset + 5] << 16) |
				((long) buffer[offset + 6] << 8) |
				((long) buffer[offset + 7] << 0);
		}

		/// <summary>
		/// Encodes a 16-bit unsigned integer as a big-endian byte sequence.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store the encoded bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="value">The value to encode.</param>
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		[CLSCompliant(false)]
		public static void WriteU16(byte[] buffer, int offset, ushort value)
		{
			buffer[offset + 0] = (byte) (value >> 8);
			buffer[offset + 1] = (byte) (value >> 0);
		}

		/// <summary>
		/// Encodes a 16-bit signed integer as a big-endian byte sequence.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store the encoded bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="value">The value to encode.</param>
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		public static void WriteS16(byte[] buffer, int offset, short value)
		{
			buffer[offset + 0] = (byte) (value >> 8);
			buffer[offset + 1] = (byte) (value >> 0);
		}

		/// <summary>
		/// Encodes a 24-bit unsigned integer as a big-endian byte sequence.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store the encoded bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="value">The value to encode. If the specified value is larger than the
		///     largest 24-bit unsigned integer, the upper bits are discarded.</param>
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		[CLSCompliant(false)]
		public static void WriteU24(byte[] buffer, int offset, uint value)
		{
			buffer[offset + 0] = (byte) (value >> 16);
			buffer[offset + 1] = (byte) (value >> 8);
			buffer[offset + 2] = (byte) (value >> 0);
		}

		/// <summary>
		/// Encodes a 24-bit signed integer as a big-endian byte sequence.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store the encoded bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="value">The value to encode. If the specified value is larger than the
		///     largest 24-bit unsigned integer, the upper bits are discarded.</param>
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		public static void WriteS24(byte[] buffer, int offset, int value)
		{
			buffer[offset + 0] = (byte) (value >> 16);
			buffer[offset + 1] = (byte) (value >> 8);
			buffer[offset + 2] = (byte) (value >> 0);
		}

		/// <summary>
		/// Encodes a 32-bit unsigned integer as a big-endian byte sequence.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store the encoded bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="value">The value to encode.</param>
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		[CLSCompliant(false)]
		public static void WriteU32(byte[] buffer, int offset, uint value)
		{
			buffer[offset + 0] = (byte) (value >> 24);
			buffer[offset + 1] = (byte) (value >> 16);
			buffer[offset + 2] = (byte) (value >> 8);
			buffer[offset + 3] = (byte) (value >> 0);
		}

		/// <summary>
		/// Encodes a 32-bit signed integer as a big-endian byte sequence.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store the encoded bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="value">The value to encode.</param>
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		public static void WriteS32(byte[] buffer, int offset, int value)
		{
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
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		[CLSCompliant(false)]
		public static void WriteU64(byte[] buffer, int offset, ulong value)
		{
			buffer[offset + 0] = (byte) (value >> 56);
			buffer[offset + 1] = (byte) (value >> 48);
			buffer[offset + 2] = (byte) (value >> 40);
			buffer[offset + 3] = (byte) (value >> 32);
			buffer[offset + 4] = (byte) (value >> 24);
			buffer[offset + 5] = (byte) (value >> 16);
			buffer[offset + 6] = (byte) (value >> 8);
			buffer[offset + 7] = (byte) (value >> 0);
		}

		/// <summary>
		/// Encodes a 64-bit signed integer as a big-endian byte sequence.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store the encoded bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="value">The value to encode.</param>
		///
		/// <exception cref="IndexOutOfRangeException">The region specified by
		///     <paramref name="offset"/> exceeds the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>

		public static void WriteS64(byte[] buffer, int offset, long value)
		{
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