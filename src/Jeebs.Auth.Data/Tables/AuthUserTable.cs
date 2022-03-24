// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data.Map;

namespace Jeebs.Auth.Data.Tables;

/// <summary>
/// Authentication User Table
/// </summary>
public sealed record class AuthUserTable() : Table(AuthDb.Schema, Name)
{
	/// <summary>
	/// Table name will be added as a prefix to all column names
	/// </summary>
	public static readonly string Name = "User";

	#region From AuthUserModel

	/// <inheritdoc cref="Id.IWithId.Id"/>
	public string Id =>
		Name + nameof(Id);

	/// <inheritdoc cref="IAuthUser.EmailAddress"/>
	public string EmailAddress =>
		Name + nameof(EmailAddress);

	/// <inheritdoc cref="IAuthUser.FriendlyName"/>
	public string FriendlyName =>
		Name + nameof(FriendlyName);

	/// <inheritdoc cref="IAuthUser.GivenName"/>
	public string GivenName =>
		Name + nameof(GivenName);

	/// <inheritdoc cref="IAuthUser.FriendlyName"/>
	public string FamilyName =>
		Name + nameof(FamilyName);

	/// <inheritdoc cref="IAuthUser.IsSuper"/>
	public string IsSuper =>
		Name + nameof(IsSuper);

	#endregion From AuthUserModel

	#region From AuthUserEntity

	/// <inheritdoc cref="AuthUserEntity.Version"/>
	public string Version =>
		Name + nameof(Version);

	/// <inheritdoc cref="AuthUserEntity.PasswordHash"/>
	public string PasswordHash =>
		Name + nameof(PasswordHash);

	/// <inheritdoc cref="AuthUserEntity.TotpSecret"/>
	public string TotpSecret =>
		Name + nameof(TotpSecret);

	/// <inheritdoc cref="AuthUserEntity.TotpBackupCodes"/>
	public string TotpBackupCodes =>
		Name + nameof(TotpBackupCodes);

	/// <inheritdoc cref="AuthUserEntity.IsEnabled"/>
	public string IsEnabled =>
		Name + nameof(IsEnabled);

	/// <inheritdoc cref="AuthUserEntity.LastSignedIn"/>
	public string LastSignedIn =>
		Name + nameof(LastSignedIn);

	#endregion From AuthUserEntity
}
