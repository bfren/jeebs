// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Jeebs.Exceptions;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Run an action depending on whether <paramref name="option"/> is a <see cref="Some{T}"/> or <see cref="Jeebs.None{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="option">Option being switched</param>
		/// <param name="some">Action to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Action to run if <see cref="Jeebs.None{T}"/></param>
		public static void Switch<T>(Option<T> option, Action<T> some, Action<IMsg?> none)
		{
			if (option is Some<T> x)
			{
				some(x.Value);
			}
			else if (option is None<T> y)
			{
				none(y.Reason);
			}
			else
			{
				throw new UnknownOptionException(); // as Option<T> is internal implementation only this should never happen...
			}
		}

		/// <summary>
		/// Run a function depending on whether <paramref name="option"/> is a <see cref="Some{T}"/> or <see cref="Jeebs.None{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="option">Option being switched</param>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="Jeebs.None{T}"/></param>
		public static U Switch<T, U>(Option<T> option, Func<T, U> some, Func<IMsg?, U> none) =>
			option switch
			{
				Some<T> x =>
					some(x.Value),

				None<T> x =>
					none(x.Reason),

				_ =>
					throw new UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
			};
	}
}
