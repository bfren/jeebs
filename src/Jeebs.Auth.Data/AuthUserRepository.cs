// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Ids;
using Jeebs.Cryptography;
using Jeebs.Data.Enums;
using Jeebs.Data.Repository;
using Jeebs.Logging;

namespace Jeebs.Auth.Data;

/// <inheritdoc cref="IAuthUserRepository"/>
public sealed class AuthUserRepository : Repository<AuthUserEntity, AuthUserId>, IAuthUserRepository
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
	public Task<Result<AuthUserId>> CreateAsync(string email, string plainTextPassword, string? friendlyName) =>
		RetrieveAsync<CheckAuthUserExists>(
			email
		)
		.MatchAsync(
			fOk: _ => R.Fail("User with {Email} already exists.", email)
				.Ctx(nameof(AuthUserRepository), nameof(CreateAsync)),
			fFail: _ => CreateAsync(new()
			{
				EmailAddress = email,
				FriendlyName = friendlyName,
				PasswordHash = plainTextPassword.HashPassword(),
				IsEnabled = true
			})
		);

	/// <inheritdoc/>
	public Task<Result<TModel>> RetrieveAsync<TModel>(string email) =>
		Fluent()
			.Where(u => u.EmailAddress, Compare.Equal, email)
			.QuerySingleAsync<TModel>();

	/// <inheritdoc/>
	public Task<Result<bool>> UpdateLastSignInAsync(AuthUserId userId)
	{
		var id = userId.Value;
		return Db.ExecuteAsync(Db.Client.GetUpdateUserLastSignInQuery(), new { id });
	}

	private sealed record class CheckAuthUserExists(string EmailAddress);
}
