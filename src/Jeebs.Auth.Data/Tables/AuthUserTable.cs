// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data.Map;

namespace Jeebs.Auth.Data.Tables;

/// <summary>
/// Authentication User Table
/// </summary>
public sealed record class AuthUserTable() : Table(AuthDb.Schema, TableName)
{
	/// <summary>
	/// Table name will be added as a prefix to all column names
	/// </summary>
	public static readonly string TableName = "User";

	#region From AuthUserModel

	/// <inheritdoc cref="Id.IWithId.Id"/>
	public string Id =>
		TableName + nameof(Id);

	/// <inheritdoc cref="IAuthUser.EmailAddress"/>
	public string EmailAddress =>
		TableName + nameof(EmailAddress);

	/// <inheritdoc cref="IAuthUser.FriendlyName"/>
	public string FriendlyName =>
		TableName + nameof(FriendlyName);

	/// <inheritdoc cref="IAuthUser.GivenName"/>
	public string GivenName =>
		TableName + nameof(GivenName);

	/// <inheritdoc cref="IAuthUser.FriendlyName"/>
	public string FamilyName =>
		TableName + nameof(FamilyName);

	/// <inheritdoc cref="IAuthUser.IsSuper"/>
	public string IsSuper =>
		TableName + nameof(IsSuper);

	#endregion From AuthUserModel

	#region From AuthUserEntity

	/// <inheritdoc cref="AuthUserEntity.Version"/>
	public string Version =>
		TableName + nameof(Version);

	/// <inheritdoc cref="AuthUserEntity.PasswordHash"/>
	public string PasswordHash =>
		TableName + nameof(PasswordHash);

	/// <inheritdoc cref="AuthUserEntity.TotpSecret"/>
	public string TotpSecret =>
		TableName + nameof(TotpSecret);

	/// <inheritdoc cref="AuthUserEntity.TotpBackupCodes"/>
	public string TotpBackupCodes =>
		TableName + nameof(TotpBackupCodes);

	/// <inheritdoc cref="AuthUserEntity.IsEnabled"/>
	public string IsEnabled =>
		TableName + nameof(IsEnabled);

	/// <inheritdoc cref="AuthUserEntity.LastSignedIn"/>
	public string LastSignedIn =>
		TableName + nameof(LastSignedIn);

	#endregion From AuthUserEntity
}
