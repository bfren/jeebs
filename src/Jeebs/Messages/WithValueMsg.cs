// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages;

/// <inheritdoc cref="IWithValueMsg{T}"/>
public abstract record class WithValueMsg<T>() : Msg, IWithValueMsg<T>
{
	/// <inheritdoc/>
	public virtual string Name =>
		name ?? nameof(Value);

	private readonly string? name;

	/// <inheritdoc/>
	public abstract T Value { get; init; }

	/// <inheritdoc/>
	public sealed override string Format =>
		$"{Name} = {{Value}}";

	/// <inheritdoc/>
	public sealed override object[]? Args =>
		[Value ?? new object()];

	/// <summary>
	/// For testing, allow <see cref="name"/> to be set via constructor
	/// </summary>
	/// <param name="name">Value name</param>
	private protected WithValueMsg(string name) : this() =>
		this.name = name;
}
