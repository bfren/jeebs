// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	public interface IAuthUserFunc<TUserEntity> : IDbFunc<TUserEntity, AuthUserId>
		where TUserEntity : IAuthUser, IEntity
	{
		/// <summary>
		/// Retrieve a user by email address
		/// </summary>
		/// <param name="email">Email address</param>
		Task<Option<TModel>> RetrieveAsync<TModel>(string email);

		/// <summary>
		/// Update the user's last sign in to now
		/// </summary>
		/// <param name="userId">User ID</param>
		Task<Option<bool>> UpdateLastSignInAsync(AuthUserId userId);
	}
}