// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Audit the current Option state and return unmodified
		/// Errors will not be returned as they affect the state of the object, but will be written to the console,
		/// or <see cref="LogAuditExceptions"/> if set
		/// </summary>
		/// <param name="audit">Audit function</param>
		internal Option<T> DoAudit(Action<Option<T>> audit) =>
			F.OptionF.Audit(this, audit);

		/// <inheritdoc cref="DoAudit(Action{Option{T}})"/>
		public Option<T> Audit(Action<Option<T>> audit) =>
			F.OptionF.Audit(this, audit);
	}
}
