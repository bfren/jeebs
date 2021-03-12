// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using static JeebsF.OptionF;

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
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		public async Task<Option<TUserModel>> ValidateUserAsync<TUserModel>(string email, string password)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
			where TUserModel : IUserModel, new()
		{
			// Check email
			if (string.IsNullOrEmpty(email))
			{
				return None<TUserModel>(new Jm.Auth.Data.DataAuthProvider.ValidateUserAsync.InvalidEmailMsg());
			}

			// Check password
			if (string.IsNullOrEmpty(password))
			{
				return None<TUserModel>(new Jm.Auth.Data.DataAuthProvider.ValidateUserAsync.InvalidPasswordMsg());
			}

			throw new NotImplementedException();
		}

		/// <inheritdoc/>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		public async Task<Option<TUserModel>> ValidateUserAsync<TUserModel, TRoleModel>(string email, string password)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
			where TUserModel : IUserModel<TRoleModel>, new()
			where TRoleModel : IRoleModel, new()
		{
			throw new NotImplementedException();
		}
	}
}
