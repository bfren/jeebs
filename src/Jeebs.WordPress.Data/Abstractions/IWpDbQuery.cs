// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// WordPress Database queries
	/// </summary>
	public interface IWpDbQuery : IDbQuery
	{
		/// <summary>
		/// IWpDb instance
		/// </summary>
		IWpDb Db { get; }
	}
}
