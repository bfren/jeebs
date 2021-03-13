// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="AuditSwitchAsync{T}(Option{T}, Func{T, Task}?, Func{IMsg?, Task}?)"/>
		[Obsolete]
		internal Task<Option<T>> DoAuditSwitchAsync(Func<T, Task>? some, Func<IMsg?, Task>? none) =>
			F.OptionF.AuditSwitchAsync(
				this,
				some,
				none
			);
	}
}
