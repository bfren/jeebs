// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;
using Jeebs.WordPress.Data.Entities;

namespace Jeebs.WordPress.Data
{
	/// <summary>
	/// WordPress Database instance
	/// </summary>
	public interface IWpDb : IDb
	{
		/// <summary>
		/// WordPress Database schema
		/// </summary>
		IWpDbSchema Schema { get; }
	}
}
