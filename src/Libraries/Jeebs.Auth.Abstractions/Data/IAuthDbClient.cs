// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Adds additional Authentication functionality to the base <see cref="IDbClient"/>
	/// </summary>
	public interface IAuthDbClient : IDbClient
	{
		/// <summary>
		/// Migrate to the latest version of the Authentication database
		/// </summary>
		void MigrateToLatest();
	}
}
