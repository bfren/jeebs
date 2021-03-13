// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="DoAudit(Action{Option{T}})"/>
		[Obsolete]
		internal Task<Option<T>> DoAuditAsync(Func<Option<T>, Task> audit) =>
			F.OptionF.AuditAsync(this, audit);
	}
}
