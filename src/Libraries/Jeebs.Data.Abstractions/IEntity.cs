// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data
{
	/// <summary>
	/// Database entity
	/// </summary>
	public interface IEntity : IWithId { }

	/// <inheritdoc cref="IEntity"/>
	/// <typeparam name="T">StrongId Type</typeparam>
	public interface IEntity<T> : IEntity, IWithId<T>
		where T : StrongId
	{ }
}
