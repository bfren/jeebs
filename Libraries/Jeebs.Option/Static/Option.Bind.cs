// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public static partial class Option
	{
		/// <summary>
		/// Bind without an initial value (i.e. it's not a proper bind
		/// but works like <see cref="Option{T}.Bind{U}(Func{T, Option{U}}, Handler?)"/>
		/// except at the beginning of an <see cref="Option{T}"/> chain).
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="bind">Binding function - will always run</param>
		/// <param name="handler">[Optional] Exception handler</param>
		public static Option<T> Bind<T>(Func<Option<T>> bind, Handler? handler = null) =>
			True.Bind(_ => bind(), handler);

		/// <inheritdoc cref="Bind{T}(Func{Option{T}}, Handler?)"/>
		public static Task<Option<T>> BindAsync<T>(Func<Task<Option<T>>> bind, Handler? handler = null) =>
			True.BindAsync(_ => bind(), handler);
	}
}
