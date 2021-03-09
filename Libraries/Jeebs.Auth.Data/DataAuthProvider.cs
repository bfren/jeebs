// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Cryptography;
using Jeebs.Data;
using Jeebs.Data.Querying;
using Jm.Auth.Data.DataAuthProvider.ValidateUserAsync;
using Microsoft.Win32.SafeHandles;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IDataAuthProvider"/>
	public abstract class DataAuthProvider : IDataAuthProvider
	{
		private readonly IAuthDb db;

		private readonly ILog log;

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">IAuthDb</param>
		/// <param name="log">ILog</param>
		protected DataAuthProvider(IAuthDb db, ILog log) =>
			(this.db, this.log) = (db, log);

		/// <inheritdoc/>
		public async Task<Option<TUserModel>> ValidateUserAsync<TUserModel>(string email, string password)
			where TUserModel : IUserModel, new()
		{
			// Check email
			if (string.IsNullOrEmpty(email))
			{
				return Option.None<TUserModel>(new InvalidEmailMsg());
			}

			// Check password
			if (string.IsNullOrEmpty(password))
			{
				return Option.None<TUserModel>(new InvalidPasswordMsg());
			}

			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public async Task<Option<TUserModel>> ValidateUserAsync<TUserModel, TRoleModel>(string email, string password)
			where TUserModel : IUserModel<TRoleModel>, new()
			where TRoleModel : IRoleModel, new()
		{
			throw new NotImplementedException();
		}
	}
}
