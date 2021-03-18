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
		/// <param name="audit">Audit function</param>
		public static Option<T> Audit<T>(Option<T> option, Action<Option<T>> audit)
		{
			// Perform the audit
			try
			{
				audit(option);
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
