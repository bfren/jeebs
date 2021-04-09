// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
		long Version { get; }
	}

	/// <inheritdoc cref="IWithVersion"/>
	/// <typeparam name="T">StrongId Type</typeparam>
	public interface IWithVersion<T> : IWithId<T>, IWithVersion
		where T : StrongId
	{ }
}
