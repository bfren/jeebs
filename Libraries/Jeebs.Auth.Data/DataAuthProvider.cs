// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.Auth.Data;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IDataAuthProvider"/>
	public abstract class DataAuthProvider : IDataAuthProvider
	{
		/// <inheritdoc/>
		public async Task<Option<TUserModel>> ValidateUserAsync<TUserModel>(string email, string password)
			where TUserModel : IUserModel, new()
		{


			throw new NotImplementedException();
		}

		public async Task<Option<TUserModel>> ValidateUserAsync<TUserModel, TRoleModel>(string email, string password)
			where TUserModel : IUserModel<TRoleModel>, new()
			where TRoleModel : IRoleModel, new()
		{
			throw new NotImplementedException();
		}
	}
}
