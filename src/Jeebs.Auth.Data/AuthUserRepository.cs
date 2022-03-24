// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Cryptography;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Logging;
using Jeebs.Messages;

namespace Jeebs.Auth;

/// <inheritdoc cref="IAuthUserRepository{TRoleEntity}"/>
public interface IAuthUserRepository : IAuthUserRepository<AuthUserEntity>
{
}

/// <inheritdoc cref="IAuthUserRepository{TUserEntity}"/>
public sealed class AuthUserRepository : Repository<AuthUserEntity, AuthUserId>, IAuthUserRepository
{
	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="db">IAuthDb</param>
	/// <param name="log">ILog</param>
	public AuthUserRepository(IAuthDb db, ILog<AuthUserRepository> log) : base(db, log) { }

	/// <inheritdoc/>
	public Task<Maybe<AuthUserId>> CreateAsync(string email, string plainTextPassword) =>
		CreateAsync(email, plainTextPassword, null);

	/// <inheritdoc/>
	public Task<Maybe<AuthUserId>> CreateAsync(string email, string plainTextPassword, string? friendlyName) =>
		RetrieveAsync<AuthUserEntity>(email)
			.SwitchAsync(
				some: _ => F.None<AuthUserId>(new M.UserAlreadyExistsMsg(email)),
				none: _ => CreateAsync(new()
				{
					EmailAddress = email,
					PasswordHash = plainTextPassword.HashPassword(),
					FriendlyName = friendlyName,
					IsEnabled = true
				})
			);

	/// <inheritdoc/>
	public Task<Maybe<TModel>> RetrieveAsync<TModel>(string email) =>
		QuerySingleAsync<TModel>(
			(u => u.EmailAddress, Compare.Equal, email)
		);

	/// <inheritdoc/>
	public Task<Maybe<bool>> UpdateLastSignInAsync(AuthUserId userId) =>
		Db.ExecuteAsync("UpdateUserLastSignIn", new { Id = userId.Value }, CommandType.StoredProcedure);

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>The user already exists</summary>
		/// <param name="Value">The user's email address</param>
		public sealed record class UserAlreadyExistsMsg(string Value) : WithValueMsg<string>
		{
			/// <summary>Change value name to 'Email Address'</summary>
			public override string Name { get; init; } = nameof(AuthUserEntity.EmailAddress);
		}
	}
}
