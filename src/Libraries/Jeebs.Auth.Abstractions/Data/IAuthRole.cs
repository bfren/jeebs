﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Authentication Role
	/// </summary>
	public interface IAuthRole : IAuthRoleModel, IEntity
	{
		/// <summary>
		/// Role description
		/// </summary>
		string Description { get; init; }
	}
}
