//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

namespace Cossack.Wpf.Converters
{
	/// <summary>
	/// Specifies whether a <see cref="BooleanLogicConverter"/> should operate in AND, OR, NAND,
	/// NOR, XOR or XNOR mode.
	/// </summary>

	public enum BooleanLogicConverterMode
	{
		/// <summary>
		/// Indicates that the <see cref="BooleanLogicConverter"/> should operate in AND mode,
		/// where the output is true if and only if all inputs are true.
		/// </summary>

		And,

		/// <summary>
		/// Indicates that the <see cref="BooleanLogicConverter"/> should operate in OR mode,
		/// where the output is true if and only if any input is true.
		/// </summary>

		Or,

		/// <summary>
		/// Indicates that the <see cref="BooleanLogicConverter"/> should operate in NAND mode,
		/// where the output is true if and only if any input is false.
		/// </summary>

		Nand,

		/// <summary>
		/// Indicates that the <see cref="BooleanLogicConverter"/> should operate in NOR mode,
		/// where the output is true if and only if all inputs are false.
		/// </summary>

		Nor,

		/// <summary>
		/// Indicates that the <see cref="BooleanLogicConverter"/> should operate in XOR mode,
		/// where the output is true if and only if an odd number of inputs are true.
		/// </summary>

		Xor,

		/// <summary>
		/// Indicates that the <see cref="BooleanLogicConverter"/> should operate in XNOR mode,
		/// where the output is true if and only if an even number of inputs are true.
		/// </summary>

		Xnor,
	}
}