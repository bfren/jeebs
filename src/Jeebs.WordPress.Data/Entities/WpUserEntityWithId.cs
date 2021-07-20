// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// User entity
	/// </summary>
	public abstract record WpUserEntityWithId : IWithId<WpUserId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Id]
		public WpUserId Id { get; init; } = new();
	}
}
