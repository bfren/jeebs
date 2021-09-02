// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Internals;

/// <summary>
/// 'Some' option - wraps value to enable safe non-null returns (see <seealso cref="None{T}"/>)
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed record class Some<T> : Option<T>
{
	/// <summary>
	/// Option Value - nullability will match the nullability of <typeparamref name="T"/>
	/// </summary>
	public T Value { get; private init; }

	/// <summary>
	/// <see cref="Some{T}"/> is only created by Return() functions and implicit operator
	/// </summary>
	/// <param name="value"></param>
	internal Some(T value) =>
		Value = value;

	/// <summary>
	/// Return:
	///    Value (if Value is not null)
	/// </summary>
	public override string ToString() =>
		base.ToString();
}
