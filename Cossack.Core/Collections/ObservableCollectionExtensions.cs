//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Cossack.Core.Collections
{
	/// <summary>
	/// Contains extension methods for the <see cref="ObservableCollection{T}"/> class.
	/// </summary>

	public static class ObservableCollectionExtensions
    {
		/// <summary>
		/// Removes all items from an <see cref="ObservableCollection{T}"/> which satisfy a
		/// predicate.
		/// </summary>
		///
		/// <typeparam name="T">The type of the elements in the collection.</typeparam>
		///
		/// <param name="collection">The collection to remove from.</param>
		/// <param name="predicate">A unary predicate which returns <c>true</c> if an item should
		///     be removed.</param>
		///
		/// <returns>The number of items removed.</returns>
		///
		/// <exception cref="ArgumentNullException"><paramref name="collection"/> or
		///     <paramref name="predicate"/> are <c>null</c>.</exception>

		public static int Remove<T>(this ObservableCollection<T> collection, Func<T, bool> predicate)
		{
			if (collection == null) throw new ArgumentNullException(nameof(collection));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			List<T> itemsToRemove = collection.Where(predicate).ToList();
			foreach (T item in itemsToRemove) collection.Remove(item);
			return itemsToRemove.Count;
		}
    }
}