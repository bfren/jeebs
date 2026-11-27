// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Ids;
using Jeebs.Cryptography;
using Jeebs.Data.Common;
using Jeebs.Data.Enums;
using Jeebs.Logging;

namespace Jeebs.Auth.Data;

/// <inheritdoc cref="IAuthUserRepository"/>
public sealed partial class AuthUserRepository : Repository<AuthUserEntity, AuthUserId>, IAuthUserRepository
{
	/// <summary>
	/// IAuthDb.
	/// </summary>
	private new IAuthDb Db { get; init; }

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="db">IAuthDb.</param>
	/// <param name="log">ILog.</param>
	public AuthUserRepository(IAuthDb db, ILog<AuthUserRepository> log) : base(db, log) =>
		Db = db;

	/// <inheritdoc/>
	public Task<Result<AuthUserId>> CreateAsync(string email, string plainTextPassword) =>
		CreateAsync(email, plainTextPassword, null);

	/// <inheritdoc/>
	public async Task<Result<AuthUserId>> CreateAsync(string email, string plainTextPassword, string? friendlyName)
	{
		if (string.IsNullOrWhiteSpace(plainTextPassword))
		{
			return PasswordEmpty();
		}

		email = NormaliseEmail(email);
		return await
			RetrieveAsync<CheckAuthUserExists>(
				email
			)
			.MatchAsync(
				fOk: _ => UserAlreadyExists(email),
				fFail: _ => CreateAsync(new()
				{
					EmailAddress = email,
					FriendlyName = friendlyName,
					PasswordHash = plainTextPassword.HashPassword(),
					IsEnabled = true
				})
			);
	}

	/// <inheritdoc/>
	public Task<Result<TModel>> RetrieveAsync<TModel>(string email) =>
		Fluent()
			.Where(u => u.EmailAddress, Compare.Equal, NormaliseEmail(email))
			.QuerySingleAsync<TModel>();

	/// <summary>
	/// Emails are stored and looked up trimmed and lower case, because PostgreSQL and SQLite compare
	/// text exactly (MySQL's column ignores case).
	/// </summary>
	/// <param name="email">Email address.</param>
	internal static string NormaliseEmail(string email) =>
		email.Trim().ToLowerInvariant();

	/// <inheritdoc/>
	public Task<Result<bool>> UpdateLastSignInAsync(AuthUserId userId)
	{
		var id = userId.Value;
		return Db.ExecuteAsync(Db.Client.GetUpdateUserLastSignInQuery(), new { id });
	}

	private sealed record class CheckAuthUserExists(string EmailAddress);

	[Failure("Password cannot be empty.")]
	private static partial Failure PasswordEmpty();

	[Failure("User with {Email} already exists.")]
	private static partial Failure UserAlreadyExists(string email);
}
