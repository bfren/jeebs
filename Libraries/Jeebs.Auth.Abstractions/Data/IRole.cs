// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Role interface
	/// </summary>
	public interface IRole : IRoleModel, IEntity<long>
	{
		/// <summary>
		/// Role description
		/// </summary>
		string Description { get; init; }
	}
}
