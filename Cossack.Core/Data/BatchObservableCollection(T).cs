//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Cossack.Core.Data
{
	/// <summary>
	/// Adds batch-update functionality to an <see cref="ObservableCollection{T}"/>.
	/// </summary>
	///
	/// <typeparam name="T">The type of element in the collection.</typeparam>

	public class BatchObservableCollection<T> : ObservableCollection<T>
	{
		/// <summary>
		/// Adds the specified items to the collection, withholding any
		/// <see cref="ObservableCollection{T}.CollectionChanged"/> events until after all items
		/// have been added.
		/// </summary>
		/// 
		/// <param name="items">The items to add.</param>
		///
		/// <exception cref="ArgumentNullException"><paramref name="items"/> is
		///     <c>null</c>.</exception>

		public void AddRange(IEnumerable<T> items)
		{
			if (items == null) throw new ArgumentNullException(nameof(items));

			_suppressCollectionChangedEvents = true;

			try
			{
				foreach (T item in items) Add(item);
			}

			finally
			{
				_suppressCollectionChangedEvents = false;
			}

			OnCollectionChanged(new NotifyCollectionChangedEventArgs(
				NotifyCollectionChangedAction.Reset));
		}

		/// <summary>
		/// Calls the base class's
		/// <see cref="ObservableCollection{T}.OnCollectionChanged(NotifyCollectionChangedEventArgs)"/>
		/// method unless events have been suppressed.
		/// </summary>
		///
		/// <param name="e">The event arguments.</param>

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			Debug.Assert(e != null);

			if (!_suppressCollectionChangedEvents)
				base.OnCollectionChanged(e);
		}

		private bool _suppressCollectionChangedEvents = false;
	}
}