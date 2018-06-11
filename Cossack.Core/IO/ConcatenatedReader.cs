//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cossack.Core.IO
{
	/// <summary>
	/// A <see cref="TextReader"/> that provides concatenated data from a sequence of
	/// <see cref="TextReader"/> objects.
	/// </summary>

	public class ConcatenatedReader : TextReader
    {
		/// <summary>
		/// Initializes a reader using the specified source readers.
		/// </summary>
		///
		/// <param name="sources">The source readers.</param>
		///
		/// <exception cref="ArgumentNullException"><paramref name="sources"/> is
		///     <c>null</c>.</exception>

		public ConcatenatedReader(IEnumerable<TextReader> sources)
		{
			if (sources == null) throw new ArgumentNullException(nameof(sources));
			_sources = new Queue<TextReader>(sources);
		}

		/// <summary>
		/// Reads concatenated data from the underlying sequence of source readers.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store data.</param>
		/// <param name="index">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="count">The maximum number of bytes to store.</param>
		///
		/// <returns>The actual number of bytes read, which may be less than the amount requested
		///     or zero if the end of the concatenated sources is reached.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> or
		///    <paramref name="count"/> are negative or fall outside the array
		///    bounds.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="ObjectDisposedException">This reader or one or more source readers
		///     have been disposed.</exception>

		public override int Read(char[] buffer, int index, int count)
		{
			if (_sources == null) throw new ObjectDisposedException(GetType().Name);
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));
			if (index < 0 || count < 0 || index + count > buffer.Length)
				throw new ArgumentOutOfRangeException(
					"Specified index and count are outside the array bounds");

			int total = 0;

			while (count > 0)
			{
				if (_sources.Count == 0) return total;
				int bytesRead = _sources.Peek().Read(buffer, index, count);
				if (bytesRead == 0) _sources.Dequeue().Dispose();
				total += bytesRead;
				count -= bytesRead;
				index += bytesRead;
			}

			return total;
		}

		/// <summary>
		/// Asynchronously reads concatenated data from the underlying sequence of source readers.
		/// </summary>
		///
		/// <param name="buffer">The array in which to store data.</param>
		/// <param name="index">The index in <paramref name="buffer"/> of the first byte to
		///     store.</param>
		/// <param name="count">The maximum number of bytes to store.</param>
		///
		/// <returns>The actual number of bytes read, which may be less than the amount requested
		///     or zero if the end of the concatenated sources is reached.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="buffer"/> is
		///     <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> or
		///    <paramref name="count"/> are negative or fall outside the array
		///    bounds.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		/// <exception cref="ObjectDisposedException">This reader or one or more source readers
		///     have been disposed.</exception>

		public override async Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			if (_sources == null) throw new ObjectDisposedException(GetType().Name);
			if (buffer == null) throw new ArgumentNullException(nameof(buffer));
			if (index < 0 || count < 0 || index + count > buffer.Length)
				throw new ArgumentOutOfRangeException(
					"Specified index and count are outside the array bounds");

			int total = 0;

			while (count > 0)
			{
				if (_sources.Count == 0) return total;
				int bytesRead = await _sources.Peek().ReadAsync(buffer, index, count).ConfigureAwait(false);
				if (bytesRead == 0) _sources.Dequeue().Dispose();
				total += bytesRead;
				count -= bytesRead;
				index += bytesRead;
			}

			return total;
		}

		/// <summary>
		/// Disposes the underlying source readers.
		/// </summary>
		///
		/// <param name="disposing"><c>true</c> if disposing managed resources.</param>

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (TextReader source in _sources)
					source.Dispose();
				_sources = null;
			}
		}

		private Queue<TextReader> _sources;
	}
}