// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace JeebsF
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Audit the current Option state and return unmodified
		/// Errors will not be returned as they affect the state of the object, but will be written to the console
		/// </summary>
		/// <param name="audit">Audit function</param>
		internal Task<Option<T>> DoAuditAsync(Func<Option<T>, Task> audit)
		{
			// Perform the audit
			try
			{
				audit(this);
			}
			catch (Exception e)
			{
				OptionF.HandleAuditException(e);
			}

			// Return the original object
			return Task.FromResult(this);
		}

		/// <inheritdoc cref="DoAuditAsync(Func{Option{T}, Task})"/>
		public Task<Option<T>> AuditAsync(Func<Option<T>, Task> audit) =>
			DoAuditAsync(audit);
	}
}
