// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	public abstract partial record Option<T>
	{
		/// <summary>
		/// Use <paramref name="bind"/> to convert the current Option to a new type - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Next value type</typeparam>
		/// <param name="bind">Binding function - will receive <see cref="Some{T}.Value"/> if this is a <see cref="Some{T}"/></param>
		public Option<U> Bind<U>(Func<T, Option<U>> bind, Option.Handler? handler = null) =>
			Option.Catch(() => SwitchFunc(
				some: x => bind(x),
				none: r => Option.None<U>(r)
			), handler);
	}
}
