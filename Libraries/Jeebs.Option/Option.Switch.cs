// Jeebs Rapid Application Development
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
		public void Switch(Action<T> some, Action none) =>
			SwitchAction(
				some: some,
				none: none
			);

		/// <summary>
		/// Run an action depending on whether this is a <see cref="Some{T}"/> or <see cref="None{T}"/>
		/// </summary>
		/// <param name="some">Action to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Action to run if <see cref="None{T}"/></param>
		public void Switch(Action<T> some, Action<IMsg?> none) =>
			SwitchAction(
				some: some,
				none: none
			);
	}
}
