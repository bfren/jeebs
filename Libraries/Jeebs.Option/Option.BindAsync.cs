// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

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
		internal Task<Option<U>> DoBindAsync<U>(Func<T, Task<Option<U>>> bind, Option.Handler? handler) =>
			Option.CatchAsync(() =>
				Switch(
					some: v => bind(v),
					none: async r => (Option<U>)new None<U>(r)
				),
				handler
			);

		/// <inheritdoc cref="DoBindAsync{U}(Func{T, Task{Option{U}}}, Option.Handler?)"/>
		public Task<Option<U>> BindAsync<U>(Func<T, Task<Option<U>>> bind, Option.Handler? handler = null) =>
			DoBindAsync(bind, handler);
	}
}
