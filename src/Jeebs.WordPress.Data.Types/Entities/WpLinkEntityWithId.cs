// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// Link entity with ID properties
	/// </summary>
	public abstract record WpLinkEntityWithId : IWithId<WpLinkId>
	{
		/// <summary>
		/// Id
		/// </summary>
		public WpLinkId Id { get; init; } = new();
	}
}
