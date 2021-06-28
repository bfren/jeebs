// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// Term entity
	/// </summary>
	public abstract record WpTermEntityWithId : IWithId<WpTermId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public WpTermId Id
		{
			get =>
				new(TermId);

			init =>
				TermId = value.Value;
		}

		/// <summary>
		/// TermId
		/// </summary>
		[Id]
		public long TermId { get; init; }
	}
}
