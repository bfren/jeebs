// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Ids;
using Jeebs.Cryptography;
using Sodium;

namespace Jeebs.Auth.Data.AuthDataProvider_Tests;

public class ValidateUserAsync_Tests
{
	private static (AuthDataProvider, IAuthUserRepository) Setup(AuthUserEntity? entity)
	{
		var user = Substitute.For<IAuthUserRepository>();
		user.RetrieveAsync<AuthUserEntity>(Arg.Any<string>()).Returns(entity switch
		{
			AuthUserEntity x =>
				R.Wrap(x),

			_ =>
				R.Fail("User not found.")
		});
		user.UpdateAsync(Arg.Any<AuthUserEntity>()).Returns(R.True);

		var provider = new AuthDataProvider(
			Substitute.For<IAuthDb>(),
			user,
			Substitute.For<IAuthRoleRepository>(),
			Substitute.For<IAuthUserRoleRepository>()
		);

		return (provider, user);
	}

	private static AuthUserEntity User(string passwordHash, bool isEnabled = true) =>
		new(IdGen.LongId<AuthUserId>(), Rnd.Str, passwordHash) { IsEnabled = isEnabled };

	private static string OldHash(string password) =>
		PasswordHash.ArgonHashString(password, PasswordHash.StrengthArgon.Moderate).TrimEnd('\0');

	[Fact]
	public async Task Current_Hash__Correct_Password__Returns_True__Does_Not_Update()
	{
		// Arrange
		var password = Rnd.Str;
		var (provider, user) = Setup(User(password.HashPassword()));

		// Act
		var result = await provider.ValidateUserAsync(Rnd.Str, password);

		// Assert
		Assert.True(result.AssertOk());
		await user.DidNotReceiveWithAnyArgs().UpdateAsync<AuthUserEntity>(default!);
	}

	[Fact]
	public async Task Old_Hash__Correct_Password__Returns_True__Saves_New_Hash()
	{
		// Arrange
		var password = Rnd.Str;
		var (provider, user) = Setup(User(OldHash(password)));

		// Act
		var result = await provider.ValidateUserAsync(Rnd.Str, password);

		// Assert
		Assert.True(result.AssertOk());
		await user.Received(1).UpdateAsync(Arg.Is<AuthUserEntity>(x =>
			x.PasswordHash.VerifyPassword(password) && !x.PasswordHash.PasswordNeedsRehash()
		));
	}

	[Fact]
	public async Task Old_Hash__Update_Fails__Still_Returns_True()
	{
		// Arrange
		var password = Rnd.Str;
		var (provider, user) = Setup(User(OldHash(password)));
		user.UpdateAsync(Arg.Any<AuthUserEntity>()).Returns(R.Fail("Version conflict."));

		// Act
		var result = await provider.ValidateUserAsync(Rnd.Str, password);

		// Assert
		Assert.True(result.AssertOk());
	}

	[Fact]
	public async Task Old_Hash__Wrong_Password__Returns_Fail__Does_Not_Update()
	{
		// Arrange
		var (provider, user) = Setup(User(OldHash(Rnd.Str)));

		// Act
		var result = await provider.ValidateUserAsync(Rnd.Str, Rnd.Str);

		// Assert
		_ = result.AssertFailure("Invalid password.");
		await user.DidNotReceiveWithAnyArgs().UpdateAsync<AuthUserEntity>(default!);
	}

	[Fact]
	public async Task User_Not_Enabled__Returns_Fail()
	{
		// Arrange
		var password = Rnd.Str;
		var (provider, _) = Setup(User(password.HashPassword(), isEnabled: false));

		// Act
		var result = await provider.ValidateUserAsync(Rnd.Str, password);

		// Assert
		_ = result.AssertFailure("User {Email} is not enabled.");
	}

	[Fact]
	public async Task User_Not_Found__Returns_Fail()
	{
		// Arrange
		var (provider, _) = Setup(null);

		// Act
		var result = await provider.ValidateUserAsync(Rnd.Str, Rnd.Str);

		// Assert
		_ = result.AssertFailure("User not found.");
	}
}
