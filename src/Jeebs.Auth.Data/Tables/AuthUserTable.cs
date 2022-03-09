// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data.Map;

namespace Jeebs.Auth.Data.Tables;

/// <summary>
/// Authentication User Table
/// </summary>
public sealed record class AuthUserTable() : Table("Auth", ColumnPrefix)
{
	/// <summary>
	/// Prefix added before all columns
	/// </summary>
	public static readonly string ColumnPrefix = "User";

	#region From AuthUserModel

	/// <inheritdoc cref="IWithId.Id"/>
	public string Id =>
		ColumnPrefix + nameof(Id);

	/// <inheritdoc cref="IAuthUser.EmailAddress"/>
	public string EmailAddress =>
		ColumnPrefix + nameof(EmailAddress);

	/// <inheritdoc cref="IAuthUser.FriendlyName"/>
	public string FriendlyName =>
		ColumnPrefix + nameof(FriendlyName);

	/// <inheritdoc cref="IAuthUser.GivenName"/>
	public string GivenName =>
		ColumnPrefix + nameof(GivenName);

	/// <inheritdoc cref="IAuthUser.FriendlyName"/>
	public string FamilyName =>
		ColumnPrefix + nameof(FamilyName);

	/// <inheritdoc cref="IAuthUser.IsSuper"/>
	public string IsSuper =>
		ColumnPrefix + nameof(IsSuper);

	#endregion From AuthUserModel

	#region From AuthUserEntity

	/// <inheritdoc cref="AuthUserEntity.Version"/>
	public string Version =>
		ColumnPrefix + nameof(Version);

	/// <inheritdoc cref="AuthUserEntity.PasswordHash"/>
	public string PasswordHash =>
		ColumnPrefix + nameof(PasswordHash);

	/// <inheritdoc cref="AuthUserEntity.TotpSecret"/>
	public string TotpSecret =>
		ColumnPrefix + nameof(TotpSecret);

	/// <inheritdoc cref="AuthUserEntity.TotpBackupCodes"/>
	public string TotpBackupCodes =>
		ColumnPrefix + nameof(TotpBackupCodes);

	/// <inheritdoc cref="AuthUserEntity.IsEnabled"/>
	public string IsEnabled =>
		ColumnPrefix + nameof(IsEnabled);

	/// <inheritdoc cref="AuthUserEntity.LastSignedIn"/>
	public string LastSignedIn =>
		ColumnPrefix + nameof(LastSignedIn);

	#endregion From AuthUserEntity
}
