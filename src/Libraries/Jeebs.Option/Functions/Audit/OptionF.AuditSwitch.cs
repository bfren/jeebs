// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Audit the option and return unmodified<br/>
		/// Errors will not be returned as they affect the state of the object, but will be written to the console,
		/// or <see cref="LogAuditExceptions"/> if set
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="option">Option being audited</param>
		/// <param name="some">[Optional] Action to run if the current Option is <see cref="Some{T}"/></param>
		/// <param name="none">[Optional] Action to run if the current Option is <see cref="Jeebs.None{T}"/></param>
		public static Option<T> AuditSwitch<T>(Option<T> option, Action<T>? some, Action<IMsg>? none)
		{
			// Do nothing if the user gave us nothing to do!
			if (some == null && none == null)
			{
				return option;
			}

			// Work out which audit function to use
			Action audit = Switch<T, Action>(
				option,
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
			return option;
		}
	}
}
