// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using static JeebsF.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Audit the current Option state and return unmodified
		/// Errors will not be returned as they affect the state of the object, but will be written to the console,
		/// or <see cref="LogAuditExceptions"/> if set
		/// </summary>
		/// <param name="some">[Optional] Action to run if the current Option is <see cref="Some{T}"/></param>
		/// <param name="none">[Optional] Action to run if the current Option is <see cref="None{T}"/></param>
		internal Option<T> DoAuditSwitch(Action<T>? some, Action<IMsg?>? none)
		{
			// Do nothing if the user gave us nothing to do!
			if (some == null && none == null)
			{
				return this;
			}

			// Work out which audit function to use
			Action audit = Switch<Action>(
				some: v => () => some?.Invoke(v),
				none: r => () => none?.Invoke(r)
			);

			// Perform the audit
			try
			{
				audit();
			}
			catch (Exception e)
			{
				HandleAuditException(e);
			}

			// Return the original object
			return this;
		}

		/// <inheritdoc cref="DoAuditSwitch(Action{T}?, Action{IMsg?}?)"/>
		public Option<T> AuditSwitch(Action<T>? some) =>
			DoAuditSwitch(
				some: some,
				none: null
			);

		/// <inheritdoc cref="DoAuditSwitch(Action{T}?, Action{IMsg?}?)"/>
		public Option<T> AuditSwitch(Action<IMsg?>? none) =>
			DoAuditSwitch(
				some: null,
				none: none
			);

		/// <inheritdoc cref="DoAuditSwitch(Action{T}?, Action{IMsg?}?)"/>
		public Option<T> AuditSwitch(Action<T>? some, Action<IMsg?>? none) =>
			DoAuditSwitch(
				some: some,
				none: none
			);
	}
}
