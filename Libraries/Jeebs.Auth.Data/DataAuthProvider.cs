// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using static F.OptionF;
using Msg = Jeebs.Auth.DataAuthProviderMsg;

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
				return None<TUserModel, Msg.InvalidEmailMsg>();
			}

			// Check password
			if (string.IsNullOrEmpty(password))
			{
				return None<TUserModel, Msg.InvalidPasswordMsg>();
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

	namespace DataAuthProviderMsg
	{
		/// <summary>Null or empty email address</summary>
		public sealed record InvalidEmailMsg : IMsg { }

		/// <summary>Null or empty password</summary>
		public sealed record InvalidPasswordMsg : IMsg { }
	}
}
