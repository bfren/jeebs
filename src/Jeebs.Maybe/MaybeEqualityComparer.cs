// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections;
using System.Collections.Generic;
using Jeebs.Internals;

namespace Jeebs;

/// <summary>
/// Maybe Equality Comparer
/// </summary>
/// <typeparam name="T">Maybe value type</typeparam>
public sealed class MaybeEqualityComparer<T> : IEqualityComparer<Maybe<T>>, IEqualityComparer
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

	/// <inheritdoc cref="Equals(Maybe{T}?, Maybe{T}?)"/>
	public new bool Equals(object? x, object? y)
	{
		if (x == y)
		{
			return true;
		}

		if (x == null || y == null)
		{
			return false;
		}

		if (x is Maybe<T> a && y is Maybe<T> b)
		{
			return Equals(a, b);
		}

		return false;
	}

	/// <inheritdoc cref="Maybe{T}.GetHashCode"/>
	public int GetHashCode(object obj)
	{
		if (obj == null)
		{
			return 0;
		}

		if (obj is Maybe<T> x)
		{
			return GetHashCode(x);
		}

		throw new System.ArgumentException("", nameof(obj));
	}
}
