// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="AuditSwitch{T}(Option{T}, Action{T}?, Action{IMsg?}?)"/>
		[Obsolete]
		internal Option<T> DoAuditSwitch(Action<T>? some, Action<IMsg?>? none) =>
			F.OptionF.AuditSwitch(this, some, none);
	}
}
