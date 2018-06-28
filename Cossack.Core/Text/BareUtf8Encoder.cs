//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>
//

using System.Text;

namespace Cossack.Core.Text
{
	/// <summary>
	/// Contains a <see cref="UTF8Encoding"/> instance which is configured not to emit byte-order
	/// marks.
	/// </summary>

	public static class BareUtf8Encoder
    {
		/// <summary>
		/// A <see cref="UTF8Encoding"/> instance which is configured not to emit byte-order
		/// marks.
		/// </summary>

		public static readonly UTF8Encoding Instance = new UTF8Encoding(false);
    }
}