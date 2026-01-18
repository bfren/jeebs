// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.Auth.Data.Tables;

/// <summary>
/// Authentication User Table.
/// </summary>
public sealed record class AuthUserTable() : Table(AuthDb.Schema, TableName)
{
	/// <summary>
	/// Table name will be added as a prefix to all column names.
	/// </summary>
	public static readonly string TableName = "user";

	#region From AuthUserModel

	/// <inheritdoc cref="StrongId.IWithId.Id"/>
	[Id]
	public string Id =>
		"user_id";

	/// <inheritdoc cref="IAuthUser.EmailAddress"/>
	public string EmailAddress =>
		"user_email";

	/// <inheritdoc cref="IAuthUser.FriendlyName"/>
	public string FriendlyName =>
		"user_friendly_name";

	/// <inheritdoc cref="IAuthUser.GivenName"/>
	public string GivenName =>
		"user_given_name";

	/// <inheritdoc cref="IAuthUser.FriendlyName"/>
	public string FamilyName =>
		"user_family_name";

	/// <inheritdoc cref="IAuthUser.IsSuper"/>
	public string IsSuper =>
		"user_is_super";

	#endregion From AuthUserModel

	#region From AuthUserEntity

	/// <inheritdoc cref="AuthUserEntity.Version"/>
	[Version]
	public string Version =>
		"user_version";

	/// <inheritdoc cref="AuthUserEntity.PasswordHash"/>
	public string PasswordHash =>
		"user_password_hash";

	/// <inheritdoc cref="AuthUserEntity.TotpSecret"/>
	public string TotpSecret =>
		"user_totp_secret";

	/// <inheritdoc cref="AuthUserEntity.TotpBackupCodes"/>
	public string TotpBackupCodes =>
		"user_totp_backup_codes";

	/// <inheritdoc cref="AuthUserEntity.IsEnabled"/>
	public string IsEnabled =>
		"user_is_enabled";

	/// <inheritdoc cref="AuthUserEntity.LastSignedIn"/>
	public string LastSignedIn =>
		"user_last_signed_in";

	#endregion From AuthUserEntity
}
