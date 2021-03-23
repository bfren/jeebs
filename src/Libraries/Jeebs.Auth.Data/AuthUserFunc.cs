// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Data;
using Jeebs.Data.Enums;

namespace Jeebs.Auth
{
	/// <summary>
	/// Provides Authentication functions for interacting with Users
	/// </summary>
	public sealed class AuthUserFunc : DbFunc<AuthUserEntity, AuthUserId>
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">AuthDb</param>
		/// <param name="log">ILog</param>
		internal AuthUserFunc(AuthDb db, ILog log) : base(db, log) { }

		/// <summary>
		/// Retrieve a user by email address
		/// </summary>
		/// <param name="email">Email address</param>
		public Task<Option<TModel>> RetrieveAsync<TModel>(string email) =>
			QuerySingleAsync<TModel>(
				(u => u.EmailAddress, SearchOperator.Equal, email)
			);

		/// <inheritdoc cref="RetrieveAsync{TModel}(string)"/>
		internal Task<Option<AuthUserEntity>> RetrieveAsync(string email) =>
			RetrieveAsync<AuthUserEntity>(email);
	}
}
