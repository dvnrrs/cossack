//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using System.Threading.Tasks;

namespace Cossack.Core.Tasks
{
	/// <summary>
	/// Contains extension methods for the <see cref="Task"/> class.
	/// </summary>

	public static class TaskExtensions
    {
		/// <summary>
		/// A dummy, empty method which can be used to explicitly eliminate compiler warnings when
		/// a method that returns a <see cref="Task"/> is called without storing the returned
		/// <see cref="Task"/> in a variable.
		/// </summary>
		///
		/// <param name="task">The task to forget about.</param>

		public static void Forget(this Task task)
		{
		}
    }
}