// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data
{
	/// <summary>
	/// Once <see cref="IQueryBuilderWithFrom"/> has defined columns to select, the query parts can be retrieved
	/// </summary>
	public interface IQueryBuilderWithSelect
	{
		/// <summary>
		/// Returns the query parts ready to be built by an <see cref="IDbClient"/>
		/// </summary>
		Option<IQueryParts> GetParts();
	}
}
