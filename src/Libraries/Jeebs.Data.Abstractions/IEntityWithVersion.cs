// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data
{
	/// <summary>
	/// Database entity with version
	/// </summary>
	public interface IEntityWithVersion : IEntity, IWithVersion { }

	/// <inheritdoc cref="IEntityWithVersion"/>
	/// <typeparam name="T">StrongId Type</typeparam>
	public interface IEntityWithVersion<T> : IEntityWithVersion, IWithId<T>
		where T : StrongId
	{ }
}
