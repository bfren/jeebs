// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using RndF;

namespace Jeebs.Id;

/// <summary>
/// Represents a strongly-typed ID
/// </summary>
/// <param name="Value">ID value</param>
public abstract record class StrongId(long Value) : IStrongId
{
	/// <summary>
	/// A parameterless constructor is necessary for model binding
	/// </summary>
	protected StrongId() : this(0L) { }

	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with <paramref name="value"/>
	/// </summary>
	/// <typeparam name="TId">Strong ID type</typeparam>
	/// <param name="value">ID value</param>
	public static TId NewId<TId>(long value)
		where TId : StrongId, new() =>
		new() { Value = value };

	/// <summary>
	/// Generate a new <typeparamref name="TId"/> with a random <see cref="IStrongId.Value"/>
	/// </summary>
	/// <typeparam name="TId">Strong ID type</typeparam>
	public static TId RndId<TId>()
		where TId : StrongId, new() =>
		NewId<TId>(Rnd.Lng);
}
