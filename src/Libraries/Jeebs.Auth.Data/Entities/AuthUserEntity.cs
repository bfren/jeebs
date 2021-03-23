// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Auth.Data.Models;
using Jeebs.Data;

namespace Jeebs.Auth.Data.Entities
{
	/// <summary>
	/// Authentication User Entity
	/// </summary>
	internal sealed record AuthUserEntity : AuthUserModel, IEntityWithVersion
	{
		/// <inheritdoc/>
		[Version]
		public long Version { get; init; }

		/// <summary>
		/// The user's encrypted password
		/// </summary>
		public string PasswordHash { get; init; }

		/// <summary>
		/// Whether or not the user account is enabled
		/// </summary>
		public bool IsEnabled { get; init; }

		/// <summary>
		/// The last time the user signed in
		/// </summary>
		public DateTimeOffset? LastSignedIn { get; init; }

		/// <summary>
		/// Create User with specified password hash
		/// </summary>
		/// <param name="passwordHash">Password hash</param>
		internal AuthUserEntity(string passwordHash) =>
			PasswordHash = passwordHash;
	}
}
