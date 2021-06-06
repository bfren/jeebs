// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Data
{
	/// <summary>
	/// Object (Entity or Model) with ID property
	/// </summary>
	public interface IWithId
	{
		/// <summary>
		/// StrongId (wrapping a long value)
		/// </summary>
		StrongId Id { get; }
	}

	/// <inheritdoc cref="IWithId"/>
	/// <typeparam name="T">StrongId Type</typeparam>
	public interface IWithId<T> : IWithId
		where T : StrongId
	{
		/// <summary>
		/// Strongly-typed StrongId (wrapping a long value)
		/// </summary>
		new T Id { get; init; }

		/// <inheritdoc/>
		StrongId IWithId.Id =>
			Id;
	}
}
