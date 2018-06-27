//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using System;

namespace Cossack.Core.Data
{
	/// <summary>
	/// Contains utility methods for encoding and decoding primitive numeric values as
	/// little-endian byte sequences.
	/// </summary>

	public static class LittleEndian
	{
		/// <summary>
		/// Decodes a little-endian byte sequence as a 16-bit unsigned integer.
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
				(buffer[offset + 0] << 0) |
				(buffer[offset + 1] << 8)
			);
		}

		/// <summary>
		/// Decodes a little-endian byte sequence as a 16-bit signed integer.
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
				(buffer[offset + 0] << 0) |
				(buffer[offset + 1] << 8)
			);
		}

		/// <summary>
		/// Decodes a little-endian byte sequence as a 24-bit unsigned integer.
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
				((uint) buffer[offset + 0] << 0) |
				((uint) buffer[offset + 1] << 8) |
				((uint) buffer[offset + 2] << 16);
		}

		/// <summary>
		/// Decodes a little-endian byte sequence as a 24-bit signed integer.
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
				(buffer[offset + 0] << 0) |
				(buffer[offset + 1] << 8) |
				((buffer[offset + 2] << 24) >> 8);
		}

		/// <summary>
		/// Decodes a little-endian byte sequence as a 32-bit unsigned integer.
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
				((uint) buffer[offset + 0] << 0) |
				((uint) buffer[offset + 1] << 8) |
				((uint) buffer[offset + 2] << 16) |
				((uint) buffer[offset + 3] << 24);
		}

		/// <summary>
		/// Decodes a little-endian byte sequence as a 32-bit signed integer.
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
				(buffer[offset + 0] << 0) |
				(buffer[offset + 1] << 8) |
				(buffer[offset + 2] << 16) |
				(buffer[offset + 3] << 24);
		}

		/// <summary>
		/// Decodes a little-endian byte sequence as a 64-bit unsigned integer.
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
				((ulong) buffer[offset + 0] << 0) |
				((ulong) buffer[offset + 1] << 8) |
				((ulong) buffer[offset + 2] << 16) |
				((ulong) buffer[offset + 3] << 24) |
				((ulong) buffer[offset + 4] << 32) |
				((ulong) buffer[offset + 5] << 40) |
				((ulong) buffer[offset + 6] << 48) |
				((ulong) buffer[offset + 7] << 56);
		}

		/// <summary>
		/// Decodes a little-endian byte sequence as a 64-bit signed integer.
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
				((long) buffer[offset + 0] << 0) |
				((long) buffer[offset + 1] << 8) |
				((long) buffer[offset + 2] << 16) |
				((long) buffer[offset + 3] << 24) |
				((long) buffer[offset + 4] << 32) |
				((long) buffer[offset + 5] << 40) |
				((long) buffer[offset + 6] << 48) |
				((long) buffer[offset + 7] << 56);
		}

		/// <summary>
		/// Encodes a 16-bit unsigned integer as a little-endian byte sequence.
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
			buffer[offset + 0] = (byte) (value >> 0);
			buffer[offset + 1] = (byte) (value >> 8);
		}

		/// <summary>
		/// Encodes a 16-bit signed integer as a little-endian byte sequence.
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
			buffer[offset + 0] = (byte) (value >> 0);
			buffer[offset + 1] = (byte) (value >> 8);
		}

		/// <summary>
		/// Encodes a 24-bit unsigned integer as a little-endian byte sequence.
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
			buffer[offset + 0] = (byte) (value >> 0);
			buffer[offset + 1] = (byte) (value >> 8);
			buffer[offset + 2] = (byte) (value >> 16);
		}

		/// <summary>
		/// Encodes a 24-bit signed integer as a little-endian byte sequence.
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
			buffer[offset + 0] = (byte) (value >> 0);
			buffer[offset + 1] = (byte) (value >> 8);
			buffer[offset + 2] = (byte) (value >> 16);
		}

		/// <summary>
		/// Encodes a 32-bit unsigned integer as a little-endian byte sequence.
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
			buffer[offset + 0] = (byte) (value >> 0);
			buffer[offset + 1] = (byte) (value >> 8);
			buffer[offset + 2] = (byte) (value >> 16);
			buffer[offset + 3] = (byte) (value >> 24);
		}

		/// <summary>
		/// Encodes a 32-bit signed integer as a little-endian byte sequence.
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
			buffer[offset + 0] = (byte) (value >> 0);
			buffer[offset + 1] = (byte) (value >> 8);
			buffer[offset + 2] = (byte) (value >> 16);
			buffer[offset + 3] = (byte) (value >> 24);
		}


		/// <summary>
		/// Encodes a 64-bit unsigned integer as a little-endian byte sequence.
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
			buffer[offset + 0] = (byte) (value >> 0);
			buffer[offset + 1] = (byte) (value >> 8);
			buffer[offset + 2] = (byte) (value >> 16);
			buffer[offset + 3] = (byte) (value >> 24);
			buffer[offset + 4] = (byte) (value >> 32);
			buffer[offset + 5] = (byte) (value >> 40);
			buffer[offset + 6] = (byte) (value >> 48);
			buffer[offset + 7] = (byte) (value >> 56);
		}

		/// <summary>
		/// Encodes a 64-bit signed integer as a little-endian byte sequence.
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
			buffer[offset + 0] = (byte) (value >> 0);
			buffer[offset + 1] = (byte) (value >> 8);
			buffer[offset + 2] = (byte) (value >> 16);
			buffer[offset + 3] = (byte) (value >> 24);
			buffer[offset + 4] = (byte) (value >> 32);
			buffer[offset + 5] = (byte) (value >> 40);
			buffer[offset + 6] = (byte) (value >> 48);
			buffer[offset + 7] = (byte) (value >> 56);
		}
	}
}