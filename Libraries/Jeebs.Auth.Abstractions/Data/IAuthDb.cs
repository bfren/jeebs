// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Database methods for authentication
	/// </summary>
	public interface IAuthDb
	{
		Task<Option<string>> GetUserPasswordHashAsync(string email);

		Task<Option<UserId>> UpdateUserLastSignIn(string email);

		Task<Option<TUserModel>> GetUserAsync<TUserModel>(UserId userId)
			where TUserModel : IUserModel;
	}
}
