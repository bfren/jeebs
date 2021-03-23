// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Models;
using Jeebs.Data;

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

		/// <inheritdoc/>
		internal Task<Option<AuthUserEntity>> RetrieveAsync(string email)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		internal Task<Option<bool>> UpdateLastSignInAsync(AuthUserId userId)
		{
			throw new NotImplementedException();
		}
	}
}
