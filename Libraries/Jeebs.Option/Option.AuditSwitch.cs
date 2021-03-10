// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	public abstract partial record Option<T>
	{
		/// <summary>
		/// Audit the current Option state and return unmodified
		/// Errors will not be returned as they affect the state of the object, but will be written to the console
		/// </summary>
		/// <param name="some">[Optional] Action to run if the current Option is <see cref="Some{T}"/></param>
		/// <param name="none">[Optional] Action to run if the current Option is <see cref="None{T}"/></param>
		private Option<T> AuditSwitchPrivate(Action<T>? some = null, Action<IMsg?>? none = null)
		{
			// Do nothing if the user gave us nothing to do!
			if (some == null && none == null)
			{
				return this;
			}

			// Work out which audit function to use
			Action audit = Switch<Action>(
				some: x => () => some?.Invoke(x),
				none: x => () => none?.Invoke(x)
			);

			// Perform the audit
			try
			{
				audit();
			}
			catch (Exception e)
			{
				Console.WriteLine("Audit Error: {0}", e);
			}

			// Return the original object
			return this;
		}

		/// <inheritdoc cref="AuditSwitchPrivate(Action{T}?, Action{IMsg?}?)"/>
		public Option<T> AuditSwitch(Action<T>? some = null, Action<IMsg?>? none = null) =>
			AuditSwitchPrivate(
				some: some,
				none: none
			);
	}
}
