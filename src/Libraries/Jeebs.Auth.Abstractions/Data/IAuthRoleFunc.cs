// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Provides Authentication functions for interacting with Roles
	/// </summary>
	public interface IAuthRoleFunc<TRoleEntity> : IDbFunc<TRoleEntity, AuthRoleId>
		where TRoleEntity : IAuthRole, IEntity
	{
	}
}