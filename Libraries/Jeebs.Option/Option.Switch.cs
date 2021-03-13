// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="F.OptionF.Switch{T}(Option{T}, Action{T}, Action{IMsg?})"/>
		internal void DoSwitch(Action<T> some, Action<IMsg?> none) =>
			F.OptionF.Switch(this, some, none);

		/// <inheritdoc cref="F.OptionF.Switch{T}(Option{T}, Action{T}, Action{IMsg?})"/>
		public void Switch(Action<T> some, Action none) =>
			F.OptionF.Switch(this, some: some, none: _ => none());

		/// <inheritdoc cref="F.OptionF.Switch{T}(Option{T}, Action{T}, Action{IMsg?})"/>
		public void Switch(Action<T> some, Action<IMsg?> none) =>
			F.OptionF.Switch(this, some: some, none: none);
	}
}
