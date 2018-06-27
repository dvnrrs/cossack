//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Cossack.Wpf.Adorners
{
	/// <summary>
	/// An adorner which adds a sort direction arrow pointing either up or down based on a
	/// specified sort direction.
	/// </summary>

	public class SortAdorner : Adorner
	{
		/// <summary>
		/// Initializes a new adorner for the specified element and sort direction.
		/// </summary>
		///
		/// <param name="element">The element to adorn.</param>
		/// <param name="direction">The sort direction.</param>

		public SortAdorner(UIElement element, ListSortDirection direction) : base(element)
		{
			Direction = direction;
		}

		/// <summary>
		/// Gets the sort direction.
		/// </summary>

		public ListSortDirection Direction { get; private set; }

		/// <summary>
		/// Renders the sort direction arrow.
		/// </summary>
		///
		/// <param name="drawingContext">The drawing context to render to.</param>

		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);

			if (AdornedElement.RenderSize.Width < 20) return;

			drawingContext.PushTransform(new TranslateTransform(
				AdornedElement.RenderSize.Width - 15,
				(AdornedElement.RenderSize.Height - 5) / 2));

			drawingContext.DrawGeometry(Brushes.Black, null,
				this.Direction == ListSortDirection.Descending ? _descendingGeometry : _ascendingGeometry);

			drawingContext.Pop();
		}

		private static Geometry _ascendingGeometry = Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");
		private static Geometry _descendingGeometry = Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");
	}
}
