// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using Jeebs.Auth.Data.Ids;
using Jeebs.Mvc.Auth.Models;
using CT = Jeebs.Auth.Jwt.Constants.ClaimTypes;

namespace Jeebs.Mvc.Auth.Functions.AuthF_Tests;

public class GetPrincipalAsync_Tests
{
	[Fact]
	public async Task Returns_ClaimsPrincipal_With_User_Info_Claims()
	{
		// Arrange
		var user = new AuthUserModel
		{
			Id = IdGen.LongId<AuthUserId>(),
			EmailAddress = Rnd.Str,
			FriendlyName = Rnd.Str,
			IsSuper = true
		};

		// Act
		var result = await AuthF.GetPrincipalAsync(user, Rnd.Str, null);

		// Assert
		Assert.Collection(result.Claims,
			c =>
			{
				Assert.Equal(CT.UserId, c.Type);
				Assert.Equal(user.Id.Value.ToString(), c.Value);
			},
			c =>
			{
				Assert.Equal(ClaimTypes.Name, c.Type);
				Assert.Equal(user.FriendlyName, c.Value);
			},
			c =>
			{
				Assert.Equal(ClaimTypes.Email, c.Type);
				Assert.Equal(user.EmailAddress, c.Value);
			},
			c =>
			{
				Assert.Equal(CT.IsSuper, c.Type);
				Assert.Equal(user.IsSuper.ToString(), c.Value);
			}
		);
	}

	[Fact]
	public async Task Returns_ClaimsPrincipal_With_Role_Claims()
	{
		// Arrange
		var role0 = new AuthRoleModel { Id = IdGen.LongId<AuthRoleId>(), Name = Rnd.Str };
		var role1 = new AuthRoleModel { Id = IdGen.LongId<AuthRoleId>(), Name = Rnd.Str };
		var user = new AuthUserModel
		{
			Id = IdGen.LongId<AuthUserId>(),
			EmailAddress = Rnd.Str,
			FriendlyName = Rnd.Str,
			IsSuper = true,
			Roles = new() { { role0 }, { role1 } }
		};

		// Act
		var result = await AuthF.GetPrincipalAsync(user, Rnd.Str, null);

		// Assert
		Assert.Collection(result.Claims,
			c =>
			{
				Assert.Equal(CT.UserId, c.Type);
				Assert.Equal(user.Id.Value.ToString(), c.Value);
			},
			c =>
			{
				Assert.Equal(ClaimTypes.Name, c.Type);
				Assert.Equal(user.FriendlyName, c.Value);
			},
			c =>
			{
				Assert.Equal(ClaimTypes.Email, c.Type);
				Assert.Equal(user.EmailAddress, c.Value);
			},
			c =>
			{
				Assert.Equal(CT.IsSuper, c.Type);
				Assert.Equal(user.IsSuper.ToString(), c.Value);
			},
			c =>
			{
				Assert.Equal(ClaimTypes.Role, c.Type);
				Assert.Equal(role0.Name, c.Value);
			},
			c =>
			{
				Assert.Equal(ClaimTypes.Role, c.Type);
				Assert.Equal(role1.Name, c.Value);
			}
		);
	}

	[Fact]
	public async Task Adds_Custom_Claims()
	{
		// Arrange
		var user = new AuthUserModel
		{
			Id = IdGen.LongId<AuthUserId>(),
			EmailAddress = Rnd.Str,
			FriendlyName = Rnd.Str,
			IsSuper = true
		};
		var addClaims = Substitute.For<AuthF.GetClaims>();
		addClaims.Invoke(user, Arg.Any<string>())
			.Returns(x =>
			{
				var user = (AuthUserModel)x[0];
				return Task.FromResult(
					new List<Claim>
					{
						new(nameof(GetPrincipalAsync_Tests), $"{user.Id}+{user.FriendlyName}")
					}
				);
			});

		// Act
		var result = await AuthF.GetPrincipalAsync(user, Rnd.Str, addClaims);

		// Assert
		Assert.Equal(5, result.Claims.Count());
		Assert.Contains(
			result.Claims,
			c => c.Type == nameof(GetPrincipalAsync_Tests) && c.Value == $"{user.Id}+{user.FriendlyName}"
		);
	}
}
