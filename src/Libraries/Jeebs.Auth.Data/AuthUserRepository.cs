// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Cryptography;
using Jeebs.Data;
using Jeebs.Data.Enums;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthUserRepository{TRoleEntity}"/>
	public interface IAuthUserRepository : IAuthUserRepository<AuthUserEntity>
	{ }

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
		public Task<Option<AuthUserId>> CreateAsync(
			string email,
			string password,
			string? friendlyName,
			IDbTransaction? transaction = null
		)
		{
			var user = new AuthUserEntity
			{
				EmailAddress = email,
				PasswordHash = password.HashPassword(),
				FriendlyName = friendlyName,
				IsEnabled = true
			};

			return CreateAsync(user, transaction);
		}

		/// <inheritdoc/>
		public Task<Option<TModel>> RetrieveAsync<TModel>(string email) =>
			QuerySingleAsync<TModel>(
				(u => u.EmailAddress, SearchOperator.Equal, email)
			);

		/// <inheritdoc/>
		public Task<Option<bool>> UpdateLastSignInAsync(AuthUserId userId, IDbTransaction? transaction = null) =>
			Db.ExecuteAsync("UpdateUserLastSignIn", new { Id = userId.Value }, CommandType.StoredProcedure, transaction);
	}
}
