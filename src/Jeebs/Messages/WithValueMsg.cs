// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages;

/// <inheritdoc cref="IWithValueMsg{T}"/>
public abstract record class WithValueMsg<T> : Msg, IWithValueMsg<T>
{
	/// <inheritdoc/>
	public virtual string Name { get; init; } = nameof(Value);

	/// <inheritdoc/>
	public abstract T Value { get; init; }

	/// <inheritdoc/>
	public sealed override string Format =>
		$"{Name} = {{Value}}";

	/// <inheritdoc/>
	public sealed override object[]? Args =>
		new[] { Value ?? new object() };
}
