//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using Cossack.Wpf.Adorners;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Cossack.Wpf.Sorting
{
	/// <summary>
	/// Implements ascending and descending column sorting on a <see cref="ListView"/> whereby the
	/// user can click on a column header to sort on that column, and click on the column header
	/// again to reverse the sort.
	/// </summary>

	public class ListViewSortManager
	{
		/// <summary>
		/// Initializes a new sorting manager.
		/// </summary>
		///
		/// <param name="listView">The <see cref="ListView"/> on which to implement
		///     sorting.</param>
		///
		/// <exception cref="ArgumentNullException"><paramref name="listView"/> is
		///     <c>null</c>.</exception>

		public ListViewSortManager(ListView listView)
		{
			_listView = listView ?? throw new ArgumentNullException(nameof(listView));
		}

		/// <summary>
		/// Changes the sort in response to a user click on the specified column header.
		/// </summary>
		///
		/// <param name="columnHeader">The column header that was clicked.</param>
		///
		/// <exception cref="ArgumentNullException"><paramref name="columnHeader"/> is
		///     <c>null</c>.</exception>

		public void SetSort(GridViewColumnHeader columnHeader)
		{
			if (columnHeader == null) throw new ArgumentNullException(nameof(columnHeader));
			string sortBy = columnHeader.Tag.ToString();

			if (_columnHeader != null)
			{
				AdornerLayer.GetAdornerLayer(_columnHeader).Remove(_adorner);
				_listView.Items.SortDescriptions.Clear();
			}

			ListSortDirection newDirection = ListSortDirection.Ascending;
			if (_columnHeader == columnHeader && _adorner.Direction == newDirection)
				newDirection = ListSortDirection.Descending;

			_columnHeader = columnHeader;
			_adorner = new SortAdorner(_columnHeader, newDirection);
			AdornerLayer.GetAdornerLayer(_columnHeader).Add(_adorner);

			_listView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDirection));
		}

		private ListView _listView;
		private SortAdorner _adorner = null;
		private GridViewColumnHeader _columnHeader = null;
	}
}