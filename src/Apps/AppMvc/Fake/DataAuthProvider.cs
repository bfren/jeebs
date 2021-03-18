// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Auth;
using static F.OptionF;

namespace AppMvc.Fake
{
	public class DataAuthProviderWithRole : IDataAuthProvider<Models.UserModel, Models.RoleModel>
	{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		public async Task<Option<Models.UserModel>> ValidateUserAsync(string email, string password)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			if (password == "fail")
			{
				return None<Models.UserModel>(true);
			}

			return new Models.UserModel
			{
				UserId = new(1),
				EmailAddress = "ben@bcgdesign.com",
				FriendlyName = "Ben",
				IsSuper = true,
				Roles = new List<Models.RoleModel>(new[]
				{
					new Models.RoleModel { Name = "One" },
					new Models.RoleModel { Name = "Two" },
				})
			};
		}
	}
}
