// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using Jeebs.Auth;
using Jeebs.Auth.Data.Models;
using Jeebs.Auth.Jwt.Constants;
using Jeebs.Logging;

namespace Jeebs.Mvc.Auth.Controllers.AuthController_Tests;

public class GetPrincipal_Tests
{
	[Fact]
	public async Task Returns_ClaimsPrincipal_With_User_Info_Claims()
	{
		// Arrange
		var auth = Substitute.For<IAuthDataProvider>();
		var log = Substitute.For<ILog>();
		var controller = new AuthTestController(auth, log);
		var user = new AuthUserModel
		{
			Id = new(Rnd.Lng),
			EmailAddress = Rnd.Str,
			FriendlyName = Rnd.Str,
			IsSuper = true
		};

		// Act
		var result = await controller.GetPrincipal(user, Rnd.Str).ConfigureAwait(false);

		// Assert
		Assert.Collection(result.Claims,
			c =>
			{
				Assert.Equal(JwtClaimTypes.UserId, c.Type);
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
				Assert.Equal(JwtClaimTypes.IsSuper, c.Type);
				Assert.Equal(user.IsSuper.ToString(), c.Value);
			}
		);
	}

	[Fact]
	public async Task Returns_ClaimsPrincipal_With_Role_Claims()
	{
		// Arrange
		var auth = Substitute.For<IAuthDataProvider>();
		var log = Substitute.For<ILog>();
		var controller = new AuthTestController(auth, log);
		var role0 = new AuthRoleModel(new(Rnd.Lng), Rnd.Str);
		var role1 = new AuthRoleModel(new(Rnd.Lng), Rnd.Str);
		var user = new AuthUserModel
		{
			Id = new(Rnd.Lng),
			EmailAddress = Rnd.Str,
			FriendlyName = Rnd.Str,
			IsSuper = true,
			Roles = new() { { role0 }, { role1 } }
		};

		// Act
		var result = await controller.GetPrincipal(user, Rnd.Str).ConfigureAwait(false);

		// Assert
		Assert.Collection(result.Claims,
			c =>
			{
				Assert.Equal(JwtClaimTypes.UserId, c.Type);
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
				Assert.Equal(JwtClaimTypes.IsSuper, c.Type);
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
		var auth = Substitute.For<IAuthDataProvider>();
		var log = Substitute.For<ILog>();
		var controller = new AuthTestControllerWithClaims(auth, log);
		var user = new AuthUserModel
		{
			Id = new(Rnd.Lng),
			EmailAddress = Rnd.Str,
			FriendlyName = Rnd.Str,
			IsSuper = true
		};

		// Act
		var result = await controller.GetPrincipal(user, Rnd.Str).ConfigureAwait(false);

		// Assert
		Assert.Equal(5, result.Claims.Count());
		Assert.Contains(
			result.Claims,
			c => c.Type == nameof(AuthTestControllerWithClaims) && c.Value == $"{user.Id}+{user.FriendlyName}"
		);
	}

	public class AuthTestController : AuthControllerBase
	{
		public AuthTestController(IAuthDataProvider auth, ILog log) :
			base(auth, log)
		{ }
	}

	public class AuthTestControllerWithClaims : AuthTestController
	{
		protected override GetClaims? AddClaims =>
			(user, _) =>
				Task.FromResult(new List<Claim>
				{
					new(nameof(AuthTestControllerWithClaims), $"{user.Id}+{user.FriendlyName}")
				});

		public AuthTestControllerWithClaims(IAuthDataProvider auth, ILog log) :
			base(auth, log)
		{ }
	}
}
