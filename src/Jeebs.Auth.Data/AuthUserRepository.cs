// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Auth.Data.Entities;
using Jeebs.Cryptography;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Logging;
using Jeebs.Messages;

namespace Jeebs.Auth.Data;

/// <inheritdoc cref="IAuthUserRepository{TRoleEntity}"/>
public interface IAuthUserRepository : IAuthUserRepository<AuthUserEntity>
{
}

/// <inheritdoc cref="IAuthUserRepository{TUserEntity}"/>
public sealed class AuthUserRepository : Repository<AuthUserEntity, AuthUserId>, IAuthUserRepository
{
	/// <summary>
	/// IAuthDb.
	/// </summary>
	private new IAuthDb Db { get; init; }

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="db">IAuthDb</param>
	/// <param name="log">ILog</param>
	public AuthUserRepository(IAuthDb db, ILog<AuthUserRepository> log) : base(db, log) =>
		Db = db;

	/// <inheritdoc/>
	public Task<Maybe<AuthUserId>> CreateAsync(string email, string plainTextPassword) =>
		CreateAsync(email, plainTextPassword, friendlyName: null);

	/// <inheritdoc/>
	public Task<Maybe<AuthUserId>> CreateAsync(string email, string plainTextPassword, IDbTransaction transaction) =>
		CreateAsync(email, plainTextPassword, null, transaction);

	/// <inheritdoc/>
	public async Task<Maybe<AuthUserId>> CreateAsync(string email, string plainTextPassword, string? friendlyName)
	{
		using var w = await Db.StartWorkAsync();
		return await CreateAsync(email, plainTextPassword, friendlyName, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<AuthUserId>> CreateAsync(string email, string plainTextPassword, string? friendlyName, IDbTransaction transaction) =>
		RetrieveAsync<CheckAuthUserExists>(email, transaction)
			.SwitchAsync(
				some: x => F.None<AuthUserId>(new M.UserAlreadyExistsMsg(x.EmailAddress)),
				none: _ => CreateAsync(
					new()
					{
						EmailAddress = email,
						PasswordHash = plainTextPassword.HashPassword(),
						FriendlyName = friendlyName,
						IsEnabled = true
					},
					transaction
				)
			);

	/// <inheritdoc/>
	public async Task<Maybe<TModel>> RetrieveAsync<TModel>(string email)
	{
		using var w = await Db.StartWorkAsync();
		return await RetrieveAsync<TModel>(email, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public Task<Maybe<TModel>> RetrieveAsync<TModel>(string email, IDbTransaction transaction) =>
		StartFluentQuery()
			.Where(u => u.EmailAddress, Compare.Equal, email)
			.QuerySingleAsync<TModel>(transaction);

	/// <inheritdoc/>
	public async Task<Maybe<bool>> UpdateLastSignInAsync(AuthUserId userId)
	{
		using var w = await Db.StartWorkAsync();
		return await UpdateLastSignInAsync(userId, w.Transaction).ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async Task<Maybe<bool>> UpdateLastSignInAsync(AuthUserId userId, IDbTransaction transaction)
	{
		var id = userId.Value;
		return await Db
			.ExecuteAsync<int>(
				Db.Client.GetUpdateUserLastSignInQuery(),
				new { id },
				CommandType.Text,
				transaction
			)
			.BindAsync(
				x => x > 0 ? F.True : F.False
			);
	}

	private sealed record class CheckAuthUserExists(string EmailAddress);

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>The user already exists</summary>
		/// <param name="Value">The user's email address</param>
		public sealed record class UserAlreadyExistsMsg(string Value) : WithValueMsg<string>
		{
			/// <summary>Change value name to 'Email Address'</summary>
			public override string Name =>
				nameof(AuthUserEntity.EmailAddress);
		}
	}
}
