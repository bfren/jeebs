// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Database methods for authentication
	/// </summary>
	public interface IAuthDb
	{
		Task<IR<string>> GetUserPasswordHashAsync(IOkV<string> email);

		Task<IR<UserId>> UpdateUserLastSignIn(IOkV<string> email);

		Task<IR<TUserModel>> GetUserAsync<TUserModel>(UserId userId)
			where TUserModel : IUserModel;
	}
}
