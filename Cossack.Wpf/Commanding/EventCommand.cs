//
// Copyright (C) 2018 David A. Norris <danorris@gmail.com>. All rights reserved.
//

using System;
using System.Windows.Input;

namespace Cossack.Wpf.Commanding
{
	/// <summary>
	/// An <see cref="ICommand"/> implementation which raises an event when it is executed.
	/// </summary>

	public class EventCommand : ICommand
	{
		/// <summary>
		/// Raised if the <see cref="CanExecute"/> property value changes.
		/// </summary>

		public event EventHandler CanExecuteChanged;

		/// <summary>
		/// Raised when the command is executed. The event handler receives the command parameter.
		/// </summary>

		public event EventHandler<object> Executed;

		/// <summary>
		/// Initializes a new command.
		/// </summary>

		public EventCommand()
		{
			// Dead code to get rid of compiler warning
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Returns <c>true</c>.
		/// </summary>
		///
		/// <param name="parameter">Ignored.</param>
		///
		/// <returns><c>true</c>.</returns>

		public bool CanExecute(object parameter)
		{
			return true;
		}

		/// <summary>
		/// Executes the command, and raises the <see cref="Executed"/> event.
		/// </summary>
		///
		/// <param name="parameter">The command parameter.</param>

		public void Execute(object parameter)
		{
			Executed?.Invoke(this, parameter);
		}
	}
}