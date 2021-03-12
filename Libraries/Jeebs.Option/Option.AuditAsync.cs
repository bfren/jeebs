// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="DoAudit(Action{Option{T}})"/>
		internal Task<Option<T>> DoAuditAsync(Func<Option<T>, Task> audit)
		{
			// Perform the audit
			try
			{
				audit(this);
			}
			catch (Exception e)
			{
				HandleAuditException(e);
			}

			// Return the original object
			return Task.FromResult(this);
		}

		/// <inheritdoc cref="DoAuditAsync(Func{Option{T}, Task})"/>
		public Task<Option<T>> AuditAsync(Func<Option<T>, Task> audit) =>
			DoAuditAsync(audit);
	}
}
