// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	public abstract partial record Option<T>
	{
		/// <summary>
		/// Run a function depending on whether this is a <see cref="Some{T}"/> or <see cref="None{T}"/>
		/// </summary>
		/// <typeparam name="U">Return type</typeparam>
		/// <param name="some">Function to run if <see cref="Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
		/// <param name="none">Function to run if <see cref="None{T}"/></param>
		private U MatchPrivate<U>(Func<T, U> some, Func<IMsg?, U> none) =>
			Switch(
				some: some,
				none: none
			);

		/// <inheritdoc cref="MatchPrivate{U}(Func{T, U}, Func{IMsg?, U})"/>
		public U Match<U>(Func<T, U> some, U none) =>
			MatchPrivate(
				some: some,
				none: _ => none
			);

		/// <inheritdoc cref="MatchPrivate{U}(Func{T, U}, Func{IMsg?, U})"/>
		public U Match<U>(Func<T, U> some, Func<U> none) =>
			MatchPrivate(
				some: some,
				none: _ => none()
			);

		/// <inheritdoc cref="MatchPrivate{U}(Func{T, U}, Func{IMsg?, U})"/>
		public U Match<U>(Func<T, U> some, Func<IMsg?, U> none) =>
			MatchPrivate(
				some: some,
				none: none
			);
	}
}
