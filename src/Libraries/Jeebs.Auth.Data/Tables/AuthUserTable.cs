// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Models;
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
		public const string ColumnPrefix = "User";

		#region From AuthUserModel

		/// <inheritdoc cref="AuthUserModel.Id"/>
		public string Id =>
			ColumnPrefix + nameof(Id);

		/// <inheritdoc cref="AuthUserModel.EmailAddress"/>
		public string EmailAddress =>
			ColumnPrefix + nameof(EmailAddress);

		/// <inheritdoc cref="AuthUserModel.FriendlyName"/>
		public string FriendlyName =>
			ColumnPrefix + nameof(FriendlyName);

		/// <inheritdoc cref="AuthUserModel.GivenName"/>
		public string GivenName =>
			ColumnPrefix + nameof(GivenName);

		/// <inheritdoc cref="AuthUserModel.FriendlyName"/>
		public string FamilyName =>
			ColumnPrefix + nameof(FamilyName);

		/// <inheritdoc cref="AuthUserModel.IsSuper"/>
		public string IsSuper =>
			ColumnPrefix + nameof(IsSuper);

		#endregion

		#region From AuthUserEntity

		/// <inheritdoc cref="AuthUserEntity.Version"/>
		public string Version =>
			ColumnPrefix + nameof(Version);

		/// <inheritdoc cref="AuthUserEntity.PasswordHash"/>
		public string PasswordHash =>
			ColumnPrefix + nameof(PasswordHash);

		/// <inheritdoc cref="AuthUserEntity.IsEnabled"/>
		public string IsEnabled =>
			ColumnPrefix + nameof(IsEnabled);

		/// <inheritdoc cref="AuthUserEntity.LastSignedIn"/>
		public string LastSignedIn =>
			ColumnPrefix + nameof(LastSignedIn);

		#endregion
	}
}
