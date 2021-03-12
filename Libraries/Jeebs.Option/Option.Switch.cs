// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;

namespace JeebsF
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Run an action depending on whether this is a <see cref="Some{T}"/> or <see cref="None{T}"/>
		/// </summary>
		/// <param name="some">Action to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Action to run if <see cref="None{T}"/></param>
		internal void DoSwitch(Action<T> some, Action<IMsg?> none)
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
				throw new Exceptions.UnknownOptionException(); // as Option<T> is internal implementation only this should never happen...
			}
		}

		/// <inheritdoc cref="DoSwitch(Action{T}, Action{IMsg?})"/>
		public void Switch(Action<T> some, Action none) =>
			DoSwitch(
				some: some,
				none: _ => none()
			);

		/// <inheritdoc cref="DoSwitch(Action{T}, Action{IMsg?})"/>
		public void Switch(Action<T> some, Action<IMsg?> none) =>
			DoSwitch(
				some: some,
				none: none
			);
	}
}
