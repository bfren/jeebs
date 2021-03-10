// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections;

namespace Jeebs
{
	/// <summary>
	/// Option type - enables null-safe returning by wrapping value in <see cref="Some{T}"/> and null in <see cref="None{T}"/>
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	public abstract partial record Option<T> : IEquatable<Option<T>>, IStructuralEquatable
	{
		internal Option() { }

		private U Switch<U>(Func<T, U> some, Func<IMsg?, U> none) =>
			this switch
			{
				Some<T> x =>
					some(x.Value),

				None<T> y =>
					none.Invoke(y.Reason),

				_ =>
					throw new Jx.Option.UnknownOptionException() // as Option<T> is internal implementation only this should never happen...
			};

		private U Switch<U>(Func<T, U> some, Func<U> none) =>
			Switch(
				some: some,
				none: _ => none()
			);
	}
}
