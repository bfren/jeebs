// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// Database entity
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Primary key
		/// </summary>
		long Id { get; }
	}

	/// <summary>
	/// Database entity
	/// </summary>
	/// <typeparam name="T">Strong ID type</typeparam>
	public interface IEntity<T>
	{
		/// <summary>
		/// Primary key
		/// </summary>
		IStrongId<T> Id { get; init; }
	}
}
