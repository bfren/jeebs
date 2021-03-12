// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using static JeebsF.OptionF;

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
		internal Option<U> DoBind<U>(Func<T, Option<U>> bind, Handler? handler = null) =>
			Catch(() =>
				Switch(
					some: v => bind(v),
					none: r => new None<U>(r)
				),
				handler
			);

		/// <inheritdoc cref="DoBind{U}(Func{T, Option{U}}, OptionF.Handler?)"/>
		public Option<U> Bind<U>(Func<T, Option<U>> bind, Handler? handler = null) =>
			DoBind(bind, handler);
	}
}
