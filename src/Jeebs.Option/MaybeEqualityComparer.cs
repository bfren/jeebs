// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Internals;

namespace Jeebs;

/// <summary>
/// Maybe Equality Comparer
/// </summary>
public sealed class MaybeEqualityComparer<T> : IEqualityComparer<Maybe<T>>
{
	/// <summary>
	/// Compare two <see cref="Maybe{T}"/> objects
	/// <para>If both are a <see cref="Some{T}"/> each <see cref="Some{T}.Value"/> will be compared</para>
	/// <para>If both are a <see cref="None{T}"/> this will return true</para>
	/// <para>Otherwise this will return false</para>
	/// </summary>
	/// <param name="x">First Maybe</param>
	/// <param name="y">Second Maybe</param>
	public bool Equals(Maybe<T>? x, Maybe<T>? y) =>
		x switch
		{
			Some<T> a when y is Some<T> b =>
				Equals(objA: a.Value, objB: b.Value),

			None<T> a when y is None<T> b =>
				Equals(objA: a.Reason, objB: b.Reason),

			_ =>
				false
		};

	/// <inheritdoc cref="Maybe{T}.GetHashCode"/>
	public int GetHashCode(Maybe<T> obj) =>
		obj.GetHashCode();
}
