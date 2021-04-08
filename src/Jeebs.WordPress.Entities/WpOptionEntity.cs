// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.WordPress.Entities
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
		public string Key { get; init; } = string.Empty;

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
