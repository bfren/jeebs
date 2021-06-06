// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
