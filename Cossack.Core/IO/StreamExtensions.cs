﻿//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//
using Cossack.Core.Miscellaneous;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Cossack.Core.IO
{
	/// <summary>
	/// Contains extension methods for the <see cref="Stream"/> class.
	/// </summary>

	public static class StreamExtensions
	{
		/// <summary>
		/// Reads exactly the specified number of bytes from a stream.
		/// </summary>
		///
		/// <param name="stream">The stream to read from.</param>
		/// <param name="count">The number of bytes to read.</param>
		///
		/// <returns>The data array.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is
		///     negative.</exception>
		/// <exception cref="EndOfStreamException">The end of the stream was reached before the
		///     requested number of bytes were read.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="NotSupportedException">The stream does not support 
		///     reading.</exception>
		/// <exception cref="ObjectDisposedException">The stream object has been
		///     disposed.</exception>

		public static byte[] ReadExactly(this Stream stream, int count)
			=> stream.ReadExactly(count, true);

		/// <summary>
		/// Reads exactly the specified number of bytes from a stream.
		/// </summary>
		///
		/// <param name="stream">The stream to read from.</param>
		/// <param name="buffer">The array in which to store bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="count">The number of bytes to store.</param>
		///
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> or
		///     <paramref name="buffer"/> are <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The specified
		///     <paramref name="offset"/> and <paramref name="count"/> fall outside the bounds of
		///     the <paramref name="buffer"/> array.</exception>
		/// <exception cref="EndOfStreamException">The end of the stream was reached before the
		///     requested number of bytes were read.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="NotSupportedException">The stream does not support 
		///     reading.</exception>
		/// <exception cref="ObjectDisposedException">The stream object has been
		///     disposed.</exception>

		public static void ReadExactly(this Stream stream, byte[] buffer, int offset, int count)
			=> stream.ReadExactly(buffer, offset, count, true);

		/// <summary>
		/// Reads exactly the specified number of bytes from a stream.
		/// </summary>
		///
		/// <param name="stream">The stream to read from.</param>
		/// <param name="count">The number of bytes to read.</param>
		/// <param name="throwIfNoData"><c>true</c> to throw an exception if the end of the stream
		///     is reached before reading any data. If <c>false</c>, this method returns
		///     <c>null</c> instead of throwing an exception. An exception is always thrown if
		///     one or more bytes were read before reaching the end of the stream.</param>
		///
		/// <returns>The data array if the requested number of bytes were read from the underlying
		///     stream, or <c>null</c> if the end of the stream was reached without reading any
		///     bytes and <paramref name="throwIfNoData"/> is <c>true</c>.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is
		///     negative.</exception>
		/// <exception cref="EndOfStreamException">The end of the stream was reached before the
		///     requested number of bytes were read.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="NotSupportedException">The stream does not support 
		///     reading.</exception>
		/// <exception cref="ObjectDisposedException">The stream object has been
		///     disposed.</exception>

		public static byte[] ReadExactly(this Stream stream, int count, bool throwIfNoData)
		{
			byte[] data = new byte[count];
			if (!stream.ReadExactly(data, 0, count, throwIfNoData)) return null;
			return data;
		}

		/// <summary>
		/// Reads exactly the specified number of bytes from a stream.
		/// </summary>
		///
		/// <param name="stream">The stream to read from.</param>
		/// <param name="buffer">The array in which to store bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="count">The number of bytes to store.</param>
		/// <param name="throwIfNoData"><c>true</c> to throw an exception if the end of the stream
		///     is reached before reading any data. If <c>false</c>, this method returns
		///     <c>false</c> instead of throwing an exception. An exception is always thrown if
		///     one or more bytes were read before reaching the end of the stream.</param>
		///
		/// <returns><c>true</c> if the requested number of bytes were read from the underlying
		///     stream, or <c>false</c> if the end of the stream was reached without reading any
		///     bytes and <paramref name="throwIfNoData"/> is <c>true</c>.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> or
		///     <paramref name="buffer"/> are <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The specified
		///     <paramref name="offset"/> and <paramref name="count"/> fall outside the bounds of
		///     the <paramref name="buffer"/> array.</exception>
		/// <exception cref="EndOfStreamException">The end of the stream was reached before the
		///     requested number of bytes were read, and <paramref name="throwIfNoData"/> is
		///     <c>true</c>.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="NotSupportedException">The stream does not support 
		///     reading.</exception>
		/// <exception cref="ObjectDisposedException">The stream object has been
		///     disposed.</exception>

		public static bool ReadExactly(this Stream stream, byte[] buffer, int offset, int count, bool throwIfNoData)
		{
			if (stream == null)
				throw new ArgumentNullException(nameof(stream));

			ValidationUtilities.ValidateArraySlice(buffer, offset, count,
				nameof(buffer), nameof(offset), nameof(count));

			int totalBytesRead = 0;

			while (count > 0)
			{
				int bytesRead = stream.Read(buffer, offset, count);
				Debug.Assert(bytesRead >= 0 && bytesRead <= count);

				if (bytesRead == 0)
				{
					if (!throwIfNoData && totalBytesRead == 0) return false;
					throw new EndOfStreamException();
				}

				count -= bytesRead;
				offset += bytesRead;
				totalBytesRead += bytesRead;
			}

			return true;
		}

		/// <summary>
		/// Reads exactly the specified number of bytes from a stream.
		/// </summary>
		///
		/// <param name="stream">The stream to read from.</param>
		/// <param name="count">The number of bytes to read.</param>
		///
		/// <returns>The data array.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is
		///     negative.</exception>
		/// <exception cref="EndOfStreamException">The end of the stream was reached before the
		///     requested number of bytes were read.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="NotSupportedException">The stream does not support 
		///     reading.</exception>
		/// <exception cref="ObjectDisposedException">The stream object has been
		///     disposed.</exception>

		public static Task<byte[]> ReadExactlyAsync(this Stream stream, int count)
			=> stream.ReadExactlyAsync(count, true, CancellationToken.None);

		/// <summary>
		/// Reads exactly the specified number of bytes from a stream.
		/// </summary>
		///
		/// <param name="stream">The stream to read from.</param>
		/// <param name="buffer">The array in which to store bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="count">The number of bytes to store.</param>
		///
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> or
		///     <paramref name="buffer"/> are <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The specified
		///     <paramref name="offset"/> and <paramref name="count"/> fall outside the bounds of
		///     the <paramref name="buffer"/> array.</exception>
		/// <exception cref="EndOfStreamException">The end of the stream was reached before the
		///     requested number of bytes were read.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="NotSupportedException">The stream does not support 
		///     reading.</exception>
		/// <exception cref="ObjectDisposedException">The stream object has been
		///     disposed.</exception>

		public static Task ReadExactlyAsync(this Stream stream,
			byte[] buffer, int offset, int count)
			=> stream.ReadExactlyAsync(buffer, offset, count,
				true, CancellationToken.None);

		/// <summary>
		/// Reads exactly the specified number of bytes from a stream.
		/// </summary>
		///
		/// <param name="stream">The stream to read from.</param>
		/// <param name="count">The number of bytes to store.</param>
		/// <param name="cancellationToken">A cancellation token.</param>
		///
		/// <returns>The data array.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is
		///     negative.</exception>
		/// <exception cref="EndOfStreamException">The end of the stream was reached before the
		///     requested number of bytes were read.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="NotSupportedException">The stream does not support 
		///     reading.</exception>
		/// <exception cref="ObjectDisposedException">The stream object has been
		///     disposed.</exception>

		public static Task<byte[]> ReadExactlyAsync(this Stream stream,
			int count, CancellationToken cancellationToken)
			=> stream.ReadExactlyAsync(count, true, cancellationToken);

		/// <summary>
		/// Reads exactly the specified number of bytes from a stream.
		/// </summary>
		///
		/// <param name="stream">The stream to read from.</param>
		/// <param name="buffer">The array in which to store bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="count">The number of bytes to store.</param>
		/// <param name="cancellationToken">A cancellation token.</param>
		///
		/// <exception cref="ArgumentNullException"><paramref name="stream"/>,
		///     <paramref name="buffer"/> or <paramref name="cancellationToken"/> are
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The specified
		///     <paramref name="offset"/> and <paramref name="count"/> fall outside the bounds of
		///     the <paramref name="buffer"/> array.</exception>
		/// <exception cref="EndOfStreamException">The end of the stream was reached before the
		///     requested number of bytes were read.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="NotSupportedException">The stream does not support 
		///     reading.</exception>
		/// <exception cref="ObjectDisposedException">The stream object has been
		///     disposed.</exception>

		public static Task ReadExactlyAsync(this Stream stream,
			byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			=> stream.ReadExactlyAsync(buffer, offset, count, true, cancellationToken);

		/// <summary>
		/// Reads exactly the specified number of bytes from a stream.
		/// </summary>
		///
		/// <param name="stream">The stream to read from.</param>
		/// <param name="count">The number of bytes to store.</param>
		/// <param name="throwIfNoData"><c>true</c> to throw an exception if the end of the stream
		///     is reached before reading any data. If <c>false</c>, this method returns
		///     <c>false</c> instead of throwing an exception. An exception is always thrown if
		///     one or more bytes were read before reaching the end of the stream.</param>
		///
		/// <returns>The data array if the requested number of bytes were read from the underlying
		///     stream, or <c>null</c> if the end of the stream was reached without reading any
		///     bytes and <paramref name="throwIfNoData"/> is <c>true</c>.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is
		///     negative.</exception>
		/// <exception cref="EndOfStreamException">The end of the stream was reached before the
		///     requested number of bytes were read, and <paramref name="throwIfNoData"/> is
		///     <c>true</c>.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="NotSupportedException">The stream does not support 
		///     reading.</exception>
		/// <exception cref="ObjectDisposedException">The stream object has been
		///     disposed.</exception>

		public static Task<byte[]> ReadExactlyAsync(this Stream stream,
			int count, bool throwIfNoData)
			=> stream.ReadExactlyAsync(count, throwIfNoData, CancellationToken.None);

		/// <summary>
		/// Reads exactly the specified number of bytes from a stream.
		/// </summary>
		///
		/// <param name="stream">The stream to read from.</param>
		/// <param name="buffer">The array in which to store bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="count">The number of bytes to store.</param>
		/// <param name="throwIfNoData"><c>true</c> to throw an exception if the end of the stream
		///     is reached before reading any data. If <c>false</c>, this method returns
		///     <c>false</c> instead of throwing an exception. An exception is always thrown if
		///     one or more bytes were read before reaching the end of the stream.</param>
		///
		/// <returns><c>true</c> if the requested number of bytes were read from the underlying
		///     stream, or <c>false</c> if the end of the stream was reached without reading any
		///     bytes and <paramref name="throwIfNoData"/> is <c>true</c>.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> or
		///     <paramref name="buffer"/> are <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The specified
		///     <paramref name="offset"/> and <paramref name="count"/> fall outside the bounds of
		///     the <paramref name="buffer"/> array.</exception>
		/// <exception cref="EndOfStreamException">The end of the stream was reached before the
		///     requested number of bytes were read, and <paramref name="throwIfNoData"/> is
		///     <c>true</c>.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="NotSupportedException">The stream does not support 
		///     reading.</exception>
		/// <exception cref="ObjectDisposedException">The stream object has been
		///     disposed.</exception>

		public static Task<bool> ReadExactlyAsync(this Stream stream,
			byte[] buffer, int offset, int count, bool throwIfNoData)
			=> stream.ReadExactlyAsync(buffer, offset, count,
				throwIfNoData, CancellationToken.None);

		/// <summary>
		/// Reads exactly the specified number of bytes from a stream.
		/// </summary>
		///
		/// <param name="stream">The stream to read from.</param>
		/// <param name="count">The number of bytes to store.</param>
		/// <param name="throwIfNoData"><c>true</c> to throw an exception if the end of the stream
		///     is reached before reading any data. If <c>false</c>, this method returns
		///     <c>false</c> instead of throwing an exception. An exception is always thrown if
		///     one or more bytes were read before reaching the end of the stream.</param>
		/// <param name="cancellationToken">A cancellation token.</param>
		///
		/// <returns>The data array if the requested number of bytes were read from the underlying
		///     stream, or <c>null</c> if the end of the stream was reached without reading any
		///     bytes and <paramref name="throwIfNoData"/> is <c>true</c>.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is
		///     negative.</exception>
		/// <exception cref="EndOfStreamException">The end of the stream was reached before the
		///     requested number of bytes were read, and <paramref name="throwIfNoData"/> is
		///     <c>true</c>.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="NotSupportedException">The stream does not support 
		///     reading.</exception>
		/// <exception cref="ObjectDisposedException">The stream object has been
		///     disposed.</exception>

		public static async Task<byte[]> ReadExactlyAsync(this Stream stream,
			int count, bool throwIfNoData, CancellationToken cancellationToken)
		{
			byte[] data = new byte[count];
			if (!await ReadExactlyAsync(stream, data, 0, count, throwIfNoData,
				cancellationToken).ConfigureAwait(false)) return null;
			return data;
		}

		/// <summary>
		/// Reads exactly the specified number of bytes from a stream.
		/// </summary>
		///
		/// <param name="stream">The stream to read from.</param>
		/// <param name="buffer">The array in which to store bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="count">The number of bytes to store.</param>
		/// <param name="throwIfNoData"><c>true</c> to throw an exception if the end of the stream
		///     is reached before reading any data. If <c>false</c>, this method returns
		///     <c>false</c> instead of throwing an exception. An exception is always thrown if
		///     one or more bytes were read before reaching the end of the stream.</param>
		/// <param name="cancellationToken">A cancellation token.</param>
		///
		/// <returns><c>true</c> if the requested number of bytes were read from the underlying
		///     stream, or <c>false</c> if the end of the stream was reached without reading any
		///     bytes and <paramref name="throwIfNoData"/> is <c>true</c>.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="stream"/>,
		///     <paramref name="buffer"/> or <paramref name="cancellationToken"/> are
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The specified
		///     <paramref name="offset"/> and <paramref name="count"/> fall outside the bounds of
		///     the <paramref name="buffer"/> array.</exception>
		/// <exception cref="EndOfStreamException">The end of the stream was reached before the
		///     requested number of bytes were read, and <paramref name="throwIfNoData"/> is
		///     <c>true</c>.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="NotSupportedException">The stream does not support 
		///     reading.</exception>
		/// <exception cref="ObjectDisposedException">The stream object has been
		///     disposed.</exception>

		public static async Task<bool> ReadExactlyAsync(this Stream stream,
			byte[] buffer, int offset, int count, bool throwIfNoData,
			CancellationToken cancellationToken)
		{
			if (stream == null)
				throw new ArgumentNullException(nameof(stream));
			if (cancellationToken == null)
				throw new ArgumentNullException(nameof(cancellationToken));

			ValidationUtilities.ValidateArraySlice(buffer, offset, count,
				nameof(buffer), nameof(offset), nameof(count));

			int totalBytesRead = 0;

			while (count > 0)
			{
				int bytesRead = await stream.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
				Debug.Assert(bytesRead >= 0 && bytesRead <= count);

				if (bytesRead == 0)
				{
					if (!throwIfNoData && totalBytesRead == 0) return false;
					throw new EndOfStreamException();
				}

				count -= bytesRead;
				offset += bytesRead;
				totalBytesRead += bytesRead;
			}

			return true;
		}
	}
}