﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	public abstract partial record Option<T>
	{
		/// <summary>
		/// Run an action depending on whether this is a <see cref="Some{T}"/> or <see cref="None{T}"/>
		/// </summary>
		/// <param name="some">Action to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Action to run if <see cref="None{T}"/></param>
		private void SwitchPrivate(Action<T> some, Action<IMsg?> none)
		{
			if (this is Some<T> x)
			{
				some(x.Value);
			}
			else if (this is None<T> y)
			{
				none(y.Reason);
			}
			else
			{
				throw new Jx.Option.UnknownOptionException(); // as Option<T> is internal implementation only this should never happen...
			}
		}

		/// <inheritdoc cref="SwitchPrivate(Action{T}, Action{IMsg?})"/>
		public void Switch(Action<T> some, Action none) =>
			SwitchPrivate(
				some: some,
				none: _ => none()
			);

		/// <inheritdoc cref="SwitchPrivate(Action{T}, Action{IMsg?})"/>
		public void Switch(Action<T> some, Action<IMsg?> none) =>
			SwitchPrivate(
				some: some,
				none: none
			);
	}
}
