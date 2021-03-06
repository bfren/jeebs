// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		private IR<TValue> PrivateRun<TResult>(Action<TResult> f)
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
		public IR<TValue> Run(Action f0) =>
			result switch
			{
				IOk x =>
					Catch(() =>
					{
						f0();
						return result;
					}),

				_ =>
					result.Error()
			};

		/// <inheritdoc/>
		public IR<TValue> Run(Action<IOk> f1) =>
			PrivateRun(f1);

		/// <inheritdoc/>
		public IR<TValue> Run(Action<IOk<TValue>> f2) =>
			PrivateRun(f2);

		/// <inheritdoc/>
		public IR<TValue> Run(Action<IOkV<TValue>> f3) =>
			PrivateRun(f3);
	}
}
