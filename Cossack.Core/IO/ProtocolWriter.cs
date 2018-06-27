//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using Cossack.Core.Data;
using System;

namespace Cossack.Core.IO
{
	/// <summary>
	/// A writable data stream into which callers can write sequences of primitive elements. The
	/// caller can also "mark" regions of the stream and update the values at the marked locations
	/// after that location has been passed.
	/// </summary>

	public class ProtocolWriter
	{
		/// <summary>
		/// Marks a region at which a 8-bit unsigned integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		public ProtocolWriterPlaceholder<byte> MarkU8()
		{
			GrowToAccommodate(1);
			int position = _bytesInBuffer++;
			return new ProtocolWriterPlaceholder<byte>(this, x => _buffer[position] = x, position, 1, byte.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 8-bit signed integer can be written later.
		/// </summary>
		///
		///
		/// <returns>An object which can be used to set the value later.</returns>

		[CLSCompliant(false)]
		public ProtocolWriterPlaceholder<sbyte> MarkS8()
		{
			GrowToAccommodate(1);
			int position = _bytesInBuffer++;
			return new ProtocolWriterPlaceholder<sbyte>(this, x => _buffer[position] = (byte) x, position, 1, (ulong) sbyte.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 16-bit big-endian unsigned integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		[CLSCompliant(false)]
		public ProtocolWriterPlaceholder<ushort> MarkU16BE()
		{
			GrowToAccommodate(2);
			int position = _bytesInBuffer;
			_bytesInBuffer += 2;
			return new ProtocolWriterPlaceholder<ushort>(this, x => BigEndian.WriteU16(_buffer, position, x), position, 2, ushort.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 16-bit little-endian unsigned integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		[CLSCompliant(false)]
		public ProtocolWriterPlaceholder<ushort> MarkU16LE()
		{
			GrowToAccommodate(2);
			int position = _bytesInBuffer;
			_bytesInBuffer += 2;
			return new ProtocolWriterPlaceholder<ushort>(this, x => LittleEndian.WriteU16(_buffer, position, x), position, 2, ushort.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 16-bit big-endian signed integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		public ProtocolWriterPlaceholder<short> MarkS16BE()
		{
			GrowToAccommodate(2);
			int position = _bytesInBuffer;
			_bytesInBuffer += 2;
			return new ProtocolWriterPlaceholder<short>(this, x => BigEndian.WriteS16(_buffer, position, x), position, 2, (ulong) short.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 16-bit little-endian signed integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		public ProtocolWriterPlaceholder<short> MarkS16LE()
		{
			GrowToAccommodate(2);
			int position = _bytesInBuffer;
			_bytesInBuffer += 2;
			return new ProtocolWriterPlaceholder<short>(this, x => LittleEndian.WriteS16(_buffer, position, x), position, 2, (ulong) short.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 24-bit big-endian unsigned integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		[CLSCompliant(false)]
		public ProtocolWriterPlaceholder<uint> MarkU24BE()
		{
			GrowToAccommodate(3);
			int position = _bytesInBuffer;
			_bytesInBuffer += 3;
			return new ProtocolWriterPlaceholder<uint>(this, x => BigEndian.WriteU24(_buffer, position, x), position, 3, 0xffffff);
		}

		/// <summary>
		/// Marks a region at which a 24-bit little-endian unsigned integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		[CLSCompliant(false)]
		public ProtocolWriterPlaceholder<uint> MarkU24LE()
		{
			GrowToAccommodate(3);
			int position = _bytesInBuffer;
			_bytesInBuffer += 3;
			return new ProtocolWriterPlaceholder<uint>(this, x => LittleEndian.WriteU24(_buffer, position, x), position, 3, 0xffffff);
		}

		/// <summary>
		/// Marks a region at which a 24-bit big-endian signed integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		public ProtocolWriterPlaceholder<int> MarkS24BE()
		{
			GrowToAccommodate(3);
			int position = _bytesInBuffer;
			_bytesInBuffer += 3;
			return new ProtocolWriterPlaceholder<int>(this, x => BigEndian.WriteS24(_buffer, position, x), position, 3, 0x7fffff);
		}

		/// <summary>
		/// Marks a region at which a 24-bit little-endian signed integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		public ProtocolWriterPlaceholder<int> MarkS24LE()
		{
			GrowToAccommodate(3);
			int position = _bytesInBuffer;
			_bytesInBuffer += 3;
			return new ProtocolWriterPlaceholder<int>(this, x => LittleEndian.WriteS24(_buffer, position, x), position, 3, 0x7fffff);
		}

		/// <summary>
		/// Marks a region at which a 32-bit big-endian unsigned integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		[CLSCompliant(false)]
		public ProtocolWriterPlaceholder<uint> MarkU32BE()
		{
			GrowToAccommodate(4);
			int position = _bytesInBuffer;
			_bytesInBuffer += 4;
			return new ProtocolWriterPlaceholder<uint>(this, x => BigEndian.WriteU32(_buffer, position, x), position, 4, uint.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 32-bit little-endian unsigned integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		[CLSCompliant(false)]
		public ProtocolWriterPlaceholder<uint> MarkU32LE()
		{
			GrowToAccommodate(4);
			int position = _bytesInBuffer;
			_bytesInBuffer += 4;
			return new ProtocolWriterPlaceholder<uint>(this, x => LittleEndian.WriteU32(_buffer, position, x), position, 4, uint.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 32-bit big-endian signed integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		public ProtocolWriterPlaceholder<int> MarkS32BE()
		{
			GrowToAccommodate(4);
			int position = _bytesInBuffer;
			_bytesInBuffer += 4;
			return new ProtocolWriterPlaceholder<int>(this, x => BigEndian.WriteS32(_buffer, position, x), position, 4, int.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 32-bit little-endian signed integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		public ProtocolWriterPlaceholder<int> MarkS32LE()
		{
			GrowToAccommodate(4);
			int position = _bytesInBuffer;
			_bytesInBuffer += 4;
			return new ProtocolWriterPlaceholder<int>(this, x => LittleEndian.WriteS32(_buffer, position, x), position, 4, int.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 64-bit big-endian unsigned integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		[CLSCompliant(false)]
		public ProtocolWriterPlaceholder<ulong> MarkU64BE()
		{
			GrowToAccommodate(8);
			int position = _bytesInBuffer;
			_bytesInBuffer += 8;
			return new ProtocolWriterPlaceholder<ulong>(this, x => BigEndian.WriteU64(_buffer, position, x), position, 8, ulong.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 64-bit little-endian unsigned integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		[CLSCompliant(false)]
		public ProtocolWriterPlaceholder<ulong> MarkU64LE()
		{
			GrowToAccommodate(8);
			int position = _bytesInBuffer;
			_bytesInBuffer += 8;
			return new ProtocolWriterPlaceholder<ulong>(this, x => LittleEndian.WriteU64(_buffer, position, x), position, 8, ulong.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 64-bit big-endian signed integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		public ProtocolWriterPlaceholder<long> MarkS64BE()
		{
			GrowToAccommodate(8);
			int position = _bytesInBuffer;
			_bytesInBuffer += 8;
			return new ProtocolWriterPlaceholder<long>(this, x => BigEndian.WriteS64(_buffer, position, x), position, 8, long.MaxValue);
		}

		/// <summary>
		/// Marks a region at which a 64-bit little-endian signed integer can be written later.
		/// </summary>
		///
		/// <returns>An object which can be used to set the value later.</returns>

		public ProtocolWriterPlaceholder<long> MarkS64LE()
		{
			GrowToAccommodate(8);
			int position = _bytesInBuffer;
			_bytesInBuffer += 8;
			return new ProtocolWriterPlaceholder<long>(this, x => LittleEndian.WriteS64(_buffer, position, x), position, 8, long.MaxValue);
		}

		/// <summary>
		/// Writes an 8-bit unsigned integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		public void WriteU8(byte value)
		{
			GrowToAccommodate(1);
			_buffer[_bytesInBuffer++] = value;
		}

		/// <summary>
		/// Writes an 8-bit signed integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		[CLSCompliant(false)]
		public void WriteS8(sbyte value)
		{
			GrowToAccommodate(1);
			_buffer[_bytesInBuffer++] = (byte) value;
		}

		/// <summary>
		/// Writes a 16-bit big-endian unsigned integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		[CLSCompliant(false)]
		public void WriteU16BE(ushort value)
		{
			GrowToAccommodate(2);
			BigEndian.WriteU16(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 2;
		}

		/// <summary>
		/// Writes a 16-bit little-endian unsigned integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		[CLSCompliant(false)]
		public void WriteU16LE(ushort value)
		{
			GrowToAccommodate(2);
			LittleEndian.WriteU16(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 2;
		}

		/// <summary>
		/// Writes a 16-bit big-endian signed integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		public void WriteS16BE(short value)
		{
			GrowToAccommodate(2);
			BigEndian.WriteS16(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 2;
		}

		/// <summary>
		/// Writes a 16-bit little-endian signed integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		public void WriteS16LE(short value)
		{
			GrowToAccommodate(2);
			LittleEndian.WriteS16(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 2;
		}

		/// <summary>
		/// Writes a 24-bit big-endian unsigned integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		[CLSCompliant(false)]
		public void WriteU24BE(uint value)
		{
			GrowToAccommodate(3);
			BigEndian.WriteU24(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 3;
		}

		/// <summary>
		/// Writes a 24-bit little-endian unsigned integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		[CLSCompliant(false)]
		public void WriteU24LE(uint value)
		{
			GrowToAccommodate(3);
			LittleEndian.WriteU24(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 3;
		}

		/// <summary>
		/// Writes a 24-bit big-endian signed integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		public void WriteS24BE(int value)
		{
			GrowToAccommodate(3);
			BigEndian.WriteS24(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 3;
		}

		/// <summary>
		/// Writes a 24-bit little-endian signed integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		public void WriteS24LE(int value)
		{
			GrowToAccommodate(3);
			LittleEndian.WriteS24(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 3;
		}

		/// <summary>
		/// Writes a 32-bit big-endian unsigned integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		[CLSCompliant(false)]
		public void WriteU32BE(uint value)
		{
			GrowToAccommodate(4);
			BigEndian.WriteU32(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 4;
		}

		/// <summary>
		/// Writes a 32-bit little-endian unsigned integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		[CLSCompliant(false)]
		public void WriteU32LE(uint value)
		{
			GrowToAccommodate(4);
			LittleEndian.WriteU32(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 4;
		}

		/// <summary>
		/// Writes a 32-bit big-endian signed integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		public void WriteS32BE(int value)
		{
			GrowToAccommodate(4);
			BigEndian.WriteS32(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 4;
		}

		/// <summary>
		/// Writes a 32-bit little-endian signed integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		public void WriteS32LE(int value)
		{
			GrowToAccommodate(4);
			LittleEndian.WriteS32(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 4;
		}

		/// <summary>
		/// Writes a 64-bit big-endian unsigned integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		[CLSCompliant(false)]
		public void WriteU64BE(ulong value)
		{
			GrowToAccommodate(8);
			BigEndian.WriteU64(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 8;
		}

		/// <summary>
		/// Writes a 64-bit little-endian unsigned integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		[CLSCompliant(false)]
		public void WriteU64LE(ulong value)
		{
			GrowToAccommodate(8);
			LittleEndian.WriteU64(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 8;
		}

		/// <summary>
		/// Writes a 64-bit big-endian signed integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		public void WriteS64BE(long value)
		{
			GrowToAccommodate(8);
			BigEndian.WriteS64(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 8;
		}

		/// <summary>
		/// Writes a 64-bit little-endian signed integer to the stream.
		/// </summary>
		///
		/// <param name="value">The value to write.</param>

		public void WriteS64LE(long value)
		{
			GrowToAccommodate(8);
			LittleEndian.WriteS64(_buffer, _bytesInBuffer, value);
			_bytesInBuffer += 8;
		}

		/// <summary>
		/// Writes an array of data to the stream.
		/// </summary>
		///
		/// <param name="data">The data to write.</param>
		///
		/// <exception cref="NullReferenceException"><paramref name="data"/> is
		///     <c>null</c>.</exception>

		public void Write(byte[] data) => Write(data, 0, data.Length);

		/// <summary>
		/// Writes an array of data to the stream.
		/// </summary>
		///
		/// <param name="data">The data to write.</param>

		public void Write(ArraySegment<byte> data) => Write(data.Array, data.Offset, data.Count);

		/// <summary>
		/// Writes an array of data to the stream.
		/// </summary>
		///
		/// <param name="data">The data to write.</param>
		/// <param name="offset">The index of the first byte in <paramref name="data"/> to
		///     write.</param>
		/// <param name="count">The number of bytes to write.</param>
		///
		/// <exception cref="IndexOutOfRangeException"><paramref name="offset"/> and
		///     <paramref name="count"/> are outside the bounds of the array.</exception>
		/// <exception cref="NullReferenceException"><paramref name="data"/> is
		///     <c>null</c>.</exception>

		public void Write(byte[] data, int offset, int count)
		{
			GrowToAccommodate(count);
			Array.Copy(data, offset, _buffer, _bytesInBuffer, count);
			_bytesInBuffer += count;
		}

		/// <summary>
		/// Copies the encoded protocol data to a new byte array.
		/// </summary>
		///
		/// <returns>A byte array containing the encoded protocol data.</returns>

		public byte[] ToArray()
		{
			byte[] data = new byte[_bytesInBuffer];
			Array.Copy(_buffer, 0, data, 0, _bytesInBuffer);
			return data;
		}

		/// <summary>
		/// Clears the protocol stream.
		/// </summary>

		public void Reset()
		{
			_bytesInBuffer = 0;
		}

		/// <summary>
		/// Gets an <see cref="ArraySegment{T}"/> containing the encoded protocol data. The
		/// contents of the array are invalidated and may be modified by any subsequent member
		/// method call or property access.
		/// </summary>

		public ArraySegment<byte> Data => new ArraySegment<byte>(_buffer, 0, _bytesInBuffer);

		/// <summary>
		/// Gets the current stream position, i.e. the number of bytes written so far.
		/// </summary>

		public int Position => _bytesInBuffer;

		private void GrowToAccommodate(int requiredBytes)
		{
			int capacity = _buffer.Length;

			if (_bytesInBuffer + requiredBytes <= capacity)
				return;

			do { capacity *= 2; }
			while (_bytesInBuffer + requiredBytes > capacity);

			byte[] newBuffer = new byte[capacity];
			Array.Copy(_buffer, 0, newBuffer, 0, _bytesInBuffer);
			_buffer = newBuffer;
		}

		private byte[] _buffer = new byte[1024];
		private int _bytesInBuffer = 0;
	}
}