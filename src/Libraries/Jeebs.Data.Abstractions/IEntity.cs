// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
