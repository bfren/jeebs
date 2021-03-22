// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Security.Claims;
using Jeebs.Auth;
using Jeebs.Auth.Data;

namespace Jeebs.Mvc.Auth.Controllers
{
	/// <summary>
	/// Implement this controller to add support for user authentication with roles
	/// </summary>
	/// <typeparam name="TUser">User model type</typeparam>
	/// <typeparam name="TRole">Role model type</typeparam>
	public abstract class AuthController<TUser, TRole> : AuthController<TUser>
		where TUser : IUserModel<TRole>, IAuthUser
		where TRole : IRoleModel
	{
		/// <summary>
		/// Add Role-based claims
		/// </summary>
		protected override Func<TUser, List<Claim>>? AddClaims =>
			user =>
				user.Roles.ConvertAll(r => new Claim(ClaimTypes.Role, r.Name));

		/// <inheritdoc cref="AuthController{TUser}.AuthController(IAuthDataProvider{TUser}, ILog)"/>
		protected AuthController(IAuthDataProvider<TUser> auth, ILog log) : base(auth, log) { }
	}
}
