//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using Cossack.Core.Data;
using Cossack.Core.Miscellaneous;
using System;

namespace Cossack.Core.IO
{
	/// <summary>
	/// A readable data stream which can be read as a sequence of primitives.
	/// </summary>

	public class ProtocolReader
    {
		/// <summary>
		/// Initializes a new data stream using the specified data.
		/// </summary>
		///
		/// <param name="data">The data to wrap.</param>

		public ProtocolReader(ArraySegment<byte> data)
		{
			_buffer = data.Array;
			_position = _originalPosition = data.Offset;
			_endPosition = data.Offset + data.Count;
		}

		/// <summary>
		/// Initializes a new data stream using the specified data.
		/// </summary>
		///
		/// <param name="data">The array to wrap.</param>
		///
		/// <exception cref="ArgumentNullException"><paramref name="data"/> is
		///     <c>null</c>.</exception>

		public ProtocolReader(byte[] data)
		{
			_buffer = data ?? throw new ArgumentNullException(nameof(data));
			_endPosition = data.Length;
		}

		/// <summary>
		/// Initializes a new data stream using the specified data.
		/// </summary>
		///
		/// <param name="data">The array to wrap.</param>
		/// <param name="offset">The index of the first byte to wrap.</param>
		/// <param name="count">The number of bytes to wrap.</param>
		///
		/// <exception cref="ArgumentNullException"><paramref name="data"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> and/or
		///      <paramref name="count"/> fall outside the bounds of the array.</exception>

		public ProtocolReader(byte[] data, int offset, int count)
		{
			ValidationUtilities.ValidateArraySlice(data, offset, count,
				nameof(data), nameof(offset), nameof(count));

			_buffer = data;
			_position = _originalPosition = offset;
			_endPosition = offset + count;
		}

		/// <summary>
		/// Reads an 8-bit unsigned integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		public byte ReadU8()
		{
			ThrowIfNotAvailable(1);
			return _buffer[_position++];
		}

		/// <summary>
		/// Reads an 8-bit signed integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		[CLSCompliant(false)]
		public sbyte ReadS8()
		{
			ThrowIfNotAvailable(1);
			return (sbyte) _buffer[_position++];
		}

		/// <summary>
		/// Reads a 16-bit big-endian unsigned integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		[CLSCompliant(false)]
		public ushort ReadU16BE()
		{
			ThrowIfNotAvailable(2);
			ushort value = BigEndian.ReadU16(_buffer, _position);
			_position += 2;
			return value;
		}

		/// <summary>
		/// Reads a 16-bit little-endian unsigned integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		[CLSCompliant(false)]
		public ushort ReadU16LE()
		{
			ThrowIfNotAvailable(2);
			ushort value = LittleEndian.ReadU16(_buffer, _position);
			_position += 2;
			return value;
		}

		/// <summary>
		/// Reads a 16-bit big-endian signed integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		public short ReadS16BE()
		{
			ThrowIfNotAvailable(2);
			short value = BigEndian.ReadS16(_buffer, _position);
			_position += 2;
			return value;
		}

		/// <summary>
		/// Reads a 16-bit little-endian signed integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		public short ReadS16LE()
		{
			ThrowIfNotAvailable(2);
			short value = LittleEndian.ReadS16(_buffer, _position);
			_position += 2;
			return value;
		}

		/// <summary>
		/// Reads a 24-bit big-endian unsigned integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		[CLSCompliant(false)]
		public uint ReadU24BE()
		{
			ThrowIfNotAvailable(3);
			uint value = BigEndian.ReadU24(_buffer, _position);
			_position += 3;
			return value;
		}

		/// <summary>
		/// Reads a 24-bit little-endian unsigned integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		[CLSCompliant(false)]
		public uint ReadU24LE()
		{
			ThrowIfNotAvailable(3);
			uint value = LittleEndian.ReadU24(_buffer, _position);
			_position += 3;
			return value;
		}

		/// <summary>
		/// Reads a 24-bit big-endian signed integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		public int ReadS24BE()
		{
			ThrowIfNotAvailable(3);
			int value = BigEndian.ReadS24(_buffer, _position);
			_position += 3;
			return value;
		}

