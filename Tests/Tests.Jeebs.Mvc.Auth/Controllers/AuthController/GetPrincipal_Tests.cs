// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Jeebs.Auth;
using Jeebs.Auth.Data;
using Microsoft.VisualBasic;
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
			var auth = Substitute.For<IDataAuthProvider>();
			var log = Substitute.For<ILog>();
			var controller = new AuthController(auth, log);
			var user = new UserModel(new(F.Rnd.Lng), F.Rnd.Str, F.Rnd.Str, F.Rnd.Str, true);

			// Act
			var result = controller.GetPrincipal(user);

			// Assert
			Assert.Collection(result.Claims,
				c =>
				{
					Assert.Equal(JwtClaimTypes.UserId, c.Type);
					Assert.Equal(user.UserId.ValueStr, c.Value);
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
			var auth = Substitute.For<IDataAuthProvider>();
			var log = Substitute.For<ILog>();
			var controller = new AuthControllerWithRoles(auth, log);
			var role0 = new RoleModel(new(F.Rnd.Lng), F.Rnd.Str);
			var role1 = new RoleModel(new(F.Rnd.Lng), F.Rnd.Str);
			var user = new UserModelWithRoles(
				new(F.Rnd.Lng),
				F.Rnd.Str,
				F.Rnd.Str,
				F.Rnd.Str,
				true,
				new List<RoleModel>(new[] { role0, role1 })
			);

			// Act
			var result = controller.GetPrincipal(user);

			// Assert
			Assert.Collection(result.Claims,
				c =>
				{
					Assert.Equal(JwtClaimTypes.UserId, c.Type);
					Assert.Equal(user.UserId.ValueStr, c.Value);
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
			var auth = Substitute.For<IDataAuthProvider>();
			var log = Substitute.For<ILog>();
			var controller = new AuthControllerWithClaims(auth, log);
			var user = new UserModel(new(F.Rnd.Lng), F.Rnd.Str, F.Rnd.Str, F.Rnd.Str, false);

			// Act
			var result = controller.GetPrincipal(user);

			// Assert
			Assert.Equal(4, result.Claims.Count());
			Assert.Contains(result.Claims, c => c.Type == nameof(AuthControllerWithClaims) && c.Value == $"{user.UserId}+{user.FriendlyName}");
		}

		public class AuthController : AuthController<UserModel>
		{
			public AuthController(IDataAuthProvider auth, ILog log) : base(auth, log) { }
		}

		public class AuthControllerWithRoles : AuthController<UserModelWithRoles, RoleModel>
		{
			public AuthControllerWithRoles(IDataAuthProvider auth, ILog log) : base(auth, log) { }
		}

		public class AuthControllerWithClaims : AuthController<UserModel>
		{
			protected override Func<UserModel, List<Claim>>? AddClaims =>
				user =>
					new() { new(nameof(AuthControllerWithClaims), $"{user.UserId}+{user.FriendlyName}") };

			public AuthControllerWithClaims(IDataAuthProvider auth, ILog log) : base(auth, log) { }
		}

		public record UserModel(UserId UserId, string EmailAddress, string FriendlyName, string FullName, bool IsSuper) : IUserModel
		{
			public UserModel() : this(new(), string.Empty, string.Empty, string.Empty, false) { }
		}

		public record UserModelWithRoles(UserId UserId, string EmailAddress, string FriendlyName, string FullName, bool IsSuper, List<RoleModel> Roles) : IUserModel<RoleModel>
		{
			public UserModelWithRoles() : this(new(), string.Empty, string.Empty, string.Empty, false, new()) { }
		}

		public record RoleModel(RoleId RoleId, string Name) : IRoleModel
		{
			public RoleModel() : this(new(), string.Empty) { }
		}
	}
}
