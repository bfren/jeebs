// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		private IR<TValue, TState> PrivateRun<TResult>(Action<TResult> f)
			where TResult : IOk =>
			result switch
			{
				TResult x =>
					Catch(() =>
					{
						f(x);
						return result;
					}),

				_ =>
					result.Error()
			};

		/// <inheritdoc/>
		new public IR<TValue, TState> Run(Action f0) =>
			result switch
			{
				IOk<TValue, TState> x =>
					Catch(() =>
					{
						f0();
						return x;
					}),

				_ =>
					result.Error()
			};

		/// <inheritdoc/>
		new public IR<TValue, TState> Run(Action<IOk> f1) =>
			PrivateRun(f1);

		/// <inheritdoc/>
		new public IR<TValue, TState> Run(Action<IOk<TValue>> f2) =>
			PrivateRun(f2);

		/// <inheritdoc/>
		new public IR<TValue, TState> Run(Action<IOkV<TValue>> f3) =>
			PrivateRun(f3);

		/// <inheritdoc/>
		public IR<TValue, TState> Run(Action<IOk<TValue, TState>> f4) =>
			PrivateRun(f4);

		/// <inheritdoc/>
		public IR<TValue, TState> Run(Action<IOkV<TValue, TState>> f5) =>
			PrivateRun(f5);
	}
}
