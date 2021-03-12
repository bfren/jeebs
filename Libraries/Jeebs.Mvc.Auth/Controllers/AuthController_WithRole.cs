// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Jeebs.Auth;
using Jeebs.Auth.Data;

namespace Jeebs.Mvc.Auth.Controllers
{
	/// <summary>
	/// Implement this controller to add support for user authentication
	/// </summary>
	/// <typeparam name="TUserModel">User type</typeparam>
	/// <typeparam name="TRoleModel">Role type</typeparam>
	public abstract class AuthController<TUserModel, TRoleModel> : AuthController<TUserModel>
		where TUserModel : IUserModel<TRoleModel>, new()
		where TRoleModel : IRoleModel, new()
	{
		/// <summary>
		/// Add Role-based claims
		/// </summary>
		protected override Func<TUserModel, List<Claim>>? AddClaims =>
			user =>
				user.Roles.ConvertAll(r => new Claim(ClaimTypes.Role, r.Name));

		/// <inheritdoc cref="AuthController{TUserModel}.AuthController(IDataAuthProvider, ILog)"/>
		protected AuthController(IDataAuthProvider auth, ILog log) : base(auth, log) { }

		/// <inheritdoc/>
		internal override async Task<Option<TUserModel>> ValidateUserAsync(string email, string password) =>
			await Auth.ValidateUserAsync<TUserModel, TRoleModel>(email, password);
	}
}
