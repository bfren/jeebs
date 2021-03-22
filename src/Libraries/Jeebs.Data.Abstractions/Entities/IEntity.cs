// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data
{
	/// <summary>
	/// Database entity
	/// </summary>
	public interface IEntity
	{
		/// <summary>
		/// Primary key
		/// </summary>
		StrongId Id { get; init; }
	}
}
