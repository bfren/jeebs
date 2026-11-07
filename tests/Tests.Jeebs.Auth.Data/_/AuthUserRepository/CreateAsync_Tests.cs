// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.Auth.Data.AuthUserRepository_Tests;

public class CreateAsync_Tests
{
	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData("\t\n")]
	public async Task Password_Empty_Or_Whitespace__Returns_Fail__Does_Not_Use_Database(string password)
	{
		// Arrange
		var db = Substitute.For<IAuthDb>();
		var repo = new AuthUserRepository(db, Substitute.For<ILog<AuthUserRepository>>());
		db.ClearReceivedCalls();

		// Act
		var result = await repo.CreateAsync(Rnd.Str, password, Rnd.Str);

		// Assert
		_ = result.AssertFailure("Password cannot be empty.");
		Assert.Empty(db.ReceivedCalls());
	}
}
