// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Use <paramref name="bind"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="bind">Binding function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		/// <param name="handler">[Optional] Exception handler</param>
		internal Task<Option<U>> DoBindAsync<U>(Func<T, Task<Option<U>>> bind, Handler? handler) =>
			CatchAsync(() =>
				Switch(
					some: v => bind(v),
					none: r => new None<U>(r).AsTask
				),
				handler
			);

		/// <inheritdoc cref="DoBindAsync{U}(Func{T, Task{Option{U}}}, OptionF.Handler?)"/>
		public Task<Option<U>> BindAsync<U>(Func<T, Task<Option<U>>> bind, Handler? handler = null) =>
			DoBindAsync(bind, handler);
	}
}
