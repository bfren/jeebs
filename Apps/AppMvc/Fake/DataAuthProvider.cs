// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs;
using Jeebs.Auth;
using Jeebs.Auth.Data;

namespace AppMvc.Fake
{
	public class DataAuthProvider : IDataAuthProvider
	{
		public async Task<Option<TUserModel>> ValidateUserAsync<TUserModel>(string email, string password)
			where TUserModel : IUserModel, new() =>
			new TUserModel
			{
				UserId = new(1),
				EmailAddress = "ben@bcgdesign.com",
				FriendlyName = "Ben",
				IsSuper = true
			};
	}
}
