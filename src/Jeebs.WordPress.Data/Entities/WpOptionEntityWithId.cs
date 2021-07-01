// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// Option entity with ID properties
	/// </summary>
	public abstract record WpOptionEntityWithId : IWithId<WpOptionId>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public WpOptionId Id
		{
			get =>
				new(OptionId);

			init =>
				OptionId = value.Value;
		}

		/// <summary>
		/// OptionId
		/// </summary>
		[Id]
		public ulong OptionId { get; init; }
	}
}
