// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

/// <summary>
/// Framework message with a value
/// </summary>
/// <typeparam name="T">Value type</typeparam>
public abstract record class WithValueMsg<T> : Msg
{
	/// <summary>
	/// Message Value property name
	/// </summary>
	public virtual string Name { get; init; } = "Value";

	/// <summary>
	/// Message Value
	/// </summary>
	public abstract T Value { get; init; }

	/// <inheritdoc/>
	public sealed override string Format =>
		"{{ " + Name + " = {Value} }}";

	/// <inheritdoc/>
	public sealed override object[]? Args =>
		new[] { Value ?? new object() };
}