		/// <summary>
		/// Reads a 24-bit little-endian signed integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		public int ReadS24LE()
		{
			ThrowIfNotAvailable(3);
			int value = LittleEndian.ReadS24(_buffer, _position);
			_position += 3;
			return value;
		}

		/// <summary>
		/// v32-bit big-endian unsigned integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		[CLSCompliant(false)]
		public uint ReadU32BE()
		{
			ThrowIfNotAvailable(4);
			uint value = BigEndian.ReadU32(_buffer, _position);
			_position += 4;
			return value;
		}

		/// <summary>
		/// Reads a 32-bit little-endian unsigned integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		[CLSCompliant(false)]
		public uint ReadU32LE()
		{
			ThrowIfNotAvailable(4);
			uint value = LittleEndian.ReadU32(_buffer, _position);
			_position += 4;
			return value;
		}

		/// <summary>
		/// Reads a 32-bit big-endian signed integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		public int ReadS32BE()
		{
			ThrowIfNotAvailable(4);
			int value = BigEndian.ReadS32(_buffer, _position);
			_position += 4;
			return value;
		}

		/// <summary>
		/// Reads a 32-bit little-endian signed integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		public int ReadS32LE()
		{
			ThrowIfNotAvailable(4);
			int value = LittleEndian.ReadS32(_buffer, _position);
			_position += 4;
			return value;
		}

		/// <summary>
		/// Reads a 64-bit big-endian unsigned integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		[CLSCompliant(false)]
		public ulong ReadU64BE()
		{
			ThrowIfNotAvailable(8);
			ulong value = BigEndian.ReadU64(_buffer, _position);
			_position += 8;
			return value;
		}

		/// <summary>
		/// Reads a 64-bit little-endian unsigned integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		[CLSCompliant(false)]
		public ulong ReadU64LE()
		{
			ThrowIfNotAvailable(8);
			ulong value = LittleEndian.ReadU64(_buffer, _position);
			_position += 8;
			return value;
		}

		/// <summary>
		/// Reads a 64-bit big-endian signed integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		public long ReadS64BE()
		{
			ThrowIfNotAvailable(8);
			long value = BigEndian.ReadS64(_buffer, _position);
			_position += 8;
			return value;
		}

		/// <summary>
		/// Reads a 64-bit little-endian signed integer from the stream.
		/// </summary>
		///
		/// <returns>The value.</returns>
		///
		/// <exception cref="IndexOutOfRangeException">This read would read past the end of the
		///     buffer.</exception>

		public long ReadS64LE()
		{
			ThrowIfNotAvailable(8);
			long value = LittleEndian.ReadS64(_buffer, _position);
			_position += 8;
			return value;
		}

		/// <summary>
		/// Reads a specified number of bytes from the stream and returns them a new byte array.
		/// </summary>
		///
		/// <param name="bytes">The number of bytes to read.</param>
		///
		/// <returns>A new array containing the bytes that were read.</returns>
		///
		/// <exception cref="IndexOutOfRangeException"></exception>

		public byte[] Read(int bytes)
		{
			ThrowIfNotAvailable(bytes);
			byte[] data = _buffer.Slice(_position, bytes);
			_position += bytes;
			return data;
		}

		/// <summary>
		/// Creates a new <see cref="ProtocolReader"/> representing a slice of this reader's data,
		/// while this reader advances beyond the sliced-out data.
		/// </summary>
		///
		/// <param name="bytes">The number of bytes to slice out.</param>
		///
		/// <returns>A new reader object.</returns>
		///
		/// <exception cref="IndexOutOfRangeException"></exception>

		public ProtocolReader Slice(int bytes)
		{
			ThrowIfNotAvailable(bytes);
			ProtocolReader slice = new ProtocolReader(_buffer, _position, bytes);
			_position += bytes;
			return slice;
		}

		/// <summary>
		/// Gets the number of bytes remaining in the buffer.
		/// </summary>

		public int Available => _endPosition - _position;

		/// <summary>
		/// Gets the current read position in bytes.
		/// </summary>

		public int Position => _position - _originalPosition;

		private void ThrowIfNotAvailable(int bytes)
		{
			if (_position + bytes > _endPosition)
				throw new IndexOutOfRangeException();
		}

		private readonly byte[] _buffer;
		private int _position = 0;
		private int _originalPosition = 0;
		private readonly int _endPosition;
	}
}