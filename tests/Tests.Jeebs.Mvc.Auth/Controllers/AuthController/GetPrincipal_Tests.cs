// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Models;
using NSubstitute;
using Xunit;

namespace Jeebs.Mvc.Auth.Controllers.AuthController_Tests
{
	public class GetPrincipal_Tests
	{
		[Fact]
		public void Returns_ClaimsPrincipal_With_User_Info_Claims()
		{
			// Arrange
			var auth = Substitute.For<IAuthDataProvider>();
			var log = Substitute.For<ILog>();
			var controller = new AuthTestController(auth, log);
			var user = new AuthUserModel
			{
				Id = new(F.Rnd.Lng),
				EmailAddress = F.Rnd.Str,
				FriendlyName = F.Rnd.Str,
				IsSuper = true
			};

			// Act
			var result = controller.GetPrincipal(user);

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
		public void Returns_ClaimsPrincipal_With_Role_Claims()
		{
			// Arrange
			var auth = Substitute.For<IAuthDataProvider>();
			var log = Substitute.For<ILog>();
			var controller = new AuthTestController(auth, log);
			var role0Id = new AuthRoleId { Value = F.Rnd.Lng };
			var role1Id = new AuthRoleId { Value = F.Rnd.Lng };
			var role0 = new AuthRoleModel(new(F.Rnd.Lng), F.Rnd.Str);
			var role1 = new AuthRoleModel(new(F.Rnd.Lng), F.Rnd.Str);
			var user = new AuthUserModel
			{
				Id = new(F.Rnd.Lng),
				EmailAddress = F.Rnd.Str,
				FriendlyName = F.Rnd.Str,
				IsSuper = true,
				Roles = new() { { role0 }, { role1 } }
			};

			// Act
			var result = controller.GetPrincipal(user);

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
		public void Adds_Custom_Claims()
		{
			// Arrange
			var auth = Substitute.For<IAuthDataProvider>();
			var log = Substitute.For<ILog>();
			var controller = new AuthTestControllerWithClaims(auth, log);
			var user = new AuthUserModel
			{
				Id = new(F.Rnd.Lng),
				EmailAddress = F.Rnd.Str,
				FriendlyName = F.Rnd.Str,
				IsSuper = true
			};

			// Act
			var result = controller.GetPrincipal(user);

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
			protected override Func<IAuthUser, List<Claim>>? AddClaims =>
				user =>
					new() { new(nameof(AuthTestControllerWithClaims), $"{user.Id}+{user.FriendlyName}") };

			public AuthTestControllerWithClaims(IAuthDataProvider auth, ILog log) :
				base(auth, log)
			{ }
		}
	}
}
