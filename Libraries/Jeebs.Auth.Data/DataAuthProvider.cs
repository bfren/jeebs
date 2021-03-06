// Copyright (c) bcg|design.
// Licensed under https://bcg.mit-license.org/2013.

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
	}
}
