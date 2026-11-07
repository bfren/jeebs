// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Ids;
using Jeebs.Auth.Data.Models;

namespace Jeebs.Auth.Data.AuthDataProvider_Tests;

public class ChangeUserPasswordAsync_Tests
{
	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData("\t\n")]
	public async Task NewPassword_Empty_Or_Whitespace__Returns_Fail__Does_Not_Retrieve_User(string newPassword)
	{
		// Arrange
		var user = Substitute.For<IAuthUserRepository>();
		var provider = new AuthDataProvider(
			Substitute.For<IAuthDb>(),
			user,
			Substitute.For<IAuthRoleRepository>(),
			Substitute.For<IAuthUserRoleRepository>()
		);
		var model = new AuthChangePasswordModel(IdGen.LongId<AuthUserId>(), Rnd.Lng, Rnd.Str, newPassword, newPassword);

		// Act
		var result = await provider.ChangeUserPasswordAsync(model);

		// Assert
		_ = result.AssertFailure("New password cannot be empty.");
		await user.DidNotReceiveWithAnyArgs().RetrieveAsync<AuthUserEntity>(default(AuthUserId)!);
	}
}
