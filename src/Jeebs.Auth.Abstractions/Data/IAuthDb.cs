// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Adds additional Authentication functionality to the base <see cref="IDb"/>
	/// </summary>
	public interface IAuthDb : IDb
	{
		/// <summary>
		/// Authentication Database Client
		/// </summary>
		new public IAuthDbClient Client { get; }

		/// <summary>
		/// Migrate to the latest version of the Authentication database
		/// </summary>
		void MigrateToLatest();
	}
}
