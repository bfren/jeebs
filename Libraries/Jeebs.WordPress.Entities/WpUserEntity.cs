// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// User entity
	/// </summary>
	public abstract record WpUserEntity : IEntity, IEntity<long>
	{
		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		long IEntity.Id =>
			Id.Value;

		/// <summary>
		/// Id
		/// </summary>
		[Ignore]
		public IStrongId<long> Id
		{
			get =>
				new WpUserId(UserId);

			init =>
				UserId = value.Value;
		}

		/// <summary>
		/// UserId
		/// </summary>
		[Id]
		public long UserId { get; init; }

		/// <summary>
		/// Username
		/// </summary>
		public string Username { get; init; } = string.Empty;

		/// <summary>
		/// Password
		/// </summary>
		public string Password { get; init; } = string.Empty;

		/// <summary>
		/// Slug
		/// </summary>
		public string Slug { get; init; } = string.Empty;

		/// <summary>
		/// Email
		/// </summary>
		public string Email { get; init; } = string.Empty;

		/// <summary>
		/// Url
		/// </summary>
		public string Url { get; init; } = string.Empty;

		/// <summary>
		/// RegisteredOn
		/// </summary>
		public DateTime RegisteredOn { get; init; }

		/// <summary>
		/// ActivationKey
		/// </summary>
		public string ActivationKey { get; init; } = string.Empty;

		/// <summary>
		/// Status
		/// </summary>
		public int Status { get; init; }

		/// <summary>
		/// DisplayName
		/// </summary>
		public string DisplayName { get; init; } = string.Empty;
	}
}
