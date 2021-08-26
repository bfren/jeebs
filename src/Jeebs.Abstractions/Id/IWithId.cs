﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs
{
	/// <summary>
	/// Object (Entity or Model) with ID property
	/// </summary>
	public interface IWithId
	{
		/// <summary>
		/// IStrongId (wrapping a long value)
		/// </summary>
		IStrongId Id { get; }
	}

	/// <inheritdoc cref="IWithId"/>
	/// <typeparam name="T">IStrongId Type</typeparam>
	public interface IWithId<T> : IWithId
		where T : IStrongId
	{
		/// <summary>
		/// Strongly-typed IStrongId (wrapping a long value)
		/// </summary>
		new T Id { get; init; }

		/// <inheritdoc/>
		IStrongId IWithId.Id =>
			Id;
	}
}
