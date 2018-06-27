//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using Cossack.Core.Errors;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Cossack.Core.IO
{
	/// <summary>
	/// A stream that exposes a view of an underlying seekable stream as a subset of that stream's
	/// content, restricted to a specified number of bytes at a specified offset. Optionally, a
	/// lock object can be held during stream operations to allow concurrent operations on the
	/// underlying stream.
	/// </summary>

	public class SubsetStream : Stream
	{
		/// <summary>
		/// Initializes a new subset stream.
		/// </summary>
		///
		/// <param name="stream">The underlying stream.</param>
		/// <param name="offset">The offset of the first byte to expose in this subset.</param>
		/// <param name="length">The number of bytes to expose in this subset.</param>
		///
		/// <exception cref="ArgumentException"><paramref name="stream"/> is not readable or not
		///     seekable.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> or
		///     <paramref name="length"/> are outside the readable range of the underlying
		///     stream.</exception>

		public SubsetStream(Stream stream, long offset, long length)
			: this(stream, offset, length, false, null)
		{
		}

		/// <summary>
		/// Initializes a new subset stream.
		/// </summary>
		///
		/// <param name="stream">The underlying stream.</param>
		/// <param name="offset">The offset of the first byte to expose in this subset.</param>
		/// <param name="length">The number of bytes to expose in this subset.</param>
		/// <param name="locker">An optional lock object which will be held during all stream
		///     operations to allow concurrent use of the underlying stream.</param>
		///
		/// <exception cref="ArgumentException"><paramref name="stream"/> is not readable or not
		///     seekable.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> or
		///     <paramref name="length"/> are outside the readable range of the underlying
		///     stream.</exception>

		public SubsetStream(Stream stream, long offset, long length, object locker)
			: this(stream, offset, length, false, locker)
		{
		}

		/// <summary>
		/// Initializes a new subset stream.
		/// </summary>
		///
		/// <param name="stream">The underlying stream.</param>
		/// <param name="offset">The offset of the first byte to expose in this subset.</param>
		/// <param name="length">The number of bytes to expose in this subset.</param>
		/// <param name="leaveOpen">A flag specifying whether <see cref="Dispose"/> should dispose
		///     the underlying stream.</param>
		///
		/// <exception cref="ArgumentException"><paramref name="stream"/> is not readable or not
		///     seekable.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> or
		///     <paramref name="length"/> are outside the readable range of the underlying
		///     stream.</exception>

		public SubsetStream(Stream stream, long offset, long length, bool leaveOpen)
			: this(stream, offset, length, leaveOpen, null)
		{
		}

		/// <summary>
		/// Initializes a new subset stream.
		/// </summary>
		///
		/// <param name="stream">The underlying stream.</param>
		/// <param name="offset">The offset of the first byte to expose in this subset.</param>
		/// <param name="length">The number of bytes to expose in this subset.</param>
		/// <param name="leaveOpen">A flag specifying whether <see cref="Dispose"/> should dispose
		///     the underlying stream.</param>
		/// <param name="locker">An optional lock object which will be held during all stream
		///     operations to allow concurrent use of the underlying stream.</param>
		///
		/// <exception cref="ArgumentException"><paramref name="stream"/> is not readable or not
		///     seekable.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> or
		///     <paramref name="length"/> are outside the readable range of the underlying
		///     stream.</exception>

		public SubsetStream(Stream stream, long offset, long length, bool leaveOpen, object locker)
		{
			if (stream == null) throw new ArgumentNullException(nameof(stream));
			if (!stream.CanRead || !stream.CanSeek) throw new ArgumentException(
				"Underlying stream of a SubsetStream must be readable and seekable");
			if (offset < 0 || offset > stream.Length) throw new ArgumentOutOfRangeException(nameof(offset));
			if (length < 0 || offset + length > stream.Length) throw new ArgumentOutOfRangeException(nameof(length));

			_stream = stream;
			_offset = offset;
			_length = length;
			_leaveOpen = leaveOpen;
			_locker = locker;
		}

		/// <summary>
		/// Throws <see cref="NotSupportedException"/> because this stream does not support
		/// writing.
		/// </summary>

		public override void Flush()
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Reads up to <paramref name="count"/> bytes from the underlying stream at the current
		/// subset position.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="count">The maximum number of bytes to read and store.</param>
		///
		/// <returns>The number of bytes actually read and stored, which may be less than
		///     <paramref name="count"/> or zero if the end of the stream has been
		///     reached.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> or
		///     <paramref name="count"/> are outside the bounds of
		///     <paramref name="buffer"/>.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="ObjectDisposedException">This object or the underlying stream have
		///     been disposed.</exception>

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));
			if (offset < 0 || offset > buffer.Length) throw new ArgumentOutOfRangeException(nameof(offset));
			if (count < 0 || offset + count > buffer.Length) throw new ArgumentOutOfRangeException(nameof(count));
			ThrowIfDisposed();

			if (_locker != null) Monitor.Enter(_locker);

			try
			{
				if (_position > _length) return 0;
				if (_length - _position <= int.MaxValue)
					count = Math.Min(count, (int) (_length - _position));
				if (count == 0) return 0;
				if (_stream.Position != _offset + _position)
					_stream.Seek(_offset + _position, SeekOrigin.Begin);
				int bytesRead = _stream.Read(buffer, offset, count);
				_position += bytesRead;
				return bytesRead;
			}

			finally
			{
				if (_locker != null) Monitor.Exit(_locker);
			}
		}

		/// <summary>
		/// Asynchronously reads up to <paramref name="count"/> bytes from the underlying stream
		/// at the current subset position.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store bytes.</param>
		/// <param name="offset">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="count">The maximum number of bytes to read and store.</param>
		/// <param name="cancellationToken">A cancellation token.</param>
		///
		/// <returns>The number of bytes actually read and stored, which may be less than
		///     <paramref name="count"/> or zero if the end of the stream has been
		///     reached.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> or
		///     <paramref name="count"/> are outside the bounds of
		///     <paramref name="buffer"/>.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="ObjectDisposedException">This object or the underlying stream have
		///     been disposed.</exception>

		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));
			if (offset < 0 || offset > buffer.Length) throw new ArgumentOutOfRangeException(nameof(offset));
			if (count < 0 || offset + count > buffer.Length) throw new ArgumentOutOfRangeException(nameof(count));
			ThrowIfDisposed();

			if (_locker != null) Monitor.Enter(_locker);

			try
			{
				if (_position > _length) return 0;
				if (_length - _position <= int.MaxValue)
					count = Math.Min(count, (int) (_length - _position));
				if (count == 0) return 0;
				if (_stream.Position != _offset + _position)
					_stream.Seek(_offset + _position, SeekOrigin.Begin);
				int bytesRead = await _stream.ReadAsync(buffer, offset, count, cancellationToken).ConfigureAwait(false);
				_position += bytesRead;
				return bytesRead;
			}

			finally
			{
				if (_locker != null) Monitor.Exit(_locker);
			}
		}

		/// <summary>
		/// Seeks to the specified position in the subset. This method only updates internal
		/// recordkeeping, and no seek is actually performed on the underlying stream until the
		/// next read call.
		/// </summary>
		///
		/// <param name="offset">The seek offset in bytes.</param>
		/// <param name="origin">The seek origin.</param>
		///
		/// <returns>The new stream position in bytes.</returns>
		///
		/// <exception cref="ArgumentException">The resulting stream position is before the
		///     beginning of the subset.</exception>
		/// <exception cref="ObjectDisposedException">This object has been disposed.</exception>

		public override long Seek(long offset, SeekOrigin origin)
		{
			ThrowIfDisposed();

			long newPosition;

			switch (origin)
			{
				case SeekOrigin.Begin: newPosition = 0; break;
				case SeekOrigin.Current: newPosition = _position; break;
				case SeekOrigin.End: newPosition = _length; break;
				default: throw new NotSupportedException("Unsupported seek origin " + origin);
			}

			newPosition += offset;
			if (newPosition < 0) throw new ArgumentException(
				"The resulting stream position is before the beginning of the stream");

			return _position = newPosition;
		}

		/// <summary>
		/// Throws <see cref="NotSupportedException"/> because this stream does not support
		/// truncation or length adjustment.
		/// </summary>

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Throws <see cref="NotSupportedException"/> because this stream does not support
		/// writing.
		/// </summary>

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Returns <c>true</c>, indicating that this stream is readable.
		/// </summary>
		///
		/// <exception cref="ObjectDisposedException">This object has been disposed.</exception>

		public override bool CanRead
		{
			get
			{
				ThrowIfDisposed();
				return true;
			}
		}

		/// <summary>
		/// Returns <c>true</c>, indicating that this stream is seekable.
		/// </summary>
		///
		/// <exception cref="ObjectDisposedException">This object has been disposed.</exception>

		public override bool CanSeek
		{
			get
			{
				ThrowIfDisposed();
				return true;
			}
		}

		/// <summary>
		/// Returns <c>false</c>, indicating that this stream is not writable.
		/// </summary>
		///
		/// <exception cref="ObjectDisposedException">This object has been disposed.</exception>

		public override bool CanWrite
		{
			get
			{
				ThrowIfDisposed();
				return false;
			}
		}

		/// <summary>
		/// Gets the subset length in bytes.
		/// </summary>
		///
		/// <exception cref="ObjectDisposedException">This object has been disposed.</exception>

		public override long Length
		{
			get
			{
				ThrowIfDisposed();
				return _length;
			}
		}

		/// <summary>
		/// Gets or sets the current stream position as the number of bytes past the beginning of
		/// the subset. Setting this property only updates internal recordkeeping, and no seek is
		/// actually performed on the underlying stream until the next read call.
		/// </summary>
		///
		/// <exception cref="ArgumentException">The resulting stream position is before the
		///     beginning of the subset.</exception>
		/// <exception cref="ObjectDisposedException">This object has been disposed.</exception>

		public override long Position
		{
			get
			{
				ThrowIfDisposed();
				return _position;
			}

			set
			{
				Seek(value, SeekOrigin.Begin);
			}
		}

		/// <summary>
		/// Gets or sets the read timeout in milliseconds.
		/// </summary>
		///
		/// <exception cref="ObjectDisposedException">This object or the underlying stream have
		///     been disposed.</exception>

		public override int ReadTimeout
		{
			get
			{
				ThrowIfDisposed();
				return _stream.ReadTimeout;
			}

			set
			{
				ThrowIfDisposed();
				_stream.ReadTimeout = value;
			}
		}

		/// <summary>
		/// Disposes the underlying stream unless the <c>leaveOpen</c> constructor parameter was
		/// <c>true</c>.
		/// </summary>
		///
		/// <param name="disposing"><c>true</c> if managed resources should be disposed.</param>

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (disposing)
			{
				if (!_leaveOpen) Exceptions.Ignore(() => _stream?.Dispose());
				_stream = null;
			}
		}

		/// <summary>
		/// Throws an <see cref="ObjectDisposedException"/> if this object has been disposed.
		/// </summary>
		///
		/// <exception cref="ObjectDisposedException">This object has been disposed.</exception>

		private void ThrowIfDisposed()
		{
			if (_stream == null)
				throw new ObjectDisposedException(GetType().Name);
		}

		private Stream _stream;
		private long _offset;
		private long _length;
		private bool _leaveOpen;
		private object _locker;
		private long _position;
	}
}