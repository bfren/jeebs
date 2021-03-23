// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Data;
using Jeebs.Data.Enums;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthUserFunc{TUserEntity}"/>
	public sealed class AuthUserFunc : DbFunc<AuthUserEntity, AuthUserId>, IAuthUserFunc<AuthUserEntity>
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">IAuthDb</param>
		/// <param name="log">ILog</param>
		public AuthUserFunc(IAuthDb db, ILog<AuthUserFunc> log) : base(db, log) { }

		/// <inheritdoc/>
		public Task<Option<TModel>> RetrieveAsync<TModel>(string email) =>
			QuerySingleAsync<TModel>(
				(u => u.EmailAddress, SearchOperator.Equal, email)
			);

		/// <inheritdoc/>
		public Task<Option<bool>> UpdateLastSignInAsync(AuthUserId userId) =>
			Db.ExecuteAsync("UpdateUserLastSignIn", new { Id = userId.Value }, CommandType.StoredProcedure);
	}
}
