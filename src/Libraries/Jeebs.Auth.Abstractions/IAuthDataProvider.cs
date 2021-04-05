// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Provides Authentication database functionality
	/// </summary>
	public interface IAuthDataProvider<TUserEntity, TRoleEntity, TUserRoleEntity>
		where TUserEntity : IAuthUser, IEntity
		where TRoleEntity : IAuthRole, IEntity
		where TUserRoleEntity : IAuthUserRole, IEntity
	{
		/// <summary>
		/// User functions
		/// </summary>
		IAuthUserRepository<TUserEntity> User { get; }

		/// <summary>
		/// Role functions
		/// </summary>
		IAuthRoleRepository<TRoleEntity> Role { get; }

		/// <summary>
		/// User Role functions
		/// </summary>
		IAuthUserRoleRepository<TUserRoleEntity> UserRole { get; }

		/// <summary>
		/// Database query object
		/// </summary>
		IAuthDbQuery Query { get; }

		/// <summary>
		/// Validate a User based on their email and password
		/// </summary>
		/// <typeparam name="TModel">Create Auth User Model type</typeparam>
		/// <param name="email">Email address</param>
		/// <param name="password">Password entered by the user</param>
		Task<Option<TModel>> ValidateUserAsync<TModel>(string email, string password)
			where TModel : IAuthUser;

		/// <summary>
		/// Retrieve a User with their Roles
		/// </summary>
		/// <typeparam name="TUser">User Model type</typeparam>
		/// <typeparam name="TRole">Role Model type</typeparam>
		/// <param name="id">User ID</param>
		Task<Option<TUser>> RetrieveUserWithRolesAsync<TUser, TRole>(AuthUserId id)
			where TUser : AuthUserWithRoles<TRole>
			where TRole : IAuthRole;

		/// <summary>
		/// Retrieve a User with their Roles
		/// </summary>
		/// <typeparam name="TUser">User Model type</typeparam>
		/// <typeparam name="TRole">Role Model type</typeparam>
		/// <param name="email">User email address</param>
		Task<Option<TUser>> RetrieveUserWithRolesAsync<TUser, TRole>(string email)
			where TUser : AuthUserWithRoles<TRole>
			where TRole : IAuthRole;
	}
}