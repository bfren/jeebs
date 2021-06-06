// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.WordPress.Data.Entities
{
	/// <summary>
	/// Option entity
	/// </summary>
	public abstract record WpOptionEntity : IWithId<WpOptionId>
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
		public long OptionId { get; init; }

		/// <summary>
		/// Key
		/// </summary>
		public string? Key { get; init; }

		/// <summary>
		/// Value
		/// </summary>
		public string Value { get; init; } = string.Empty;

		/// <summary>
		/// IsAutoloaded
		/// </summary>
		public bool IsAutoloaded { get; init; }
	}
}
