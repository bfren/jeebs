// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// Post entity with ID properties
	/// </summary>
	public abstract record WpPostEntityWithId : IWithId<WpPostId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public WpPostId Id
		{
			get =>
				new(PostId);

			init =>
				PostId = value.Value;
		}

		/// <summary>
		/// PostId
		/// </summary>
		[Id]
		public long PostId { get; init; }
	}
}
