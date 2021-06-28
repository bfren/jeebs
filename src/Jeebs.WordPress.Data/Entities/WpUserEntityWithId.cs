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
		[Ignore]
		public WpUserId Id
		{
			get =>
				new(UserId);

			init =>
				UserId = value.Value;
		}

		/// <summary>
		/// UserId
		/// </summary>
		[Id]
		public long UserId { get; init; }
	}
}
