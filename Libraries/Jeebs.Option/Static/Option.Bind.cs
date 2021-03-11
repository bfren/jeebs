// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public static partial class Option
	{
		/// <inheritdoc cref="Option{T}.DoBind{U}(Func{T, Option{U}}, Handler?)"/>
		public static Option<T> Bind<T>(Func<Option<T>> bind, Handler? handler = null) =>
			True.Bind(_ => bind(), handler);

		/// <inheritdoc cref="Option{T}.DoBindAsync{U}(Func{T, Task{Option{U}}}, Handler?)"/>
		public static Task<Option<T>> BindAsync<T>(Func<Task<Option<T>>> bind, Handler? handler = null) =>
			True.BindAsync(_ => bind(), handler);
	}
}
