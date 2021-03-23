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
	/// <summary>
	/// Provides Authentication functions for interacting with Users
	/// </summary>
	internal sealed class AuthUserFunc : DbFunc<AuthUserEntity, AuthUserId>
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">AuthDb</param>
		/// <param name="log">ILog</param>
		internal AuthUserFunc(AuthDb db, ILog log) : base(db, log) { }

		protected override void WriteToLog(string message, object[] args) =>
			Log.Warning(message, args);

		/// <summary>
		/// Retrieve a user by email address
		/// </summary>
		/// <param name="email">Email address</param>
		internal Task<Option<AuthUserEntity>> RetrieveAsync(string email) =>
			QuerySingleAsync<AuthUserEntity>(
				(u => u.EmailAddress, SearchOperator.Equal, email)
			);

		/// <summary>
		/// Update the user's last sign in to now
		/// </summary>
		/// <param name="userId">User ID</param>
		internal Task<Option<bool>> UpdateLastSignInAsync(AuthUserId userId) =>
			Db.ExecuteAsync("UpdateUserLastSignIn", new { Id = userId.Value }, CommandType.StoredProcedure);
	}
}
