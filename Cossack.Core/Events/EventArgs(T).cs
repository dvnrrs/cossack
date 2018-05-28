//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using System;

namespace Cossack.Core.Events
{
	/// <summary>
	/// A generic event arguments class that contains a single data member of the specified type.
	/// </summary>
	///
	/// <typeparam name="T">The type of the event argument object's data member.</typeparam>

	public class EventArgs<T> : EventArgs
	{
		/// <summary>
		/// Initializes a new event arguments object.
		/// </summary>
		///
		/// <param name="data">The data member.</param>

		public EventArgs(T data)
		{
			Data = data;
		}

		/// <summary>
		/// Gets the data member.
		/// </summary>

		public T Data { get; private set; }
	}
}