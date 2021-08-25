// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs
{
	/// <summary>
	/// Object (Entity or Model) with Version property
	/// </summary>
	public interface IWithVersion : IWithId
	{
		/// <summary>
		/// Version
		/// </summary>
		ulong Version { get; }
	}

	/// <inheritdoc cref="IWithVersion"/>
	/// <typeparam name="T">IStrongId Type</typeparam>
	public interface IWithVersion<T> : IWithId<T>, IWithVersion
		where T : IStrongId
	{ }
}
