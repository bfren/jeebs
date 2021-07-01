// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

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
		[Ignore]
		public WpLinkId Id
		{
			get =>
				new(LinkId);

			init =>
				LinkId = value.Value;
		}

		/// <summary>
		/// LinkId
		/// </summary>
		[Id]
		public ulong LinkId { get; init; }
	}
}
