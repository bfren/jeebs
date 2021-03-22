// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data;

namespace Jeebs.Auth.Data.Tables
{
	/// <summary>
	/// User Table
	/// </summary>
	public sealed record UserTable : Table
	{
		/// <summary>
		/// Prefix added before all columns
		/// </summary>
		public const string Prefix = "User";

		/// <inheritdoc cref="UserEntity.UserId"/>
		public string UserId =>
			nameof(UserId);

		/// <inheritdoc cref="UserEntity.EmailAddress"/>
		public string EmailAddress =>
			Prefix + nameof(EmailAddress);

		/// <inheritdoc cref="UserEntity.PasswordHash"/>
		public string PasswordHash =>
			Prefix + nameof(PasswordHash);

		/// <inheritdoc cref="UserEntity.FriendlyName"/>
		public string FriendlyName =>
			Prefix + nameof(FriendlyName);

		/// <inheritdoc cref="UserEntity.GivenName"/>
		public string GivenName =>
			Prefix + nameof(GivenName);

		/// <inheritdoc cref="UserEntity.FriendlyName"/>
		public string FamilyName =>
			Prefix + nameof(FamilyName);

		/// <inheritdoc cref="UserEntity.IsEnabled"/>
		public string IsEnabled =>
			Prefix + nameof(IsEnabled);

		/// <inheritdoc cref="UserEntity.IsSuper"/>
		public string IsSuper =>
			Prefix + nameof(IsSuper);

		/// <inheritdoc cref="UserEntity.LastSignedIn"/>
		public string LastSignedIn =>
			Prefix + nameof(LastSignedIn);

		/// <summary>
		/// Create object
		/// </summary>
		public UserTable() : base("auth_user") { }
	}
}
