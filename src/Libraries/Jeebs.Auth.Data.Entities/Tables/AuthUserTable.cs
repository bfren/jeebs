// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data;

namespace Jeebs.Auth.Data.Tables
{
	/// <summary>
	/// Authentication User Table
	/// </summary>
	public sealed record AuthUserTable() : Table("auth_user")
	{
		/// <summary>
		/// Prefix added before all columns
		/// </summary>
		public const string Prefix = "User";

		/// <inheritdoc cref="AuthUserEntity.UserId"/>
		public string UserId =>
			nameof(UserId);

		/// <inheritdoc cref="AuthUserEntity.EmailAddress"/>
		public string EmailAddress =>
			Prefix + nameof(EmailAddress);

		/// <inheritdoc cref="AuthUserEntity.PasswordHash"/>
		public string PasswordHash =>
			Prefix + nameof(PasswordHash);

		/// <inheritdoc cref="AuthUserEntity.FriendlyName"/>
		public string FriendlyName =>
			Prefix + nameof(FriendlyName);

		/// <inheritdoc cref="AuthUserEntity.GivenName"/>
		public string GivenName =>
			Prefix + nameof(GivenName);

		/// <inheritdoc cref="AuthUserEntity.FriendlyName"/>
		public string FamilyName =>
			Prefix + nameof(FamilyName);

		/// <inheritdoc cref="AuthUserEntity.IsEnabled"/>
		public string IsEnabled =>
			Prefix + nameof(IsEnabled);

		/// <inheritdoc cref="AuthUserEntity.IsSuper"/>
		public string IsSuper =>
			Prefix + nameof(IsSuper);

		/// <inheritdoc cref="AuthUserEntity.LastSignedIn"/>
		public string LastSignedIn =>
			Prefix + nameof(LastSignedIn);
	}
}
