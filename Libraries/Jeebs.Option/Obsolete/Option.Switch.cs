// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="F.OptionF.Switch{T}(Option{T}, Action{T}, Action{IMsg?})"/>
		[Obsolete]
		internal void DoSwitch(Action<T> some, Action<IMsg?> none) =>
			F.OptionF.Switch(this, some, none);
	}
}
