//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using System;

namespace Cossack.Core.IO
{
	/// <summary>
	/// A placeholder object which allows the caller to modify the value of an earlier reserved
	/// section of data in a <see cref="ProtocolWriter"/>.
	/// </summary>
	///
	/// <typeparam name="T">The type of the placeholder data.</typeparam>

	public class ProtocolWriterPlaceholder<T>
    {
		/// <summary>
		/// Sets the placeholder value to the number of bytes between the current writer position
		/// and the end of the placeholder (i.e., the number of bytes written since immediately
		/// after the placeholder was marked).
		/// </summary>
		///
		/// <param name="includeSelf"><c>true</c> to also count the placeholder value's own
		///     bytes.</param>

		public void Close(bool includeSelf = false)
		{
			ulong delta = (ulong) (_writer.Position - Position);
			if (!includeSelf) delta -= (ulong) _size;
			if (delta > _maxValue) throw new InvalidOperationException(
				"Space between marks is too large to store in placeholder");
			Value = (T) Convert.ChangeType(delta, typeof(T));
		}

		/// <summary>
		/// Sets the value of the reserved section of data.
		/// </summary>

		public T Value { set { _writeAction(value); } }

		/// <summary>
		/// Gets the offset in the <see cref="ProtocolWriter"/> stream at which the reserved
		/// section of data begins.
		/// </summary>

		public int Position { get; private set; }

		/// <summary>
		/// Initializes a new placeholder.
		/// </summary>
		///
		/// <param name="writer">The writer that generated this placeholder.</param>
		/// <param name="writeAction">The action to invoke to write a new value.</param>
		/// <param name="position">The offset in the <see cref="ProtocolWriter"/> stream at which
		///     the reserved section of data begins.</param>
		/// <param name="size">The size of the placeholder in bytes.</param>
		/// <param name="maxValue">The maximum allowable length value for
		///     <see cref="Close(bool)"/>.</param>

		internal ProtocolWriterPlaceholder(ProtocolWriter writer,
			Action<T> writeAction, int position, int size, ulong maxValue)
		{
			_writer = writer;
			_writeAction = writeAction;
			Position = position;
			_size = size;
			_maxValue = maxValue;
		}

		private readonly ProtocolWriter _writer;
		private readonly Action<T> _writeAction;
		private readonly int _size;
		private readonly ulong _maxValue;
	}
}