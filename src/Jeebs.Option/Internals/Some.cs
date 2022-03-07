// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Internals;

/// <summary>
/// 'Some' Maybe - wraps value to enable safe non-null returns (see <seealso cref="None{T}"/>)
/// </summary>
/// <typeparam name="T">Maybe value type</typeparam>
public sealed record class Some<T> : Maybe<T>
{
	/// <summary>
	/// Maybe value - nullability will match the nullability of <typeparamref name="T"/>
	/// </summary>
	public T Value { get; private init; }

	/// <summary>
	/// Only allow internal creation by Some() functions and implicit operator
	/// </summary>
	/// <param name="value">Value to wrap</param>
	internal Some(T value) =>
		Value = value;
}
