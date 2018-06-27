//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System;

namespace Cossack.Core.Events
{
	/// <summary>
	/// A generic event arguments class that contains a single data value of the specified type.
	/// </summary>
	///
	/// <typeparam name="T">The type of the event argument object's data value.</typeparam>

	public class EventArgs<T> : EventArgs
	{
		/// <summary>
		/// Initializes a new event arguments object.
		/// </summary>
		///
		/// <param name="data">The data value associated with this event.</param>

		public EventArgs(T data)
		{
			Data = data;
		}

		/// <summary>
		/// Gets the data value associated with this event.
		/// </summary>

		public T Data { get; private set; }
	}
}