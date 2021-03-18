// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Run a function depending on whether this is a <see cref="Some{T}"/> or <see cref="Jeebs.None{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <typeparam name="U">Return value type</typeparam>
		/// <param name="option">Input option</param>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="Jeebs.None{T}"/></param>
		public static U Match<T, U>(Option<T> option, Func<T, U> some, Func<IMsg, U> none) =>
			Switch(
				option,
				some: some,
				none: none
			);
	}
}
